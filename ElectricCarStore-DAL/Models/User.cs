using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricCarStore_DAL.Models;

[Table("user")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(255)]
    public string Username { get; set; }

    [JsonIgnore]
    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; }

    [JsonIgnore]
    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }
}
