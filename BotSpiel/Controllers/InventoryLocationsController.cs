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

    public class InventoryLocationsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInventoryLocationsService _inventorylocationsService;

        public InventoryLocationsController(IInventoryLocationsService inventorylocationsService )
        {
            _inventorylocationsService = inventorylocationsService;
        }

        // GET: InventoryLocations
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inventorylocations = _inventorylocationsService.Index();
            return View(inventorylocations.ToList());
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
            var inventorylocations = _inventorylocationsService.Index();
            return PartialView("IndexGrid", inventorylocations.ToList());
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
                IGrid<InventoryLocations> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InventoryLocations> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInventoryLocations.xlsx");
            }
        }

        private IGrid<InventoryLocations> CreateExportableGrid()
        {
            IGrid<InventoryLocations> grid = new Grid<InventoryLocations>(_inventorylocationsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInventoryLocation).Titled("Inventory Location").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.LocationFunctions.sLocationFunction).Titled("Location Function").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Facilities.sFacility).Titled("Facility").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.FacilityFloors.sFacilityFloor).Titled("Facility Floor").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.FacilityZones.sFacilityZone).Titled("Facility Zone").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.FacilityWorkAreas.sFacilityWorkArea).Titled("Facility Work Area").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.FacilityAisleFaces.sFacilityAisleFace).Titled("Facility Aisle Face").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sLevel).Titled("Level").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sBay).Titled("Bay").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sSlot).Titled("Slot").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.nSequence).Titled("Sequence").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nXOffset).Titled("X Offset").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nYOffset).Titled("Y Offset").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nZOffset).Titled("Z Offset").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sLatitude).Titled("Latitude").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sLongitude).Titled("Longitude").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bTrackUtilisation).Titled("Track Utilisation").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nUtilisationPercent).Titled("Utilisation Percent").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nQueuedUtilisationPercent).Titled("Queued Utilisation Percent").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InventoryLocations>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InventoryLocations/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inventorylocationsService.Get(id));
        }

        // GET: InventoryLocations/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCompany = new SelectList(_inventorylocationsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_inventorylocationsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixFacilityAisleFace = new SelectList(_inventorylocationsService.selectFacilityAisleFaces().Select( x => new { x.ixFacilityAisleFace, x.sFacilityAisleFace }), "ixFacilityAisleFace", "sFacilityAisleFace");
			ViewBag.ixFacilityFloor = new SelectList(_inventorylocationsService.selectFacilityFloors().Select( x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor");
			ViewBag.ixFacilityWorkArea = new SelectList(_inventorylocationsService.selectFacilityWorkAreas().Select( x => new { x.ixFacilityWorkArea, x.sFacilityWorkArea }), "ixFacilityWorkArea", "sFacilityWorkArea");
			ViewBag.ixFacilityZone = new SelectList(_inventorylocationsService.selectFacilityZones().Select( x => new { x.ixFacilityZone, x.sFacilityZone }), "ixFacilityZone", "sFacilityZone");
			ViewBag.ixInventoryLocationSize = new SelectList(_inventorylocationsService.selectInventoryLocationSizes().Select( x => new { x.ixInventoryLocationSize, x.sInventoryLocationSize }), "ixInventoryLocationSize", "sInventoryLocationSize");
			ViewBag.ixLocationFunction = new SelectList(_inventorylocationsService.selectLocationFunctions().Select( x => new { x.ixLocationFunction, x.sLocationFunction }), "ixLocationFunction", "sLocationFunction");
			ViewBag.ixXOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixYOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixZOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");

            return View();
        }

        // POST: InventoryLocations/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInventoryLocation,sInventoryLocation,ixLocationFunction,ixFacility,ixCompany,ixFacilityFloor,ixFacilityZone,ixFacilityWorkArea,ixFacilityAisleFace,sLevel,sBay,sSlot,ixInventoryLocationSize,nSequence,nXOffset,ixXOffsetUnit,nYOffset,ixYOffsetUnit,nZOffset,ixZOffsetUnit,sLatitude,sLongitude,bTrackUtilisation,nUtilisationPercent,nQueuedUtilisationPercent")] InventoryLocationsPost inventorylocations)
        {
            if (ModelState.IsValid)
            {
                inventorylocations.UserName = User.Identity.Name;
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //_inventorylocationsService.Create(inventorylocations);
                //return RedirectToAction("Index");
                //Replaced Code Block End
                var ixInventoryLocation = _inventorylocationsService.Create(inventorylocations).Result;
                return RedirectToAction("Edit", new { id = ixInventoryLocation });
                //Custom Code End
            }
            ViewBag.ixCompany = new SelectList(_inventorylocationsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_inventorylocationsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixFacilityAisleFace = new SelectList(_inventorylocationsService.selectFacilityAisleFaces().Select( x => new { x.ixFacilityAisleFace, x.sFacilityAisleFace }), "ixFacilityAisleFace", "sFacilityAisleFace");
			ViewBag.ixFacilityFloor = new SelectList(_inventorylocationsService.selectFacilityFloors().Select( x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor");
			ViewBag.ixFacilityWorkArea = new SelectList(_inventorylocationsService.selectFacilityWorkAreas().Select( x => new { x.ixFacilityWorkArea, x.sFacilityWorkArea }), "ixFacilityWorkArea", "sFacilityWorkArea");
			ViewBag.ixFacilityZone = new SelectList(_inventorylocationsService.selectFacilityZones().Select( x => new { x.ixFacilityZone, x.sFacilityZone }), "ixFacilityZone", "sFacilityZone");
			ViewBag.ixInventoryLocationSize = new SelectList(_inventorylocationsService.selectInventoryLocationSizes().Select( x => new { x.ixInventoryLocationSize, x.sInventoryLocationSize }), "ixInventoryLocationSize", "sInventoryLocationSize");
			ViewBag.ixLocationFunction = new SelectList(_inventorylocationsService.selectLocationFunctions().Select( x => new { x.ixLocationFunction, x.sLocationFunction }), "ixLocationFunction", "sLocationFunction");
			ViewBag.ixXOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixYOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixZOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");

            return View(inventorylocations);
        }

        //Custom Code Start | Added Code Block 

        [Authorize]
        [HttpGet]
        public ActionResult CreateWithID(long id)
        {
            ViewBag.ixCompany = new SelectList(_inventorylocationsService.selectCompanies().Select(x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
            ViewBag.ixFacility = new SelectList(_inventorylocationsService.selectFacilities().Select(x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
            ViewBag.ixFacilityAisleFace = new SelectList(_inventorylocationsService.selectFacilityAisleFaces().Where(x => x.ixFacilityAisleFace == id).Select(x => new { x.ixFacilityAisleFace, x.sFacilityAisleFace }), "ixFacilityAisleFace", "sFacilityAisleFace");
            ViewBag.ixFacilityFloor = new SelectList(_inventorylocationsService.selectFacilityFloors().Select(x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor");
            ViewBag.ixFacilityWorkArea = new SelectList(_inventorylocationsService.selectFacilityWorkAreas().Select(x => new { x.ixFacilityWorkArea, x.sFacilityWorkArea }), "ixFacilityWorkArea", "sFacilityWorkArea");
            ViewBag.ixFacilityZone = new SelectList(_inventorylocationsService.selectFacilityZones().Select(x => new { x.ixFacilityZone, x.sFacilityZone }), "ixFacilityZone", "sFacilityZone");
            ViewBag.ixInventoryLocationSize = new SelectList(_inventorylocationsService.selectInventoryLocationSizes().Select(x => new { x.ixInventoryLocationSize, x.sInventoryLocationSize }), "ixInventoryLocationSize", "sInventoryLocationSize");
            ViewBag.ixLocationFunction = new SelectList(_inventorylocationsService.selectLocationFunctions().Select(x => new { x.ixLocationFunction, x.sLocationFunction }), "ixLocationFunction", "sLocationFunction");
            ViewBag.ixXOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixYOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixZOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");

            return View();
        }

        // POST: InventoryLocations/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithID([Bind("ixInventoryLocation,sInventoryLocation,ixLocationFunction,ixFacility,ixCompany,ixFacilityFloor,ixFacilityZone,ixFacilityWorkArea,ixFacilityAisleFace,sLevel,sBay,sSlot,ixInventoryLocationSize,nSequence,nXOffset,ixXOffsetUnit,nYOffset,ixYOffsetUnit,nZOffset,ixZOffsetUnit,sLatitude,sLongitude,bTrackUtilisation,nUtilisationPercent,nQueuedUtilisationPercent")] InventoryLocationsPost inventorylocations)
        {
            if (ModelState.IsValid)
            {
                inventorylocations.UserName = User.Identity.Name;
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //_inventorylocationsService.Create(inventorylocations);
                //return RedirectToAction("Index");
                //Replaced Code Block End
                var ixInventoryLocation = _inventorylocationsService.Create(inventorylocations).Result;
                return RedirectToAction("Edit", "FacilityAisleFaces", new { id = inventorylocations.ixFacilityAisleFace });
                //Custom Code End
            }
            ViewBag.ixCompany = new SelectList(_inventorylocationsService.selectCompanies().Select(x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
            ViewBag.ixFacility = new SelectList(_inventorylocationsService.selectFacilities().Select(x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
            ViewBag.ixFacilityAisleFace = new SelectList(_inventorylocationsService.selectFacilityAisleFaces().Where(x => x.ixFacilityAisleFace == inventorylocations.ixFacilityAisleFace).Select(x => new { x.ixFacilityAisleFace, x.sFacilityAisleFace }), "ixFacilityAisleFace", "sFacilityAisleFace");
            ViewBag.ixFacilityFloor = new SelectList(_inventorylocationsService.selectFacilityFloors().Select(x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor");
            ViewBag.ixFacilityWorkArea = new SelectList(_inventorylocationsService.selectFacilityWorkAreas().Select(x => new { x.ixFacilityWorkArea, x.sFacilityWorkArea }), "ixFacilityWorkArea", "sFacilityWorkArea");
            ViewBag.ixFacilityZone = new SelectList(_inventorylocationsService.selectFacilityZones().Select(x => new { x.ixFacilityZone, x.sFacilityZone }), "ixFacilityZone", "sFacilityZone");
            ViewBag.ixInventoryLocationSize = new SelectList(_inventorylocationsService.selectInventoryLocationSizes().Select(x => new { x.ixInventoryLocationSize, x.sInventoryLocationSize }), "ixInventoryLocationSize", "sInventoryLocationSize");
            ViewBag.ixLocationFunction = new SelectList(_inventorylocationsService.selectLocationFunctions().Select(x => new { x.ixLocationFunction, x.sLocationFunction }), "ixLocationFunction", "sLocationFunction");
            ViewBag.ixXOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixYOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixZOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");

            return View(inventorylocations);
        }



        //Custom Code End



        // GET: InventoryLocations/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InventoryLocationsPost inventorylocations = _inventorylocationsService.GetPost(id);
            if (inventorylocations == null)
            {
                return NotFound();
            }
			ViewBag.ixCompany = new SelectList(_inventorylocationsService.selectCompaniesNullable().Select( x => new { ixCompany = x.Key, sCompany = x.Value }), "ixCompany", "sCompany", inventorylocations.ixCompany);
			ViewBag.ixFacility = new SelectList(_inventorylocationsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", inventorylocations.ixFacility);
			ViewBag.ixFacilityAisleFace = new SelectList(_inventorylocationsService.selectFacilityAisleFaces().Select( x => new { x.ixFacilityAisleFace, x.sFacilityAisleFace }), "ixFacilityAisleFace", "sFacilityAisleFace", inventorylocations.ixFacilityAisleFace);
			ViewBag.ixFacilityFloor = new SelectList(_inventorylocationsService.selectFacilityFloors().Select( x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor", inventorylocations.ixFacilityFloor);
			ViewBag.ixFacilityWorkArea = new SelectList(_inventorylocationsService.selectFacilityWorkAreas().Select( x => new { x.ixFacilityWorkArea, x.sFacilityWorkArea }), "ixFacilityWorkArea", "sFacilityWorkArea", inventorylocations.ixFacilityWorkArea);
			ViewBag.ixFacilityZone = new SelectList(_inventorylocationsService.selectFacilityZones().Select( x => new { x.ixFacilityZone, x.sFacilityZone }), "ixFacilityZone", "sFacilityZone", inventorylocations.ixFacilityZone);
			ViewBag.ixInventoryLocationSize = new SelectList(_inventorylocationsService.selectInventoryLocationSizesNullable().Select( x => new { ixInventoryLocationSize = x.Key, sInventoryLocationSize = x.Value }), "ixInventoryLocationSize", "sInventoryLocationSize", inventorylocations.ixInventoryLocationSize);
			ViewBag.ixLocationFunction = new SelectList(_inventorylocationsService.selectLocationFunctions().Select( x => new { x.ixLocationFunction, x.sLocationFunction }), "ixLocationFunction", "sLocationFunction", inventorylocations.ixLocationFunction);
			ViewBag.ixXOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocations.ixXOffsetUnit);
			ViewBag.ixYOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocations.ixYOffsetUnit);
			ViewBag.ixZOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocations.ixZOffsetUnit);

            return View(inventorylocations);
        }

        // POST: InventoryLocations/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInventoryLocation,sInventoryLocation,ixLocationFunction,ixFacility,ixCompany,ixFacilityFloor,ixFacilityZone,ixFacilityWorkArea,ixFacilityAisleFace,sLevel,sBay,sSlot,ixInventoryLocationSize,nSequence,nXOffset,ixXOffsetUnit,nYOffset,ixYOffsetUnit,nZOffset,ixZOffsetUnit,sLatitude,sLongitude,bTrackUtilisation,nUtilisationPercent,nQueuedUtilisationPercent")] InventoryLocationsPost inventorylocations)
        {
            if (ModelState.IsValid)
            {
                inventorylocations.UserName = User.Identity.Name;
                _inventorylocationsService.Edit(inventorylocations);
                return RedirectToAction("Index");
            }
			ViewBag.ixCompany = new SelectList(_inventorylocationsService.selectCompaniesNullable().Select( x => new { ixCompany = x.Key, sCompany = x.Value }), "ixCompany", "sCompany", inventorylocations.ixCompany);
			ViewBag.ixFacility = new SelectList(_inventorylocationsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", inventorylocations.ixFacility);
			ViewBag.ixFacilityAisleFace = new SelectList(_inventorylocationsService.selectFacilityAisleFaces().Select( x => new { x.ixFacilityAisleFace, x.sFacilityAisleFace }), "ixFacilityAisleFace", "sFacilityAisleFace", inventorylocations.ixFacilityAisleFace);
			ViewBag.ixFacilityFloor = new SelectList(_inventorylocationsService.selectFacilityFloors().Select( x => new { x.ixFacilityFloor, x.sFacilityFloor }), "ixFacilityFloor", "sFacilityFloor", inventorylocations.ixFacilityFloor);
			ViewBag.ixFacilityWorkArea = new SelectList(_inventorylocationsService.selectFacilityWorkAreas().Select( x => new { x.ixFacilityWorkArea, x.sFacilityWorkArea }), "ixFacilityWorkArea", "sFacilityWorkArea", inventorylocations.ixFacilityWorkArea);
			ViewBag.ixFacilityZone = new SelectList(_inventorylocationsService.selectFacilityZones().Select( x => new { x.ixFacilityZone, x.sFacilityZone }), "ixFacilityZone", "sFacilityZone", inventorylocations.ixFacilityZone);
			ViewBag.ixInventoryLocationSize = new SelectList(_inventorylocationsService.selectInventoryLocationSizesNullable().Select( x => new { ixInventoryLocationSize = x.Key, sInventoryLocationSize = x.Value }), "ixInventoryLocationSize", "sInventoryLocationSize", inventorylocations.ixInventoryLocationSize);
			ViewBag.ixLocationFunction = new SelectList(_inventorylocationsService.selectLocationFunctions().Select( x => new { x.ixLocationFunction, x.sLocationFunction }), "ixLocationFunction", "sLocationFunction", inventorylocations.ixLocationFunction);
			ViewBag.ixXOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocations.ixXOffsetUnit);
			ViewBag.ixYOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocations.ixYOffsetUnit);
			ViewBag.ixZOffsetUnit = new SelectList(_inventorylocationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", inventorylocations.ixZOffsetUnit);

            return View(inventorylocations);
        }


        // GET: InventoryLocations/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inventorylocationsService.Get(id));
        }

        // POST: InventoryLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InventoryLocationsPost inventorylocations = _inventorylocationsService.GetPost(id);
            inventorylocations.UserName = User.Identity.Name;
            _inventorylocationsService.Delete(inventorylocations);
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
            string sInventoryLocation;

            InventoryLocationsPost inventorylocations;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInventoryLocation = _inventorylocationsService.Get(nID).sInventoryLocation;
                            if (!_inventorylocationsService.VerifyInventoryLocationDeleteOK(nID, sInventoryLocation).Any())
                            {
                                inventorylocations = _inventorylocationsService.GetPost(nID);
                                inventorylocations.UserName = User.Identity.Name;
                                _inventorylocationsService.Delete(inventorylocations);
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
        public IActionResult VerifyInventoryLocation(long ixInventoryLocation, string sInventoryLocation)
        {
            string validationResponse = "";

            if (!_inventorylocationsService.VerifyInventoryLocationUnique(ixInventoryLocation, sInventoryLocation))
            {
                validationResponse = $"InventoryLocation {sInventoryLocation} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

