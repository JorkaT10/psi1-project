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
            var items = await _context.Profiles.Where(elem => elem.Id == id).FirstOrDefaultAsync();
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
    }
}
