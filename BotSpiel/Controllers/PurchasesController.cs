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

    public class PurchasesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPurchasesService _purchasesService;

        public PurchasesController(IPurchasesService purchasesService )
        {
            _purchasesService = purchasesService;
        }

        // GET: Purchases
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var purchases = _purchasesService.Index();
            return View(purchases.ToList());
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
            var purchases = _purchasesService.Index();
            return PartialView("IndexGrid", purchases.ToList());
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
                IGrid<Purchases> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Purchases> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPurchases.xlsx");
            }
        }

        private IGrid<Purchases> CreateExportableGrid()
        {
            IGrid<Purchases> grid = new Grid<Purchases>(_purchasesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPurchase).Titled("Purchase").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.People.sPerson).Titled("Person").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Purchases>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Purchases/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_purchasesService.Get(id));
        }

        // GET: Purchases/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCompany = new SelectList(_purchasesService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixPerson = new SelectList(_purchasesService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson");

            return View();
        }

        // POST: Purchases/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPurchase,sPurchase,ixPerson,ixCompany")] PurchasesPost purchases)
        {
            if (ModelState.IsValid)
            {
                purchases.UserName = User.Identity.Name;
                _purchasesService.Create(purchases);
                return RedirectToAction("Index");
            }
			ViewBag.ixCompany = new SelectList(_purchasesService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixPerson = new SelectList(_purchasesService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson");

            return View(purchases);
        }

        // GET: Purchases/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PurchasesPost purchases = _purchasesService.GetPost(id);
            if (purchases == null)
            {
                return NotFound();
            }
			ViewBag.ixCompany = new SelectList(_purchasesService.selectCompaniesNullable().Select( x => new { ixCompany = x.Key, sCompany = x.Value }), "ixCompany", "sCompany", purchases.ixCompany);
			ViewBag.ixPerson = new SelectList(_purchasesService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson", purchases.ixPerson);

            return View(purchases);
        }

        // POST: Purchases/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPurchase,sPurchase,ixPerson,ixCompany")] PurchasesPost purchases)
        {
            if (ModelState.IsValid)
            {
                purchases.UserName = User.Identity.Name;
                _purchasesService.Edit(purchases);
                return RedirectToAction("Index");
            }
			ViewBag.ixCompany = new SelectList(_purchasesService.selectCompaniesNullable().Select( x => new { ixCompany = x.Key, sCompany = x.Value }), "ixCompany", "sCompany", purchases.ixCompany);
			ViewBag.ixPerson = new SelectList(_purchasesService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson", purchases.ixPerson);

            return View(purchases);
        }


        // GET: Purchases/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_purchasesService.Get(id));
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PurchasesPost purchases = _purchasesService.GetPost(id);
            purchases.UserName = User.Identity.Name;
            _purchasesService.Delete(purchases);
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
            string sPurchase;

            PurchasesPost purchases;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPurchase = _purchasesService.Get(nID).sPurchase;
                            if (!_purchasesService.VerifyPurchaseDeleteOK(nID, sPurchase).Any())
                            {
                                purchases = _purchasesService.GetPost(nID);
                                purchases.UserName = User.Identity.Name;
                                _purchasesService.Delete(purchases);
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
        public IActionResult VerifyPurchase(long ixPurchase, string sPurchase)
        {
            string validationResponse = "";

            if (!_purchasesService.VerifyPurchaseUnique(ixPurchase, sPurchase))
            {
                validationResponse = $"Purchase {sPurchase} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

