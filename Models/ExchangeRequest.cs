using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBC.Models;

public partial class ExchangeRequest
{
    public int ExchangeId { get; set; }

    public string Title { get; set; } = null!;

    public int Category { get; set; }

    public string? Author { get; set; }

    public string? Description { get; set; }

    public int Condition { get; set; }

    public string? Image1 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public string? Image4 { get; set; }

    public int Status { get; set; }

    public decimal? Credit { get; set; }

    public string Id { get; set; }
    [ForeignKey("Id")]
    public virtual User User { get; set; } = null!;
}
