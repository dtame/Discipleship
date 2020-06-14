using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WandaWebAdmin.Models;

namespace WandaWebAdmin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {                    
        }
        public DbSet<VideoModel> VideoModels { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Thumbnail> Thumnails { get; set; }
    }
}
