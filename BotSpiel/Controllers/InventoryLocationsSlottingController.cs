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

    public class InventoryLocationsSlottingController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInventoryLocationsSlottingService _inventorylocationsslottingService;

        public InventoryLocationsSlottingController(IInventoryLocationsSlottingService inventorylocationsslottingService )
        {
            _inventorylocationsslottingService = inventorylocationsslottingService;
        }

        // GET: InventoryLocationsSlotting
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inventorylocationsslotting = _inventorylocationsslottingService.Index();
            return View(inventorylocationsslotting.ToList());
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
            var inventorylocationsslotting = _inventorylocationsslottingService.Index();
            return PartialView("IndexGrid", inventorylocationsslotting.ToList());
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
                IGrid<InventoryLocationsSlotting> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InventoryLocationsSlotting> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInventoryLocationsSlotting.xlsx");
            }
        }

        private IGrid<InventoryLocationsSlotting> CreateExportableGrid()
        {
            IGrid<InventoryLocationsSlotting> grid = new Grid<InventoryLocationsSlotting>(_inventorylocationsslottingService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInventoryLocationSlotting).Titled("Inventory Location Slotting").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.InventoryLocations.sInventoryLocation).Titled("Inventory Location").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Materials.sMaterial).Titled("Material").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nMinimumBaseUnitQuantity).Titled("Minimum Base Unit Quantity").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nMaximumBaseUnitQuantity).Titled("Maximum Base Unit Quantity").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InventoryLocationsSlotting>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InventoryLocationsSlotting/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inventorylocationsslottingService.Get(id));
        }

        // GET: InventoryLocationsSlotting/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixInventoryLocation = new SelectList(_inventorylocationsslottingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixMaterial = new SelectList(_inventorylocationsslottingService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");

            return View();
        }

        // POST: InventoryLocationsSlotting/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInventoryLocationSlotting,sInventoryLocationSlotting,ixInventoryLocation,ixMaterial,nMinimumBaseUnitQuantity,nMaximumBaseUnitQuantity")] InventoryLocationsSlottingPost inventorylocationsslotting)
        {
            if (ModelState.IsValid)
            {
                inventorylocationsslotting.UserName = User.Identity.Name;
                _inventorylocationsslottingService.Create(inventorylocationsslotting);
                return RedirectToAction("Index");
            }
			ViewBag.ixInventoryLocation = new SelectList(_inventorylocationsslottingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixMaterial = new SelectList(_inventorylocationsslottingService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");

            return View(inventorylocationsslotting);
        }

        // GET: InventoryLocationsSlotting/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InventoryLocationsSlottingPost inventorylocationsslotting = _inventorylocationsslottingService.GetPost(id);
            if (inventorylocationsslotting == null)
            {
                return NotFound();
            }
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixInventoryLocation = new SelectList(_inventorylocationsslottingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", inventorylocationsslotting.ixInventoryLocation);
            //ViewBag.ixMaterial = new SelectList(_inventorylocationsslottingService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", inventorylocationsslotting.ixMaterial);
            //Replaced Code Block End
            var currentLocation = _inventorylocationsslottingService.Index().Where(x => x.ixInventoryLocation == inventorylocationsslotting.ixInventoryLocation).Select(x => new { x.ixInventoryLocation, x.InventoryLocations.sInventoryLocation });
            var currentMaterial = _inventorylocationsslottingService.Index().Where(x => x.ixMaterial == inventorylocationsslotting.ixMaterial).Select(x => new { x.ixMaterial, x.Materials.sMaterial });
            ViewBag.ixInventoryLocation = new SelectList(_inventorylocationsslottingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }).Union(currentLocation), "ixInventoryLocation", "sInventoryLocation", inventorylocationsslotting.ixInventoryLocation);
            ViewBag.ixMaterial = new SelectList(_inventorylocationsslottingService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }).Union(currentMaterial), "ixMaterial", "sMaterial", inventorylocationsslotting.ixMaterial);
            //Custom Code End
            return View(inventorylocationsslotting);
        }

        // POST: InventoryLocationsSlotting/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInventoryLocationSlotting,sInventoryLocationSlotting,ixInventoryLocation,ixMaterial,nMinimumBaseUnitQuantity,nMaximumBaseUnitQuantity")] InventoryLocationsSlottingPost inventorylocationsslotting)
        {
            if (ModelState.IsValid)
            {
                inventorylocationsslotting.UserName = User.Identity.Name;
                _inventorylocationsslottingService.Edit(inventorylocationsslotting);
                return RedirectToAction("Index");
            }
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixInventoryLocation = new SelectList(_inventorylocationsslottingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", inventorylocationsslotting.ixInventoryLocation);
            //ViewBag.ixMaterial = new SelectList(_inventorylocationsslottingService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", inventorylocationsslotting.ixMaterial);
            //Replaced Code Block End
            var currentLocation = _inventorylocationsslottingService.Index().Where(x => x.ixInventoryLocation == inventorylocationsslotting.ixInventoryLocation).Select(x => new { x.ixInventoryLocation, x.InventoryLocations.sInventoryLocation });
            var currentMaterial = _inventorylocationsslottingService.Index().Where(x => x.ixMaterial == inventorylocationsslotting.ixMaterial).Select(x => new { x.ixMaterial, x.Materials.sMaterial });
            ViewBag.ixInventoryLocation = new SelectList(_inventorylocationsslottingService.selectInventoryLocations().Select(x => new { x.ixInventoryLocation, x.sInventoryLocation }).Union(currentLocation), "ixInventoryLocation", "sInventoryLocation", inventorylocationsslotting.ixInventoryLocation);
            ViewBag.ixMaterial = new SelectList(_inventorylocationsslottingService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }).Union(currentMaterial), "ixMaterial", "sMaterial", inventorylocationsslotting.ixMaterial);
            //Custom Code End
            return View(inventorylocationsslotting);
        }


        // GET: InventoryLocationsSlotting/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inventorylocationsslottingService.Get(id));
        }

        // POST: InventoryLocationsSlotting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InventoryLocationsSlottingPost inventorylocationsslotting = _inventorylocationsslottingService.GetPost(id);
            inventorylocationsslotting.UserName = User.Identity.Name;
            _inventorylocationsslottingService.Delete(inventorylocationsslotting);
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
            string sInventoryLocationSlotting;

            InventoryLocationsSlottingPost inventorylocationsslotting;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInventoryLocationSlotting = _inventorylocationsslottingService.Get(nID).sInventoryLocationSlotting;
                            if (!_inventorylocationsslottingService.VerifyInventoryLocationSlottingDeleteOK(nID, sInventoryLocationSlotting).Any())
                            {
                                inventorylocationsslotting = _inventorylocationsslottingService.GetPost(nID);
                                inventorylocationsslotting.UserName = User.Identity.Name;
                                _inventorylocationsslottingService.Delete(inventorylocationsslotting);
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
        public IActionResult VerifyInventoryLocationSlotting(long ixInventoryLocationSlotting, string sInventoryLocationSlotting)
        {
            string validationResponse = "";

            if (!_inventorylocationsslottingService.VerifyInventoryLocationSlottingUnique(ixInventoryLocationSlotting, sInventoryLocationSlotting))
            {
                validationResponse = $"InventoryLocationSlotting {sInventoryLocationSlotting} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 
