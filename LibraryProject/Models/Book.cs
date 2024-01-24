using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models;

public partial class Book
{
    [Key]
    public int Bookid { get; set; } 

    public string? BookName { get; set; }

    public string? BookDescription { get; set; }

    public string? Author { get; set; }

    public int? Price { get; set; }
    
    public DateTime? Published_Date
    {
        get;set;
    }
}
