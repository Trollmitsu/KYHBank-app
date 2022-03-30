using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public class CustomersViewModel
        {
            public int Id { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public DateTime Birthday { get; set; }

            public string Country { get; set; }

            public string EmailAddress { get; set; }

        }
        public List<CustomersViewModel> Customers = new List<CustomersViewModel>();

        public void OnGet()
        {
            Customers = _context.Customers.Select(x => new CustomersViewModel
            {
                Id = x.Id,
                Givenname = x.Givenname,
                Surname = x.Surname,
                Birthday = x.Birthday,
                Country = x.Country,
                EmailAddress = x.EmailAddress
        }).ToList();  
        }

        public IActionResult OnPostByName()
        {
            var Customer = _context.Customers
                .Include(s => s.Surname)
                .Include(s => s.Birthday)
                .Include(s => s.Country)
                .Include(s => s.EmailAddress)
                .OrderBy(b => b.Givenname).ToList();
            return Page();
        }
    }
}
