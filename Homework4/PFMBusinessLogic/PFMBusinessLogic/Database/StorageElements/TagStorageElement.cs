using System.ComponentModel.DataAnnotations;

namespace PFMBusinessLogic.Database.StorageElements;

public class TagStorageElement
{
    [Key]
    public string Name { get; set; }
    public string Movies { get; set; }
}