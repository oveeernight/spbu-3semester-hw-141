using System.ComponentModel.DataAnnotations;
using PFMBusinnecLogic;

namespace PFMBusinessLogic.Models;

public class Tag
{
    [Key]
    public string Name { get; set; }
    public virtual ICollection<string> Movies { get; set; }
}