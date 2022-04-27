using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankStartWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace TestProjectBank.Services
{
    public class TransferServiceTest
    {
        private readonly ApplicationDbContext _context;
        private readonly TransferServiceTest _sut;

        public TransferServiceTest()
        {
                var options = new DbContextOptions<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "test")
                    .Options;
                _context = new ApplicationDbContext()options;
                _sut = new TransferServiceTest(_context);
        }
    }
}
