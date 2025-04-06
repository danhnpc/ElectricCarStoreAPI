using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models.Model;

[Table("user")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(255)]
    public string Username { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }
}
