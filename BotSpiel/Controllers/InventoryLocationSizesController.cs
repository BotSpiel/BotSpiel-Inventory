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
//Custom Code Start | Added Code Block
using BotSpiel.DataAccess.Utilities;
//Custom Code End


namespace BotSpiel
{

    public class InventoryLocationSizesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInventoryLocationSizesService _inventorylocationsizesService;
        //Custom Code Start | Added Code Block
        private readonly CommonlyUsedSelects _commonlyUsedSelects;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public InventoryLocationSizesController(IInventoryLocationSizesService inventorylocationsizesService )
        //Replaced Code Block End
        public InventoryLocationSizesController(IInventoryLocationSizesService inventorylocationsizesService, CommonlyUsedSelects commonlyUsedSelects)
        { 
        //Custom Code End
            _inventorylocationsizesService = inventorylocationsizesService;
            //Custom Code Start | Added Code Block
            _commonlyUsedSelects = commonlyUsedSelects;
            //Custom Code End
        }

        // GET: InventoryLocationSizes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inventorylocationsizes = _inventorylocationsizesService.Index();
            return View(inventorylocationsizes.ToList());
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
            var inventorylocationsizes = _inventorylocationsizesService.Index();
            return PartialView("IndexGrid", inventorylocationsizes.ToList());
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
                IGrid<InventoryLocationSizes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InventoryLocationSizes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInventoryLocationSizes.xlsx");
            }
        }

        private IGrid<InventoryLocationSizes> CreateExportableGrid()
        {
            IGrid<InventoryLocationSizes> grid = new Grid<InventoryLocationSizes>(_inventorylocationsizesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInventoryLocationSize).Titled("Inventory Location Size").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.nLength).Titled("Length").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.UnitsOfMeasurementFKDiffLengthUnit.sUnitOfMeasurement).Titled("Length Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nWidth).Titled("Width").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.UnitsOfMeasurementFKDiffWidthUnit.sUnitOfMeasurement).Titled("Width Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nHeight).Titled("Height").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.UnitsOfMeasurementFKDiffHeightUnit.sUnitOfMeasurement).Titled("Height Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nUsableVolume).Titled("Usable Volume").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.UnitsOfMeasurementFKDiffUsableVolumeUnit.sUnitOfMeasurement).Titled("Usable Volume Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InventoryLocationSizes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InventoryLocationSizes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inventorylocationsizesService.Get(id));
        }

        // GET: InventoryLocationSizes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixUsableVolumeUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixWidthUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixUsableVolumeUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementVolume().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End
            return View();
        }

        // POST: InventoryLocationSizes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInventoryLocationSize,sInventoryLocationSize,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit,nUsableVolume,ixUsableVolumeUnit")] InventoryLocationSizesPost inventorylocationsizes)
        {
            if (ModelState.IsValid)
            {
                inventorylocationsizes.UserName = User.Identity.Name;
                _inventorylocationsizesService.Create(inventorylocationsizes);
                return RedirectToAction("Index");
            }
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixUsableVolumeUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixWidthUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixUsableVolumeUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementVolume().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End
            return View(inventorylocationsizes);
        }

        // GET: InventoryLocationSizes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InventoryLocationSizesPost inventorylocationsizes = _inventorylocationsizesService.GetPost(id);
            if (inventorylocationsizes == null)
            {
                return NotFound();
            }
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixHeightUnit);
            //ViewBag.ixLengthUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixLengthUnit);
            //ViewBag.ixUsableVolumeUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixUsableVolumeUnit);
            //ViewBag.ixWidthUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixWidthUnit);
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixHeightUnit);
			ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixLengthUnit);
			ViewBag.ixUsableVolumeUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementVolume().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixUsableVolumeUnit);
			ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixWidthUnit);
            //Custom Code End
            return View(inventorylocationsizes);
        }

        // POST: InventoryLocationSizes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInventoryLocationSize,sInventoryLocationSize,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit,nUsableVolume,ixUsableVolumeUnit")] InventoryLocationSizesPost inventorylocationsizes)
        {
            if (ModelState.IsValid)
            {
                inventorylocationsizes.UserName = User.Identity.Name;
                _inventorylocationsizesService.Edit(inventorylocationsizes);
                return RedirectToAction("Index");
            }
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixHeightUnit);
            //ViewBag.ixLengthUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixLengthUnit);
            //ViewBag.ixUsableVolumeUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixUsableVolumeUnit);
            //ViewBag.ixWidthUnit = new SelectList(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixWidthUnit);
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixHeightUnit);
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixLengthUnit);
            ViewBag.ixUsableVolumeUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementVolume().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixUsableVolumeUnit);
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocationsizes.ixWidthUnit);
            //Custom Code End
            return View(inventorylocationsizes);
        }


        // GET: InventoryLocationSizes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inventorylocationsizesService.Get(id));
        }

        // POST: InventoryLocationSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InventoryLocationSizesPost inventorylocationsizes = _inventorylocationsizesService.GetPost(id);
            inventorylocationsizes.UserName = User.Identity.Name;
            _inventorylocationsizesService.Delete(inventorylocationsizes);
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
            string sInventoryLocationSize;

            InventoryLocationSizesPost inventorylocationsizes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInventoryLocationSize = _inventorylocationsizesService.Get(nID).sInventoryLocationSize;
                            if (!_inventorylocationsizesService.VerifyInventoryLocationSizeDeleteOK(nID, sInventoryLocationSize).Any())
                            {
                                inventorylocationsizes = _inventorylocationsizesService.GetPost(nID);
                                inventorylocationsizes.UserName = User.Identity.Name;
                                _inventorylocationsizesService.Delete(inventorylocationsizes);
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
        public IActionResult VerifyInventoryLocationSize(long ixInventoryLocationSize, string sInventoryLocationSize)
        {
            string validationResponse = "";

            if (!_inventorylocationsizesService.VerifyInventoryLocationSizeUnique(ixInventoryLocationSize, sInventoryLocationSize))
            {
                validationResponse = $"InventoryLocationSize {sInventoryLocationSize} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 
