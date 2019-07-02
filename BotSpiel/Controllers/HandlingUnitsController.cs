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

    public class HandlingUnitsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IHandlingUnitsService _handlingunitsService;
        //Custom Code Start | Added Code Block
        private readonly CommonlyUsedSelects _commonlyUsedSelects;
        //Custom Code End
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public HandlingUnitsController(IHandlingUnitsService handlingunitsService )
        //Replaced Code Block End
        public HandlingUnitsController(IHandlingUnitsService handlingunitsService, CommonlyUsedSelects commonlyUsedSelects)
        {
            //Custom Code End
            _handlingunitsService = handlingunitsService;
            //Custom Code Start | Added Code Block
            _commonlyUsedSelects = commonlyUsedSelects;
            //Custom Code End
        }

        // GET: HandlingUnits
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var handlingunits = _handlingunitsService.Index();
            return View(handlingunits.ToList());
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
            var handlingunits = _handlingunitsService.Index();
            return PartialView("IndexGrid", handlingunits.ToList());
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
                IGrid<HandlingUnits> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<HandlingUnits> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportHandlingUnits.xlsx");
            }
        }

        private IGrid<HandlingUnits> CreateExportableGrid()
        {
            IGrid<HandlingUnits> grid = new Grid<HandlingUnits>(_handlingunitsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sHandlingUnit).Titled("Handling Unit").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.HandlingUnitTypes.sHandlingUnitType).Titled("Handling Unit Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nLength).Titled("Length").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nWidth).Titled("Width").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nHeight).Titled("Height").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nWeight).Titled("Weight").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<HandlingUnits>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: HandlingUnits/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_handlingunitsService.Get(id));
        }

        // GET: HandlingUnits/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnitType = new SelectList(_handlingunitsService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_handlingunitsService.selectMaterialHandlingUnitConfigurations().Select( x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
            //ViewBag.ixPackingMaterial = new SelectList(_handlingunitsService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            //ViewBag.ixParentHandlingUnit = new SelectList(_handlingunitsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
            //ViewBag.ixStatus = new SelectList(_handlingunitsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");
            //ViewBag.ixWeightUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixWidthUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_handlingunitsService.selectMaterialHandlingUnitConfigurations().Select(x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
            ViewBag.ixPackingMaterial = new SelectList(_handlingunitsService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            ViewBag.ixParentHandlingUnit = new SelectList(_handlingunitsService.selectHandlingUnits().Select(x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
            ViewBag.ixStatus = new SelectList(_handlingunitsService.selectStatuses().Select(x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");
            ViewBag.ixWeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementWeight().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End
            return View();
        }

        // POST: HandlingUnits/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixHandlingUnit,sHandlingUnit,ixHandlingUnitType,ixParentHandlingUnit,ixPackingMaterial,ixMaterialHandlingUnitConfiguration,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit,nWeight,ixWeightUnit,ixStatus")] HandlingUnitsPost handlingunits)
        {
            if (ModelState.IsValid)
            {
                handlingunits.UserName = User.Identity.Name;
                _handlingunitsService.Create(handlingunits);
                return RedirectToAction("Index");
            }
            ViewBag.ixHandlingUnitType = new SelectList(_handlingunitsService.selectHandlingUnitTypes().Select(x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_handlingunitsService.selectMaterialHandlingUnitConfigurations().Select( x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
            //ViewBag.ixPackingMaterial = new SelectList(_handlingunitsService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            //ViewBag.ixParentHandlingUnit = new SelectList(_handlingunitsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
            //ViewBag.ixStatus = new SelectList(_handlingunitsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");
            //ViewBag.ixWeightUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixWidthUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_handlingunitsService.selectMaterialHandlingUnitConfigurations().Select(x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
            ViewBag.ixPackingMaterial = new SelectList(_handlingunitsService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            ViewBag.ixParentHandlingUnit = new SelectList(_handlingunitsService.selectHandlingUnits().Select(x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
            ViewBag.ixStatus = new SelectList(_handlingunitsService.selectStatuses().Select(x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");
            ViewBag.ixWeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementWeight().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End
            return View(handlingunits);
        }

        // GET: HandlingUnits/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            HandlingUnitsPost handlingunits = _handlingunitsService.GetPost(id);
            if (handlingunits == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnitType = new SelectList(_handlingunitsService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType", handlingunits.ixHandlingUnitType);
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixHeightUnit);
            //ViewBag.ixLengthUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixLengthUnit);
            //ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_handlingunitsService.selectMaterialHandlingUnitConfigurationsNullable().Select( x => new { ixMaterialHandlingUnitConfiguration = x.Key, sMaterialHandlingUnitConfiguration = x.Value }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration", handlingunits.ixMaterialHandlingUnitConfiguration);
            //ViewBag.ixPackingMaterial = new SelectList(_handlingunitsService.selectMaterialsNullable().Select(x => new { ixMaterial = x.Key, sMaterial = x.Value }), "ixMaterial", "sMaterial", handlingunits.ixPackingMaterial);
            //ViewBag.ixParentHandlingUnit = new SelectList(_handlingunitsService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", handlingunits.ixParentHandlingUnit);
            //ViewBag.ixStatus = new SelectList(_handlingunitsService.selectStatusesNullable().Select( x => new { ixStatus = x.Key, sStatus = x.Value }), "ixStatus", "sStatus", handlingunits.ixStatus);
            //ViewBag.ixWeightUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixWeightUnit);
            //ViewBag.ixWidthUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixWidthUnit);
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixHeightUnit);
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixLengthUnit);
            ViewBag.ixPackingMaterial = new SelectList(_handlingunitsService.selectMaterialsNullable().Select(x => new { ixMaterial = x.Key, sMaterial = x.Value }), "ixMaterial", "sMaterial", handlingunits.ixPackingMaterial);
            ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_handlingunitsService.selectMaterialHandlingUnitConfigurationsNullable().Select(x => new { ixMaterialHandlingUnitConfiguration = x.Key, sMaterialHandlingUnitConfiguration = x.Value }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration", handlingunits.ixMaterialHandlingUnitConfiguration);
            ViewBag.ixParentHandlingUnit = new SelectList(_handlingunitsService.selectHandlingUnitsNullable().Select(x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", handlingunits.ixParentHandlingUnit);
            ViewBag.ixStatus = new SelectList(_handlingunitsService.selectStatusesNullable().Select(x => new { ixStatus = x.Key, sStatus = x.Value }), "ixStatus", "sStatus", handlingunits.ixStatus);
            ViewBag.ixWeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementWeight().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixWeightUnit);
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixWidthUnit);
            //Custom Code End
            return View(handlingunits);
        }

        // POST: HandlingUnits/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixHandlingUnit,sHandlingUnit,ixHandlingUnitType,ixParentHandlingUnit,ixPackingMaterial,ixMaterialHandlingUnitConfiguration,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit,nWeight,ixWeightUnit,ixStatus")] HandlingUnitsPost handlingunits)
        {
            if (ModelState.IsValid)
            {
                handlingunits.UserName = User.Identity.Name;
                _handlingunitsService.Edit(handlingunits);
                return RedirectToAction("Index");
            }
            ViewBag.ixHandlingUnitType = new SelectList(_handlingunitsService.selectHandlingUnitTypes().Select(x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType", handlingunits.ixHandlingUnitType);
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixHeightUnit);
            //ViewBag.ixLengthUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixLengthUnit);
            //ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_handlingunitsService.selectMaterialHandlingUnitConfigurationsNullable().Select( x => new { ixMaterialHandlingUnitConfiguration = x.Key, sMaterialHandlingUnitConfiguration = x.Value }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration", handlingunits.ixMaterialHandlingUnitConfiguration);
            //ViewBag.ixPackingMaterial = new SelectList(_handlingunitsService.selectMaterialsNullable().Select(x => new { ixMaterial = x.Key, sMaterial = x.Value }), "ixMaterial", "sMaterial", handlingunits.ixPackingMaterial);
            //ViewBag.ixParentHandlingUnit = new SelectList(_handlingunitsService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", handlingunits.ixParentHandlingUnit);
            //ViewBag.ixStatus = new SelectList(_handlingunitsService.selectStatusesNullable().Select( x => new { ixStatus = x.Key, sStatus = x.Value }), "ixStatus", "sStatus", handlingunits.ixStatus);
            //ViewBag.ixWeightUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixWeightUnit);
            //ViewBag.ixWidthUnit = new SelectList(_handlingunitsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixWidthUnit);
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixHeightUnit);
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixLengthUnit);
            ViewBag.ixPackingMaterial = new SelectList(_handlingunitsService.selectMaterialsNullable().Select(x => new { ixMaterial = x.Key, sMaterial = x.Value }), "ixMaterial", "sMaterial", handlingunits.ixPackingMaterial);
            ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_handlingunitsService.selectMaterialHandlingUnitConfigurationsNullable().Select(x => new { ixMaterialHandlingUnitConfiguration = x.Key, sMaterialHandlingUnitConfiguration = x.Value }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration", handlingunits.ixMaterialHandlingUnitConfiguration);
            ViewBag.ixParentHandlingUnit = new SelectList(_handlingunitsService.selectHandlingUnitsNullable().Select(x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", handlingunits.ixParentHandlingUnit);
            ViewBag.ixStatus = new SelectList(_handlingunitsService.selectStatusesNullable().Select(x => new { ixStatus = x.Key, sStatus = x.Value }), "ixStatus", "sStatus", handlingunits.ixStatus);
            ViewBag.ixWeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementWeight().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixWeightUnit);
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", handlingunits.ixWidthUnit);
            //Custom Code End
            return View(handlingunits);
        }


        // GET: HandlingUnits/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_handlingunitsService.Get(id));
        }

        // POST: HandlingUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HandlingUnitsPost handlingunits = _handlingunitsService.GetPost(id);
            handlingunits.UserName = User.Identity.Name;
            _handlingunitsService.Delete(handlingunits);
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
            string sHandlingUnit;

            HandlingUnitsPost handlingunits;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sHandlingUnit = _handlingunitsService.Get(nID).sHandlingUnit;
                            if (!_handlingunitsService.VerifyHandlingUnitDeleteOK(nID, sHandlingUnit).Any())
                            {
                                handlingunits = _handlingunitsService.GetPost(nID);
                                handlingunits.UserName = User.Identity.Name;
                                _handlingunitsService.Delete(handlingunits);
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
        public IActionResult VerifyHandlingUnit(long ixHandlingUnit, string sHandlingUnit)
        {
            string validationResponse = "";

            if (!_handlingunitsService.VerifyHandlingUnitUnique(ixHandlingUnit, sHandlingUnit))
            {
                validationResponse = $"HandlingUnit {sHandlingUnit} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

