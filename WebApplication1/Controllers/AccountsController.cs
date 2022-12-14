using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileClasses;

namespace WebApplication1.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ProjectDatabaseContext _context;

        public AccountsController(ProjectDatabaseContext context)
        {
            _context = context;
        }
        [HttpGet("~/GetAllAccounts")]
        public async Task<List<Account>> GetAllAccounts()
        {
            var items = await _context.Accounts.ToListAsync();
            return items;
        }
        [HttpGet("~/GetAccountsById")]
        public async Task<Account> GetAccountById(Guid id)
        {
            var items = await _context.Accounts.Where(elem => elem.Id == id).FirstOrDefaultAsync();
            return items;
        }
        [HttpPut("~/AddNewAccount")]
        public async void AddNewAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}
