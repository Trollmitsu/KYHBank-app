using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BankStartWeb.Services;
using NToastNotify;


namespace BankStartWeb.Pages
{
    
    public class SwishModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransferService _transferService;
        private readonly IToastNotification _toastNotification;

        public SwishModel(ApplicationDbContext context, ITransferService transferService, IToastNotification toastNotification)
        {
            _context = context;
            _transferService = transferService;
            _toastNotification = toastNotification;
        }
        [BindProperty]
        public int AccountId { get; set; }

        public int CustomerId { get; set; }

        public string Type { get; set; }

        
        public string Operation { get; set; }


        public List<Account> Accounts { get; set; }


        [BindProperty]
        public decimal Amount { get; set; }
        
        public Customer Customer { get; set; }
        [BindProperty]
        public int ReceiverId { get; set; }
        

        public void OnGet(int customerId, int accountId)
        {
            AccountId = accountId;
            CustomerId = customerId;
            Customer = _context.Customers.Include(a => a.Accounts).First(s => s.Id == CustomerId);

            Accounts = Customer.Accounts.Select(a => new Account
            {
                Id = a.Id,
            }).ToList();
            


            //CustomersAccountId = _context.Customers.Include(a => a.Accounts.Any(s => s.CustomersAccountId));
        }
       public IActionResult OnPost(int Id, int CustomerId)
       {
           Id = AccountId;
         

            if (ModelState.IsValid)
            {
                Customer = _context.Customers.First(e => e.Id == CustomerId);
                var swish = _transferService.Swish(Id, ReceiverId, Amount);

                if (swish == ITransferService.Status.NegativeAmount)
                {
                    _toastNotification.AddErrorToastMessage("Cannot send negative amount");
                    return Page();
                }

                if (swish == ITransferService.Status.InsufficientFunds)
                {
                    _toastNotification.AddErrorToastMessage("Cannot send more than your account balance");
                    return Page();
                }

                if (swish == ITransferService.Status.Error)
                {
                    _toastNotification.AddErrorToastMessage("Cannot Swish to same account");
                    return Page();
                }

                if (swish == ITransferService.Status.AccountNotFound)
                {
                    _toastNotification.AddErrorToastMessage("Invalid Account Number");
                    return Page();
                }
                _toastNotification.AddSuccessToastMessage();
                return RedirectToPage("/AllCustomers/Customer", new { CustomerId });

            }

                
            return Page();
       }

      
        
    }
}
