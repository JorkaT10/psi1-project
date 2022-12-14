using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisementsController : Controller
    {

        private readonly ProjectDatabaseContext _context;

        public AdvertisementsController(ProjectDatabaseContext context)
        {
            _context = context;
        }
        [HttpGet("~/GetAllAdvertisements")]  
        public async Task<List<Advertisement>> GetAllAdvertisements()
        {
            var items = await _context.Advertisements.ToListAsync();
            return items;
        }
        [HttpGet("~/GetAdvertisementsById")]
        public async Task<Advertisement> GetAdvertisementsById(Guid id)
        {
            var items = await _context.Advertisements.Where(elem => elem.Id == id).FirstOrDefaultAsync();
            return items;   
        }
        [HttpGet("~/GetAdsByDistributorId")]
        public async Task<List<Advertisement>> GetAdsByDistributorId(Guid id)
        {
            var items = await _context.Advertisements.Where(elem => elem.Distributor.Id == id).ToListAsync();
            return items;
        }
        [HttpGet("~/GetAdsByBuyerId")]
        public async Task<List<Advertisement>> GetAdsByBuyerId(Guid id)
        {
            var items = await _context.Advertisements.Where(elem => elem.Buyer.Id == id).ToListAsync();
            return items;
        }
        [HttpPut("~/AddAd")]
        public async void AddAd(Advertisement ad)
        {
           await _context.Advertisements.AddAsync(ad);
           await _context.SaveChangesAsync();
           
        }
    }
}
