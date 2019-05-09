using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintingBI.Data.Entities
{
    public class Student : Entity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(5,30)]
        public int Age { get; set; }

        public ClassRoom ClassRoom { get; set; }
    }

    public class ClassRoom : Entity<int>
    {
        [Required]
        public string Name { get; set; }

        public IList<Student> Students { get; set; }
    }
}
