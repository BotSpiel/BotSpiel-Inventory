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

    public class InventoryModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInventoryModuleGridsService _inventorymodulegridsService;

        public InventoryModuleGridsController(IInventoryModuleGridsService inventorymodulegridsService )
        {
            _inventorymodulegridsService = inventorymodulegridsService;
        }

        // GET: InventoryModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inventorymodulegrids = _inventorymodulegridsService.Index();
            return View(inventorymodulegrids.ToList());
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
            var inventorymodulegrids = _inventorymodulegridsService.Index();
            return PartialView("IndexGrid", inventorymodulegrids.ToList());
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
                IGrid<InventoryModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InventoryModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInventoryModuleGrids.xlsx");
            }
        }

        private IGrid<InventoryModuleGrids> CreateExportableGrid()
        {
            IGrid<InventoryModuleGrids> grid = new Grid<InventoryModuleGrids>(_inventorymodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInventoryModuleGrid).Titled("Inventory Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<InventoryModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InventoryModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var inventorymodulegridsconfig = _inventorymodulegridsService.Indexconfig();
            return View(inventorymodulegridsconfig.ToList());
        }

        // GET: InventoryModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var inventorymodulegridsmd = _inventorymodulegridsService.Indexmd();
            return View(inventorymodulegridsmd.ToList());
        }

        // GET: InventoryModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var inventorymodulegridstx = _inventorymodulegridsService.Indextx();
            return View(inventorymodulegridstx.ToList());
        }

        // GET: InventoryModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var inventorymodulegridsanalytics = _inventorymodulegridsService.Indexanalytics();
            return View(inventorymodulegridsanalytics.ToList());
        }

        // GET: InventoryModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inventorymodulegridsService.Get(id));
        }

        // GET: InventoryModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: InventoryModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInventoryModuleGrid,sInventoryModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] InventoryModuleGridsPost inventorymodulegrids)
        {
            if (ModelState.IsValid)
            {
                inventorymodulegrids.UserName = User.Identity.Name;
                _inventorymodulegridsService.Create(inventorymodulegrids);
                return RedirectToAction("Index");
            }

            return View(inventorymodulegrids);
        }

        // GET: InventoryModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InventoryModuleGridsPost inventorymodulegrids = _inventorymodulegridsService.GetPost(id);
            if (inventorymodulegrids == null)
            {
                return NotFound();
            }

            return View(inventorymodulegrids);
        }

        // POST: InventoryModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInventoryModuleGrid,sInventoryModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] InventoryModuleGridsPost inventorymodulegrids)
        {
            if (ModelState.IsValid)
            {
                inventorymodulegrids.UserName = User.Identity.Name;
                _inventorymodulegridsService.Edit(inventorymodulegrids);
                return RedirectToAction("Index");
            }

            return View(inventorymodulegrids);
        }


        // GET: InventoryModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inventorymodulegridsService.Get(id));
        }

        // POST: InventoryModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InventoryModuleGridsPost inventorymodulegrids = _inventorymodulegridsService.GetPost(id);
            inventorymodulegrids.UserName = User.Identity.Name;
            _inventorymodulegridsService.Delete(inventorymodulegrids);
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
            string sInventoryModuleGrid;

            InventoryModuleGridsPost inventorymodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInventoryModuleGrid = _inventorymodulegridsService.Get(nID).sInventoryModuleGrid;
                            if (!_inventorymodulegridsService.VerifyInventoryModuleGridDeleteOK(nID, sInventoryModuleGrid).Any())
                            {
                                inventorymodulegrids = _inventorymodulegridsService.GetPost(nID);
                                inventorymodulegrids.UserName = User.Identity.Name;
                                _inventorymodulegridsService.Delete(inventorymodulegrids);
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
        public IActionResult VerifyInventoryModuleGrid(long ixInventoryModuleGrid, string sInventoryModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

