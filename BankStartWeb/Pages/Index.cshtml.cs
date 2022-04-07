using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static BankStartWeb.Pages.CustomersModel;

namespace BankStartWeb.Pages
{
    public class IndexModel : PageModel
    {
        
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            
            _context = context;
        }


        public int CustomerCount { get; set; }

        public int AccountCount { get; set; }

        public decimal BalanceCount { get; set; }

        


        public void OnGet()
        {
           CustomerCount = _context.Customers.Count();
            AccountCount = _context.Accounts.Count();
            BalanceCount = _context.Accounts.Sum(a => a.Balance);
        }
    }
}