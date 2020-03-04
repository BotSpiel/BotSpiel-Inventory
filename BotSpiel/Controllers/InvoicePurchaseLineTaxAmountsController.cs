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

    public class InvoicePurchaseLineTaxAmountsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInvoicePurchaseLineTaxAmountsService _invoicepurchaselinetaxamountsService;

        public InvoicePurchaseLineTaxAmountsController(IInvoicePurchaseLineTaxAmountsService invoicepurchaselinetaxamountsService )
        {
            _invoicepurchaselinetaxamountsService = invoicepurchaselinetaxamountsService;
        }

        // GET: InvoicePurchaseLineTaxAmounts
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var invoicepurchaselinetaxamounts = _invoicepurchaselinetaxamountsService.Index();
            return View(invoicepurchaselinetaxamounts.ToList());
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
            var invoicepurchaselinetaxamounts = _invoicepurchaselinetaxamountsService.Index();
            return PartialView("IndexGrid", invoicepurchaselinetaxamounts.ToList());
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
                IGrid<InvoicePurchaseLineTaxAmounts> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InvoicePurchaseLineTaxAmounts> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInvoicePurchaseLineTaxAmounts.xlsx");
            }
        }

        private IGrid<InvoicePurchaseLineTaxAmounts> CreateExportableGrid()
        {
            IGrid<InvoicePurchaseLineTaxAmounts> grid = new Grid<InvoicePurchaseLineTaxAmounts>(_invoicepurchaselinetaxamountsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInvoicePurchaseLineTaxAmount).Titled("Invoice Purchase Line Tax Amount").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.InvoicePurchaseLineAmounts.sInvoicePurchaseLineAmount).Titled("Invoice Purchase Line Amount").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Taxes.sTax).Titled("Tax").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.mAmount).Titled("Amount").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Currencies.sCurrency).Titled("Currency").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InvoicePurchaseLineTaxAmounts>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InvoicePurchaseLineTaxAmounts/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_invoicepurchaselinetaxamountsService.Get(id));
        }

        // GET: InvoicePurchaseLineTaxAmounts/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCurrency = new SelectList(_invoicepurchaselinetaxamountsService.selectCurrencies().Select( x => new { x.ixCurrency, x.sCurrency }), "ixCurrency", "sCurrency");
			ViewBag.ixInvoicePurchaseLineAmount = new SelectList(_invoicepurchaselinetaxamountsService.selectInvoicePurchaseLineAmounts().Select( x => new { x.ixInvoicePurchaseLineAmount, x.sInvoicePurchaseLineAmount }), "ixInvoicePurchaseLineAmount", "sInvoicePurchaseLineAmount");
			ViewBag.ixTax = new SelectList(_invoicepurchaselinetaxamountsService.selectTaxes().Select( x => new { x.ixTax, x.sTax }), "ixTax", "sTax");

            return View();
        }

        // POST: InvoicePurchaseLineTaxAmounts/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInvoicePurchaseLineTaxAmount,sInvoicePurchaseLineTaxAmount,ixInvoicePurchaseLineAmount,ixTax,mAmount,ixCurrency")] InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamounts)
        {
            if (ModelState.IsValid)
            {
                invoicepurchaselinetaxamounts.UserName = User.Identity.Name;
                _invoicepurchaselinetaxamountsService.Create(invoicepurchaselinetaxamounts);
                return RedirectToAction("Index");
            }
			ViewBag.ixCurrency = new SelectList(_invoicepurchaselinetaxamountsService.selectCurrencies().Select( x => new { x.ixCurrency, x.sCurrency }), "ixCurrency", "sCurrency");
			ViewBag.ixInvoicePurchaseLineAmount = new SelectList(_invoicepurchaselinetaxamountsService.selectInvoicePurchaseLineAmounts().Select( x => new { x.ixInvoicePurchaseLineAmount, x.sInvoicePurchaseLineAmount }), "ixInvoicePurchaseLineAmount", "sInvoicePurchaseLineAmount");
			ViewBag.ixTax = new SelectList(_invoicepurchaselinetaxamountsService.selectTaxes().Select( x => new { x.ixTax, x.sTax }), "ixTax", "sTax");

            return View(invoicepurchaselinetaxamounts);
        }

        // GET: InvoicePurchaseLineTaxAmounts/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamounts = _invoicepurchaselinetaxamountsService.GetPost(id);
            if (invoicepurchaselinetaxamounts == null)
            {
                return NotFound();
            }
			ViewBag.ixCurrency = new SelectList(_invoicepurchaselinetaxamountsService.selectCurrencies().Select( x => new { x.ixCurrency, x.sCurrency }), "ixCurrency", "sCurrency", invoicepurchaselinetaxamounts.ixCurrency);
			ViewBag.ixInvoicePurchaseLineAmount = new SelectList(_invoicepurchaselinetaxamountsService.selectInvoicePurchaseLineAmounts().Select( x => new { x.ixInvoicePurchaseLineAmount, x.sInvoicePurchaseLineAmount }), "ixInvoicePurchaseLineAmount", "sInvoicePurchaseLineAmount", invoicepurchaselinetaxamounts.ixInvoicePurchaseLineAmount);
			ViewBag.ixTax = new SelectList(_invoicepurchaselinetaxamountsService.selectTaxes().Select( x => new { x.ixTax, x.sTax }), "ixTax", "sTax", invoicepurchaselinetaxamounts.ixTax);

            return View(invoicepurchaselinetaxamounts);
        }

        // POST: InvoicePurchaseLineTaxAmounts/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInvoicePurchaseLineTaxAmount,sInvoicePurchaseLineTaxAmount,ixInvoicePurchaseLineAmount,ixTax,mAmount,ixCurrency")] InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamounts)
        {
            if (ModelState.IsValid)
            {
                invoicepurchaselinetaxamounts.UserName = User.Identity.Name;
                _invoicepurchaselinetaxamountsService.Edit(invoicepurchaselinetaxamounts);
                return RedirectToAction("Index");
            }
			ViewBag.ixCurrency = new SelectList(_invoicepurchaselinetaxamountsService.selectCurrencies().Select( x => new { x.ixCurrency, x.sCurrency }), "ixCurrency", "sCurrency", invoicepurchaselinetaxamounts.ixCurrency);
			ViewBag.ixInvoicePurchaseLineAmount = new SelectList(_invoicepurchaselinetaxamountsService.selectInvoicePurchaseLineAmounts().Select( x => new { x.ixInvoicePurchaseLineAmount, x.sInvoicePurchaseLineAmount }), "ixInvoicePurchaseLineAmount", "sInvoicePurchaseLineAmount", invoicepurchaselinetaxamounts.ixInvoicePurchaseLineAmount);
			ViewBag.ixTax = new SelectList(_invoicepurchaselinetaxamountsService.selectTaxes().Select( x => new { x.ixTax, x.sTax }), "ixTax", "sTax", invoicepurchaselinetaxamounts.ixTax);

            return View(invoicepurchaselinetaxamounts);
        }


        // GET: InvoicePurchaseLineTaxAmounts/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_invoicepurchaselinetaxamountsService.Get(id));
        }

        // POST: InvoicePurchaseLineTaxAmounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamounts = _invoicepurchaselinetaxamountsService.GetPost(id);
            invoicepurchaselinetaxamounts.UserName = User.Identity.Name;
            _invoicepurchaselinetaxamountsService.Delete(invoicepurchaselinetaxamounts);
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
            string sInvoicePurchaseLineTaxAmount;

            InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamounts;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInvoicePurchaseLineTaxAmount = _invoicepurchaselinetaxamountsService.Get(nID).sInvoicePurchaseLineTaxAmount;
                            if (!_invoicepurchaselinetaxamountsService.VerifyInvoicePurchaseLineTaxAmountDeleteOK(nID, sInvoicePurchaseLineTaxAmount).Any())
                            {
                                invoicepurchaselinetaxamounts = _invoicepurchaselinetaxamountsService.GetPost(nID);
                                invoicepurchaselinetaxamounts.UserName = User.Identity.Name;
                                _invoicepurchaselinetaxamountsService.Delete(invoicepurchaselinetaxamounts);
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
        public IActionResult VerifyInvoicePurchaseLineTaxAmount(long ixInvoicePurchaseLineTaxAmount, string sInvoicePurchaseLineTaxAmount)
        {
            string validationResponse = "";

            if (!_invoicepurchaselinetaxamountsService.VerifyInvoicePurchaseLineTaxAmountUnique(ixInvoicePurchaseLineTaxAmount, sInvoicePurchaseLineTaxAmount))
            {
                validationResponse = $"InvoicePurchaseLineTaxAmount {sInvoicePurchaseLineTaxAmount} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

