using Bloggy.web.Models.Domain;
using Bloggy.web.Models.ViewModels;
using Bloggy.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggy.web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get tags from repository
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogpostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogpostRequest addBlogpostRequest)
        {
            //Map view model to domain Model
            var blogpost = new BlogPost
            {
                Heading = addBlogpostRequest.Heading,
                PageTitle = addBlogpostRequest.PageTitle,
                Content = addBlogpostRequest.Content,
                ShortDescription = addBlogpostRequest.ShortDescription,
                FeaturedImageUrl = addBlogpostRequest.FeaturedImageUrl,
                UrlHandle = addBlogpostRequest.UrlHandle,
                PublishedDate = addBlogpostRequest.PublishedDate,
                Author = addBlogpostRequest.Author,
                Visible = addBlogpostRequest.Visible,
            };
            //Map Tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogpostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            //Mapping tags back to domain Model
            blogpost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogpost);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //var tag=await _bloggieDbContext.Tags.ToListAsync();
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogpost = await blogPostRepository.GetAsync(id);
            var tagdomain=await tagRepository.GetAllAsync();
            if (blogpost != null)
            {
                var editblogPost = new EditBlogPostRequest
                {
                    Id = id,
                    Heading = blogpost.Heading,
                    Author = blogpost.Author,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    ShortDescription = blogpost.ShortDescription,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    UrlHandle = blogpost.UrlHandle,
                    PublishedDate = blogpost.PublishedDate,
                    Visible = blogpost.Visible,
                    Tags=tagdomain.Select(x=> new SelectListItem
                    {
                        Text=x.Name,
                        Value=x.Id.ToString()
                    }),
                    SelectedTags=blogpost.Tags.Select(x=> x.Id.ToString()).ToArray(),

                };
                return View(editblogPost);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var editblogPost = new BlogPost
            {
                Id =  editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                Author = editBlogPostRequest.Author,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Visible = editBlogPostRequest.Visible,

            };
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag,out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);
                    if(foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            editblogPost.Tags = selectedTags;
            var ExistingblogPost = await blogPostRepository.UpdateAsync(editblogPost);
            if (ExistingblogPost != null)
            {
                return RedirectToAction("Edit", new { id = editblogPost.Id });
            }
            else
            {
                return RedirectToAction("Edit", new { id = editblogPost.Id });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
           
            var existingBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if (existingBlogPost != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
            }
            
        }
    }
}
