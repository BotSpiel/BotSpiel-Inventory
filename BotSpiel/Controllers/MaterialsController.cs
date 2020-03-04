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

    public class MaterialsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMaterialsService _materialsService;

        //Custom Code Start | Added Code Block
        private readonly CommonlyUsedSelects _commonlyUsedSelects;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public MaterialsController(IMaterialsService materialsService )
        //Replaced Code Block End
        public MaterialsController(IMaterialsService materialsService, CommonlyUsedSelects commonlyUsedSelects)
        {
            //Custom Code End
            _materialsService = materialsService;
            //Custom Code Start | Added Code Block
            _commonlyUsedSelects = commonlyUsedSelects;
            //Custom Code End
        }

        // GET: Materials
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var materials = _materialsService.Index();
            return View(materials.ToList());
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
            var materials = _materialsService.Index();
            return PartialView("IndexGrid", materials.ToList());
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
                IGrid<Materials> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Materials> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMaterials.xlsx");
            }
        }

        private IGrid<Materials> CreateExportableGrid()
        {
            IGrid<Materials> grid = new Grid<Materials>(_materialsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMaterial).Titled("Material").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDescription).Titled("Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.MaterialTypes.sMaterialType).Titled("Material Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.UnitsOfMeasurementFKDiffBaseUnit.sUnitOfMeasurement).Titled("Base Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bTrackSerialNumber).Titled("Track Serial Number").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bTrackBatchNumber).Titled("Track Batch Number").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bTrackExpiry).Titled("Track Expiry").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nDensity).Titled("Density").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nShelflife).Titled("Shelflife").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nLength).Titled("Length").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nWidth).Titled("Width").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nHeight).Titled("Height").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nWeight).Titled("Weight").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Materials>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Materials/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_materialsService.Get(id));
        }

        // GET: Materials/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixBaseUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixDensityUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixHeightUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixMaterialType = new SelectList(_materialsService.selectMaterialTypes().Select(x => new { x.ixMaterialType, x.sMaterialType }), "ixMaterialType", "sMaterialType");
            //ViewBag.ixShelflifeUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixWeightUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixWidthUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixBaseUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementQuantity().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixDensityUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementDensity().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixMaterialType = new SelectList(_materialsService.selectMaterialTypes().Select(x => new { x.ixMaterialType, x.sMaterialType }), "ixMaterialType", "sMaterialType");
            ViewBag.ixShelflifeUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementTime().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixWeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementWeight().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End



            return View();
        }

        // POST: Materials/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMaterial,sMaterial,sDescription,ixMaterialType,ixBaseUnit,bTrackSerialNumber,bTrackBatchNumber,bTrackExpiry,nDensity,ixDensityUnit,nShelflife,ixShelflifeUnit,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit,nWeight,ixWeightUnit")] MaterialsPost materials)
        {
            if (ModelState.IsValid)
            {
                materials.UserName = User.Identity.Name;
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //_materialsService.Create(materials);
                //return RedirectToAction("Index");
                //Replaced Code Block End
                var ixMaterial = _materialsService.Create(materials).Result;
                return RedirectToAction("Edit", "Materials", new { id = ixMaterial });
                //Custom Code End
            }
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixBaseUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixDensityUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixHeightUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixLengthUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixMaterialType = new SelectList(_materialsService.selectMaterialTypes().Select(x => new { x.ixMaterialType, x.sMaterialType }), "ixMaterialType", "sMaterialType");
            //ViewBag.ixShelflifeUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixWeightUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //ViewBag.ixWidthUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Replaced Code Block End
            ViewBag.ixBaseUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementQuantity().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixDensityUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementDensity().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixMaterialType = new SelectList(_materialsService.selectMaterialTypes().Select(x => new { x.ixMaterialType, x.sMaterialType }), "ixMaterialType", "sMaterialType");
            ViewBag.ixShelflifeUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementTime().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixWeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementWeight().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
            //Custom Code End
            return View(materials);
        }

        // GET: Materials/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MaterialsPost materials = _materialsService.GetPost(id);
            if (materials == null)
            {
                return NotFound();
            }

            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixBaseUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixBaseUnit);
            //ViewBag.ixDensityUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixDensityUnit);
            //ViewBag.ixHeightUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixHeightUnit);
            //ViewBag.ixLengthUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixLengthUnit);
            //ViewBag.ixMaterialType = new SelectList(_materialsService.selectMaterialTypes().Select(x => new { x.ixMaterialType, x.sMaterialType }), "ixMaterialType", "sMaterialType", materials.ixMaterialType);
            //ViewBag.ixShelflifeUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixShelflifeUnit);
            //ViewBag.ixWeightUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixWeightUnit);
            //ViewBag.ixWidthUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixWidthUnit);
            //Replaced Code Block End
            ViewBag.ixBaseUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementQuantity().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixBaseUnit);
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixHeightUnit);
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixLengthUnit);
            ViewBag.ixDensityUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementDensity().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixDensityUnit);
            ViewBag.ixMaterialType = new SelectList(_materialsService.selectMaterialTypes().Select(x => new { x.ixMaterialType, x.sMaterialType }), "ixMaterialType", "sMaterialType", materials.ixMaterialType);
            ViewBag.ixShelflifeUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementTime().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixShelflifeUnit);
            ViewBag.ixWeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementWeight().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixWeightUnit);
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixWidthUnit);
            //Custom Code End



            return View(materials);
        }

        // POST: Materials/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMaterial,sMaterial,sDescription,ixMaterialType,ixBaseUnit,bTrackSerialNumber,bTrackBatchNumber,bTrackExpiry,nDensity,ixDensityUnit,nShelflife,ixShelflifeUnit,nLength,ixLengthUnit,nWidth,ixWidthUnit,nHeight,ixHeightUnit,nWeight,ixWeightUnit")] MaterialsPost materials)
        {
            if (ModelState.IsValid)
            {
                materials.UserName = User.Identity.Name;
                _materialsService.Edit(materials);
                return RedirectToAction("Index");
            }
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixBaseUnit = new SelectList(_materialsService.selectUnitsOfMeasurement().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixBaseUnit);
            //ViewBag.ixDensityUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixDensityUnit);
            //ViewBag.ixHeightUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixHeightUnit);
            //ViewBag.ixLengthUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixLengthUnit);
            //ViewBag.ixMaterialType = new SelectList(_materialsService.selectMaterialTypes().Select(x => new { x.ixMaterialType, x.sMaterialType }), "ixMaterialType", "sMaterialType", materials.ixMaterialType);
            //ViewBag.ixShelflifeUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixShelflifeUnit);
            //ViewBag.ixWeightUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixWeightUnit);
            //ViewBag.ixWidthUnit = new SelectList(_materialsService.selectUnitsOfMeasurementNullable().Select(x => new { ixUnitOfMeasurement = x.Key, sUnitOfMeasurement = x.Value }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixWidthUnit);
            //Replaced Code Block End
            ViewBag.ixBaseUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementQuantity().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixBaseUnit);
            ViewBag.ixHeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixHeightUnit);
            ViewBag.ixLengthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixLengthUnit);
            ViewBag.ixDensityUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementDensity().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixDensityUnit);
            ViewBag.ixMaterialType = new SelectList(_materialsService.selectMaterialTypes().Select(x => new { x.ixMaterialType, x.sMaterialType }), "ixMaterialType", "sMaterialType", materials.ixMaterialType);
            ViewBag.ixShelflifeUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementTime().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixShelflifeUnit);
            ViewBag.ixWeightUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementWeight().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixWeightUnit);
            ViewBag.ixWidthUnit = new SelectList(_commonlyUsedSelects.selectUnitsOfMeasurementLength().Select(x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", materials.ixWidthUnit);
            //Custom Code End
            return View(materials);
        }


        // GET: Materials/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_materialsService.Get(id));
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MaterialsPost materials = _materialsService.GetPost(id);
            materials.UserName = User.Identity.Name;
            _materialsService.Delete(materials);
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
            string sMaterial;

            MaterialsPost materials;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMaterial = _materialsService.Get(nID).sMaterial;
                            if (!_materialsService.VerifyMaterialDeleteOK(nID, sMaterial).Any())
                            {
                                materials = _materialsService.GetPost(nID);
                                materials.UserName = User.Identity.Name;
                                _materialsService.Delete(materials);
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
        public IActionResult VerifyMaterial(long ixMaterial, string sMaterial)
        {
            string validationResponse = "";

            if (!_materialsService.VerifyMaterialUnique(ixMaterial, sMaterial))
            {
                validationResponse = $"Material {sMaterial} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

