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

    public class InvoicesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInvoicesService _invoicesService;

        public InvoicesController(IInvoicesService invoicesService )
        {
            _invoicesService = invoicesService;
        }

        // GET: Invoices
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var invoices = _invoicesService.Index();
            return View(invoices.ToList());
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
            var invoices = _invoicesService.Index();
            return PartialView("IndexGrid", invoices.ToList());
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
                IGrid<Invoices> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Invoices> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInvoices.xlsx");
            }
        }

        private IGrid<Invoices> CreateExportableGrid()
        {
            IGrid<Invoices> grid = new Grid<Invoices>(_invoicesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInvoice).Titled("Invoice").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Purchases.sPurchase).Titled("Purchase").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Invoices>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Invoices/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_invoicesService.Get(id));
        }

        // GET: Invoices/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPurchase = new SelectList(_invoicesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase");

            return View();
        }

        // POST: Invoices/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInvoice,sInvoice,ixPurchase")] InvoicesPost invoices)
        {
            if (ModelState.IsValid)
            {
                invoices.UserName = User.Identity.Name;
                _invoicesService.Create(invoices);
                return RedirectToAction("Index");
            }
			ViewBag.ixPurchase = new SelectList(_invoicesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase");

            return View(invoices);
        }

        // GET: Invoices/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InvoicesPost invoices = _invoicesService.GetPost(id);
            if (invoices == null)
            {
                return NotFound();
            }
			ViewBag.ixPurchase = new SelectList(_invoicesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase", invoices.ixPurchase);

            return View(invoices);
        }

        // POST: Invoices/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInvoice,sInvoice,ixPurchase")] InvoicesPost invoices)
        {
            if (ModelState.IsValid)
            {
                invoices.UserName = User.Identity.Name;
                _invoicesService.Edit(invoices);
                return RedirectToAction("Index");
            }
			ViewBag.ixPurchase = new SelectList(_invoicesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase", invoices.ixPurchase);

            return View(invoices);
        }


        // GET: Invoices/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_invoicesService.Get(id));
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InvoicesPost invoices = _invoicesService.GetPost(id);
            invoices.UserName = User.Identity.Name;
            _invoicesService.Delete(invoices);
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
            string sInvoice;

            InvoicesPost invoices;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInvoice = _invoicesService.Get(nID).sInvoice;
                            if (!_invoicesService.VerifyInvoiceDeleteOK(nID, sInvoice).Any())
                            {
                                invoices = _invoicesService.GetPost(nID);
                                invoices.UserName = User.Identity.Name;
                                _invoicesService.Delete(invoices);
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
        public IActionResult VerifyInvoice(long ixInvoice, string sInvoice)
        {
            string validationResponse = "";

            if (!_invoicesService.VerifyInvoiceUnique(ixInvoice, sInvoice))
            {
                validationResponse = $"Invoice {sInvoice} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

