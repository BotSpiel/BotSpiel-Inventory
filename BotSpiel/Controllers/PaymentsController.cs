using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BotSpiel.DataAccess.Models;
using BotSpiel.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using NonFactors.Mvc.Grid;


namespace BotSpiel
{

    public class PaymentsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPaymentsService _paymentsService;

        public PaymentsController(IPaymentsService paymentsService )
        {
            _paymentsService = paymentsService;
        }

        // GET: Payments
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var payments = _paymentsService.Index();
            return View(payments.ToList());
        }

        [Authorize]
        [HttpGet]
        public ActionResult IndexExport()
        {
            return View(CreateExportableGrid());
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult IndexGrid()
        {
            var payments = _paymentsService.Index();
            return PartialView("IndexGrid", payments.ToList());
        }

        [Authorize]
        [HttpGet]
        public FileContentResult ExportIndex()
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                Int32 row = 2;
                Int32 col = 1;

                package.Workbook.Worksheets.Add("Data");
                IGrid<Payments> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Payments> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPayments.xlsx");
            }
        }

        private IGrid<Payments> CreateExportableGrid()
        {
            IGrid<Payments> grid = new Grid<Payments>(_paymentsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPayment).Titled("Payment").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Invoices.sInvoice).Titled("Invoice").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Payments>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Payments/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_paymentsService.Get(id));
        }

        // GET: Payments/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixInvoice = new SelectList(_paymentsService.selectInvoices().Select( x => new { x.ixInvoice, x.sInvoice }), "ixInvoice", "sInvoice");

            return View();
        }

        // POST: Payments/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPayment,sPayment,ixInvoice")] PaymentsPost payments)
        {
            if (ModelState.IsValid)
            {
                payments.UserName = User.Identity.Name;
                _paymentsService.Create(payments);
                return RedirectToAction("Index");
            }
			ViewBag.ixInvoice = new SelectList(_paymentsService.selectInvoices().Select( x => new { x.ixInvoice, x.sInvoice }), "ixInvoice", "sInvoice");

            return View(payments);
        }

        // GET: Payments/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PaymentsPost payments = _paymentsService.GetPost(id);
            if (payments == null)
            {
                return NotFound();
            }
			ViewBag.ixInvoice = new SelectList(_paymentsService.selectInvoices().Select( x => new { x.ixInvoice, x.sInvoice }), "ixInvoice", "sInvoice", payments.ixInvoice);

            return View(payments);
        }

        // POST: Payments/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPayment,sPayment,ixInvoice")] PaymentsPost payments)
        {
            if (ModelState.IsValid)
            {
                payments.UserName = User.Identity.Name;
                _paymentsService.Edit(payments);
                return RedirectToAction("Index");
            }
			ViewBag.ixInvoice = new SelectList(_paymentsService.selectInvoices().Select( x => new { x.ixInvoice, x.sInvoice }), "ixInvoice", "sInvoice", payments.ixInvoice);

            return View(payments);
        }


        // GET: Payments/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_paymentsService.Get(id));
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PaymentsPost payments = _paymentsService.GetPost(id);
            payments.UserName = User.Identity.Name;
            _paymentsService.Delete(payments);
            return RedirectToAction("Index");
        } 

        [AcceptVerbs("Get", "Post")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult MultiRowDelete(string Ids)
        {
            string validationResponse = "";
            string[] sIDs = Ids.Split("|");
            List<long> iDs = new List<long>();
            long nID;
            string sPayment;

            PaymentsPost payments;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPayment = _paymentsService.Get(nID).sPayment;
                            if (!_paymentsService.VerifyPaymentDeleteOK(nID, sPayment).Any())
                            {
                                payments = _paymentsService.GetPost(nID);
                                payments.UserName = User.Identity.Name;
                                _paymentsService.Delete(payments);
                                iDs.Add(nID);
                            }
                        }
                    }
                );

            iDs.ForEach(n => validationResponse = validationResponse + ", " + n.ToString());

            return Json(validationResponse);
        }

        [AcceptVerbs("Get","Post")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult VerifyPayment(long ixPayment, string sPayment)
        {
            string validationResponse = "";

            if (!_paymentsService.VerifyPaymentUnique(ixPayment, sPayment))
            {
                validationResponse = $"Payment {sPayment} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

