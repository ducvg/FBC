using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBC.Models;

public partial class CartOrder
{
    public int CartId { get; set; }
    public int BookId { get; set; }
    public string Title { get; set; }
    public string? Image1 { get; set; }
    public decimal? Credit { get; set; }
    public string Condition { get; set; } = null!;
    public string Id { get; set; }
    [ForeignKey("Id")]
    public virtual User User { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
