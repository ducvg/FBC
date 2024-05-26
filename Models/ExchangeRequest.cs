using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBC.Models;

public partial class ExchangeRequest
{
    public int ExchangeId { get; set; }
    public string Title { get; set; } = null!;
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? Description { get; set; }
    public string Condition { get; set; } = null!;
    public int? NoPage { get; set; }
    public decimal? Weight{ get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Length { get; set; }
    public string? Image1 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public string? Image4 { get; set; }

    public int Status { get; set; }

    public decimal? Credit { get; set; }

    public string Id { get; set; }
    [ForeignKey("Id")]
    public virtual User User { get; set; } = null!;
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

}
