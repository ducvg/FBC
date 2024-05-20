using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBC.Models;

public partial class CartOrder
{
    public int CartId { get; set; }

    public string Id { get; set; }
    [ForeignKey("Id")]
    public virtual User User { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
