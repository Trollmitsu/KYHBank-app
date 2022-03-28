using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static BankStartWeb.Pages.CustomersModel;

namespace BankStartWeb.Pages
{
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
        public void OnGet(int AccountId, int customerId)
        {
            Account = _context.Accounts
                .Include(a => a.Transactions)
                .First(a => a.Id == AccountId);
            transaction = Account.Transactions.Select(a => new TransactionsViewModel
            {
                Id = a.Id,
                Type = a.Type,
                Operation = a.Operation,
                Date = a.Date,
                Amount = a.Amount
            }).ToList();

            CustomerId = customerId;
        }
        public IActionResult OnPostCustomerId(int customerId)
        {
            Customer = _context.Customers.Include( s=>s.Accounts ).First( a => a.Id == customerId);
            return RedirectToPage("/Customer", new {customerId});
        }
    }
}
