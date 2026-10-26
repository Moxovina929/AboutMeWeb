using AboutMeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AboutMeWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var bio = new List<BioItemViewModel>
            {
                new BioItemViewModel
                {
                    Year = "2018",
                    Title = "First steps",
                    Text = "I started playing Garry's Mod and became interested in how to create your own server. I wanted to understand the mechanics and create my own content.",
                    Image = "gmod.png"
                },
                new BioItemViewModel
                {
                    Year = "2021",
                    Title = "Minecraft servers",
                    Text = "I set up my first Minecraft server for friends, learned how to install plugins and mods and hosting services.",
                    Image = "minecraft.png"
                },
                new BioItemViewModel
                {
                    Year = "2024",
                    Title = "Programming",
                    Text = "I started learning C# and object-oriented programming. My first console applications and small games.",
                    Image = "software.png"
                },
                new BioItemViewModel
                {
                    Year = "2025",
                    Title = "AI Integration",
                    Text = "I helped my friends to make AI-powered applications.",
                    Image = "ai.png"
                },
                new BioItemViewModel
                {
                    Year = "2026",
                    Title = "Web development",
                    Text = "Now I am learning ASP.NET Core. This website is my course project.",
                    Image = "website.png"
                }
            };

            for (int i = 0; i < bio.Count; i++)
            {
                bio[i].IsLeft = i % 2 == 0;
            }

            return View(bio);
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
