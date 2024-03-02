using Azure;
using Bloggy.web.Data;
using Bloggy.web.Models.Domain;
using Bloggy.web.Models.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
           await bloggieDbContext.AddAsync(blogPost);
           await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid Id)
        {
            var ExistingblogPost = await bloggieDbContext.BlogPosts.FindAsync(Id);
            if (ExistingblogPost != null)
            {
                bloggieDbContext.BlogPosts.Remove(ExistingblogPost);
                await bloggieDbContext.SaveChangesAsync();
                return ExistingblogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await bloggieDbContext.BlogPosts.Include(x=> x.Tags).FirstOrDefaultAsync
                (x=> x.UrlHandle == urlHandle);

        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var ExistingblogPost = await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (ExistingblogPost != null)
            {
                ExistingblogPost.Id = blogPost.Id;
                ExistingblogPost.Heading = blogPost.Heading;
                ExistingblogPost.Author = blogPost.Author;
                ExistingblogPost.PageTitle = blogPost.PageTitle;
                ExistingblogPost.Content = blogPost.Content;
                ExistingblogPost.ShortDescription = blogPost.ShortDescription;
                ExistingblogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                ExistingblogPost.UrlHandle = blogPost.UrlHandle;
                ExistingblogPost.PublishedDate = blogPost.PublishedDate;
                ExistingblogPost.Visible = blogPost.Visible;
                ExistingblogPost.Tags = blogPost.Tags;
                //save changes
                await bloggieDbContext.SaveChangesAsync();
                return ExistingblogPost;
            }
            return null;
        }
    }
}
