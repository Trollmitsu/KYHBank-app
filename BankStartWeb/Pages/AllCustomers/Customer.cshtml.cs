using AutoMapper;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    [Authorize(Roles = "Admin,Cashier")]
    public class CustomerModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper _mapper;

        public CustomerModel(ApplicationDbContext _context, IMapper mapper
            )
        {
            context = _context;
            _mapper = mapper;
        }

        public class AccountViewModel
        {
            public int Id { get; set; }

            public string AccountType { get; set; }

            public DateTime Created { get; set; }
            public decimal Balance { get; set; }

           
            public int CustomerId { get; set; }
        }
        public List<AccountViewModel> Accounts { get; set; }


        
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
