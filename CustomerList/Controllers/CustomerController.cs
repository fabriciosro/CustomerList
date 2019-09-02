using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerList.Data;
using CustomerList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerList.Controllers
{
  public class CustomerController : Controller
  {
    private readonly CustomerListContext _context;

    public CustomerController(CustomerListContext context)
    {
      _context = context;
    }

    // GET: Students
    public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
    {
      ViewData["CurrentSort"] = sortOrder;
      ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

      if (searchString != null)
      {
        pageNumber = 1;
      }
      else
      {
        searchString = currentFilter;
      }

      ViewData["CurrentFilter"] = searchString;

      var customers = from s in _context.Customers
                      select s;
      if (!String.IsNullOrEmpty(searchString))
      {
        customers = customers.Where(s => s.Name.Contains(searchString));
      }
      switch (sortOrder)
      {
        case "name_desc":
          customers = customers.OrderByDescending(s => s.Name);
          break;
        case "Date":
          customers = customers.OrderBy(s => s.LastPurchase);
          break;
        case "date_desc":
          customers = customers.OrderByDescending(s => s.LastPurchase);
          break;
        default:
          customers = customers.OrderBy(s => s.Name);
          break;
      }

      int pageSize = 5;

      return View(await PaginatedList<Customer>.CreateAsync(customers.AsNoTracking(), pageNumber ?? 1, pageSize));
    }

    //// GET: Customers/Details/5
    //public async Task<IActionResult> Details(int? id)
    //{
    //  if (id == null)
    //  {
    //    return NotFound();
    //  }

    //  var customer = await _context.Customers
    //       .Include(s => s.UserId)
    //           .ThenInclude(e => e.)
    //       .AsNoTracking()
    //       .FirstOrDefaultAsync(m => m.ID == id);

    //  if (customer == null)
    //  {
    //    return NotFound();
    //  }

    //  return View(customer);
    //}

    // GET: Customers/Create
    public IActionResult Create()
    {
      return View();
    }
  }
}
