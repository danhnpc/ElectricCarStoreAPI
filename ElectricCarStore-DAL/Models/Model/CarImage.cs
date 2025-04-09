using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models.Model;

[Table("car_image")]
public partial class CarImage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("car_id")]
    public int? CarId { get; set; }

    [Column("image_id")]
    public int? ImageId { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("CarImages")]
    public virtual Car Car { get; set; }

    [ForeignKey("ImageId")]
    [InverseProperty("CarImages")]
    public virtual Image Image { get; set; }
}
