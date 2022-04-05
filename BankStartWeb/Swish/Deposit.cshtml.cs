using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Swish
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly ApplicationDbContext _context;



        [Range(10, 3000)]
        public int Amount { get; set; }

        public DateTime DateWhen { get; set; }

        [Required(ErrorMessage = "Skriv en en kommentar dummer")]
        [MinLength(5)]
        [MaxLength(100)]
        public string Comment { get; set; }

        public DepositModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int accountId)
        {
            DateWhen = DateTime.Now.AddDays(1).Date;
            Amount = 100;
        }


        public IActionResult OnPost(int accountId)
        {
            if (DateWhen < DateTime.Now.AddDays(1).Date)  //2022-01-11 00:00
            {
                ModelState.AddModelError("DateWhen", "Datum måste vara minst en dag fram ");
            }
            if (ModelState.IsValid)
            {
                //var account = _context.Accounts(accountId);
                //account.Balance += Amount;
                //_context.Update(account);
                //return RedirectToPage("Index");
            }

            return Page();
        }

    }
}
