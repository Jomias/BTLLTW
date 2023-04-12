using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;

namespace QLNH.Areas.Controllers
{
	[Area("Admin")]
	public class AccessController : Controller
	{

		QlnhContext db = new QlnhContext();
		private readonly IMapper _mapper;
		public AccessController(IMapper mapper)
		{
			_mapper = mapper;
		}
		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetString("Username") == null)
			{
				return View();
			}
			else
			{
				if (HttpContext.Session.GetInt32("RoleId") == 1)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					HttpContext.Session.Clear();
					HttpContext.Session.Remove("Username");
					return StatusCode(403);
				}
			}
		}


		[HttpPost]
		public IActionResult Login(User user)
		{
			var u = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
			if (u != null && u.IsDeleted == true)
			{
				TempData["Error"] = "Tài khoản đã bị khóa";
				return RedirectToAction("Login");
			}
			else
			{
				if (u == null)
				{
					TempData["Error"] = "Tên đăng nhập hoặc mật khẩu không đúng";
					return RedirectToAction("Login");

				}
				else
				{
					if (u.RoleId == 1)
					{
						HttpContext.Session.SetString("Username", u.Username);
						return RedirectToAction("Index", "Home");

					}
					else
					{
						HttpContext.Session.Clear();
						HttpContext.Session.Remove("Username");
						return StatusCode(403);
					}
				}
			}

		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		//POST: Register

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Register(UserModel user)
		{
			var entity = _mapper.Map<User>(user);
			if (ModelState.IsValid)
			{
				db.Users.Add(entity);
				db.SaveChanges();
				TempData["Success"] = "Đăng kí thành công";
				return RedirectToAction("Login", "Access");
			}
			TempData["Error"] = "Lỗi";
			return BadRequest(ModelState);
		}
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("Username");
			return RedirectToAction("Login", "Access");
		}
	}
}