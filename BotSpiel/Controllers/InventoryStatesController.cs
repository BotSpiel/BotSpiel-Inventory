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

    public class InventoryStatesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInventoryStatesService _inventorystatesService;

        public InventoryStatesController(IInventoryStatesService inventorystatesService )
        {
            _inventorystatesService = inventorystatesService;
        }

        // GET: InventoryStates
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inventorystates = _inventorystatesService.Index();
            return View(inventorystates.ToList());
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
            var inventorystates = _inventorystatesService.Index();
            return PartialView("IndexGrid", inventorystates.ToList());
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
                IGrid<InventoryStates> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InventoryStates> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInventoryStates.xlsx");
            }
        }

        private IGrid<InventoryStates> CreateExportableGrid()
        {
            IGrid<InventoryStates> grid = new Grid<InventoryStates>(_inventorystatesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInventoryState).Titled("Inventory State").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InventoryStates>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InventoryStates/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inventorystatesService.Get(id));
        }

        // GET: InventoryStates/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: InventoryStates/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInventoryState,sInventoryState")] InventoryStatesPost inventorystates)
        {
            if (ModelState.IsValid)
            {
                inventorystates.UserName = User.Identity.Name;
                _inventorystatesService.Create(inventorystates);
                return RedirectToAction("Index");
            }

            return View(inventorystates);
        }

        // GET: InventoryStates/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InventoryStatesPost inventorystates = _inventorystatesService.GetPost(id);
            if (inventorystates == null)
            {
                return NotFound();
            }

            return View(inventorystates);
        }

        // POST: InventoryStates/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInventoryState,sInventoryState")] InventoryStatesPost inventorystates)
        {
            if (ModelState.IsValid)
            {
                inventorystates.UserName = User.Identity.Name;
                _inventorystatesService.Edit(inventorystates);
                return RedirectToAction("Index");
            }

            return View(inventorystates);
        }


        // GET: InventoryStates/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inventorystatesService.Get(id));
        }

        // POST: InventoryStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InventoryStatesPost inventorystates = _inventorystatesService.GetPost(id);
            inventorystates.UserName = User.Identity.Name;
            _inventorystatesService.Delete(inventorystates);
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
            string sInventoryState;

            InventoryStatesPost inventorystates;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInventoryState = _inventorystatesService.Get(nID).sInventoryState;
                            if (!_inventorystatesService.VerifyInventoryStateDeleteOK(nID, sInventoryState).Any())
                            {
                                inventorystates = _inventorystatesService.GetPost(nID);
                                inventorystates.UserName = User.Identity.Name;
                                _inventorystatesService.Delete(inventorystates);
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
        public IActionResult VerifyInventoryState(long ixInventoryState, string sInventoryState)
        {
            string validationResponse = "";

            if (!_inventorystatesService.VerifyInventoryStateUnique(ixInventoryState, sInventoryState))
            {
                validationResponse = $"InventoryState {sInventoryState} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

