using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BankStartWeb.Services;

namespace BankStartWeb.Pages
{
    public class depositModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransferService _transferService;

        public depositModel(ApplicationDbContext context, ITransferService transferService)
        {
            _context = context;
            _transferService = transferService;
        }

        [BindProperty] public string Type { get; set; }

        [BindProperty] public string? Operation { get; set; }

        [BindProperty] public DateTime Date { get; set; }

        [BindProperty]
        [Required]
        //[Range(100, 5000)]
        public decimal Amount { get; set; }

        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Account Account { get; set; }

        public List<SelectListItem> Types { get; set; }

        public List<SelectListItem> Operations { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
            types();
            operations();
        }

        public IActionResult OnPost(int accountId, int customerId)
        {

            AccountId = accountId;
            CustomerId = customerId;
            Customer = _context.Customers.First(e => e.Id == CustomerId);
            Account = _context.Accounts.Include(c => c.Transactions).First(e => e.Id == AccountId);


            if (ModelState.IsValid)
            {
                var deposit = _transferService.Deposit(AccountId, Amount);

                if (deposit == ITransferService.Status.ValueToHigh)
                {
                    ModelState.AddModelError(nameof(Amount), "Cannot deposit more than 7000kr");
                    types();
                    operations();
                    return Page();
                }

                if (deposit == ITransferService.Status.NegativeAmount)
                {
                    ModelState.AddModelError(nameof(Amount), "Cannot deposit negative amount");
                    types();
                    operations();
                    return Page();
                }

                return RedirectToPage("/AllCustomers/Customer", new {CustomerId});
            }
            types();
            operations();
            return Page();
        }

        private void types()
        {
            Types = new List<SelectListItem>()
            {
                new SelectListItem {Text = "Debit", Value = "Debit"},
                new SelectListItem {Text = "Credit", Value = "Credit"}
            };
        }

        private void operations()
        {
            Operations = new List<SelectListItem>()
            {
                new SelectListItem {Text = "Deposit Cash", Value = "Deposit Cash"}
            };
        }
    }
}