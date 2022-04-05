using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.CRUD
{
    public class NewPersonModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NewPersonModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string NationalId { get; set; }

        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var customer = new Data.Customer();
                customer.Givenname = Givenname;
                customer.Surname = Surname;
                customer.Streetaddress = Streetaddress;
                customer.City = City;
                customer.Zipcode = Zipcode;
                customer.Country = Country;
                customer.CountryCode = CountryCode;
                _context.SaveChanges();

                return RedirectToPage("/Pages/Customers");
            }
            return Page();
        }
    }
}
