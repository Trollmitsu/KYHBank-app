using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public int CustomerCountSwe { get; set; }
        public int AccountCountSwe { get; set; }
        public decimal BalanceCountSwe { get; set; }

        public int CustomerCountNo { get; set; }
        public int AccountCountNo { get; set; }
        public decimal BalanceCountNo { get; set; }

        public int CustomerCountFi { get; set; }
        public int AccountCountFi { get; set; }
        public decimal BalanceCountFi { get; set; }



        public void OnGet()
        {
            CustomerCount = _context.Customers.Count();
            CustomerCountSwe = _context.Customers.Where(e=>e.Country == "sverige").Count();
            CustomerCountNo = _context.Customers.Where(e => e.Country == "norge").Count();
            CustomerCountFi = _context.Customers.Where(e => e.Country == "finland").Count();


            AccountCount = _context.Accounts.Count();
            AccountCountSwe = _context.Customers.Where(e=>e.Country == "sverige").SelectMany(e=>e.Accounts).Count();
            AccountCountNo = _context.Customers.Where(e => e.Country == "norge").SelectMany(e => e.Accounts).Count();
            AccountCountFi = _context.Customers.Where(e => e.Country == "finland").SelectMany(e => e.Accounts).Count();


            BalanceCount = _context.Accounts.Sum(a => a.Balance);
            BalanceCountSwe = _context.Customers.Where(e => e.Country == "sverige").SelectMany(e => e.Accounts).Sum(a => a.Balance);
            BalanceCountNo = _context.Customers.Where(e => e.Country == "norge").SelectMany(e => e.Accounts).Sum(a => a.Balance);
            BalanceCountFi = _context.Customers.Where(e => e.Country == "finland").SelectMany(e => e.Accounts).Sum(a => a.Balance);
        }
    }
}