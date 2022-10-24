using System;

namespace FSPBook.App.FSPBook.Core.Models
{
    public class ListPostsViewModel
    {
        public string AuthorName { get; set; }
        public DateTimeOffset DateTimePosted { get; set; }
        public string Content { get; set; }
    }
}
