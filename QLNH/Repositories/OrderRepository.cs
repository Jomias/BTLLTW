using AutoMapper;
using QLNH.Entities;
using QLNH.Models.ViewModels;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using QLNH.Helpers;
using AppManager.Utils;

namespace QLNH.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly QlnhContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long> AddAsync(OrderModel model)
        {
            var dish = _context.Dishes!.Find(model.DishId);
            model.Price = dish.Price;
            var newOrder = _mapper.Map<Order>(model);
            _context.Orders!.Add(newOrder);
            await _context.SaveChangesAsync();

            var order = _context.Orders
                .Include(o => o.Dish)
                    .ThenInclude(d => d.Recipes)
                        .ThenInclude(r => r.Ingredient)
                .FirstOrDefault(o => o.Id == newOrder.Id);

            if (order != null)
            {
                foreach (var recipe in order.Dish.Recipes)
                {
                    if (recipe.IsDeleted == false)
                    {
                        var ingredient = recipe.Ingredient;
                        var newQuantity = ingredient.Quantity - (recipe.Quantity * order.Quantity);
                        ingredient.Quantity = newQuantity;
                        _context.SaveChanges();
                    }
                }
            }
            var bill = _context.Bills
                .Include(b => b.Orders)
                .SingleOrDefault(b => b.Orders.Any(o => o.Id == newOrder.Id && o.IsDeleted == false));

            if (bill != null)
            {
                bill.SubTotal = bill.Orders.Where(o => o.IsDeleted == false).Sum(o => o.Price * o.Quantity);
                await _context.SaveChangesAsync();
            }
            return newOrder.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var deleteOrder = await _context.Orders!
                .Include(o => o.Dish)
                    .ThenInclude(d => d.Recipes)
                        .ThenInclude(r => r.Ingredient)
                .SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (deleteOrder == null)
            {
                throw new ArgumentException($"Order with ID {id} not found or has already been deleted.");
            }

            // Revert ingredient quantities for the associated dish
            foreach (var recipe in deleteOrder.Dish.Recipes)
            {
                if (recipe.IsDeleted == false)
                {
                    var ingredient = recipe.Ingredient;
                    var quantity = ingredient.Quantity + (recipe.Quantity * deleteOrder.Quantity);
                    ingredient.Quantity = quantity;
                }
            }

            // Mark order as deleted and update bill subtotal
            deleteOrder.IsDeleted = true;
            deleteOrder.UpdatedAt = DateTime.Now;

            var bill = await _context.Bills
                .Include(b => b.Orders)
                .SingleOrDefaultAsync(b => b.Orders.Any(o => o.Id == deleteOrder.Id && o.IsDeleted == false));

            if (bill != null)
            {
                bill.SubTotal = bill.Orders.Where(o => o.IsDeleted == false).Sum(o => o.Price * o.Quantity);
            }

            await _context.SaveChangesAsync();
        }


        public async Task<List<OrderModel>> GetAllAsync()
        {
            var Orders = await _context.Orders!.Where(x => x.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<OrderModel>>(Orders);
        }

        public async Task<OrderModel> GetAsync(long id)
        {
            var Order = await _context.Orders!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<OrderModel>(Order);
        }

        public async Task<List<OrderViewModel>> GetAllOrderViewModel(long? billId)
        {
            var temp = await (from a in _context.Dishes
                              join b in _context.Orders on a.Id equals b.DishId
                              join c in _context.Bills on b.BillId equals c.Id
                              where a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false
                              where billId == null || b.BillId == billId
                              select new OrderViewModel()
                              {
                                  Id = b.Id,
                                  DishName = a.Name,
                                  Quantity = b.Quantity,
                                  Price = b.Price,
                                  Total = b.Price * b.Quantity,
                                  Note = b.Note
                              }).ToListAsync();
            return temp;
        }

        public async Task UpdateAsync(OrderModel model)
        {
            var oldOrder = await _context.Orders!
                .Include(o => o.Dish)
                    .ThenInclude(d => d.Recipes)
                        .ThenInclude(r => r.Ingredient)
                .SingleOrDefaultAsync(o => o.Id == model.Id);

            if (oldOrder == null)
            {
                throw new ArgumentException($"Order with ID {model.Id} not found or has been deleted.");
            }

            foreach (var oldRecipe in oldOrder.Dish.Recipes)
            {
                if (oldRecipe.IsDeleted == false)
                {
                    var oldIngredient = oldRecipe.Ingredient;
                    var oldQuantity = oldIngredient.Quantity + (oldRecipe.Quantity * oldOrder.Quantity);
                    oldIngredient.Quantity = oldQuantity;
                }
            }

            // Update order with new dish and quantity
            var newDish = await _context.Dishes!.FindAsync(model.DishId);
            model.Price = newDish.Price;
            _mapper.Map(model, oldOrder);
            _context.Update(oldOrder);
            await _context.SaveChangesAsync();
            // Update ingredient quantities for the new dish
            foreach (var newRecipe in newDish.Recipes)
            {
                if (newRecipe.IsDeleted == false)
                {
                    var newIngredient = newRecipe.Ingredient;
                    var newQuantity = newIngredient.Quantity - (newRecipe.Quantity * oldOrder.Quantity);
                    newIngredient.Quantity = newQuantity;
                }
            }

            var bill = await _context.Bills
                .Include(b => b.Orders)
                .SingleOrDefaultAsync(b => b.Orders.Any(o => o.Id == oldOrder.Id && o.IsDeleted == false));

            if (bill != null)
            {
                bill.SubTotal = bill.Orders.Where(o => o.IsDeleted == false).Sum(o => o.Price * o.Quantity);
            }

            await _context.SaveChangesAsync();
        }

    }
}
