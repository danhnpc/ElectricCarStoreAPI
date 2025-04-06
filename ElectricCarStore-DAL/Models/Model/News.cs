using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models.Model;

[Table("news")]
public partial class News
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("desc")]
    [StringLength(255)]
    public string Desc { get; set; }

    [Column("image_id")]
    public int? ImageId { get; set; }

    [Column("content")]
    public string Content { get; set; }

    [Column("created_date")]
    public DateTime? CreatedDate { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }
}
