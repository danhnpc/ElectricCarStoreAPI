using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models.Model;

[Table("image")]
public partial class Image
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("url")]
    [StringLength(500)]
    public string Url { get; set; }

    [Column("created_date", TypeName = "timestamp(0) without time zone")]
    public DateTime? CreatedDate { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [InverseProperty("Image")]
    public virtual ICollection<Banner> Banners { get; set; } = new List<Banner>();

    [InverseProperty("Image")]
    public virtual ICollection<CarImage> CarImages { get; set; } = new List<CarImage>();

    [InverseProperty("Image")]
    public virtual ICollection<News> News { get; set; } = new List<News>();
}
