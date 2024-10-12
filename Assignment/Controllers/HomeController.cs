using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Load Component Selection Page (Page 1)
        public IActionResult Index()
        {
            return View();
        }

        // Load RM Page (Page 2)
        public IActionResult RMPage(string code, string desc)
        {
            var model = new ComponentWithRMViewModel
            {
                ComponentCode = code,
                ComponentDescription = desc
            };
            return View(model);
        }

        // Load List Page (Page 3)
        public IActionResult ListPage()
        {
            var model = _context.Components.Include(c => c.RMData).ToList();
            return View(model);
        }

        // Save RM data to the database (POST)
        [HttpPost]
        public async Task<IActionResult> SaveRM([FromBody] ComponentWithRMViewModel model)
        {
            if (model.RMData == null || !model.RMData.Any())
            {
                return Json(new { success = false, message = "Please add at least one RM entry." });
            }

            if (string.IsNullOrEmpty(model.ComponentCode) || string.IsNullOrEmpty(model.ComponentDescription))
            {
                return Json(new { success = false, message = "Component Code and Description are required." });
            }

            // Find component by code
            // Find component by code and include RMData
            var existingComponent = await _context.Components
                .Include(c => c.RMData)
                .FirstOrDefaultAsync(c => c.ComponentCode == model.ComponentCode);

            if (existingComponent == null)
            {
                // Create a new Component entity with its RM data
                var component = new RmComponent
                {
                    ComponentCode = model.ComponentCode,
                    ComponentDescription = model.ComponentDescription,
                    RMData = model.RMData.Select(rm => new RM
                    {
                        RMName = rm.RMName,
                        RMRate = rm.RMRate
                    }).ToList()
                };

                // Save to the database
                _context.Components.Add(component);
            }
            else
            {
                // Add the RM data into existing component
                existingComponent.RMData.AddRange(model.RMData.Select(rm => new RM
                {
                    RMName = rm.RMName,
                    RMRate = rm.RMRate,
                    ComponentId = existingComponent.Id // Setting foreign key reference
                }));
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }




    }
}
