using FSPBook.App.FSPBook.Core.Models;
using FSPBook.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSPBook.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly Context _postContext;
    private readonly int _currentPage;

    public IEnumerable<ListPostsViewModel> ListPostsViewModels { get; set; } = new List<ListPostsViewModel>();

    public IndexModel(ILogger<IndexModel> logger, Context postContext)
    {
        _logger = logger;
        _postContext = postContext;
    }

    public async Task OnGetAsync()
    
    {
        // move this to a repository
        var posts = await _postContext.Post.ToListAsync();
        var profles = await _postContext.Profile.ToListAsync();

        ListPostsViewModels = from p in posts
                              join pr in profles
                              on p.Id equals pr.Id
                              orderby p.DateTimePosted descending
                              select new ListPostsViewModel
                              {
                                  AuthorName = pr.FullName,
                                  DateTimePosted = p.DateTimePosted,
                                  Content = p.Content
                              };

        int RecordsPerPage = 2;
        int totalPages = ListPostsViewModels.Count() / RecordsPerPage;
        int PageNumber = 0;
        if (PageNumber > 0 && PageNumber < 3)
        {

            var PostPerPage = ListPostsViewModels.Skip((PageNumber - 1) * RecordsPerPage)
                                    .Take(RecordsPerPage).ToList();
        }

    }
}