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

    public class PurchaseLinesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPurchaseLinesService _purchaselinesService;

        public PurchaseLinesController(IPurchaseLinesService purchaselinesService )
        {
            _purchaselinesService = purchaselinesService;
        }

        // GET: PurchaseLines
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var purchaselines = _purchaselinesService.Index();
            return View(purchaselines.ToList());
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
            var purchaselines = _purchaselinesService.Index();
            return PartialView("IndexGrid", purchaselines.ToList());
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
                IGrid<PurchaseLines> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PurchaseLines> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPurchaseLines.xlsx");
            }
        }

        private IGrid<PurchaseLines> CreateExportableGrid()
        {
            IGrid<PurchaseLines> grid = new Grid<PurchaseLines>(_purchaselinesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPurchaseLine).Titled("Purchase Line").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Purchases.sPurchase).Titled("Purchase").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Materials.sMaterial).Titled("Material").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PurchaseLines>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PurchaseLines/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_purchaselinesService.Get(id));
        }

        // GET: PurchaseLines/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixMaterial = new SelectList(_purchaselinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixPurchase = new SelectList(_purchaselinesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase");

            return View();
        }

        // POST: PurchaseLines/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPurchaseLine,sPurchaseLine,ixPurchase,ixMaterial")] PurchaseLinesPost purchaselines)
        {
            if (ModelState.IsValid)
            {
                purchaselines.UserName = User.Identity.Name;
                _purchaselinesService.Create(purchaselines);
                return RedirectToAction("Index");
            }
			ViewBag.ixMaterial = new SelectList(_purchaselinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixPurchase = new SelectList(_purchaselinesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase");

            return View(purchaselines);
        }

        // GET: PurchaseLines/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PurchaseLinesPost purchaselines = _purchaselinesService.GetPost(id);
            if (purchaselines == null)
            {
                return NotFound();
            }
			ViewBag.ixMaterial = new SelectList(_purchaselinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", purchaselines.ixMaterial);
			ViewBag.ixPurchase = new SelectList(_purchaselinesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase", purchaselines.ixPurchase);

            return View(purchaselines);
        }

        // POST: PurchaseLines/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPurchaseLine,sPurchaseLine,ixPurchase,ixMaterial")] PurchaseLinesPost purchaselines)
        {
            if (ModelState.IsValid)
            {
                purchaselines.UserName = User.Identity.Name;
                _purchaselinesService.Edit(purchaselines);
                return RedirectToAction("Index");
            }
			ViewBag.ixMaterial = new SelectList(_purchaselinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", purchaselines.ixMaterial);
			ViewBag.ixPurchase = new SelectList(_purchaselinesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase", purchaselines.ixPurchase);

            return View(purchaselines);
        }


        // GET: PurchaseLines/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_purchaselinesService.Get(id));
        }

        // POST: PurchaseLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PurchaseLinesPost purchaselines = _purchaselinesService.GetPost(id);
            purchaselines.UserName = User.Identity.Name;
            _purchaselinesService.Delete(purchaselines);
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
            string sPurchaseLine;

            PurchaseLinesPost purchaselines;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPurchaseLine = _purchaselinesService.Get(nID).sPurchaseLine;
                            if (!_purchaselinesService.VerifyPurchaseLineDeleteOK(nID, sPurchaseLine).Any())
                            {
                                purchaselines = _purchaselinesService.GetPost(nID);
                                purchaselines.UserName = User.Identity.Name;
                                _purchaselinesService.Delete(purchaselines);
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
        public IActionResult VerifyPurchaseLine(long ixPurchaseLine, string sPurchaseLine)
        {
            string validationResponse = "";

            if (!_purchaselinesService.VerifyPurchaseLineUnique(ixPurchaseLine, sPurchaseLine))
            {
                validationResponse = $"PurchaseLine {sPurchaseLine} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

