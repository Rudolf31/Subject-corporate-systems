using Corpa4Sem4.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Corp4Sem4.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Message>? Messages { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			var options = new
			{
				Server = "89.169.1.120:5437",
				Database = "transport",
				User = "postgres",
				Password = "Rudolf31",
			};
			optionsBuilder.UseNpgsql($"Server = {options.Server}; Database ={options.Database}; Uid = {options.User}; Pwd = {options.Password};");
		}
    }
}
