using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Type = KS2_2project.Models.Type;
using KS2_2project.Models;
using System.Xml.Linq;


namespace KS2_2project
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<Models.Area> Areas { get; set; }
        public DbSet<Models.Material> Materials { get; set; }
        public DbSet<Models.Estate> Estates { get; set; }
        public DbSet<Models.Criteria> Criterias { get; set; }
        public DbSet<Models.Mark> Reviews { get; set; }
        public DbSet<Models.Agent> Agents { get; set; }
        public DbSet<Models.Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Type>().HasData(
                new Type { Id = 1, Name = "Квартира" },
                new Type { Id = 2, Name = "Дом" },
                new Type { Id = 3, Name = "Апартаменты" },
                new Type { Id = 4, Name = "Монастырь" }
            );
            modelBuilder.Entity<Material>().HasData(
                new Material { Id = 1, Name = "Дерево" },
                new Material { Id = 2, Name = "Киприч" },
                new Material { Id = 3, Name = "Обломки уран-графитового реактора" },
                new Material { Id = 4, Name = "Снег" },
                new Material { Id = 5, Name = "Шлакоблокунь" }
            );
            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "Северный" },
                new Area { Id = 2, Name = "Южный" },
                new Area { Id = 3, Name = "Западный" },
                new Area { Id = 4, Name = "Восточный" },
                new Area { Id = 5, Name = "Верхний" },
                new Area { Id = 6, Name = "Нижний" }
            );
            modelBuilder.Entity<Criteria>().HasData(
                new Criteria { Id = 1, Name = "Ремонт" },
                new Criteria { Id = 2, Name = "Мебель" },
                new Criteria { Id = 3, Name = "Безопасность" },
                new Criteria { Id = 4, Name = "Транспорт" },
                new Criteria { Id = 5, Name = "Соседи" }
            );
            modelBuilder.Entity<Estate>().HasData(
                new Estate { Id = 1, Cost = 20, Date = Vrem.Unix(2020, 1, 1), Address = "644041, г. Омск, ул. Лесная, 12", AreaId = 3, MaterialId = 1, TypeId = 1, Floor = 5, Rooms = 2, Space = 10000000, Status = true, Description = "Что-то на богатом" },
                new Estate { Id = 2, Cost = 16, Date = Vrem.Unix(2020, 2, 2), Address = "614330, г. Пермь, ул. Лесная, 3", AreaId = 1, MaterialId = 2, TypeId = 2, Floor = 1, Rooms = 2, Space = 200, Status = true, Description = "Что-то на богатом" },
                new Estate { Id = 3, Cost = 1400000, Date = Vrem.Unix(2020, 3, 1), Address = "344024, г. Ростов-на-Дону, ул. Зеленая, 37", AreaId = 1, MaterialId = 3, TypeId = 3, Floor = 3, Rooms = 3, Space = 300, Status = false, Description = "Что-то на богатом" },
                new Estate { Id = 4, Cost = 1294777, Date = Vrem.Unix(2020, 5, 6), Address = "630796, г. Новосибирск, ул. Лесная, 20", AreaId = 3, MaterialId = 4, TypeId = 4, Floor = 1, Rooms = 2, Space = 4000000, Status = true, Description = "Что-то на богатом" },
                new Estate { Id = 5, Cost = 3000000, Date = Vrem.Unix(2020, 6, 5), Address = "190577, г. Санкт-Петербург, ул. Свободы, 41", AreaId = 3, MaterialId = 5, TypeId = 1, Floor = 6, Rooms = 4, Space = 2000, Status = false, Description = "Что-то на богатом" },
                new Estate { Id = 6, Cost = 5000000, Date = Vrem.Unix(2020, 11, 4), Address = "614005, г. Пермь, ул. Больничная, 1", AreaId = 3, MaterialId = 1, TypeId = 2, Floor = 1, Rooms = 5, Space = 3000, Status = false, Description = "Что-то на богатом" },
                new Estate { Id = 7, Cost = 5, Date = Vrem.Unix(2021, 4, 7), Address = "344279, г. Ростов-на-Дону, ул. Интернациональная, 14", AreaId = 1, MaterialId = 2, TypeId = 3, Floor = 12, Rooms = 3, Space = 1200, Status = true, Description = "Бегите отсюда" },
                new Estate { Id = 8, Cost = 9999999, Date = Vrem.Unix(2021, 4, 23), Address = "394172, г. Воронеж, ул. Береговая, 9", AreaId = 2, MaterialId = 3, TypeId = 4, Floor = 1, Rooms = 2, Space = 50000000, Status = true, Description = "Что-то на очень богатом" },
                new Estate { Id = 9, Cost = 3281084, Date = Vrem.Unix(2021, 5, 4), Address = "443564, г. Самара, ул. Заречная, 26", AreaId = 3, MaterialId = 4, TypeId = 1, Floor = 5, Rooms = 7, Space = 600, Status = true, Description = "Цену задал кот, который прошелся по клавиатуре" },
                new Estate { Id = 10, Cost = 2000000, Date = Vrem.Unix(2022, 5, 2), Address = "454579, г. Челябинск, ул. Зеленая, 22", AreaId = 4, MaterialId = 5, TypeId = 2, Floor = 1, Rooms = 4, Space = 700, Status = false, Description = "Вид классный" },
                new Estate { Id = 11, Cost = 2500000, Date = Vrem.Unix(2022, 1, 30), Address = "660738, г. Красноярск, ул. Чкалова, 36, кв", AreaId = 5, MaterialId = 1, TypeId = 3, Floor = 2, Rooms = 3, Space = 80000, Status = true, Description = "Вид не очень классный, но это не повод занижать цену" },
                new Estate { Id = 12, Cost = 500000, Date = Vrem.Unix(2023, 1, 20), Address = "614868, г. Пермь, ул. Клубная, 43", AreaId = 6, MaterialId = 2, TypeId = 2, Floor = 1, Rooms = 2, Space = 400, Status = false, Description = "Что-то на бедном" },
                new Estate { Id = 13, Cost = 100000, Date = Vrem.Unix(2023, 2, 5), Address = "190040, г. Санкт-Петербург, ул. Чапаева, 27", AreaId = 1, MaterialId = 3, TypeId = 2, Floor = 17, Rooms = 2, Space = 450, Status = true, Description = "Что-то на бедном" },
                new Estate { Id = 14, Cost = 600000, Date = Vrem.Unix(2023, 3, 11), Address = "125274, г. Москва, ул. Нагорная, 44", AreaId = 2, MaterialId = 4, TypeId = 2, Floor = 1, Rooms = 2, Space = 200, Status = false, Description = "Что-то на бедном" },
                new Estate { Id = 15, Cost = 300000, Date = Vrem.Unix(2024, 5, 12), Address = "660932, г. Красноярск, ул. Красная", AreaId = 3, MaterialId = 5, TypeId = 2, Floor = 16, Rooms = 2, Space = 500, Status = false, Description = "Что-то на бедном" }
            );
            modelBuilder.Entity<Mark>().HasData(
                new Mark { Id = 1, Date = Vrem.Unix(2020, 1, 1), EstateId = 1, CriteriaId = 1, Points = 100 },
                new Mark { Id = 2, Date = Vrem.Unix(2020, 2, 1), EstateId = 1, CriteriaId = 2, Points = 70 },
                new Mark { Id = 3, Date = Vrem.Unix(2020, 3, 1), EstateId = 1, CriteriaId = 3, Points = 30 },
                new Mark { Id = 4, Date = Vrem.Unix(2020, 4, 1), EstateId = 1, CriteriaId = 4, Points = 70 },
                new Mark { Id = 5, Date = Vrem.Unix(2020, 5, 1), EstateId = 1, CriteriaId = 5, Points = 100 },
                new Mark { Id = 6, Date = Vrem.Unix(2020, 6, 1), EstateId = 1, CriteriaId = 5, Points = 50 }
            );
            modelBuilder.Entity<Agent>().HasData(
                new Agent { Id = 1, Name = "Андрей", Surname = "Андерсон", Midname = "Анатольевич", Number = "111-111-11-11" },
                new Agent { Id = 2, Name = "Борис", Surname = "Большаков", Midname = "Богданович", Number = "222-222-22-22" },
                new Agent { Id = 3, Name = "Владимир", Surname = "Вавилов", Midname = "Викторович", Number = "333-333-33-33" },
                new Agent { Id = 4, Name = "Глеб", Surname = "Герасимов", Midname = "Галактионович", Number = "444-444-44-44" },
                new Agent { Id = 5, Name = "Дамир", Surname = "Донской", Midname = "Дмитриевич", Number = "555-555-55-55" }
            );
            modelBuilder.Entity<Sale>().HasData(
                new Sale { Id = 1, AgentId = 1, EstateId = 1, Cost = 10, Date = Vrem.Unix(2007, 3, 1) },
                new Sale { Id = 2, AgentId = 1, EstateId = 2, Cost = 8, Date = Vrem.Unix(2021, 3, 1) },
                new Sale { Id = 3, AgentId = 1, EstateId = 10, Cost = 1980000, Date = Vrem.Unix(2000, 3, 1) },
                new Sale { Id = 4, AgentId = 1, EstateId = 11, Cost = 460000, Date = Vrem.Unix(2000, 3, 1) },
                new Sale { Id = 5, AgentId = 1, EstateId = 12, Cost = 415000, Date = Vrem.Unix(2007, 3, 1) },
                new Sale { Id = 6, AgentId = 1, EstateId = 13, Cost = 1200000, Date = Vrem.Unix(2007, 3, 1) },
                new Sale { Id = 7, AgentId = 1, EstateId = 14, Cost = 1200000, Date = Vrem.Unix(2007, 3, 1) },
                new Sale { Id = 8, AgentId = 1, EstateId = 15, Cost = 1200000, Date = Vrem.Unix(2008, 3, 1) },
                new Sale { Id = 9, AgentId = 4, EstateId = 6, Cost = 1200000, Date = Vrem.Unix(2008, 3, 1) },
                new Sale { Id = 10, AgentId = 4, EstateId = 5, Cost = 1200000, Date = Vrem.Unix(2007, 3, 1) },
                new Sale { Id = 11, AgentId = 4, EstateId = 4, Cost = 1200000, Date = Vrem.Unix(2008, 3, 1) },
                new Sale { Id = 12, AgentId = 2, EstateId = 8, Cost = 1200000, Date = Vrem.Unix(2024, 3, 1) },
                new Sale { Id = 13, AgentId = 3, EstateId = 7, Cost = 1200000, Date = Vrem.Unix(2024, 3, 1) }
            );
        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var options = new
            {
                Server = ("89.169.1.120:5437"),
                Database = ("postgres"),
                User = ("postgres"),
                Password = ("Rudolf31"),
            };
            optionsBuilder.UseNpgsql($"Server = {options.Server}; Database ={options.Database}; Uid = {options.User}; Pwd = {options.Password};");
        }
    }
}