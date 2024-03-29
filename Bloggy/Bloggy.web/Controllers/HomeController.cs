﻿using Bloggy.web.Models;
using Bloggy.web.Models.ViewModels;
using Bloggy.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggy.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger,IBlogPostRepository blogPostRepository,ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()

        {
            var blogPost=await blogPostRepository.GetAllAsync();
            var tag=await tagRepository.GetAllAsync();
            var model = new HomeViewModel
            {
                BlogPosts = blogPost,
                Tags = tag
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}