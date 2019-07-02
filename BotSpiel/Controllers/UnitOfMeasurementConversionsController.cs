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

    public class UnitOfMeasurementConversionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IUnitOfMeasurementConversionsService _unitofmeasurementconversionsService;

        public UnitOfMeasurementConversionsController(IUnitOfMeasurementConversionsService unitofmeasurementconversionsService )
        {
            _unitofmeasurementconversionsService = unitofmeasurementconversionsService;
        }

        // GET: UnitOfMeasurementConversions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var unitofmeasurementconversions = _unitofmeasurementconversionsService.Index();
            return View(unitofmeasurementconversions.ToList());
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
            var unitofmeasurementconversions = _unitofmeasurementconversionsService.Index();
            return PartialView("IndexGrid", unitofmeasurementconversions.ToList());
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
                IGrid<UnitOfMeasurementConversions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<UnitOfMeasurementConversions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportUnitOfMeasurementConversions.xlsx");
            }
        }

        private IGrid<UnitOfMeasurementConversions> CreateExportableGrid()
        {
            IGrid<UnitOfMeasurementConversions> grid = new Grid<UnitOfMeasurementConversions>(_unitofmeasurementconversionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sUnitOfMeasurementConversion).Titled("Unit Of Measurement Conversion").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.UnitsOfMeasurementFKDiffUnitOfMeasurementFrom.sUnitOfMeasurement).Titled("Unit Of Measurement From").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.UnitsOfMeasurementFKDiffUnitOfMeasurementTo.sUnitOfMeasurement).Titled("Unit Of Measurement To").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nMultiplier).Titled("Multiplier").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<UnitOfMeasurementConversions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: UnitOfMeasurementConversions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_unitofmeasurementconversionsService.Get(id));
        }

        // GET: UnitOfMeasurementConversions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixUnitOfMeasurementFrom = new SelectList(_unitofmeasurementconversionsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixUnitOfMeasurementTo = new SelectList(_unitofmeasurementconversionsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");

            return View();
        }

        // POST: UnitOfMeasurementConversions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixUnitOfMeasurementConversion,sUnitOfMeasurementConversion,ixUnitOfMeasurementFrom,ixUnitOfMeasurementTo,nMultiplier")] UnitOfMeasurementConversionsPost unitofmeasurementconversions)
        {
            if (ModelState.IsValid)
            {
                unitofmeasurementconversions.UserName = User.Identity.Name;
                _unitofmeasurementconversionsService.Create(unitofmeasurementconversions);
                return RedirectToAction("Index");
            }
			ViewBag.ixUnitOfMeasurementFrom = new SelectList(_unitofmeasurementconversionsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");
			ViewBag.ixUnitOfMeasurementTo = new SelectList(_unitofmeasurementconversionsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement");

            return View(unitofmeasurementconversions);
        }

        // GET: UnitOfMeasurementConversions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            UnitOfMeasurementConversionsPost unitofmeasurementconversions = _unitofmeasurementconversionsService.GetPost(id);
            if (unitofmeasurementconversions == null)
            {
                return NotFound();
            }
			ViewBag.ixUnitOfMeasurementFrom = new SelectList(_unitofmeasurementconversionsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", unitofmeasurementconversions.ixUnitOfMeasurementFrom);
			ViewBag.ixUnitOfMeasurementTo = new SelectList(_unitofmeasurementconversionsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", unitofmeasurementconversions.ixUnitOfMeasurementTo);

            return View(unitofmeasurementconversions);
        }

        // POST: UnitOfMeasurementConversions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixUnitOfMeasurementConversion,sUnitOfMeasurementConversion,ixUnitOfMeasurementFrom,ixUnitOfMeasurementTo,nMultiplier")] UnitOfMeasurementConversionsPost unitofmeasurementconversions)
        {
            if (ModelState.IsValid)
            {
                unitofmeasurementconversions.UserName = User.Identity.Name;
                _unitofmeasurementconversionsService.Edit(unitofmeasurementconversions);
                return RedirectToAction("Index");
            }
			ViewBag.ixUnitOfMeasurementFrom = new SelectList(_unitofmeasurementconversionsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", unitofmeasurementconversions.ixUnitOfMeasurementFrom);
			ViewBag.ixUnitOfMeasurementTo = new SelectList(_unitofmeasurementconversionsService.selectUnitsOfMeasurement().Select( x => new { x.ixUnitOfMeasurement, x.sUnitOfMeasurement }), "ixUnitOfMeasurement", "sUnitOfMeasurement", unitofmeasurementconversions.ixUnitOfMeasurementTo);

            return View(unitofmeasurementconversions);
        }


        // GET: UnitOfMeasurementConversions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_unitofmeasurementconversionsService.Get(id));
        }

        // POST: UnitOfMeasurementConversions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            UnitOfMeasurementConversionsPost unitofmeasurementconversions = _unitofmeasurementconversionsService.GetPost(id);
            unitofmeasurementconversions.UserName = User.Identity.Name;
            _unitofmeasurementconversionsService.Delete(unitofmeasurementconversions);
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
            string sUnitOfMeasurementConversion;

            UnitOfMeasurementConversionsPost unitofmeasurementconversions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sUnitOfMeasurementConversion = _unitofmeasurementconversionsService.Get(nID).sUnitOfMeasurementConversion;
                            if (!_unitofmeasurementconversionsService.VerifyUnitOfMeasurementConversionDeleteOK(nID, sUnitOfMeasurementConversion).Any())
                            {
                                unitofmeasurementconversions = _unitofmeasurementconversionsService.GetPost(nID);
                                unitofmeasurementconversions.UserName = User.Identity.Name;
                                _unitofmeasurementconversionsService.Delete(unitofmeasurementconversions);
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
        public IActionResult VerifyUnitOfMeasurementConversion(long ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion)
        {
            string validationResponse = "";

            if (!_unitofmeasurementconversionsService.VerifyUnitOfMeasurementConversionUnique(ixUnitOfMeasurementConversion, sUnitOfMeasurementConversion))
            {
                validationResponse = $"UnitOfMeasurementConversion {sUnitOfMeasurementConversion} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

