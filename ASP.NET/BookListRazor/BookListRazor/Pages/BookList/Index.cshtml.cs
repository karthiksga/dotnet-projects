using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Book> Books { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            _db = context;
        }
        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }
    }
}