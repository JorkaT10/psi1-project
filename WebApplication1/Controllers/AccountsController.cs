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
        public async Task AddNewAccount([FromBody] Account account)
        {
            await _context.Accounts.AddAsync(account);
            _context.SaveChanges();
        }
        [HttpGet("~/GetAccount")]
        public async Task<Guid> GetAccount(string username, string password)
        {
            var account = await _context.Accounts.Where(a => a.Password.Equals(password) && a.UserName.Equals(username)).Select(a => a.Id).FirstOrDefaultAsync();
            return account;
        }
    }
}
