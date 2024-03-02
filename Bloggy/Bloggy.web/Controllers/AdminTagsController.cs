
using Bloggy.web.Data;
using Bloggy.web.Models.Domain;
using Bloggy.web.Models.ViewModels;
using Bloggy.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        //private readonly BloggieDbContext _bloggieDbContext;
        //public AdminTagsController(BloggieDbContext bloggieDbContext)
        //{
        //        this._bloggieDbContext = bloggieDbContext;
        //}
        private readonly ITagRepository _tagRepository;
        public AdminTagsController(ITagRepository tagRepository)
        {
                _tagRepository = tagRepository;
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //var Name=addTagRequest.Name;
            //var DisplayName=addTagRequest.DisplayName;
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                Displayname = addTagRequest.DisplayName

            };
            //_bloggieDbContext.Tags.Add(tag);
            //_bloggieDbContext.SaveChanges();
           // await _bloggieDbContext.Tags.AddAsync(tag);
            //await _bloggieDbContext.SaveChangesAsync();
            await _tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }
        /*public IActionResult submitTag()
        {
            var Name = Request.Form["name"];
            var Displayname = Request.Form["displayName"];
            return View("Add");
        }*/
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //var tag=await _bloggieDbContext.Tags.ToListAsync();
            var tag=await _tagRepository.GetAllAsync();
            return View(tag);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st Method
            //var tag= _bloggieDbContext.Tags.Find(id);
            //2nd Method
            //var Tag= await  _bloggieDbContext.Tags.FirstOrDefaultAsync(x=> x.Id==id);
            var Tag=await _tagRepository.GetAsync(id);
            if(Tag!=null)
            {
                var editTag = new EditTagRequest
                {
                    Id = id,
                    Name = Tag.Name,
                    DisplayName=Tag.Displayname
                };
                return View(editTag);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                Displayname = editTagRequest.DisplayName
            };
            var ExistingTag= await _tagRepository.UpdateAsync(tag);
            if(ExistingTag!=null)
            {
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            else
            {
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            /*var ExistingTag=await _bloggieDbContext.Tags.FindAsync(tag.Id);
            if (ExistingTag!=null)
            {
                ExistingTag.Name = tag.Name;
                ExistingTag.Displayname= tag.Displayname;
                //save changes
                await _bloggieDbContext.SaveChangesAsync();

                //Show Success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });


            }*/
            //show failure notification
            
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                Displayname = editTagRequest.DisplayName
            };
            var existingTag=await _tagRepository.DeleteAsync(tag);
            if(existingTag != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            //var tag=_bloggieDbContext.Tags.Find(editTagRequest.Id);
            //if(tag!=null)
            //{
            //    _bloggieDbContext.Tags.Remove(tag);
            //    await _bloggieDbContext.SaveChangesAsync();
            //    return RedirectToAction("List");
            //}
            //return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
