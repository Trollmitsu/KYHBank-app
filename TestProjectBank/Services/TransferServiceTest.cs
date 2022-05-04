using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProjectBank.Services
{
    [TestClass]
    public class TransferServiceTest
    {
        private readonly ApplicationDbContext _context;
        private readonly TransferService _sut;

        public TransferServiceTest()
        {
               var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: "test")
                   .Options;
               _context = new ApplicationDbContext(options);
               _sut = new TransferService(_context);
        }

        [TestMethod]
        public void If_Withdrawal_Is_More_Than_Account_Balance_Return_InsufficientFunds()
        {
            var a = new Account()
            {
                AccountType = "Credit",
                Balance = 1,
                Created = DateTime.Now,
                Id = 1
            };
            _context.Accounts.Add(a);
            _context.SaveChanges();

            var status = _sut.Withdraw(a.Id, 1000);
            Assert.AreEqual(ITransferService.Status.InsufficientFunds, status);
        }

        [TestMethod]
        public void If_Withdrawal_Is_In_Negative_Return_Negative_Amount()
        {
            var a = new Account()
            {
                AccountType = "Credit",
                Balance = 1,
                Created = DateTime.Now,
                Id = 2
            };
            _context.Accounts.Add(a);
            _context.SaveChanges();

            var status = _sut.Withdraw(a.Id, -1000);
            Assert.AreEqual(ITransferService.Status.NegativeAmount, status);
        }

        [TestMethod]
        public void If_Deposit_Is_In_Negative_Amount()
        {
            var a = new Account()
            {
                AccountType = "Debit",
                Balance = 100,
                Created = DateTime.Now,
                Id = 3
            };
            _context.Accounts.Add(a);
            _context.SaveChanges();

            var status = _sut.Deposit(a.Id, -1000);
            Assert.AreEqual(ITransferService.Status.NegativeAmount, status);
        }

       
        [TestMethod]
        public void If_Swish_Is_In_Negative_Amount()
        {
            var b = new Account()
            {
                AccountType = "Credit",
                Balance = 100,
                Created = DateTime.Now,
                Id = 4
            };
            var a = new Account()
            {
                AccountType = "Debit",
                Balance = 100,
                Created = DateTime.Now,
                Id = 5
            };
            _context.Accounts.Add(a);
            _context.Accounts.Add(b);
            _context.SaveChanges();

            var status = _sut.Swish(a.Id,b.Id ,-1000);
            Assert.AreEqual(ITransferService.Status.NegativeAmount, status);
        }

        //[TestMethod]
        //public void If_Swish_Is_More_Than_Account_Balance()
        //{

        //}
    }
}
