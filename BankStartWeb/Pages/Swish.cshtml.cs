using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace BankStartWeb.Pages
{
    public class SwishModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SwishModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string AccountId { get; set; }

        
        public List<SelectListItem> AllAccounts { get; set; }

        
       
        [BindProperty]
        public string Message { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }
        
        public Customer Customer { get; set; }

        
        

        public void OnGet(int CustomerId)
        {
            Customer = _context.Customers.Include(a => a.Accounts).First(s => s.Id == CustomerId);
            AllAccounts = Customer.Accounts.Select(a => new SelectListItem
            {
                Text = a.Id.ToString(),
                Value = a.Id.ToString()
            }).ToList();
            AllAccounts.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Please select Account"
            });

            //CustomersAccountId = _context.Customers.Include(a => a.Accounts.Any(s => s.CustomersAccountId));
        }
       public IActionResult OnPost(int Id, int CustomerId)
        {
            if (ModelState.IsValid)
            {

                var Account = _context.Accounts.Include(e => e.Transactions).First(s => s.Id == Id);
                //Account.Transactions.Add(new Transaction
                //{
                //    Id = Id,
                //    Amount = Amount,
                //    Date = DateTime.Now,
                //    Operation = Operation,
                //    Type = Type,
                //    NewBalance = Account.Balance + Amount

                //});

                if (_context.Accounts.Any(e => e.Id == Id))
                {

                    Account.Balance = Account.Balance + Amount;

                }

                _context.SaveChanges();
                return RedirectToPage("/Customer", new { CustomerId });
            }
            
            return Page();
       }

        
    }
}
