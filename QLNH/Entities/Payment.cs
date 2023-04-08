using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class Payment
{
    public long Id { get; set; }

    public string PaymentType { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();
}
