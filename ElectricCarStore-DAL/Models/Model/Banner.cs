using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models.Model;

[Table("banner")]
public partial class Banner
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("image_id")]
    public int? ImageId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }
}
