using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models.Model;

[Table("car")]
public partial class Car
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("description_a")]
    [StringLength(500)]
    public string DescriptionA { get; set; }

    [Column("description_b")]
    [StringLength(500)]
    public string DescriptionB { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }
}
