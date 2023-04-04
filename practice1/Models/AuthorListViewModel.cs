using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace practice1.Models
{
    public class AuthorListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        //public SelectList Companies { get; set; }
        public string Author { get; set; }
    }
}
