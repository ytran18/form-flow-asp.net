using System.ComponentModel.DataAnnotations;

namespace demo.Models;

public class Forms
{
    [Key]
    public String _id { set; get; }

    public String formTitle { set; get; }
    
    public String formDescription { set; get; }

    public String preview_img { set; get; }
    
    public bool isAvailable { set; get; }
    
    public String modified_at { set; get; }
}