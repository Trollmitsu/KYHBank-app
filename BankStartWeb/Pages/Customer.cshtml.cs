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

        public class AccountViewModel
        {
            public int Id { get; set; }

            public string AccountType { get; set; }

            public DateTime Created { get; set; }
            public decimal Balance { get; set; }

            public List<Transaction> Transactions { get; set; } = new List<Transaction>();
            public int CustomerId { get; set; }
        }
        public List<AccountViewModel> Accounts { get; set; }
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string NationalId     { get; set; }
        

        public int TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }

        
        public Customer Customer { get; set; }

        public void OnGet(int CustomerId)
        {
            Customer = context.Customers.Include(a=>a.Accounts).First(s => s.Id == CustomerId);
           
            Accounts = Customer.Accounts.Select(a => new AccountViewModel
            {
                Id = a.Id,
                Balance = a.Balance,
                AccountType = a.AccountType,
                Created = a.Created,
                CustomerId = CustomerId
            }).ToList();
        }
    }
}
