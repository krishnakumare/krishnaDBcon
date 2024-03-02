﻿using Bloggy.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
        }
         public DbSet<BlogPost> BlogPosts { get; set; }
         public DbSet<Tag> Tags { get; set; }

         public DbSet<BlogPostLike> BlogPostsLike { get; set;}
    }
}
