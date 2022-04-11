using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    [BindProperties]
    public class depositModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public depositModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public string Type { get; set; }
        public string Operation { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Account Account { get; set; }

        public List<SelectListItem> Types { get; set; }

        public List<SelectListItem> Operations { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
            types();
            operations();
        }

        public IActionResult OnPost(int AccountId, int CustomerId)
        {
            if (ModelState.IsValid)
            {
                Customer = _context.Customers.First(e => e.Id == CustomerId);
                Account = _context.Accounts.Include(c=>c.Transactions).First(e => e.Id == AccountId);
                Account.Transactions.Add(new Transaction
                {
                    Type = Type,
                    Date = Date,
                    Amount = Amount,
                    Operation = Operation,
                    NewBalance = Account.Balance + Amount
                });

                Account.Balance = Account.Balance + Amount;
                _context.SaveChanges();
            }
            return RedirectToPage("/Customer", new {CustomerId});
        }
        private void types()
        {
            Types = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Debit", Value = "Debit"},
                new SelectListItem{Text = "Credit", Value = "Credit"}
            };

        }
        private void operations()
        {
            Operations = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Deposit Cash", Value = "Deposit Cash"}
            };

        }
    }
}
