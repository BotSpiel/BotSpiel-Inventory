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

    public class PurchaseEmailsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPurchaseEmailsService _purchaseemailsService;

        public PurchaseEmailsController(IPurchaseEmailsService purchaseemailsService )
        {
            _purchaseemailsService = purchaseemailsService;
        }

        // GET: PurchaseEmails
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var purchaseemails = _purchaseemailsService.Index();
            return View(purchaseemails.ToList());
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
            var purchaseemails = _purchaseemailsService.Index();
            return PartialView("IndexGrid", purchaseemails.ToList());
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
                IGrid<PurchaseEmails> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PurchaseEmails> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPurchaseEmails.xlsx");
            }
        }

        private IGrid<PurchaseEmails> CreateExportableGrid()
        {
            IGrid<PurchaseEmails> grid = new Grid<PurchaseEmails>(_purchaseemailsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPurchaseEmail).Titled("Purchase Email").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Purchases.sPurchase).Titled("Purchase").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.SendEmails.sSendEmail).Titled("Send Email").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PurchaseEmails>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PurchaseEmails/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_purchaseemailsService.Get(id));
        }

        // GET: PurchaseEmails/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPurchase = new SelectList(_purchaseemailsService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase");
			ViewBag.ixSendEmail = new SelectList(_purchaseemailsService.selectSendEmails().Select( x => new { x.ixSendEmail, x.sSendEmail }), "ixSendEmail", "sSendEmail");

            return View();
        }

        // POST: PurchaseEmails/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPurchaseEmail,sPurchaseEmail,ixPurchase,ixSendEmail")] PurchaseEmailsPost purchaseemails)
        {
            if (ModelState.IsValid)
            {
                purchaseemails.UserName = User.Identity.Name;
                _purchaseemailsService.Create(purchaseemails);
                return RedirectToAction("Index");
            }
			ViewBag.ixPurchase = new SelectList(_purchaseemailsService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase");
			ViewBag.ixSendEmail = new SelectList(_purchaseemailsService.selectSendEmails().Select( x => new { x.ixSendEmail, x.sSendEmail }), "ixSendEmail", "sSendEmail");

            return View(purchaseemails);
        }

        // GET: PurchaseEmails/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PurchaseEmailsPost purchaseemails = _purchaseemailsService.GetPost(id);
            if (purchaseemails == null)
            {
                return NotFound();
            }
			ViewBag.ixPurchase = new SelectList(_purchaseemailsService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase", purchaseemails.ixPurchase);
			ViewBag.ixSendEmail = new SelectList(_purchaseemailsService.selectSendEmails().Select( x => new { x.ixSendEmail, x.sSendEmail }), "ixSendEmail", "sSendEmail", purchaseemails.ixSendEmail);

            return View(purchaseemails);
        }

        // POST: PurchaseEmails/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPurchaseEmail,sPurchaseEmail,ixPurchase,ixSendEmail")] PurchaseEmailsPost purchaseemails)
        {
            if (ModelState.IsValid)
            {
                purchaseemails.UserName = User.Identity.Name;
                _purchaseemailsService.Edit(purchaseemails);
                return RedirectToAction("Index");
            }
			ViewBag.ixPurchase = new SelectList(_purchaseemailsService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase", purchaseemails.ixPurchase);
			ViewBag.ixSendEmail = new SelectList(_purchaseemailsService.selectSendEmails().Select( x => new { x.ixSendEmail, x.sSendEmail }), "ixSendEmail", "sSendEmail", purchaseemails.ixSendEmail);

            return View(purchaseemails);
        }


        // GET: PurchaseEmails/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_purchaseemailsService.Get(id));
        }

        // POST: PurchaseEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PurchaseEmailsPost purchaseemails = _purchaseemailsService.GetPost(id);
            purchaseemails.UserName = User.Identity.Name;
            _purchaseemailsService.Delete(purchaseemails);
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
            string sPurchaseEmail;

            PurchaseEmailsPost purchaseemails;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPurchaseEmail = _purchaseemailsService.Get(nID).sPurchaseEmail;
                            if (!_purchaseemailsService.VerifyPurchaseEmailDeleteOK(nID, sPurchaseEmail).Any())
                            {
                                purchaseemails = _purchaseemailsService.GetPost(nID);
                                purchaseemails.UserName = User.Identity.Name;
                                _purchaseemailsService.Delete(purchaseemails);
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
        public IActionResult VerifyPurchaseEmail(long ixPurchaseEmail, string sPurchaseEmail)
        {
            string validationResponse = "";

            if (!_purchaseemailsService.VerifyPurchaseEmailUnique(ixPurchaseEmail, sPurchaseEmail))
            {
                validationResponse = $"PurchaseEmail {sPurchaseEmail} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

