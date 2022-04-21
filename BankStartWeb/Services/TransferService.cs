using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public class TransferService // : ITransferService
    {
        private readonly ITransferService _transferService;
        private readonly ApplicationDbContext _context;

        public TransferService(ApplicationDbContext context)
        {
            _context = context;
        }
       

        //public ITransferService.SendingMoneyStatus Withdraw(DateTime Date, decimal Amount, string Type, string Operation)
        //{
        //    if (CheckCorrectAmount(Amount < 0) == false)
        //        return ITransferService.SendingMoneyStatus.InsufficientFunds;
        //}

        //public ITransferService.SendingMoneyStatus Deposit(DateTime Date, decimal Amount, string Type, string Operation)
        //{
        //    throw new NotImplementedException();
        //}

        //public ITransferService.SendingMoneyStatus Swish(DateTime Date, decimal Amount, string Type, string Operation)
        //{
        //    throw new NotImplementedException();
        //}

        //private bool CheckCorrectAmount(bool amount)
        //{
        //    if (amount)
        //        return true;
        //    return false;

        //}
    }
}
