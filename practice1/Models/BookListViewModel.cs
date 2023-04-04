using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace practice1.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        
        //public SelectList Companies { get; set; }
        public string Name { get; set; }
        public IFormFile Avatar { get; set; }
       
    }
}
