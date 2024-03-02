using Azure;
using Bloggy.web.Data;
using Bloggy.web.Models.Domain;
using Bloggy.web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;
        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }


        public async Task<Tag?> DeleteAsync(Tag tag)
        {
            var Existingtag = await bloggieDbContext.Tags.FindAsync(tag.Id);
            if (Existingtag != null)
            {
                bloggieDbContext.Tags.Remove(Existingtag);
                await bloggieDbContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }


        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await bloggieDbContext.Tags.ToListAsync();
        }



        public async Task<Tag?> GetAsync(Guid id)
        {
            return  await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);   
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var ExistingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);
            if (ExistingTag != null)
            {
                ExistingTag.Name = tag.Name;
                ExistingTag.Displayname = tag.Displayname;
                //save changes
                await bloggieDbContext.SaveChangesAsync();
                return ExistingTag;
            }
            return null;
        }
    }
}
