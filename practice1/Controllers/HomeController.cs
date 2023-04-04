using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using practice1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace practice1.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
          private ApplicationContext db;
           public HomeController(ApplicationContext context)
          {
              db = context;
           
          }
        public async Task<IActionResult> Index(string bookGenre, string searchString)
        {
           
            IQueryable<string> genreQuery = from m in db.Books
                                            orderby m.Genre
                                            select m.Genre;
            var books = from m in db.Books
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Name.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(x => x.Genre == bookGenre);
            }

            var bookGenreVM = new GenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Books = await books.ToListAsync()
            };
            
            return View(bookGenreVM);
            // return View(await db.Books.ToListAsync());
            //return View(await movies.ToListAsync());
        }
        public async Task<IActionResult> Auth(string bookGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in db.Books
                                            orderby m.Genre
                                            select m.Genre;
            var books = from m in db.Books
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Author.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(x => x.Genre == bookGenre);
            }

            var bookGenreVM = new GenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Books = await books.ToListAsync()
            };

            return View(bookGenreVM);
            // return View(await db.Books.ToListAsync());
            //return View(await movies.ToListAsync());
        }


        public async Task<IActionResult> Gen(string genre, SortState sortOrder = SortState.NameAsc)
        {
            var books = from m in db.Books
                        select m;

            //ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            //ViewData["YearSort"] = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
            
            books = sortOrder switch
            {
                SortState.NameAsc => books.OrderBy(s => s.Name),
                SortState.NameDesc => books.OrderByDescending(s => s.Name),
                SortState.YearAsc => books.OrderBy(s => s.Year),
                SortState.YearDesc => books.OrderByDescending(s => s.Year),
                
            };
            GenreViewModel viewModel = new GenreViewModel
            {
                Books = await books.AsNoTracking().ToListAsync(),
                SortViewModel = new SortViewModel(sortOrder)
            };
            return View(viewModel);
        }
        //public async Task<IActionResult> Auth (string bookAuthor, string searchString)
        //{
        //    IQueryable<string> authorQuery = from m in db.Books
        //                                    orderby m.Author
        //                                    select m.Author;
        //    var books = from m in db.Books
        //                select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        books = books.Where(s => s.Author.Contains(searchString));
        //    }
        //    if (!string.IsNullOrEmpty(bookAuthor))
        //    {
        //        books = books.Where(x => x.Author == bookAuthor);
        //    }

        //    var bookAuthorVM = new AuthorViewModel
        //    {
        //        Authors = new SelectList(await authorQuery.Distinct().ToListAsync()),
        //        Books = await books.ToListAsync()
        //    };

        //    return View(bookAuthorVM);
        //}


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book, BookListViewModel pvm)
        {
            
            if (pvm.Avatar != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(pvm.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)pvm.Avatar.Length);
                }
                // установка массива байтов
                book.Avatar = imageData;
            }
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        /* public HomeController(ILogger<HomeController> logger)
         {
             _logger = logger;
         }

         public IActionResult Index()
         {
             return View();
         }

         public IActionResult Privacy()
         {
             return View();
         }

         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
         public IActionResult Error()
         {
             return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
         }*/
        public async Task<IActionResult> Details(int? id)
        {
            //return View(db.Books.ToList());
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                    return View(book);

            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
