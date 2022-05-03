using BankStartWeb.Data;
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
            if (Amount < )
            {
                return ITransferService.Status.InsufficientFunds;
            }
            var account = _context.Accounts.Include(T => T.Transactions).First(a => a.Id == AccountId);
            
            account.Transactions.Add(new Transaction
            {
                Type = "Credit",
                Operation = "ATM Withdrawal",
                Date = DateTime.Now,
                Amount = Amount,
                NewBalance = account.Balance - Amount
            });

            account.Balance = account.Balance - Amount;
            if (_context.SaveChanges() > account.Balance)
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

                return ITransferService.Status.Error;
        }

        public ITransferService.Status Swish(int AccountId, int CustomerId)
        {
            throw new NotImplementedException();
        }
    }
}
