using System;
using System.Collections.Generic;

namespace FBC.Models;

public partial class BookOrder
{
    public int BookOrderId { get; set; }

    public int? Total { get; set; }

    public int? Status { get; set; }

    public string? Recipient { get; set; }

    public string? Address { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public string? Phone { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
