namespace BankStartWeb.Services
{
    public interface ITransferService
    {

        //Det ska finnas Unit Test-projekt där du skriver tester som testar att det 

        //1) Det ska inte gå att ta ut mer pengar än det som finns på kontot
        //2) Det ska inte gå att överföra mer pengar än det som finns på kontot
        //3) Det ska inte gå att sätta in negativ belopp.
        //4) Det ska inte gå att ta ut negativa belopp.
        public enum SendingMoneyStatus
        {
            ok,
            InsufficientFunds,
            NegativeAmount
        }

        SendingMoneyStatus Withdraw(DateTime Date, decimal Amount, string Type, string Operation);

        SendingMoneyStatus Deposit(DateTime Date, decimal Amount, string Type, string Operation);

        SendingMoneyStatus Swish(DateTime Date, decimal Amount, string Type, string Operation);
    }
}
