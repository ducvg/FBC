using System;
using System.Collections.Generic;

namespace FBC.Models;

public partial class CartOrder
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
