using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public text Available { get; set; } //доступность
        public string Author { get; set; }
        public string Genre { get; set; }
        public byte[] Avatar { get; set; }
        public int Quantity { get; set; } //кол-во
        public int Year { get; set; } 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Rack { get; set; } //полка
        public string Description { get; set; } // описание
        public string Publisher { get; set; } 
    }
   /* public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }*/
   /* public class Genre
    {
        public int Id { get; set; }
        public string Gener_Name { get; set; }
        
    }
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }*/
}
