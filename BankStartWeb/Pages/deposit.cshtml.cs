using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages
{
    
    public class depositModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public depositModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string Type { get; set; }

        [BindProperty]
        public string Operation { get; set; }

        [BindProperty]
        public DateTime Date { get; set; }

        [BindProperty]
        [Required]
        //[Range(100, 5000)]
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
            Customer = _context.Customers.First(e => e.Id == CustomerId);
            Account = _context.Accounts.Include(c => c.Transactions).First(e => e.Id == AccountId);

            if (ModelState.IsValid)
            {
                if (Amount < 100)
                {
                    ModelState.AddModelError(nameof(Amount), "Beloppet är för lågt");
                }
                else if (Amount > 5000)
                {
                    ModelState.AddModelError(nameof(Amount), "Beloppet är för hög");
                }
            }

            if (ModelState.IsValid)
            {
                Account.Transactions.Add(new Transaction
                {
                    Type = Type,
                    Date = DateTime.Now,
                    Amount = Amount,
                    Operation = Operation,
                    NewBalance = Account.Balance + Amount
                });

                Account.Balance = Account.Balance + Amount;
                _context.SaveChanges();
                return RedirectToPage("/Customer", new { CustomerId });
            }

            types();
            operations();

            return Page();

            
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
