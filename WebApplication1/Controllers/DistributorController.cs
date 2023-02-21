using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileClasses;

namespace WebApplication1.Controllers
{
    public class DistributorController : Controller
    {
        private readonly ProjectDatabaseContext _context;

        public DistributorController(ProjectDatabaseContext context)
        {
            _context = context;
        }
        [HttpGet("~/GetAllDistributors")]
        public async Task<List<Distributor>> GetAllDistributors()
        {
            var items = await _context.Distributors.ToListAsync();
            return items;
        }
        [HttpGet("~/GetDistributorsById")]
        public async Task<Distributor> GetDistributorsById(Guid id)
        {
            var items = await _context.Distributors.Where(elem => elem.Id == id).FirstOrDefaultAsync();
            return items;
        }
        [HttpGet("~/GetDistributorsByIdConcurrent")]
        public Distributor GetDistributorsByIdConcurrent(Guid id)
        {
            var items = _context.Distributors.Where(elem => elem.Id == id).FirstOrDefault();
            return items;
        }
        [HttpPut("~/AddDistributors")]
        public async Task AddDistributors([FromBody] Guid id)
        {
            Distributor distributor = new();
            distributor.Id = id;
            await _context.Distributors.AddAsync(distributor);
            await _context.SaveChangesAsync();

        }
    }
}
