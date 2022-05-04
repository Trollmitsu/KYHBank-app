using BankStartWeb.Data;
using BankStartWeb.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static BankStartWeb.Pages.CustomersModel;

namespace BankStartWeb.Pages
{
    [Authorize(Roles = "Admin,Cashier")]
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public class TransactionsViewModel
        {
            public int Id { get; set; }

            public string Type { get; set; }
            
            public string Operation { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public decimal NewBalance { get; set; }
           

        }
        public Account Account { get; set; }
        public Customer Customer { get; set; }

        public int CustomerId { get; set; }
        public List<TransactionsViewModel> transaction { get; set; }
        public int AccoundId { get; set; }
        public void OnGet(int accountId, int customerId)
        {
            Account = _context.Accounts
                .Include(a => a.Transactions)
                .First(a => a.Id == accountId);
            transaction = Account.Transactions.Select(a => new TransactionsViewModel
            {
                Id = accountId,
                Type = a.Type,
                Operation = a.Operation,
                Date = a.Date,
                NewBalance = a.NewBalance,
                Amount = a.Amount
            }).OrderByDescending(e=>e.Date).ToList();

            CustomerId = customerId;
            AccoundId = accountId;
        }
        public IActionResult OnPostCustomerId(int customerId)
        {
            if (ModelState.IsValid)
            {
                Customer = _context.Customers.Include(s => s.Accounts).First(a => a.Id == customerId);
                return RedirectToPage("/AllCustomers/Customer", new { customerId });
            }

            return Page();
        }

        public IActionResult OnGetFetchMore(int personId, int pageNo)
        {
            var query = _context.Accounts.Where(e => e.Id == personId)
                .SelectMany(e => e.Transactions)
                .OrderByDescending(e => e.Date);
            
            var r = query.GetPaged(pageNo, 5);

            var list = r.Results.Select(e => new 
            {
                Id = personId,
                Type = e.Type,
                Operation = e.Operation,
                Date = e.Date.ToString("d"),
                NewBalance = e.NewBalance,
                Amount = e.Amount


            }).ToList();

            return new JsonResult(new { items = list });
        }
    }
}
