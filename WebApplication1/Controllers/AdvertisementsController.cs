using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;
using ProfileClasses;

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
            var items = await _context.Advertisements.Include(a => a.Buyer).ToListAsync();
            return items;
        }
        [HttpGet("~/GetAdvertisementsById")]
        public async Task<Advertisement> GetAdvertisementsById(Guid id)
        {
            var items = await _context.Advertisements.Include(a => a.Buyer).Where(elem => elem.Id == id).FirstOrDefaultAsync();
            return items;   
        }
        [HttpGet("~/GetAdsByDistributorId")]
        public async Task<List<Advertisement>> GetAdsByDistributorId(Guid id)
        {
            var items = await _context.Advertisements.Include(a => a.Buyer).Where(elem => elem.Distributor.Id == id).ToListAsync();
            return items;
        }
        [HttpGet("~/GetAdsByBuyerId")]
        public async Task<List<Advertisement>> GetAdsByBuyerId(Guid id)
        {
            var items = await _context.Advertisements.Include("Buyer").Include("Distributor").Where(elem => elem.Buyer != null && elem.Buyer.Id == id).ToListAsync();
            return items;
        }
        [HttpPut("~/AddAd")]
        public async Task AddAd([FromBody] Advertisement ad, [FromQuery] Guid distributorId)
        {
            var distributor = _context.Distributors.Where(a => a.Id == distributorId).FirstOrDefault();
            ad.Distributor = distributor;
           await _context.Advertisements.AddAsync(ad);
           await _context.SaveChangesAsync();
           
        }
        [HttpPut("~/RemoveOutdated")]
        public void RemoveOutdated([FromBody] DateTime now)
        {
            _context.RemoveRange(_context.Advertisements.Where(a => a.PickupTimeSpan < now));
            _context.SaveChanges();
        }
        [HttpPut("~/RemoveAdvertisement")]
        public async Task RemoveAdvertisement(Advertisement advertisement)
        {
            _context.Remove(advertisement);
            await _context.SaveChangesAsync();
        }
        [HttpPut("~/ChangeOrderStatus")]
        public async Task ChangeOrderStatus([FromQuery] Guid advertisementId, [FromQuery] Guid id)
        {
            var profile = _context.Profiles.Where(a => a.Id == id).FirstOrDefault();
            var advertisement = _context.Advertisements.Where(a => a.Id == advertisementId).First();
            advertisement.Buyer = profile;
            await _context.SaveChangesAsync();
        }
    }
}
