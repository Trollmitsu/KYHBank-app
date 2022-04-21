using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        
        public string Type { get; set; }

        
        public string Operation { get; set; }


        public List<SelectListItem> AllAccounts { get; set; }


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

            var reciveraccount = _context.Accounts.Include(e => e.Transactions).First(s => s.Id == Id);
            var senderaccount = _context.Accounts.Include(e => e.Transactions).First(e => e.Id == CustomerId);

            if (senderaccount.Balance < Amount)
            {
                ModelState.AddModelError("Amount","Finns inte tillräckligt mycket pengar");
            }

            if (ModelState.IsValid)
            {



                reciveraccount.Transactions.Add(new Transaction
                {
                    Amount = Amount,
                    Date = DateTime.Now,
                    Operation = "Payment",
                    Type = "Credit",
                    NewBalance = reciveraccount.Balance + Amount

                });
                senderaccount.Transactions.Add(new Transaction
                {
                    Type = "Credit",
                    Operation = "Transfer",
                    Date = DateTime.Now,
                    Amount = Amount,
                    NewBalance = senderaccount.Balance - Amount
                });

              //  CustomerId.Balance = CustomerId.Balance - Amount;
               


                if (_context.Accounts.Any(e => e.Id == Id && _context.Customers.Any(e => e.Id == CustomerId)))
                {
                    reciveraccount.Balance = reciveraccount.Balance + Amount;
                    senderaccount.Balance = senderaccount.Balance - Amount;
                   
                }
               

                _context.SaveChanges();
                return RedirectToPage("/AllCustomers/Customer", new { CustomerId });
            }
            
            return Page();
       }

        
    }
}
