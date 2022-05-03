using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BankStartWeb.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BankStartWeb.Pages
{
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransferService _transferService;

        public WithdrawModel(ApplicationDbContext context, ITransferService transferService)
        {
            _context = context;
            _transferService = transferService;
        }

        [BindProperty]
        public DateTime Date { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }

        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Account Account { get; set; }


        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
        }

        public IActionResult OnPost(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
            Customer = _context.Customers.First(e => e.Id == customerId);
            Account = _context.Accounts.Include(c => c.Transactions).First(e => e.Id == accountId);
            
        

            if (ModelState.IsValid)
            {

                var Withdraw = _transferService.Withdraw(accountId, Amount);

                if (Withdraw == ITransferService.Status.ok)
                {
                    _context.SaveChanges();
                }
                if (Withdraw == ITransferService.Status.Error)
                {
                    ModelState.AddModelError(nameof(Amount), "Cannot withdraw more than 7000kr");

                    return Page();
                }

                if (Withdraw == ITransferService.Status.InsufficientFunds)
                {
                    ModelState.AddModelError(nameof(Amount), "InsufficientFunds");

                    return Page();
                }

                if (Withdraw == ITransferService.Status.NegativeAmount)
                {
                    ModelState.AddModelError(nameof(Amount), "Cannot Withdraw Negative Amount");
                    return Page();
                }
                return RedirectToPage("/AllCustomers/Customer", new { customerId });
            
            }

            

            return Page();
        }
    }
}
