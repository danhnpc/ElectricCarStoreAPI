using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models.Model;

[Table("contact")]
public partial class Contact
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("full_name")]
    [StringLength(255)]
    public string FullName { get; set; }

    [Column("phone_number")]
    public int? PhoneNumber { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string Address { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; }

    [Column("content")]
    [StringLength(255)]
    public string Content { get; set; }

    [Column("car_id")]
    public int? CarId { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [Column("is_contact")]
    public bool? IsContact { get; set; }
}
