using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProfileClasses;

namespace WebApplication1.Controllers
{
    public class RatingController : Controller
    {
        private readonly ProjectDatabaseContext _context;

        public RatingController(ProjectDatabaseContext context)
        {
            _context = context;
        }
        [HttpGet("~/GetAllRatings")]
        public async Task<List<Ratings>> GetAllRatings()
        {
            var items = await _context.Ratings.ToListAsync();
            return items;
        }
        [HttpGet("~/GetRatingsById")]
        public async Task<Ratings> GetRatingsById(Guid id)
        {
            var items = await _context.Ratings.Where(elem => elem.Id == id).FirstOrDefaultAsync();
            return items;
        }
        [HttpGet("~/GetRatingsByIdConcurrent")]
        public Ratings GetRatingsByIdConcurrent(Guid id)
        {
            var items = _context.Ratings.Where(elem => elem.Id == id).FirstOrDefault();
            return items;
        }
        [HttpPut("~/AddRating")]
        public async Task AddRating([FromBody] Ratings r)
        {
            Ratings rating = r;
            await _context.Ratings.AddAsync(rating);
            UpdateDistributorRating(r.DistributorId, r.rating);
			await _context.SaveChangesAsync();
		}
		[HttpPut("~/ChangeRating")]
		public async Task ChangeRating([FromBody] Ratings r)
        {
			//var profile = await _context.Profiles.Where(profile => profile.Id == id).FirstOrDefaultAsync();
            var rating = await _context.Ratings.Where(rating => rating.Id == r.Id && rating.DistributorId == r.DistributorId).FirstOrDefaultAsync();
            int changed = r.rating - rating.rating;
            rating.rating = r.rating;
            //await _context.Ratings.AddAsync(rating);
            UpdateDistributorRating(r.DistributorId, changed);
			await _context.SaveChangesAsync();
		}

        public async void UpdateDistributorRating(Guid DistributorId, int RatingChange) // padaryti per HTTP kaip ir tarkim ChangeRating
        {
            var distributor = await _context.Distributors.Where(d => d.Id == DistributorId).FirstOrDefaultAsync();
            if (distributor == null)
            {
                return;
            }

            distributor.RatingAmount++;
            distributor.Rating += RatingChange;
            //await _context.SaveChangesAsync();
        }

		[HttpGet("~/GetRatingsByUserId")]
		public async Task<List<Ratings>> GetRatingsByUserId(Guid id)
		{
			var items = await _context.Ratings.Where(rating => rating.Id == id).ToListAsync();

			return items;

		}
	}
}
