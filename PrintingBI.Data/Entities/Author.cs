using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintingBI.Data.Entities
{
    public class Author : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public IList<Book> Books { get; set; }
    }
}
