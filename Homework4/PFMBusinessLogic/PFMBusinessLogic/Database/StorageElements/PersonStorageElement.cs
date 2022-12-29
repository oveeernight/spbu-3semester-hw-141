using System.ComponentModel.DataAnnotations;

namespace PFMBusinessLogic.Database.StorageElements;

public class PersonStorageElement
{
    [Key]
    public string Name { get; set; }
    public string Movies { get; set; }
}