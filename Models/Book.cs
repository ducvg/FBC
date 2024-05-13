using System;
using System.Collections.Generic;

namespace FBC.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int CategoryId { get; set; }

    public string? Author { get; set; }

    public string? Description { get; set; }

    public int Condition { get; set; }

    public string? Image1 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public string? Image4 { get; set; }

    public int Status { get; set; }

    public decimal? Credit { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<CartOrder> Carts { get; set; } = new List<CartOrder>();
}
