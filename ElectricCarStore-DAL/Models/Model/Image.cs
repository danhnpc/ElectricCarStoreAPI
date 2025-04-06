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

    [Column("created_date")]
    public DateTime? CreatedDate { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }
}
