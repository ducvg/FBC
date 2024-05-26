using System;
using System.Collections.Generic;

namespace FBC.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public string? Description { get; set; }

    public string Condition { get; set; } = null!;
    public int? ConditionNumeric { get; set; }
    public int NoPage { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Width { get; set; }

    public decimal? Length { get; set; }

    public decimal? Height { get; set; }

    public string? Image1 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public string? Image4 { get; set; }

    public int Status { get; set; }

    public decimal? Credit { get; set; }

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<CartOrder> Carts { get; set; } = new List<CartOrder>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
