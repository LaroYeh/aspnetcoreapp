# Ajax 發送 Request



碎碎念

1. 如果 xxx.cshtml.cs 中沒有用到 `return Page()` ， 仍然需要 `xxx.cshtml`，內容可以寫成如下 

``` html
@page
@model aspnetcoreapp.Pages.Movies.TestModel
```

2. OnGet和OnGetAsync應只有一個，其他如OnPost與OnPostAsync同理。否則會出現 `An unhandled exception occurred while processing the request.





## 犯錯經驗

#### 犯錯1

原本為了RazorPage使用時是走哪個而這樣寫

```csharp
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

        [ValidateAntiForgeryToken]
        public IActionResult OnGet()
        {
            return new JsonResult ("Hello Response Back");
        }
        public async Task<IActionResult> OnGetAsync()
        {
            return new JsonResult ("Hello Response Back");
        }
        public IActionResult OnPost()
        {
            return new JsonResult ("Hello Response Back");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            return new JsonResult ("Hello Response Back");
        }
    }
}
```

反而出現下列 `An unhandled exception occurred while processing the request.`

>InvalidOperationException: Multiple handlers matched. The following handlers matched route data and had all constraints satisfied:
>Microsoft.AspNetCore.Mvc.IActionResult OnGet(), System.Threading.Tasks.Task`1[Microsoft.AspNetCore.Mvc.IActionResult] OnGetAsync()
>Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.DefaultPageHandlerMethodSelector.Select(PageContext context)

正確方式：OnGet和OnGetAsync應只有一個，其他如OnPost與OnPostAsync同理。

## 參考資料

<https://www.thereformedprogrammer.net/asp-net-core-razor-pages-how-to-implement-ajax-requests/>

<https://www.mikesdotnetting.com/article/325/partials-and-ajax-in-razor-pages>

<https://stackoverflow.com/questions/46410716/example-ajax-call-back-to-an-asp-net-core-razor-page>