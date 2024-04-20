using System.ComponentModel.DataAnnotations;

namespace demo.Models;

public class Users
{
    [Key]
    public String _id { get; set; }

    [Required]
    public String email { get; set; }

    [Required] 
    public String password { get; set; }

    public String avt_url { get; set; }

    public String created_at { get; set; }
}