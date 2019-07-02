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

    public class InventoryUnitTransactionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInventoryUnitTransactionsService _inventoryunittransactionsService;

        public InventoryUnitTransactionsController(IInventoryUnitTransactionsService inventoryunittransactionsService )
        {
            _inventoryunittransactionsService = inventoryunittransactionsService;
        }

        // GET: InventoryUnitTransactions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inventoryunittransactions = _inventoryunittransactionsService.Index();
            return View(inventoryunittransactions.ToList());
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
            var inventoryunittransactions = _inventoryunittransactionsService.Index();
            return PartialView("IndexGrid", inventoryunittransactions.ToList());
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
                IGrid<InventoryUnitTransactions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InventoryUnitTransactions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInventoryUnitTransactions.xlsx");
            }
        }

        private IGrid<InventoryUnitTransactions> CreateExportableGrid()
        {
            IGrid<InventoryUnitTransactions> grid = new Grid<InventoryUnitTransactions>(_inventoryunittransactionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInventoryUnitTransaction).Titled("Inventory Unit Transaction").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.InventoryUnits.sInventoryUnit).Titled("Inventory Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InventoryUnitTransactionContexts.sInventoryUnitTransactionContext).Titled("Inventory Unit Transaction Context").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.FacilitiesFKDiffFacilityAfter.sFacility).Titled("Facility After").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.CompaniesFKDiffCompanyAfter.sCompany).Titled("Company After").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.MaterialsFKDiffMaterialAfter.sMaterial).Titled("Material After").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InventoryStatesFKDiffInventoryStateAfter.sInventoryState).Titled("Inventory State After").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InventoryLocationsFKDiffInventoryLocationAfter.sInventoryLocation).Titled("Inventory Location After").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityBefore).Titled("Base Unit Quantity Before").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityAfter).Titled("Base Unit Quantity After").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sSerialNumberBefore).Titled("Serial Number Before").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sSerialNumberAfter).Titled("Serial Number After").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sBatchNumberBefore).Titled("Batch Number Before").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sBatchNumberAfter).Titled("Batch Number After").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtExpireAtBefore).Titled("Expire At Before").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtExpireAtAfter).Titled("Expire At After").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.StatusesFKDiffStatusAfter.sStatus).Titled("Status After").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InventoryUnitTransactions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InventoryUnitTransactions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inventoryunittransactionsService.Get(id));
        }

        // GET: InventoryUnitTransactions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCompanyAfter = new SelectList(_inventoryunittransactionsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixCompanyBefore = new SelectList(_inventoryunittransactionsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacilityAfter = new SelectList(_inventoryunittransactionsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixFacilityBefore = new SelectList(_inventoryunittransactionsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixHandlingUnitAfter = new SelectList(_inventoryunittransactionsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixHandlingUnitBefore = new SelectList(_inventoryunittransactionsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixInventoryLocationAfter = new SelectList(_inventoryunittransactionsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixInventoryLocationBefore = new SelectList(_inventoryunittransactionsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixInventoryStateAfter = new SelectList(_inventoryunittransactionsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState");
			ViewBag.ixInventoryStateBefore = new SelectList(_inventoryunittransactionsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState");
			ViewBag.ixInventoryUnit = new SelectList(_inventoryunittransactionsService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit");
			ViewBag.ixInventoryUnitTransactionContext = new SelectList(_inventoryunittransactionsService.selectInventoryUnitTransactionContexts().Select( x => new { x.ixInventoryUnitTransactionContext, x.sInventoryUnitTransactionContext }), "ixInventoryUnitTransactionContext", "sInventoryUnitTransactionContext");
			ViewBag.ixMaterialAfter = new SelectList(_inventoryunittransactionsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixMaterialBefore = new SelectList(_inventoryunittransactionsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixStatusAfter = new SelectList(_inventoryunittransactionsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");
			ViewBag.ixStatusBefore = new SelectList(_inventoryunittransactionsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: InventoryUnitTransactions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInventoryUnitTransaction,sInventoryUnitTransaction,ixInventoryUnit,ixInventoryUnitTransactionContext,ixFacilityBefore,ixFacilityAfter,ixCompanyBefore,ixCompanyAfter,ixMaterialBefore,ixMaterialAfter,ixInventoryStateBefore,ixInventoryStateAfter,ixHandlingUnitBefore,ixHandlingUnitAfter,ixInventoryLocationBefore,ixInventoryLocationAfter,nBaseUnitQuantityBefore,nBaseUnitQuantityAfter,sSerialNumberBefore,sSerialNumberAfter,sBatchNumberBefore,sBatchNumberAfter,dtExpireAtBefore,dtExpireAtAfter,ixStatusBefore,ixStatusAfter")] InventoryUnitTransactionsPost inventoryunittransactions)
        {
            if (ModelState.IsValid)
            {
                inventoryunittransactions.UserName = User.Identity.Name;
                _inventoryunittransactionsService.Create(inventoryunittransactions);
                return RedirectToAction("Index");
            }
			ViewBag.ixCompanyAfter = new SelectList(_inventoryunittransactionsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixCompanyBefore = new SelectList(_inventoryunittransactionsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacilityAfter = new SelectList(_inventoryunittransactionsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixFacilityBefore = new SelectList(_inventoryunittransactionsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixHandlingUnitAfter = new SelectList(_inventoryunittransactionsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixHandlingUnitBefore = new SelectList(_inventoryunittransactionsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixInventoryLocationAfter = new SelectList(_inventoryunittransactionsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixInventoryLocationBefore = new SelectList(_inventoryunittransactionsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixInventoryStateAfter = new SelectList(_inventoryunittransactionsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState");
			ViewBag.ixInventoryStateBefore = new SelectList(_inventoryunittransactionsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState");
			ViewBag.ixInventoryUnit = new SelectList(_inventoryunittransactionsService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit");
			ViewBag.ixInventoryUnitTransactionContext = new SelectList(_inventoryunittransactionsService.selectInventoryUnitTransactionContexts().Select( x => new { x.ixInventoryUnitTransactionContext, x.sInventoryUnitTransactionContext }), "ixInventoryUnitTransactionContext", "sInventoryUnitTransactionContext");
			ViewBag.ixMaterialAfter = new SelectList(_inventoryunittransactionsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixMaterialBefore = new SelectList(_inventoryunittransactionsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixStatusAfter = new SelectList(_inventoryunittransactionsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");
			ViewBag.ixStatusBefore = new SelectList(_inventoryunittransactionsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(inventoryunittransactions);
        }

        // GET: InventoryUnitTransactions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InventoryUnitTransactionsPost inventoryunittransactions = _inventoryunittransactionsService.GetPost(id);
            if (inventoryunittransactions == null)
            {
                return NotFound();
            }
			ViewBag.ixCompanyAfter = new SelectList(_inventoryunittransactionsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", inventoryunittransactions.ixCompanyAfter);
			ViewBag.ixCompanyBefore = new SelectList(_inventoryunittransactionsService.selectCompaniesNullable().Select( x => new { ixCompany = x.Key, sCompany = x.Value }), "ixCompany", "sCompany", inventoryunittransactions.ixCompanyBefore);
			ViewBag.ixFacilityAfter = new SelectList(_inventoryunittransactionsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", inventoryunittransactions.ixFacilityAfter);
			ViewBag.ixFacilityBefore = new SelectList(_inventoryunittransactionsService.selectFacilitiesNullable().Select( x => new { ixFacility = x.Key, sFacility = x.Value }), "ixFacility", "sFacility", inventoryunittransactions.ixFacilityBefore);
			ViewBag.ixHandlingUnitAfter = new SelectList(_inventoryunittransactionsService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", inventoryunittransactions.ixHandlingUnitAfter);
			ViewBag.ixHandlingUnitBefore = new SelectList(_inventoryunittransactionsService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", inventoryunittransactions.ixHandlingUnitBefore);
			ViewBag.ixInventoryLocationAfter = new SelectList(_inventoryunittransactionsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", inventoryunittransactions.ixInventoryLocationAfter);
			ViewBag.ixInventoryLocationBefore = new SelectList(_inventoryunittransactionsService.selectInventoryLocationsNullable().Select( x => new { ixInventoryLocation = x.Key, sInventoryLocation = x.Value }), "ixInventoryLocation", "sInventoryLocation", inventoryunittransactions.ixInventoryLocationBefore);
			ViewBag.ixInventoryStateAfter = new SelectList(_inventoryunittransactionsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState", inventoryunittransactions.ixInventoryStateAfter);
			ViewBag.ixInventoryStateBefore = new SelectList(_inventoryunittransactionsService.selectInventoryStatesNullable().Select( x => new { ixInventoryState = x.Key, sInventoryState = x.Value }), "ixInventoryState", "sInventoryState", inventoryunittransactions.ixInventoryStateBefore);
			ViewBag.ixInventoryUnit = new SelectList(_inventoryunittransactionsService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit", inventoryunittransactions.ixInventoryUnit);
			ViewBag.ixInventoryUnitTransactionContext = new SelectList(_inventoryunittransactionsService.selectInventoryUnitTransactionContexts().Select( x => new { x.ixInventoryUnitTransactionContext, x.sInventoryUnitTransactionContext }), "ixInventoryUnitTransactionContext", "sInventoryUnitTransactionContext", inventoryunittransactions.ixInventoryUnitTransactionContext);
			ViewBag.ixMaterialAfter = new SelectList(_inventoryunittransactionsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", inventoryunittransactions.ixMaterialAfter);
			ViewBag.ixMaterialBefore = new SelectList(_inventoryunittransactionsService.selectMaterialsNullable().Select( x => new { ixMaterial = x.Key, sMaterial = x.Value }), "ixMaterial", "sMaterial", inventoryunittransactions.ixMaterialBefore);
			ViewBag.ixStatusAfter = new SelectList(_inventoryunittransactionsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", inventoryunittransactions.ixStatusAfter);
			ViewBag.ixStatusBefore = new SelectList(_inventoryunittransactionsService.selectStatusesNullable().Select( x => new { ixStatus = x.Key, sStatus = x.Value }), "ixStatus", "sStatus", inventoryunittransactions.ixStatusBefore);

            return View(inventoryunittransactions);
        }

        // POST: InventoryUnitTransactions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInventoryUnitTransaction,sInventoryUnitTransaction,ixInventoryUnit,ixInventoryUnitTransactionContext,ixFacilityBefore,ixFacilityAfter,ixCompanyBefore,ixCompanyAfter,ixMaterialBefore,ixMaterialAfter,ixInventoryStateBefore,ixInventoryStateAfter,ixHandlingUnitBefore,ixHandlingUnitAfter,ixInventoryLocationBefore,ixInventoryLocationAfter,nBaseUnitQuantityBefore,nBaseUnitQuantityAfter,sSerialNumberBefore,sSerialNumberAfter,sBatchNumberBefore,sBatchNumberAfter,dtExpireAtBefore,dtExpireAtAfter,ixStatusBefore,ixStatusAfter")] InventoryUnitTransactionsPost inventoryunittransactions)
        {
            if (ModelState.IsValid)
            {
                inventoryunittransactions.UserName = User.Identity.Name;
                _inventoryunittransactionsService.Edit(inventoryunittransactions);
                return RedirectToAction("Index");
            }
			ViewBag.ixCompanyAfter = new SelectList(_inventoryunittransactionsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", inventoryunittransactions.ixCompanyAfter);
			ViewBag.ixCompanyBefore = new SelectList(_inventoryunittransactionsService.selectCompaniesNullable().Select( x => new { ixCompany = x.Key, sCompany = x.Value }), "ixCompany", "sCompany", inventoryunittransactions.ixCompanyBefore);
			ViewBag.ixFacilityAfter = new SelectList(_inventoryunittransactionsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", inventoryunittransactions.ixFacilityAfter);
			ViewBag.ixFacilityBefore = new SelectList(_inventoryunittransactionsService.selectFacilitiesNullable().Select( x => new { ixFacility = x.Key, sFacility = x.Value }), "ixFacility", "sFacility", inventoryunittransactions.ixFacilityBefore);
			ViewBag.ixHandlingUnitAfter = new SelectList(_inventoryunittransactionsService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", inventoryunittransactions.ixHandlingUnitAfter);
			ViewBag.ixHandlingUnitBefore = new SelectList(_inventoryunittransactionsService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", inventoryunittransactions.ixHandlingUnitBefore);
			ViewBag.ixInventoryLocationAfter = new SelectList(_inventoryunittransactionsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", inventoryunittransactions.ixInventoryLocationAfter);
			ViewBag.ixInventoryLocationBefore = new SelectList(_inventoryunittransactionsService.selectInventoryLocationsNullable().Select( x => new { ixInventoryLocation = x.Key, sInventoryLocation = x.Value }), "ixInventoryLocation", "sInventoryLocation", inventoryunittransactions.ixInventoryLocationBefore);
			ViewBag.ixInventoryStateAfter = new SelectList(_inventoryunittransactionsService.selectInventoryStates().Select( x => new { x.ixInventoryState, x.sInventoryState }), "ixInventoryState", "sInventoryState", inventoryunittransactions.ixInventoryStateAfter);
			ViewBag.ixInventoryStateBefore = new SelectList(_inventoryunittransactionsService.selectInventoryStatesNullable().Select( x => new { ixInventoryState = x.Key, sInventoryState = x.Value }), "ixInventoryState", "sInventoryState", inventoryunittransactions.ixInventoryStateBefore);
			ViewBag.ixInventoryUnit = new SelectList(_inventoryunittransactionsService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit", inventoryunittransactions.ixInventoryUnit);
			ViewBag.ixInventoryUnitTransactionContext = new SelectList(_inventoryunittransactionsService.selectInventoryUnitTransactionContexts().Select( x => new { x.ixInventoryUnitTransactionContext, x.sInventoryUnitTransactionContext }), "ixInventoryUnitTransactionContext", "sInventoryUnitTransactionContext", inventoryunittransactions.ixInventoryUnitTransactionContext);
			ViewBag.ixMaterialAfter = new SelectList(_inventoryunittransactionsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", inventoryunittransactions.ixMaterialAfter);
			ViewBag.ixMaterialBefore = new SelectList(_inventoryunittransactionsService.selectMaterialsNullable().Select( x => new { ixMaterial = x.Key, sMaterial = x.Value }), "ixMaterial", "sMaterial", inventoryunittransactions.ixMaterialBefore);
			ViewBag.ixStatusAfter = new SelectList(_inventoryunittransactionsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", inventoryunittransactions.ixStatusAfter);
			ViewBag.ixStatusBefore = new SelectList(_inventoryunittransactionsService.selectStatusesNullable().Select( x => new { ixStatus = x.Key, sStatus = x.Value }), "ixStatus", "sStatus", inventoryunittransactions.ixStatusBefore);

            return View(inventoryunittransactions);
        }


        // GET: InventoryUnitTransactions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inventoryunittransactionsService.Get(id));
        }

        // POST: InventoryUnitTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InventoryUnitTransactionsPost inventoryunittransactions = _inventoryunittransactionsService.GetPost(id);
            inventoryunittransactions.UserName = User.Identity.Name;
            _inventoryunittransactionsService.Delete(inventoryunittransactions);
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
            string sInventoryUnitTransaction;

            InventoryUnitTransactionsPost inventoryunittransactions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInventoryUnitTransaction = _inventoryunittransactionsService.Get(nID).sInventoryUnitTransaction;
                            if (!_inventoryunittransactionsService.VerifyInventoryUnitTransactionDeleteOK(nID, sInventoryUnitTransaction).Any())
                            {
                                inventoryunittransactions = _inventoryunittransactionsService.GetPost(nID);
                                inventoryunittransactions.UserName = User.Identity.Name;
                                _inventoryunittransactionsService.Delete(inventoryunittransactions);
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
        public IActionResult VerifyInventoryUnitTransaction(long ixInventoryUnitTransaction, string sInventoryUnitTransaction)
        {
            string validationResponse = "";

            if (!_inventoryunittransactionsService.VerifyInventoryUnitTransactionUnique(ixInventoryUnitTransaction, sInventoryUnitTransaction))
            {
                validationResponse = $"InventoryUnitTransaction {sInventoryUnitTransaction} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

