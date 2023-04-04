using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace practice1.Models
{
    public class AuthorViewModel
    {
        public List<Book> Books { get; set; }
        public SelectList Authors { get; set; }
        public string BookAuthor { get; set; }
        public string SearchString { get; set; }
    }
}
