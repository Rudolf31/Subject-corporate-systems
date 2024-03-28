using KS2_2project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace KS2_2project
{
    public class Controller
    {
        //2.1
        public static void EstateInAreaByPrice(int areaId, float minPrice, float maxPrice)
        {
            using ApplicationContext db = new ApplicationContext();
            var estates = db.Estates.Where(est => est.AreaId == areaId && est.Cost <= maxPrice && est.Cost >= minPrice);

            foreach (var estate in estates)
            {
                Console.WriteLine($"ID: {estate.Id}, Area ID: {estate.AreaId}, Cost: {estate.Cost}");
            }
        }

        //2.2
        public static void AgentWhichSaleNFloorRoom(int rooms)
        {

            // Display the names of realtors who sold two-room properties.
            using ApplicationContext db = new ApplicationContext();
            var tempAgents = (
                from sale in db.Sales
                join agent in db.Agents on sale.AgentId equals agent.Id
                join estate in db.Estates on sale.EstateId equals estate.Id
                where estate.Rooms == rooms
                select agent
            ).Distinct();
            foreach (var a in tempAgents)
            {
                Console.WriteLine($"ID: {a.Id}, Name: {a.Name}, Middle-name: {a.Midname}, Last-name: {a.Surname}");
            }
        }

        //2.3
        public static void CostEstatesWithNRoomInXArea(int rooms, string choosing_area)
        {

            // 
            using ApplicationContext db = new ApplicationContext();
            var tempEstates = (
                from area in db.Areas
                join estate in db.Estates on area.Id equals estate.AreaId
                where area.Name == choosing_area && estate.Rooms == rooms
                select estate
            ).Distinct();
            var finallyCost = 0;
            foreach (var e in tempEstates)
            {
                finallyCost += (int)e.Cost;
                Console.WriteLine($"Finelly Cost: {finallyCost}");
            }
        }

        //2.4
        public static void MinAndMaxCostOfAgent(string surname)
        {

            // Determine the maximum and minimum cost of the object real estate sold by the specified realtor
            using ApplicationContext db = new ApplicationContext();
            var tempSales = (
                from sale in db.Sales
                join agent in db.Agents on sale.AgentId equals agent.Id
                join estate in db.Estates on sale.EstateId equals estate.Id
                where agent.Surname == surname
                select estate.Cost
            );
            Console.WriteLine($"Max cost: {tempSales.Max()}");
            Console.WriteLine($"Min cost: {tempSales.Min()}");
        }

        //2.5
        public static void AverageRatingOfXByAgentWasSailed(string choosing_type, string surname, string choosing_criteria)
        {
            using ApplicationContext db = new ApplicationContext();
            var temsSales = (
                from sale in db.Sales
                join agent in db.Agents on sale.AgentId equals agent.Id
                join estate in db.Estates on sale.EstateId equals estate.Id
                join reviews in db.Reviews on estate.Id equals reviews.EstateId
                join criteria in db.Criterias on reviews.Id equals criteria.Id
                join type in db.Types on estate.TypeId equals type.Id
                where agent.Surname == surname && type.Name == choosing_type && criteria.Name == choosing_criteria
                select reviews.Points
           );

            var sum = 0;
            foreach (var e in temsSales)
            {
                sum += (int)e;
            }
            Console.WriteLine($"Average cost: {sum / temsSales.Count()}");
        }

        //2.6
        //Display information about the number of real estate objects created on the 2nd floor for each district
        //display the name of the area and the number of objects nearby
        public static void EstateInAreaByFloor(int floor)
        {
            using ApplicationContext db = new ApplicationContext();
            var estates = db.Estates.Where(e => e.Floor == floor);
            var districts = db.Areas.Select(d => new
            {
                Id = d.Id,
                Title = d.Name,
                Amount = estates.Count(e => e.AreaId == d.Id)
            });
            foreach (dynamic district in districts)
            {
                var title = district.Title;
                var amount = district.Amount;
                Console.WriteLine($"{title}: {amount}");
            }
        }

        //2.7
        //display the realtor's name and how many properties he sold opposite
        //display how many properties each realtor sold
        
        public static void NumberEstatesWhichSoldByAgent(string choosing_type)
        {
            using ApplicationContext db = new ApplicationContext();
            var tempAgents = (
                from sale in db.Sales
                join agent in db.Agents on sale.AgentId equals agent.Id
                join estate in db.Estates on sale.EstateId equals estate.Id
                join type in db.Types on estate.TypeId equals type.Id
                where type.Name == choosing_type
                select new
                {
                    FName = agent.Name,
                    LName = agent.Surname,
                    Id = agent.Id,
                    count = db.Sales.Count(s => s.AgentId == agent.Id)
                }
            ).Distinct();

            foreach (var a in tempAgents)
            {
                Console.WriteLine($"ID: {a.Id}, Name: {a.FName}, Middle-name: {a.LName}, Count: {a.count}");
            }
        } 
        
        //2.8

        public static void TheThreeMostExpensiveBuildingsInEachArea()
        {
            using ApplicationContext db = new ApplicationContext();
            var areas = db.Areas.ToList();
            foreach (var area in areas)
            {
                var estates = db.Estates.Where(e => e.AreaId == area.Id).OrderByDescending(e => e.Cost).Take(3);
                Console.WriteLine($"Area: {area.Name}");
                foreach (var estate in estates)
                {
                    Console.WriteLine($"ID: {estate.Id}, Cost: {estate.Cost}");
                }
            }
        }

        //2.9

        public static void AgentYearsWhenSoldMoreThanNObjects(int agentId, int n)
        {
            
            ApplicationContext db = new ApplicationContext();
            var sellYears = db.Sales
                .Where(s => s.AgentId == agentId).ToList()
                .Select(s => new { Year = Vrem.UnixTimeStampToYear(s.Date) });

            var properYears = sellYears.Distinct().Where(s => sellYears.Count(y => s.Year == y.Year) >= n);

            foreach (var year in properYears)
            {
                Console.WriteLine(year.Year);
            }
        }

        //2.10
        //Identify years in which 2 to 3 objects were placed real estate.
        public static void WasPostedFromXToY(int n, int m)
        {
            using ApplicationContext db = new ApplicationContext();
            var sales = db.Sales.ToList();
            var years = sales.Select(s => new { Year = Vrem.UnixTimeStampToYear(s.Date) });
            var properYears = years.Distinct().Where(y => years.Count(yr => yr.Year == y.Year) >= n && years.Count(yr => yr.Year == y.Year) <= m);
            foreach (var year in properYears)
            {
                Console.WriteLine(year.Year);
            }
        }

        //2.11

        public static async Task EstatesInAreaWithTheDifferenceInTheDeclaredAndSellingPrice(int percent)
        {
            using ApplicationContext db = new ApplicationContext();
            var fractionMax = 0.01 * (double)percent;
            var builders = (
                from sale in db.Sales
                .Include(e => e.Estate)
                select new
                {
                    adress = sale.Estate.Address,
                    area = sale.Estate.Area,
                    cost = sale.Estate.Cost,
                    was_saled = sale.Cost,
                    diff = Math.Abs(sale.Cost / sale.Estate.Cost)
                }
            ) // Retrieve data from the database
            .Where(p => p.diff <= fractionMax);
            Console.WriteLine(fractionMax);
            foreach ( var estate in builders) 
            { 
                Console.WriteLine($"Adress: {estate.adress}, Area {estate.area}, Cost: {estate.cost}, Was saled: {estate.was_saled}, Diff: {estate.diff}"); 
            }
        }

        //2.12
        //Determine the address of the apartment, cost of 1m^2 which are less than the average area.




        //2.13
        //Determine the names of realtors who have not sold anything this year
        public static void AgentsWhoHaveNotSoldAnythingThisYear()
        {
            using ApplicationContext db = new ApplicationContext();
            var sales = db.Sales.ToList();
            var years = sales.Select(s => new { Year = Vrem.UnixTimeStampToYear(s.Date) });
            var agents = db.Agents.ToList();
            var properAgents = agents.Where(a => !sales.Any(s => s.AgentId == a.Id && years.Any(y => y.Year == Vrem.UnixTimeStampToYear(s.Date))));
            foreach (var agent in properAgents)
            {
                Console.WriteLine($"ID: {agent.Id}, Name: {agent.Name}, Middle-name: {agent.Midname}, Last-name: {agent.Surname}");
            }
        }

        //2.14
        //Display information about the number of sales in the previous and current yearsfor each district, as well as the percentage change

        public static void SalesInPreviousAndCurrentYears()
        {
            using ApplicationContext db = new ApplicationContext();
            var sales = db.Sales.Include(s => s.Estate).ToList();
            var districts = db.Areas.ToList();

            foreach (var district in districts)
            {
                var salesInDistrict = sales.Where(s => s.Estate.AreaId == district.Id).ToList();
                if (!salesInDistrict.Any()) continue;

                var currentYear = DateTime.Now.Year;
                var salesInPreviousYear = salesInDistrict.Count(s => Vrem.UnixTimeStampToYear(s.Date) == currentYear - 1);
                var salesInCurrentYear = salesInDistrict.Count(s => Vrem.UnixTimeStampToYear(s.Date) == currentYear);

                var percentChange = salesInPreviousYear != 0 ? 100 * (salesInCurrentYear - salesInPreviousYear) / (double)salesInPreviousYear : 0;

                Console.WriteLine($"District: {district.Name}, Sales in previous year: {salesInPreviousYear}, Sales in current year: {salesInCurrentYear}, Percent change: {percentChange}");
            }
        }

        //2.15
        //Determine the average score for each criterion for the specified real estate object. Print the average score and equivalent text according to table

        public static void AverageScoreForEstate(int estateId)
        {
            ApplicationContext db = new ApplicationContext();

            var standards = db.Criterias.ToList();

            var reviews = db.Reviews
                .Where(r => r.EstateId == estateId)
                .ToList()
                .GroupBy(r => r.CriteriaId)
                .Select(group => new
                {
                    Standard = db.Criterias.First(s => s.Id == group.Key).Name,
                    Percent = group.Where(g => g.CriteriaId == group.Key).Average(g => g.Points)
                })
                .Select(group => new
                {
                    Standard = group.Standard,
                    Percent = group.Percent,
                    Grade = Pointer.Grade((int)group.Percent)

                });

            foreach (var review in reviews)
            {
                Console.Write($"{review.Standard}: {review.Grade} из 5 | ");
                switch (review.Grade)
                {
                    case 5: Console.WriteLine("Превосходно"); break;
                    case 4: Console.WriteLine("Очень хорошо"); break;
                    case 3: Console.WriteLine("Хорошо"); break;
                    case 2: Console.WriteLine("Удовлетворительно"); break;
                    case 1: Console.WriteLine("Неудовлетворительно"); break;
                }
            }
        }

        //new_request
        //Average price per property on all floors
        public static void AveragePricePerPropertyOnAllFloors()
        {
            using ApplicationContext db = new ApplicationContext();
            var estates = db.Estates.ToList();
            var floors = estates.Select(e => e.Floor).Distinct();
            foreach (var floor in floors)
            {
                var averagePrice = estates.Where(e => e.Floor == floor).Average(e => e.Cost);
                Console.WriteLine($"Floor: {floor}, Average price: {averagePrice}");
            }
        }   
    }
}
