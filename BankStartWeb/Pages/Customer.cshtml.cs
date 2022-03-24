using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public CustomerModel(ApplicationDbContext _context)
        {
            context = _context;
        }
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string NationalId { get; set; }
        
        public int TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }

        public List<Account> Accounts { get; set; }
        public void OnGet(int CustomerId)
        {
            var customer = context.Customers.Include(a=>a.Accounts).First(s => s.Id == CustomerId);
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress;
            City = customer.City;
            Zipcode = customer.Zipcode;
            Country = customer.Country;
            CountryCode = customer.CountryCode;
            NationalId = customer.NationalId;
            TelephoneCountryCode = customer.TelephoneCountryCode;
            Telephone = customer.Telephone;
            EmailAddress = customer.EmailAddress;
            Birthday = customer.Birthday;

            Accounts = customer.Accounts;
            
        }
        
         
        
    }
}
