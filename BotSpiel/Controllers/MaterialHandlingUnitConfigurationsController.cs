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

    public class MaterialHandlingUnitConfigurationsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMaterialHandlingUnitConfigurationsService _materialhandlingunitconfigurationsService;
        //Custom Code Start | Added Code Block
        private readonly CommonlyUsedSelects _commonlyUsedSelects;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public MaterialHandlingUnitConfigurationsController(IMaterialHandlingUnitConfigurationsService materialhandlingunitconfigurationsService )
        //Replaced Code Block End
        public MaterialHandlingUnitConfigurationsController(IMaterialHandlingUnitConfigurationsService materialhandlingunitconfigurationsService, CommonlyUsedSelects commonlyUsedSelects)
        //Custom Code End            
        {
            _materialhandlingunitconfigurationsService = materialhandlingunitconfigurationsService;
            //Custom Code Start | Added Code Block
            _commonlyUsedSelects = commonlyUsedSelects;
            //Custom Code End
        }

        // GET: MaterialHandlingUnitConfigurations
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var materialhandlingunitconfigurations = _materialhandlingunitconfigurationsService.Index();
            return View(materialhandlingunitconfigurations.ToList());
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
            var materialhandlingunitconfigurations = _materialhandlingunitconfigurationsService.Index();
            return PartialView("IndexGrid", materialhandlingunitconfigurations.ToList());
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
                IGrid<MaterialHandlingUnitConfigurations> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MaterialHandlingUnitConfigurations> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMaterialHandlingUnitConfigurations.xlsx");
            }
        }

        private IGrid<MaterialHandlingUnitConfigurations> CreateExportableGrid()
        {
            IGrid<MaterialHandlingUnitConfigurations> grid = new Grid<MaterialHandlingUnitConfigurations>(_materialhandlingunitconfigurationsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMaterialHandlingUnitConfiguration).Titled("Material Handling Unit Configuration").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Materials.sMaterial).Titled("Material").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nNestingLevel).Titled("Nesting Level").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.HandlingUnitTypes.sHandlingUnitType).Titled("Handling Unit Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nQuantity).Titled("Quantity").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nLength).Titled("Length").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nWidth).Titled("Width").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nHeight).Titled("Height").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MaterialHandlingUnitConfigurations>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MaterialHandlingUnitConfigurations/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_materialhandlingunitconfigurationsService.Get(id));
        }

        // GET: MaterialHandlingUnitConfigurations/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnitType = new SelectList(_materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            //ViewBag.ixWidthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End
            return View();
        }

        // POST: MaterialHandlingUnitConfigurations/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMaterialHandlingUnitConfiguration,sMaterialHandlingUnitConfiguration,ixMaterial,nNestingLevel,ixHandlingUnitType,nQuantity,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit")] MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurations)
        {
            if (ModelState.IsValid)
            {
                materialhandlingunitconfigurations.UserName = User.Identity.Name;
                _materialhandlingunitconfigurationsService.Create(materialhandlingunitconfigurations);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnitType = new SelectList(_materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            //ViewBag.ixWidthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End

            return View(materialhandlingunitconfigurations);
        }

        //Custom Code Start | Added Code Block 
        [Authorize]
        [HttpGet]
        public ActionResult CreateWithID(long id)
        {
            ViewBag.ixHandlingUnitType = new SelectList(_materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Select(x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            //ViewBag.ixWidthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Where(x => x.ixMaterial == id).Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End
            return View();
        }

        // POST: MaterialHandlingUnitConfigurations/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithID([Bind("ixMaterialHandlingUnitConfiguration,sMaterialHandlingUnitConfiguration,ixMaterial,nNestingLevel,ixHandlingUnitType,nQuantity,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit")] MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurations)
        {
            if (ModelState.IsValid)
            {
                materialhandlingunitconfigurations.UserName = User.Identity.Name;
                _materialhandlingunitconfigurationsService.Create(materialhandlingunitconfigurations);
                return RedirectToAction("Edit", "Materials", new { id = materialhandlingunitconfigurations.ixMaterial });
            }
            ViewBag.ixHandlingUnitType = new SelectList(_materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Select(x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            //ViewBag.ixWidthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Where(x => x.ixMaterial == materialhandlingunitconfigurations.ixMaterial).Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End

            return View(materialhandlingunitconfigurations);
        }

        //Custom Code End




        // GET: MaterialHandlingUnitConfigurations/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurations = _materialhandlingunitconfigurationsService.GetPost(id);
            if (materialhandlingunitconfigurations == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnitType = new SelectList(_materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType", materialhandlingunitconfigurations.ixHandlingUnitType);
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixHeightUnit);
			//ViewBag.ixLengthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixLengthUnit);
			//ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", materialhandlingunitconfigurations.ixMaterial);
			//ViewBag.ixWidthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixWidthUnit);
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixHeightUnit);
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixLengthUnit);
            ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", materialhandlingunitconfigurations.ixMaterial);
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixWidthUnit);
            //Custom Code End

            return View(materialhandlingunitconfigurations);
        }

        // POST: MaterialHandlingUnitConfigurations/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMaterialHandlingUnitConfiguration,sMaterialHandlingUnitConfiguration,ixMaterial,nNestingLevel,ixHandlingUnitType,nQuantity,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit")] MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurations)
        {
            if (ModelState.IsValid)
            {
                materialhandlingunitconfigurations.UserName = User.Identity.Name;
                _materialhandlingunitconfigurationsService.Edit(materialhandlingunitconfigurations);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnitType = new SelectList(_materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType", materialhandlingunitconfigurations.ixHandlingUnitType);
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixHeightUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixHeightUnit);
            //ViewBag.ixLengthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixLengthUnit);
            //ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", materialhandlingunitconfigurations.ixMaterial);
            //ViewBag.ixWidthUnit = new SelectList(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurementNullable().Select( x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixWidthUnit);
            //Replaced Code Block End
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixHeightUnit);
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixLengthUnit);
            ViewBag.ixMaterial = new SelectList(_materialhandlingunitconfigurationsService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", materialhandlingunitconfigurations.ixMaterial);
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materialhandlingunitconfigurations.ixWidthUnit);
            //Custom Code End

            return View(materialhandlingunitconfigurations);
        }


        // GET: MaterialHandlingUnitConfigurations/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_materialhandlingunitconfigurationsService.Get(id));
        }

        // POST: MaterialHandlingUnitConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurations = _materialhandlingunitconfigurationsService.GetPost(id);
            materialhandlingunitconfigurations.UserName = User.Identity.Name;
            _materialhandlingunitconfigurationsService.Delete(materialhandlingunitconfigurations);
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
            string sMaterialHandlingUnitConfiguration;

            MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurations;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMaterialHandlingUnitConfiguration = _materialhandlingunitconfigurationsService.Get(nID).sMaterialHandlingUnitConfiguration;
                            if (!_materialhandlingunitconfigurationsService.VerifyMaterialHandlingUnitConfigurationDeleteOK(nID, sMaterialHandlingUnitConfiguration).Any())
                            {
                                materialhandlingunitconfigurations = _materialhandlingunitconfigurationsService.GetPost(nID);
                                materialhandlingunitconfigurations.UserName = User.Identity.Name;
                                _materialhandlingunitconfigurationsService.Delete(materialhandlingunitconfigurations);
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
        public IActionResult VerifyMaterialHandlingUnitConfiguration(long ixMaterialHandlingUnitConfiguration, string sMaterialHandlingUnitConfiguration)
        {
            string validationResponse = "";

            if (!_materialhandlingunitconfigurationsService.VerifyMaterialHandlingUnitConfigurationUnique(ixMaterialHandlingUnitConfiguration, sMaterialHandlingUnitConfiguration))
            {
                validationResponse = $"MaterialHandlingUnitConfiguration {sMaterialHandlingUnitConfiguration} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 
