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

    public class MeasurementUnitsOfController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMeasurementUnitsOfService _measurementunitsofService;

        public MeasurementUnitsOfController(IMeasurementUnitsOfService measurementunitsofService )
        {
            _measurementunitsofService = measurementunitsofService;
        }

        // GET: MeasurementUnitsOf
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var measurementunitsof = _measurementunitsofService.Index();
            return View(measurementunitsof.ToList());
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
            var measurementunitsof = _measurementunitsofService.Index();
            return PartialView("IndexGrid", measurementunitsof.ToList());
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
                IGrid<MeasurementUnitsOf> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MeasurementUnitsOf> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMeasurementUnitsOf.xlsx");
            }
        }

        private IGrid<MeasurementUnitsOf> CreateExportableGrid()
        {
            IGrid<MeasurementUnitsOf> grid = new Grid<MeasurementUnitsOf>(_measurementunitsofService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMeasurementUnitOf).Titled("Measurement Unit Of").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MeasurementUnitsOf>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MeasurementUnitsOf/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_measurementunitsofService.Get(id));
        }

        // GET: MeasurementUnitsOf/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MeasurementUnitsOf/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMeasurementUnitOf,sMeasurementUnitOf")] MeasurementUnitsOfPost measurementunitsof)
        {
            if (ModelState.IsValid)
            {
                measurementunitsof.UserName = User.Identity.Name;
                _measurementunitsofService.Create(measurementunitsof);
                return RedirectToAction("Index");
            }

            return View(measurementunitsof);
        }

        // GET: MeasurementUnitsOf/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MeasurementUnitsOfPost measurementunitsof = _measurementunitsofService.GetPost(id);
            if (measurementunitsof == null)
            {
                return NotFound();
            }

            return View(measurementunitsof);
        }

        // POST: MeasurementUnitsOf/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMeasurementUnitOf,sMeasurementUnitOf")] MeasurementUnitsOfPost measurementunitsof)
        {
            if (ModelState.IsValid)
            {
                measurementunitsof.UserName = User.Identity.Name;
                _measurementunitsofService.Edit(measurementunitsof);
                return RedirectToAction("Index");
            }

            return View(measurementunitsof);
        }


        // GET: MeasurementUnitsOf/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_measurementunitsofService.Get(id));
        }

        // POST: MeasurementUnitsOf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MeasurementUnitsOfPost measurementunitsof = _measurementunitsofService.GetPost(id);
            measurementunitsof.UserName = User.Identity.Name;
            _measurementunitsofService.Delete(measurementunitsof);
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
            string sMeasurementUnitOf;

            MeasurementUnitsOfPost measurementunitsof;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMeasurementUnitOf = _measurementunitsofService.Get(nID).sMeasurementUnitOf;
                            if (!_measurementunitsofService.VerifyMeasurementUnitOfDeleteOK(nID, sMeasurementUnitOf).Any())
                            {
                                measurementunitsof = _measurementunitsofService.GetPost(nID);
                                measurementunitsof.UserName = User.Identity.Name;
                                _measurementunitsofService.Delete(measurementunitsof);
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
        public IActionResult VerifyMeasurementUnitOf(long ixMeasurementUnitOf, string sMeasurementUnitOf)
        {
            string validationResponse = "";

            if (!_measurementunitsofService.VerifyMeasurementUnitOfUnique(ixMeasurementUnitOf, sMeasurementUnitOf))
            {
                validationResponse = $"MeasurementUnitOf {sMeasurementUnitOf} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

