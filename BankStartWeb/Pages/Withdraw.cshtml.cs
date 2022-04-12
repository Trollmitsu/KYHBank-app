using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages
{
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public WithdrawModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DateTime Date { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }

        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Account Account { get; set; }


        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
        }

        public IActionResult OnPost(int AccountId, int CustomerId)
        {
            

            if (ModelState.IsValid)
            {
                Customer = _context.Customers.First(e => e.Id == CustomerId);
                Account = _context.Accounts.Include(c => c.Transactions).First(e => e.Id == AccountId);
                Account.Transactions.Add(new Transaction
                {
                    Type =  "Credit",
                    Operation = "ATM Withdrawal",
                    Date = DateTime.Now,
                    Amount = Amount,
                    NewBalance = Account.Balance - Amount
                });

                Account.Balance = Account.Balance - Amount;
                _context.SaveChanges();
                
            }
            return RedirectToPage("/Customer", new { CustomerId });
        }
    }
}
