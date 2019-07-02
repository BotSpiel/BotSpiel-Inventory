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

    public class InventoryUnitTransactionContextsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInventoryUnitTransactionContextsService _inventoryunittransactioncontextsService;

        public InventoryUnitTransactionContextsController(IInventoryUnitTransactionContextsService inventoryunittransactioncontextsService )
        {
            _inventoryunittransactioncontextsService = inventoryunittransactioncontextsService;
        }

        // GET: InventoryUnitTransactionContexts
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inventoryunittransactioncontexts = _inventoryunittransactioncontextsService.Index();
            return View(inventoryunittransactioncontexts.ToList());
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
            var inventoryunittransactioncontexts = _inventoryunittransactioncontextsService.Index();
            return PartialView("IndexGrid", inventoryunittransactioncontexts.ToList());
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
                IGrid<InventoryUnitTransactionContexts> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InventoryUnitTransactionContexts> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInventoryUnitTransactionContexts.xlsx");
            }
        }

        private IGrid<InventoryUnitTransactionContexts> CreateExportableGrid()
        {
            IGrid<InventoryUnitTransactionContexts> grid = new Grid<InventoryUnitTransactionContexts>(_inventoryunittransactioncontextsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInventoryUnitTransactionContext).Titled("Inventory Unit Transaction Context").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InventoryUnitTransactionContexts>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InventoryUnitTransactionContexts/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inventoryunittransactioncontextsService.Get(id));
        }

        // GET: InventoryUnitTransactionContexts/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: InventoryUnitTransactionContexts/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInventoryUnitTransactionContext,sInventoryUnitTransactionContext")] InventoryUnitTransactionContextsPost inventoryunittransactioncontexts)
        {
            if (ModelState.IsValid)
            {
                inventoryunittransactioncontexts.UserName = User.Identity.Name;
                _inventoryunittransactioncontextsService.Create(inventoryunittransactioncontexts);
                return RedirectToAction("Index");
            }

            return View(inventoryunittransactioncontexts);
        }

        // GET: InventoryUnitTransactionContexts/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InventoryUnitTransactionContextsPost inventoryunittransactioncontexts = _inventoryunittransactioncontextsService.GetPost(id);
            if (inventoryunittransactioncontexts == null)
            {
                return NotFound();
            }

            return View(inventoryunittransactioncontexts);
        }

        // POST: InventoryUnitTransactionContexts/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInventoryUnitTransactionContext,sInventoryUnitTransactionContext")] InventoryUnitTransactionContextsPost inventoryunittransactioncontexts)
        {
            if (ModelState.IsValid)
            {
                inventoryunittransactioncontexts.UserName = User.Identity.Name;
                _inventoryunittransactioncontextsService.Edit(inventoryunittransactioncontexts);
                return RedirectToAction("Index");
            }

            return View(inventoryunittransactioncontexts);
        }


        // GET: InventoryUnitTransactionContexts/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inventoryunittransactioncontextsService.Get(id));
        }

        // POST: InventoryUnitTransactionContexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InventoryUnitTransactionContextsPost inventoryunittransactioncontexts = _inventoryunittransactioncontextsService.GetPost(id);
            inventoryunittransactioncontexts.UserName = User.Identity.Name;
            _inventoryunittransactioncontextsService.Delete(inventoryunittransactioncontexts);
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
            string sInventoryUnitTransactionContext;

            InventoryUnitTransactionContextsPost inventoryunittransactioncontexts;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInventoryUnitTransactionContext = _inventoryunittransactioncontextsService.Get(nID).sInventoryUnitTransactionContext;
                            if (!_inventoryunittransactioncontextsService.VerifyInventoryUnitTransactionContextDeleteOK(nID, sInventoryUnitTransactionContext).Any())
                            {
                                inventoryunittransactioncontexts = _inventoryunittransactioncontextsService.GetPost(nID);
                                inventoryunittransactioncontexts.UserName = User.Identity.Name;
                                _inventoryunittransactioncontextsService.Delete(inventoryunittransactioncontexts);
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
        public IActionResult VerifyInventoryUnitTransactionContext(long ixInventoryUnitTransactionContext, string sInventoryUnitTransactionContext)
        {
            string validationResponse = "";

            if (!_inventoryunittransactioncontextsService.VerifyInventoryUnitTransactionContextUnique(ixInventoryUnitTransactionContext, sInventoryUnitTransactionContext))
            {
                validationResponse = $"InventoryUnitTransactionContext {sInventoryUnitTransactionContext} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

