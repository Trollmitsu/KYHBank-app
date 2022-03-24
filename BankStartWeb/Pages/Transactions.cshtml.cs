using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

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

        public List<TransactionsViewModel> transactions = new List<TransactionsViewModel>();
        public void OnGet(int TransactionId)
        {
            Account = _context.Accounts.Include(t => transactions).First(a => a.Id == TransactionId); 
              
        }
    }
}
