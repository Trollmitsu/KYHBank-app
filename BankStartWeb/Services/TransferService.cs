using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferService _transferService;
        private readonly ApplicationDbContext _context;

        public TransferService(ApplicationDbContext context)
        {
            _context = context;
        }


        public ITransferService.Status Withdraw(int AccountId, decimal Amount)
        {
            
            var account = _context.Accounts.Include(T => T.Transactions).First(a => a.Id == AccountId);

            if (Amount < 0)
            {
                return ITransferService.Status.NegativeAmount;
                
            }

            if (Amount > 7000)
            {
                return ITransferService.Status.Error;

            }

            account.Transactions.Add(new Transaction
            {
                Type = "Credit",
                Operation = "ATM Withdrawal",
                Date = DateTime.Now,
                Amount = Amount,
                NewBalance = account.Balance - Amount
            });

            account.Balance = account.Balance - Amount;
            if (Amount < account.Balance)
            {
                return ITransferService.Status.ok;
            }

            
            if (Amount > account.Balance)
            {
                return ITransferService.Status.InsufficientFunds;
            }

          
            return ITransferService.Status.Error;
        }

        public ITransferService.Status Deposit(int AccountId, decimal Amount)
        {
            if (Amount < 0)
            {
                return ITransferService.Status.NegativeAmount;
            }
            if (Amount > 7000)
            {
                return ITransferService.Status.Error;

            }

            var account = _context.Accounts.Include(T => T.Transactions).First(a => a.Id == AccountId);

                account.Transactions.Add(new Transaction
            {
                Type = "Debit",
                Date = DateTime.Now,
                Amount = Amount,
                Operation = "Deposit cash",
                NewBalance = account.Balance + Amount
            });

                account.Balance = account.Balance + Amount;
                if (_context.SaveChanges() > 0)
                {
                    return ITransferService.Status.ok;
                }

                return ITransferService.Status.ok;
        }

      

        public ITransferService.Status Swish(int senderId, int receiverId, decimal Amount)
        {

            var senderaccount = _context.Accounts.Include(e => e.Transactions).First(a => a.Id == senderId);
            var reciveraccount = _context.Accounts.Include(e => e.Transactions).First(a => a.Id == receiverId);


            senderaccount.Transactions.Add(new Transaction
            {
                Type = "Credit",
                Operation = "Transfer",
                Date = DateTime.Now,
                Amount = Amount,
                NewBalance = senderaccount.Balance - Amount
            });

            reciveraccount.Transactions.Add(new Transaction
            {
                Amount = Amount,
                Date = DateTime.Now,
                Operation = "Payment",
                Type = "Credit",
                NewBalance = reciveraccount.Balance + Amount

            });

            if (senderId == receiverId)
            {
                return ITransferService.Status.Error;
            }

            if (Amount > senderaccount.Balance)
            {
                return ITransferService.Status.InsufficientFunds;
            }
            if (Amount < 0)
            {
                return ITransferService.Status.NegativeAmount;
            }
            if (Amount > 5000)
            {
                return ITransferService.Status.Error;
            }

            if (Amount < senderaccount.Balance)
            {
                senderaccount.Balance = senderaccount.Balance - Amount;
                reciveraccount.Balance = reciveraccount.Balance + Amount;
                

                _context.SaveChanges();

                return ITransferService.Status.ok;
            }

          

            return ITransferService.Status.Error;
        }
    }
}
