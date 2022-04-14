using BankStartWeb.Data;
using BankStartWeb.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    [Authorize(Roles = "Admin,Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public string SortOrder { get; set; }

        public string SortCol { get; set; }

        public int PageNo { get; set; }

        public int TotalPageCount { get; set; }

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

        public void OnGet(string searchWord, string col = "id", string order = "asc", int pageno = 1)
        {

            PageNo = pageno;
            SearchWord = searchWord;
            SortCol = col;
            SortOrder = order;

            var o = _context.Customers.AsQueryable();
         
            if (!string.IsNullOrEmpty(SearchWord))
                o = o.Where(ord => ord.Givenname.Contains(SearchWord)
                              || ord.Surname.Contains(SearchWord)
                              || ord.City.Contains(SearchWord)
                              || ord.Id.ToString() == (SearchWord)
                );

            o = o.OrderBy(col, order == "asc" ? ExtensionMethods.QuerySortOrder.Asc : ExtensionMethods.QuerySortOrder.Desc);

            //if (col == "givenname")
            //{
            //    if (order == "asc")
            //        o = o.orderby(ord => ord.givenname);
            //    else
            //        o = o.orderbydescending(ord => ord.givenname);
            //}
            //else if (col == "surname")
            //{
            //    if (order == "asc")
            //        o = o.orderby(ord => ord.surname);
            //    else
            //        o = o.orderbydescending(ord => ord.surname);
            //}
            //else if (col == "nationalid")
            //{
            //    if (order == "asc")
            //        o = o.orderby(ord => ord.nationalid);
            //    else
            //        o = o.orderbydescending(ord => ord.nationalid);
            //}
            //else if (col == "city")
            //{
            //    if (order == "asc")
            //        o = o.orderby(ord => ord.city);
            //    else
            //        o = o.orderbydescending(ord => ord.city);
            //}
            //else if (col == "streetaddress")
            //{
            //    if (order == "asc")
            //        o = o.orderby(ord => ord.streetaddress);
            //    else
            //        o = o.orderbydescending(ord => ord.streetaddress);
            //}
            //else if (col == "custid")
            //{
            //    if (order == "asc")
            //        o = o.orderby(ord => ord.id);
            //    else
            //        o = o.orderbydescending(ord => ord.id);
            //}

            var pageResult = o.GetPaged(PageNo, 20);
            TotalPageCount = pageResult.PageCount;

            Customers = pageResult.Results.Select(x => new CustomersViewModel
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
