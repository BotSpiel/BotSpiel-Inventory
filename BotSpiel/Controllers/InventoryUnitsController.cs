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

    public class InventoryUnitsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInventoryUnitsService _inventoryunitsService;
        //Custom Code Start | Added Code Block
        private readonly IInventoryUnitTransactionContextsService _inventoryunittransactioncontextsService;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public InventoryUnitsController(IInventoryUnitsService inventoryunitsService)
        //Replaced Code Block End
        public InventoryUnitsController(IInventoryUnitsService inventoryunitsService, IInventoryUnitTransactionContextsService inventoryunittransactioncontextsService)
        {
            //Custom Code End
            _inventoryunitsService = inventoryunitsService;
            //Custom Code Start | Added Code Block
            _inventoryunittransactioncontextsService = inventoryunittransactioncontextsService;
            //Custom Code End
        }

        // GET: InventoryUnits
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inventoryunits = _inventoryunitsService.Index();
            return View(inventoryunits.ToList());
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
            var inventoryunits = _inventoryunitsService.Index();
            return PartialView("IndexGrid", inventoryunits.ToList());
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
                IGrid<InventoryUnits> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InventoryUnits> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInventoryUnits.xlsx");
            }
        }

        private IGrid<InventoryUnits> CreateExportableGrid()
        {
            IGrid<InventoryUnits> grid = new Grid<InventoryUnits>(_inventoryunitsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInventoryUnit).Titled("Inventory Unit").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Facilities.sFacility).Titled("Facility").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Companies.sCompany).Titled("Company").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Materials.sMaterial).Titled("Material").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InventoryStates.sInventoryState).Titled("Inventory State").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InventoryLocations.sInventoryLocation).Titled("Inventory Location").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantity).Titled("Base Unit Quantity").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sSerialNumber).Titled("Serial Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sBatchNumber).Titled("Batch Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtExpireAt).Titled("Expire At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InventoryUnits>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InventoryUnits/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inventoryunitsService.Get(id));
        }

        // GET: InventoryUnits/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCompany = new SelectList(_inventoryunitsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_inventoryunitsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixHandlingUnit = new SelectList(_inventoryunitsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixInventoryLocation = new SelectList(_inventoryunitsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixInventoryState = new SelectList(_inventoryunitsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState");
			ViewBag.ixMaterial = new SelectList(_inventoryunitsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixStatus = new SelectList(_inventoryunitsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: InventoryUnits/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInventoryUnit,sInventoryUnit,ixFacility,ixCompany,ixMaterial,ixInventoryState,ixHandlingUnit,ixInventoryLocation,nBaseUnitQuantity,sSerialNumber,sBatchNumber,dtExpireAt,ixStatus")] InventoryUnitsPost inventoryunits)
        {
            //Custom Code Start | Added Code Block
            var ixInventoryUnitTransactionContext = _inventoryunittransactioncontextsService.Index().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault();
            //Custom Code End
            if (ModelState.IsValid)
            {
                inventoryunits.UserName = User.Identity.Name;
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //_inventoryunitsService.Create(inventoryunits);
                //Replaced Code Block End
                _inventoryunitsService.Create(inventoryunits, ixInventoryUnitTransactionContext);
                //Custom Code End
                return RedirectToAction("Index");
            }
			ViewBag.ixCompany = new SelectList(_inventoryunitsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_inventoryunitsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixHandlingUnit = new SelectList(_inventoryunitsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixInventoryLocation = new SelectList(_inventoryunitsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixInventoryState = new SelectList(_inventoryunitsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState");
			ViewBag.ixMaterial = new SelectList(_inventoryunitsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixStatus = new SelectList(_inventoryunitsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(inventoryunits);
        }

        // GET: InventoryUnits/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InventoryUnitsPost inventoryunits = _inventoryunitsService.GetPost(id);
            if (inventoryunits == null)
            {
                return NotFound();
            }
			ViewBag.ixCompany = new SelectList(_inventoryunitsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", inventoryunits.ixCompany);
			ViewBag.ixFacility = new SelectList(_inventoryunitsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", inventoryunits.ixFacility);
			ViewBag.ixHandlingUnit = new SelectList(_inventoryunitsService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", inventoryunits.ixHandlingUnit);
			ViewBag.ixInventoryLocation = new SelectList(_inventoryunitsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", inventoryunits.ixInventoryLocation);
			ViewBag.ixInventoryState = new SelectList(_inventoryunitsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState", inventoryunits.ixInventoryState);
			ViewBag.ixMaterial = new SelectList(_inventoryunitsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", inventoryunits.ixMaterial);
			ViewBag.ixStatus = new SelectList(_inventoryunitsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", inventoryunits.ixStatus);

            return View(inventoryunits);
        }

        // POST: InventoryUnits/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInventoryUnit,sInventoryUnit,ixFacility,ixCompany,ixMaterial,ixInventoryState,ixHandlingUnit,ixInventoryLocation,nBaseUnitQuantity,sSerialNumber,sBatchNumber,dtExpireAt,ixStatus")] InventoryUnitsPost inventoryunits)
        {
            //Custom Code Start | Added Code Block
            var ixInventoryUnitTransactionContext = _inventoryunittransactioncontextsService.Index().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault();
            //Custom Code End
            if (ModelState.IsValid)
            {
                inventoryunits.UserName = User.Identity.Name;
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //_inventoryunitsService.Edit(inventoryunits);
                //Replaced Code Block End
                _inventoryunitsService.Edit(inventoryunits, ixInventoryUnitTransactionContext);
                //Custom Code End                
                return RedirectToAction("Index");
            }
			ViewBag.ixCompany = new SelectList(_inventoryunitsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", inventoryunits.ixCompany);
			ViewBag.ixFacility = new SelectList(_inventoryunitsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", inventoryunits.ixFacility);
			ViewBag.ixHandlingUnit = new SelectList(_inventoryunitsService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", inventoryunits.ixHandlingUnit);
			ViewBag.ixInventoryLocation = new SelectList(_inventoryunitsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", inventoryunits.ixInventoryLocation);
			ViewBag.ixInventoryState = new SelectList(_inventoryunitsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState", inventoryunits.ixInventoryState);
			ViewBag.ixMaterial = new SelectList(_inventoryunitsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", inventoryunits.ixMaterial);
			ViewBag.ixStatus = new SelectList(_inventoryunitsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", inventoryunits.ixStatus);

            return View(inventoryunits);
        }


        // GET: InventoryUnits/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inventoryunitsService.Get(id));
        }

        // POST: InventoryUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InventoryUnitsPost inventoryunits = _inventoryunitsService.GetPost(id);
            inventoryunits.UserName = User.Identity.Name;
            _inventoryunitsService.Delete(inventoryunits);
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
            string sInventoryUnit;

            InventoryUnitsPost inventoryunits;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInventoryUnit = _inventoryunitsService.Get(nID).sInventoryUnit;
                            if (!_inventoryunitsService.VerifyInventoryUnitDeleteOK(nID, sInventoryUnit).Any())
                            {
                                inventoryunits = _inventoryunitsService.GetPost(nID);
                                inventoryunits.UserName = User.Identity.Name;
                                _inventoryunitsService.Delete(inventoryunits);
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
        public IActionResult VerifyInventoryUnit(long ixInventoryUnit, string sInventoryUnit)
        {
            string validationResponse = "";

            if (!_inventoryunitsService.VerifyInventoryUnitUnique(ixInventoryUnit, sInventoryUnit))
            {
                validationResponse = $"InventoryUnit {sInventoryUnit} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 
