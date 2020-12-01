using System.IO;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Expert
{
    public class Edit : PageModel
    {
        public User Expert { get; private set; }
        
        private readonly IWebHostEnvironment environment;
        
        public Edit(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        
        public IActionResult OnGet()
        {
            Expert = Auth.GetUser(HttpContext);
            
            if (Expert is null)
                return Redirect("/login?from=expert");

            return Page();
        }

        public IActionResult OnPost(
            string name, 
            string surname,
            string birthday,
            IFormFile image)
        {
            Expert = Auth.GetUser(HttpContext);
            
            if (Expert is null)
                return Redirect("/login?from=expert");
            
            Auth.EditProfile(Expert, name, surname, birthday, image?.FileName);

            if (image == null) 
                return Redirect("/Expert/Index");
            
            var path = Path.Combine(environment.ContentRootPath, "wwwroot", "img\\" , image.FileName);
            using var fileStream = new FileStream(path, FileMode.Create);
            image.CopyToAsync(fileStream);

            return Redirect("/Expert/Index");
        }
    }
}