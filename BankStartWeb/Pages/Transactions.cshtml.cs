using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static BankStartWeb.Pages.CustomersModel;

namespace BankStartWeb.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public class TransactionsViewModel
        {
            public int Id { get; set; }

            public string Type { get; set; }
            
            public string Operation { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public decimal NewBalance { get; set; }
           

        }
        public Account Account { get; set; }

        public List<CustomersViewModel> Customers = new List<CustomersViewModel>();
        public List<TransactionsViewModel> transaction { get; set; }
        public void OnGet(int AccountId)
        {
            Account = _context.Accounts
                .Include(a => a.Transactions)
                .First(a => a.Id == AccountId);
            transaction = Account.Transactions.Select(a => new TransactionsViewModel
            {
                Id = a.Id,
                Type = a.Type,
                Operation = a.Operation,
                Date = a.Date,
                Amount = a.Amount
            }).ToList();
        }

        

    }
}
