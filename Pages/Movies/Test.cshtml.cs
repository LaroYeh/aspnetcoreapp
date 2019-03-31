using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Models;

namespace aspnetcoreapp.Pages.Movies
{
    public class TestModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public TestModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public string Message { get; set; }

        [ValidateAntiForgeryToken]
        public IActionResult OnGet(string Message)
        {
            //return new JsonResult ("Hello Response Back");
            return new JsonResult (Message);
        }
    }
}