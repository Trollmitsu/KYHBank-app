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


        public List<Customer> Customers { get; set; }
        public List<Account> Accounts { get; set; }


        public void OnGet()
        {
           
        }
    }
}