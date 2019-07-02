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

    public class FacilityAisleFacesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IFacilityAisleFacesService _facilityaislefacesService;

        public FacilityAisleFacesController(IFacilityAisleFacesService facilityaislefacesService )
        {
            _facilityaislefacesService = facilityaislefacesService;
        }

        // GET: FacilityAisleFaces
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var facilityaislefaces = _facilityaislefacesService.Index();
            return View(facilityaislefaces.ToList());
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
            var facilityaislefaces = _facilityaislefacesService.Index();
            return PartialView("IndexGrid", facilityaislefaces.ToList());
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
                IGrid<FacilityAisleFaces> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<FacilityAisleFaces> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportFacilityAisleFaces.xlsx");
            }
        }

        private IGrid<FacilityAisleFaces> CreateExportableGrid()
        {
            IGrid<FacilityAisleFaces> grid = new Grid<FacilityAisleFaces>(_facilityaislefacesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sFacilityAisleFace).Titled("Facility Aisle Face").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.FacilityFloors.sFacilityFloor).Titled("Facility Floor").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nSequence).Titled("Sequence").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.BaySequenceTypes.sBaySequenceType).Titled("Bay Sequence Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.LogicalOrientations.sLogicalOrientation).Titled("Logical Orientation").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.AisleFaceStorageTypes.sAisleFaceStorageType).Titled("Aisle Face Storage Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nXOffset).Titled("X Offset").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nYOffset).Titled("Y Offset").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nLevels).Titled("Levels").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nDefaultNumberOfBays).Titled("Default Number Of Bays").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nDefaultNumberOfSlotsInBay).Titled("Default Number Of Slots In Bay").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InventoryLocationSizesFKDiffDefaultInventoryLocationSize.sInventoryLocationSize).Titled("Default Inventory Location Size").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<FacilityAisleFaces>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: FacilityAisleFaces/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_facilityaislefacesService.Get(id));
        }

        // GET: FacilityAisleFaces/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixAisleFaceStorageType = new SelectList(_facilityaislefacesService.selectAisleFaceStorageTypes().Select( x => new { x.ixAisleFaceStorageType, x.sAisleFaceStorageType }), "ixAisleFaceStorageType", "sAisleFaceStorageType");
			ViewBag.ixBaySequenceType = new SelectList(_facilityaislefacesService.selectBaySequenceTypes().Select( x => new { x.ixBaySequenceType, x.sBaySequenceType }), "ixBaySequenceType", "sBaySequenceType");
			ViewBag.ixDefaultFacilityZone = new SelectList(_facilityaislefacesService.selectFacilityZones().Select( x => new { x.ixFacilityZone, x.sFacilityZone }), "ixFacilityZone", "sFacilityZone");
			ViewBag.ixDefaultInventoryLocationSize = new SelectList(_facilityaislefacesService.selectInventoryLocationSizes().Select( x => new { x.ixInventoryLocationSize, x.sInventoryLocationSize }), "ixInventoryLocationSize", "sInventoryLocationSize");
			ViewBag.ixDefaultLocationFunction = new SelectList(_facilityaislefacesService.selectLocationFunctions().Select( x => new { x.ixLocationFunction, x.sLocationFunction }), "ixLocationFunction", "sLocationFunction");
			ViewBag.ixFacilityFloor = new SelectList(_facilityaislefacesService.selectFacilityFloors().Select( x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor");
			ViewBag.ixLogicalOrientation = new SelectList(_facilityaislefacesService.selectLogicalOrientations().Select( x => new { x.ixLogicalOrientation, x.sLogicalOrientation }), "ixLogicalOrientation", "sLogicalOrientation");
			ViewBag.ixPairedAisleFace = new SelectList(_facilityaislefacesService.selectFacilityAisleFaces().Select( x => new { x.ixFacilityAisleFace, x.sFacilityAisleFace }), "ixFacilityAisleFace", "sFacilityAisleFace");
			ViewBag.ixXOffsetUnit = new SelectList(_facilityaislefacesService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixYOffsetUnit = new SelectList(_facilityaislefacesService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");

            return View();
        }

        // POST: FacilityAisleFaces/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixFacilityAisleFace,sFacilityAisleFace,ixFacilityFloor,nSequence,ixBaySequenceType,ixPairedAisleFace,ixLogicalOrientation,ixAisleFaceStorageType,nXOffset,ixXOffsetUnit,nYOffset,ixYOffsetUnit,nLevels,nDefaultNumberOfBays,nDefaultNumberOfSlotsInBay,ixDefaultFacilityZone,ixDefaultLocationFunction,ixDefaultInventoryLocationSize")] FacilityAisleFacesPost facilityaislefaces)
        {
            if (ModelState.IsValid)
            {
                facilityaislefaces.UserName = User.Identity.Name;
                _facilityaislefacesService.Create(facilityaislefaces);
                return RedirectToAction("Index");
            }
			ViewBag.ixAisleFaceStorageType = new SelectList(_facilityaislefacesService.selectAisleFaceStorageTypes().Select( x => new { x.ixAisleFaceStorageType, x.sAisleFaceStorageType }), "ixAisleFaceStorageType", "sAisleFaceStorageType");
			ViewBag.ixBaySequenceType = new SelectList(_facilityaislefacesService.selectBaySequenceTypes().Select( x => new { x.ixBaySequenceType, x.sBaySequenceType }), "ixBaySequenceType", "sBaySequenceType");
			ViewBag.ixDefaultFacilityZone = new SelectList(_facilityaislefacesService.selectFacilityZones().Select( x => new { x.ixFacilityZone, x.sFacilityZone }), "ixFacilityZone", "sFacilityZone");
			ViewBag.ixDefaultInventoryLocationSize = new SelectList(_facilityaislefacesService.selectInventoryLocationSizes().Select( x => new { x.ixInventoryLocationSize, x.sInventoryLocationSize }), "ixInventoryLocationSize", "sInventoryLocationSize");
			ViewBag.ixDefaultLocationFunction = new SelectList(_facilityaislefacesService.selectLocationFunctions().Select( x => new { x.ixLocationFunction, x.sLocationFunction }), "ixLocationFunction", "sLocationFunction");
			ViewBag.ixFacilityFloor = new SelectList(_facilityaislefacesService.selectFacilityFloors().Select( x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor");
			ViewBag.ixLogicalOrientation = new SelectList(_facilityaislefacesService.selectLogicalOrientations().Select( x => new { x.ixLogicalOrientation, x.sLogicalOrientation }), "ixLogicalOrientation", "sLogicalOrientation");
			ViewBag.ixPairedAisleFace = new SelectList(_facilityaislefacesService.selectFacilityAisleFaces().Select( x => new { x.ixFacilityAisleFace, x.sFacilityAisleFace }), "ixFacilityAisleFace", "sFacilityAisleFace");
			ViewBag.ixXOffsetUnit = new SelectList(_facilityaislefacesService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixYOffsetUnit = new SelectList(_facilityaislefacesService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");

            return View(facilityaislefaces);
        }

        // GET: FacilityAisleFaces/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            FacilityAisleFacesPost facilityaislefaces = _facilityaislefacesService.GetPost(id);
            if (facilityaislefaces == null)
            {
                return NotFound();
            }
			ViewBag.ixAisleFaceStorageType = new SelectList(_facilityaislefacesService.selectAisleFaceStorageTypes().Select( x => new { x.ixAisleFaceStorageType, x.sAisleFaceStorageType }), "ixAisleFaceStorageType", "sAisleFaceStorageType", facilityaislefaces.ixAisleFaceStorageType);
			ViewBag.ixBaySequenceType = new SelectList(_facilityaislefacesService.selectBaySequenceTypes().Select( x => new { x.ixBaySequenceType, x.sBaySequenceType }), "ixBaySequenceType", "sBaySequenceType", facilityaislefaces.ixBaySequenceType);
			ViewBag.ixDefaultFacilityZone = new SelectList(_facilityaislefacesService.selectFacilityZonesNullable().Select( x => new { ixFacilityZone = x.Key, sFacilityZone = x.Value }), "ixFacilityZone", "sFacilityZone", facilityaislefaces.ixDefaultFacilityZone);
			ViewBag.ixDefaultInventoryLocationSize = new SelectList(_facilityaislefacesService.selectInventoryLocationSizes().Select( x => new { x.ixInventoryLocationSize, x.sInventoryLocationSize }), "ixInventoryLocationSize", "sInventoryLocationSize", facilityaislefaces.ixDefaultInventoryLocationSize);
			ViewBag.ixDefaultLocationFunction = new SelectList(_facilityaislefacesService.selectLocationFunctionsNullable().Select( x => new { ixLocationFunction = x.Key, sLocationFunction = x.Value }), "ixLocationFunction", "sLocationFunction", facilityaislefaces.ixDefaultLocationFunction);
			ViewBag.ixFacilityFloor = new SelectList(_facilityaislefacesService.selectFacilityFloors().Select( x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor", facilityaislefaces.ixFacilityFloor);
			ViewBag.ixLogicalOrientation = new SelectList(_facilityaislefacesService.selectLogicalOrientations().Select( x => new { x.ixLogicalOrientation, x.sLogicalOrientation }), "ixLogicalOrientation", "sLogicalOrientation", facilityaislefaces.ixLogicalOrientation);
			ViewBag.ixPairedAisleFace = new SelectList(_facilityaislefacesService.selectFacilityAisleFacesNullable().Select( x => new { ixFacilityAisleFace = x.Key, sFacilityAisleFace = x.Value }), "ixFacilityAisleFace", "sFacilityAisleFace", facilityaislefaces.ixPairedAisleFace);
			ViewBag.ixXOffsetUnit = new SelectList(_facilityaislefacesService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", facilityaislefaces.ixXOffsetUnit);
			ViewBag.ixYOffsetUnit = new SelectList(_facilityaislefacesService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", facilityaislefaces.ixYOffsetUnit);

            return View(facilityaislefaces);
        }

        // POST: FacilityAisleFaces/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixFacilityAisleFace,sFacilityAisleFace,ixFacilityFloor,nSequence,ixBaySequenceType,ixPairedAisleFace,ixLogicalOrientation,ixAisleFaceStorageType,nXOffset,ixXOffsetUnit,nYOffset,ixYOffsetUnit,nLevels,nDefaultNumberOfBays,nDefaultNumberOfSlotsInBay,ixDefaultFacilityZone,ixDefaultLocationFunction,ixDefaultInventoryLocationSize")] FacilityAisleFacesPost facilityaislefaces)
        {
            if (ModelState.IsValid)
            {
                facilityaislefaces.UserName = User.Identity.Name;
                _facilityaislefacesService.Edit(facilityaislefaces);
                return RedirectToAction("Index");
            }
			ViewBag.ixAisleFaceStorageType = new SelectList(_facilityaislefacesService.selectAisleFaceStorageTypes().Select( x => new { x.ixAisleFaceStorageType, x.sAisleFaceStorageType }), "ixAisleFaceStorageType", "sAisleFaceStorageType", facilityaislefaces.ixAisleFaceStorageType);
			ViewBag.ixBaySequenceType = new SelectList(_facilityaislefacesService.selectBaySequenceTypes().Select( x => new { x.ixBaySequenceType, x.sBaySequenceType }), "ixBaySequenceType", "sBaySequenceType", facilityaislefaces.ixBaySequenceType);
			ViewBag.ixDefaultFacilityZone = new SelectList(_facilityaislefacesService.selectFacilityZonesNullable().Select( x => new { ixFacilityZone = x.Key, sFacilityZone = x.Value }), "ixFacilityZone", "sFacilityZone", facilityaislefaces.ixDefaultFacilityZone);
			ViewBag.ixDefaultInventoryLocationSize = new SelectList(_facilityaislefacesService.selectInventoryLocationSizes().Select( x => new { x.ixInventoryLocationSize, x.sInventoryLocationSize }), "ixInventoryLocationSize", "sInventoryLocationSize", facilityaislefaces.ixDefaultInventoryLocationSize);
			ViewBag.ixDefaultLocationFunction = new SelectList(_facilityaislefacesService.selectLocationFunctionsNullable().Select( x => new { ixLocationFunction = x.Key, sLocationFunction = x.Value }), "ixLocationFunction", "sLocationFunction", facilityaislefaces.ixDefaultLocationFunction);
			ViewBag.ixFacilityFloor = new SelectList(_facilityaislefacesService.selectFacilityFloors().Select( x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor", facilityaislefaces.ixFacilityFloor);
			ViewBag.ixLogicalOrientation = new SelectList(_facilityaislefacesService.selectLogicalOrientations().Select( x => new { x.ixLogicalOrientation, x.sLogicalOrientation }), "ixLogicalOrientation", "sLogicalOrientation", facilityaislefaces.ixLogicalOrientation);
			ViewBag.ixPairedAisleFace = new SelectList(_facilityaislefacesService.selectFacilityAisleFacesNullable().Select( x => new { ixFacilityAisleFace = x.Key, sFacilityAisleFace = x.Value }), "ixFacilityAisleFace", "sFacilityAisleFace", facilityaislefaces.ixPairedAisleFace);
			ViewBag.ixXOffsetUnit = new SelectList(_facilityaislefacesService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", facilityaislefaces.ixXOffsetUnit);
			ViewBag.ixYOffsetUnit = new SelectList(_facilityaislefacesService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", facilityaislefaces.ixYOffsetUnit);

            return View(facilityaislefaces);
        }


        // GET: FacilityAisleFaces/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_facilityaislefacesService.Get(id));
        }

        // POST: FacilityAisleFaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FacilityAisleFacesPost facilityaislefaces = _facilityaislefacesService.GetPost(id);
            facilityaislefaces.UserName = User.Identity.Name;
            _facilityaislefacesService.Delete(facilityaislefaces);
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
            string sFacilityAisleFace;

            FacilityAisleFacesPost facilityaislefaces;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sFacilityAisleFace = _facilityaislefacesService.Get(nID).sFacilityAisleFace;
                            if (!_facilityaislefacesService.VerifyFacilityAisleFaceDeleteOK(nID, sFacilityAisleFace).Any())
                            {
                                facilityaislefaces = _facilityaislefacesService.GetPost(nID);
                                facilityaislefaces.UserName = User.Identity.Name;
                                _facilityaislefacesService.Delete(facilityaislefaces);
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
        public IActionResult VerifyFacilityAisleFace(long ixFacilityAisleFace, string sFacilityAisleFace)
        {
            string validationResponse = "";

            if (!_facilityaislefacesService.VerifyFacilityAisleFaceUnique(ixFacilityAisleFace, sFacilityAisleFace))
            {
                validationResponse = $"FacilityAisleFace {sFacilityAisleFace} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

