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

        public string AccountId { get; set; }

        public List<SelectListItem> AllAccounts { get; set; }
        
        [Required]
        public string Message { get; set; }

        public decimal Amount { get; set; }

        public Customer Customer { get; set; }

        
        public int CustomersAccountId { get; set; }

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
       public IActionResult OnPost()
        {
            return RedirectToPage("Customer");
        }

    }
}
