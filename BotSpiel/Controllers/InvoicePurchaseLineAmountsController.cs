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

    public class InvoicePurchaseLineAmountsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInvoicePurchaseLineAmountsService _invoicepurchaselineamountsService;

        public InvoicePurchaseLineAmountsController(IInvoicePurchaseLineAmountsService invoicepurchaselineamountsService )
        {
            _invoicepurchaselineamountsService = invoicepurchaselineamountsService;
        }

        // GET: InvoicePurchaseLineAmounts
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var invoicepurchaselineamounts = _invoicepurchaselineamountsService.Index();
            return View(invoicepurchaselineamounts.ToList());
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
            var invoicepurchaselineamounts = _invoicepurchaselineamountsService.Index();
            return PartialView("IndexGrid", invoicepurchaselineamounts.ToList());
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
                IGrid<InvoicePurchaseLineAmounts> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InvoicePurchaseLineAmounts> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInvoicePurchaseLineAmounts.xlsx");
            }
        }

        private IGrid<InvoicePurchaseLineAmounts> CreateExportableGrid()
        {
            IGrid<InvoicePurchaseLineAmounts> grid = new Grid<InvoicePurchaseLineAmounts>(_invoicepurchaselineamountsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInvoicePurchaseLineAmount).Titled("Invoice Purchase Line Amount").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Invoices.sInvoice).Titled("Invoice").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.PurchaseLines.sPurchaseLine).Titled("Purchase Line").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.mAmount).Titled("Amount").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Currencies.sCurrency).Titled("Currency").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InvoicePurchaseLineAmounts>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InvoicePurchaseLineAmounts/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_invoicepurchaselineamountsService.Get(id));
        }

        // GET: InvoicePurchaseLineAmounts/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCurrency = new SelectList(_invoicepurchaselineamountsService.selectCurrencies().Select( x => new { x.ixCurrency, x.sCurrency }), "ixCurrency", "sCurrency");
			ViewBag.ixInvoice = new SelectList(_invoicepurchaselineamountsService.selectInvoices().Select( x => new { x.ixInvoice, x.sInvoice }), "ixInvoice", "sInvoice");
			ViewBag.ixPurchaseLine = new SelectList(_invoicepurchaselineamountsService.selectPurchaseLines().Select( x => new { x.ixPurchaseLine, x.sPurchaseLine }), "ixPurchaseLine", "sPurchaseLine");

            return View();
        }

        // POST: InvoicePurchaseLineAmounts/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInvoicePurchaseLineAmount,sInvoicePurchaseLineAmount,ixInvoice,ixPurchaseLine,mAmount,ixCurrency")] InvoicePurchaseLineAmountsPost invoicepurchaselineamounts)
        {
            if (ModelState.IsValid)
            {
                invoicepurchaselineamounts.UserName = User.Identity.Name;
                _invoicepurchaselineamountsService.Create(invoicepurchaselineamounts);
                return RedirectToAction("Index");
            }
			ViewBag.ixCurrency = new SelectList(_invoicepurchaselineamountsService.selectCurrencies().Select( x => new { x.ixCurrency, x.sCurrency }), "ixCurrency", "sCurrency");
			ViewBag.ixInvoice = new SelectList(_invoicepurchaselineamountsService.selectInvoices().Select( x => new { x.ixInvoice, x.sInvoice }), "ixInvoice", "sInvoice");
			ViewBag.ixPurchaseLine = new SelectList(_invoicepurchaselineamountsService.selectPurchaseLines().Select( x => new { x.ixPurchaseLine, x.sPurchaseLine }), "ixPurchaseLine", "sPurchaseLine");

            return View(invoicepurchaselineamounts);
        }

        // GET: InvoicePurchaseLineAmounts/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InvoicePurchaseLineAmountsPost invoicepurchaselineamounts = _invoicepurchaselineamountsService.GetPost(id);
            if (invoicepurchaselineamounts == null)
            {
                return NotFound();
            }
			ViewBag.ixCurrency = new SelectList(_invoicepurchaselineamountsService.selectCurrencies().Select( x => new { x.ixCurrency, x.sCurrency }), "ixCurrency", "sCurrency", invoicepurchaselineamounts.ixCurrency);
			ViewBag.ixInvoice = new SelectList(_invoicepurchaselineamountsService.selectInvoices().Select( x => new { x.ixInvoice, x.sInvoice }), "ixInvoice", "sInvoice", invoicepurchaselineamounts.ixInvoice);
			ViewBag.ixPurchaseLine = new SelectList(_invoicepurchaselineamountsService.selectPurchaseLines().Select( x => new { x.ixPurchaseLine, x.sPurchaseLine }), "ixPurchaseLine", "sPurchaseLine", invoicepurchaselineamounts.ixPurchaseLine);

            return View(invoicepurchaselineamounts);
        }

        // POST: InvoicePurchaseLineAmounts/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInvoicePurchaseLineAmount,sInvoicePurchaseLineAmount,ixInvoice,ixPurchaseLine,mAmount,ixCurrency")] InvoicePurchaseLineAmountsPost invoicepurchaselineamounts)
        {
            if (ModelState.IsValid)
            {
                invoicepurchaselineamounts.UserName = User.Identity.Name;
                _invoicepurchaselineamountsService.Edit(invoicepurchaselineamounts);
                return RedirectToAction("Index");
            }
			ViewBag.ixCurrency = new SelectList(_invoicepurchaselineamountsService.selectCurrencies().Select( x => new { x.ixCurrency, x.sCurrency }), "ixCurrency", "sCurrency", invoicepurchaselineamounts.ixCurrency);
			ViewBag.ixInvoice = new SelectList(_invoicepurchaselineamountsService.selectInvoices().Select( x => new { x.ixInvoice, x.sInvoice }), "ixInvoice", "sInvoice", invoicepurchaselineamounts.ixInvoice);
			ViewBag.ixPurchaseLine = new SelectList(_invoicepurchaselineamountsService.selectPurchaseLines().Select( x => new { x.ixPurchaseLine, x.sPurchaseLine }), "ixPurchaseLine", "sPurchaseLine", invoicepurchaselineamounts.ixPurchaseLine);

            return View(invoicepurchaselineamounts);
        }


        // GET: InvoicePurchaseLineAmounts/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_invoicepurchaselineamountsService.Get(id));
        }

        // POST: InvoicePurchaseLineAmounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InvoicePurchaseLineAmountsPost invoicepurchaselineamounts = _invoicepurchaselineamountsService.GetPost(id);
            invoicepurchaselineamounts.UserName = User.Identity.Name;
            _invoicepurchaselineamountsService.Delete(invoicepurchaselineamounts);
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
            string sInvoicePurchaseLineAmount;

            InvoicePurchaseLineAmountsPost invoicepurchaselineamounts;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInvoicePurchaseLineAmount = _invoicepurchaselineamountsService.Get(nID).sInvoicePurchaseLineAmount;
                            if (!_invoicepurchaselineamountsService.VerifyInvoicePurchaseLineAmountDeleteOK(nID, sInvoicePurchaseLineAmount).Any())
                            {
                                invoicepurchaselineamounts = _invoicepurchaselineamountsService.GetPost(nID);
                                invoicepurchaselineamounts.UserName = User.Identity.Name;
                                _invoicepurchaselineamountsService.Delete(invoicepurchaselineamounts);
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
        public IActionResult VerifyInvoicePurchaseLineAmount(long ixInvoicePurchaseLineAmount, string sInvoicePurchaseLineAmount)
        {
            string validationResponse = "";

            if (!_invoicepurchaselineamountsService.VerifyInvoicePurchaseLineAmountUnique(ixInvoicePurchaseLineAmount, sInvoicePurchaseLineAmount))
            {
                validationResponse = $"InvoicePurchaseLineAmount {sInvoicePurchaseLineAmount} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

