﻿using System.Collections;
using System.ComponentModel.DataAnnotations;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Models;

public class Movie
{
    [Key]
    public string Title { get; set; }
    public  virtual ICollection<Person> Persons { get; set; }
    public string? Director { get; set; }
    public virtual ICollection<Tag> Tags { get; set; }
    public string? Rate { get; set; }
}