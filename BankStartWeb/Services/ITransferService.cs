namespace BankStartWeb.Services
{
    public interface ITransferService
    {

        //Det ska finnas Unit Test-projekt där du skriver tester som testar att det 

        //1) Det ska inte gå att ta ut mer pengar än det som finns på kontot
        //2) Det ska inte gå att överföra mer pengar än det som finns på kontot
        //3) Det ska inte gå att sätta in negativ belopp.
        //4) Det ska inte gå att ta ut negativa belopp.
        public enum Status
        {
            ok,
            InsufficientFunds,
            NegativeAmount,
            Error
        }

        public Status Withdraw(int AccountId, decimal Amount);

        public Status Deposit(int AccountId, decimal Amount);

        public Status Swish(int AccountId, int ReceiverId, decimal Amount);
    }
}
