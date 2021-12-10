using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Services;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;

        public InvoiceController(IInvoiceService invoiceService, IUserService userService)
        {
            _invoiceService = invoiceService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            bool hasAccess = await _userService.HasAccess();
            ViewBag.HasAccess = hasAccess;

            if (hasAccess)
                ViewBag.Invoices = _invoiceService.GetInvoices();
            else
                ViewBag.Invoices = _invoiceService.GetUserInvoices();
            return View();
        }
    }
}
