using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileClasses;
using ClassLibrary;
using System.ComponentModel;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : Controller
    {
        private readonly ProjectDatabaseContext _context;

        public ProfilesController(ProjectDatabaseContext context)
        {
            _context = context;
        }
        [HttpGet("~/GetAllProfiles")]
        public async Task<List<Profile>> GetAllProfiles()
        {
            var items = await _context.Profiles.ToListAsync();
            return items;
        }
        [HttpGet("~/GetProfileById")]
        public async Task<Profile> GetProfileById(Guid id)
        {
            var items = await _context.Profiles.Include(a => a.Subscriptions).Where(elem => elem.Id == id).FirstOrDefaultAsync();
            return items;
        }
        [HttpGet("~/GetDistributorProfiles")]
        public async Task<List<Profile>> GetDistributorProfiles()
        {
            var distributors = await _context.Distributors.Select(elem => elem.Id).ToListAsync();
            var items = await _context.Profiles.Where(elem => distributors.Contains(elem.Id)).ToListAsync();
            return items;
        }
        [HttpPut("~/AddNewProfile")]
        public async Task AddNewProfile(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }
        [HttpPut("~/Update")]
        public async Task UpdateProfile()
        {
            await _context.SaveChangesAsync();
        }
        [HttpGet("~/ChangeSubscriptionStatus")]
        public async Task ChangeSubscriptionStatus(Guid distributorId, Guid subscriberId)
        {
            var profile = await _context.Profiles.Include(a => a.Subscriptions).Where(profile => profile.Id == subscriberId).FirstOrDefaultAsync();
            var distributor = await _context.Distributors.Where(distributor => distributor.Id == distributorId).FirstOrDefaultAsync();
            if (profile.Subscriptions?.Contains(distributor) == true)
            {
                profile.Subscriptions.Remove(distributor);
            }
            else if(profile.Subscriptions == null)
            {
                profile.Subscriptions = new();
                profile.Subscriptions.Add(distributor);
            }
            else
            {
                profile.Subscriptions.Add(distributor);
            }
            await _context.SaveChangesAsync();
        }
    }
}
