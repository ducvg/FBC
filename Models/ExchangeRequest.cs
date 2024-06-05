using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBC.Models;

public partial class ExchangeRequest
{
    public int ExchangeId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
    public string Title { get; set; } = null!;

    [StringLength(100, ErrorMessage = "Publisher can't be longer than 100 characters.")]
    [Required(ErrorMessage = "Author is required.")]
    public string? Author { get; set; }
    [Required(ErrorMessage = "Publisher is required.")]
    [StringLength(100, ErrorMessage = "Publisher can't be longer than 100 characters.")]
    public string? Publisher { get; set; }
    public string? Description { get; set; }
    public string? Response { get; set; }
    public DateOnly CreateDate { get; set; }
    public DateOnly? CompleteDate { get; set; }

    [Required(ErrorMessage = "Condition is required.")]
    [StringLength(50, ErrorMessage = "Condition can't be longer than 50 characters.")]

    public string Condition { get; set; } = null!;
    [Required(ErrorMessage = "Số trang không hợp lệ")]
    [Range(1, int.MaxValue, ErrorMessage = "Number of pages must be a positive integer.")]

    public int? NoPage { get; set; }
    [Required(ErrorMessage = "Weight is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Weight must be a positive number.")]

    public decimal? Weight{ get; set; }
    [Required(ErrorMessage = "Width is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Width must be a positive number.")]

    public decimal? Width { get; set; }
    [Required(ErrorMessage = "Height is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Height must be a positive number.")]

    public decimal? Height { get; set; }
    [Required(ErrorMessage = "Thickness is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Length must be a positive number.")]

    public decimal? Length { get; set; }
    [Required(ErrorMessage = "front image is required.")]

    public string? Image1 { get; set; }
    [Required(ErrorMessage = "back image is required.")]


    public string? Image2 { get; set; }
    [Required(ErrorMessage = "spine image is required.")]


    public string? Image3 { get; set; }
    [Required(ErrorMessage = "edge image is required.")]


    public string? Image4 { get; set; }
    [Required(ErrorMessage = "Status is required.")]


    public int Status { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Credit must be a positive number.")]

    public decimal? Credit { get; set; }
    public string Id { get; set; }
    [ForeignKey("Id")]
    public virtual User User { get; set; } = null!;
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();


    public string getStatus()
    {
        switch (Status)
        {
            case 0: return "Pending Approval";
            case 1: return "Transporting";
            case 2: return "Completed";
            case 3: return "Canceled";
            default: return "Unknown";

        }
    }

    public string listCategories()
    {
        return string.Join(", ", Categories.Select(c => c.Name));
    }
}
