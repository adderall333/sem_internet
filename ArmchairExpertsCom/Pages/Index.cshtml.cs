using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ArmchairExpertsCom.Pages
{
    public class IndexModel : PageModel
    {
        public List<IArtwork> Popular;
        
        public void OnGet()
        {
            Popular = ContentMaker.GetMostPopular().ToList();
        }
    }
}