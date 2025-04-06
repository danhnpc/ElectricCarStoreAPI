using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models.Model;

[Table("car_type")]
public partial class CarType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("car_id")]
    public int? CarId { get; set; }

    [Column("type_name")]
    [StringLength(255)]
    public string TypeName { get; set; }

    [Column("price")]
    [Precision(10, 2)]
    public decimal? Price { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }
}
