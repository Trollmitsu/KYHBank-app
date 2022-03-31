using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchWord { get; set; }

        public class CustomersViewModel
        {
            public int Id { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
            public string NationalId { get; set; }
            public string City { get; set; }

        }
        public List<CustomersViewModel> Customers = new List<CustomersViewModel>();

        public void OnGet(string searchWord, string col = "id", string order = "asc")
        {
            SearchWord = searchWord;
            var o = _context.Customers.AsQueryable();
         
            if (!string.IsNullOrEmpty(SearchWord))
                o = o.Where(ord => ord.Givenname.Contains(SearchWord)
                              || ord.Surname.Contains(SearchWord)
                              || ord.City.Contains(SearchWord)
                              || ord.Id.ToString() == (SearchWord)
                );

            if (col == "GivenName")
            {
                if (order == "asc")
                    o = o.OrderBy(ord => ord.Givenname);
                else
                    o = o.OrderByDescending(ord => ord.Givenname);
            }
            else if (col == "SurName")
            {
                if (order == "asc")
                    o = o.OrderBy(ord => ord.Surname);
                else
                    o = o.OrderByDescending(ord => ord.Surname);
            }
            else if (col == "NationalId")
            {
                if (order == "asc")
                    o = o.OrderBy(ord => ord.NationalId);
                else
                    o = o.OrderByDescending(ord => ord.NationalId);
            }
            else if (col == "City")
            {
                if (order == "asc")
                    o = o.OrderBy(ord => ord.City);
                else
                    o = o.OrderByDescending(ord => ord.City);
            }
            else if (col == "Streetaddress")
            {
                if (order == "asc")
                    o = o.OrderBy(ord => ord.Streetaddress);
                else
                    o = o.OrderByDescending(ord => ord.Streetaddress);
            }
            else if (col == "CustId")
            {
                if (order == "asc")
                    o = o.OrderBy(ord => ord.Id);
                else
                    o = o.OrderByDescending(ord => ord.Id);
            }

            Customers = o.Select(x => new CustomersViewModel
            {
                Id = x.Id,
                Givenname = x.Givenname,
                Surname = x.Surname,
                NationalId = x.NationalId,
                City = x.City,
                Streetaddress = x.Streetaddress
            }).ToList();  
        }

      
    }
}
