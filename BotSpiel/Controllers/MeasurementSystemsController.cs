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

    public class MeasurementSystemsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMeasurementSystemsService _measurementsystemsService;

        public MeasurementSystemsController(IMeasurementSystemsService measurementsystemsService )
        {
            _measurementsystemsService = measurementsystemsService;
        }

        // GET: MeasurementSystems
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var measurementsystems = _measurementsystemsService.Index();
            return View(measurementsystems.ToList());
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
            var measurementsystems = _measurementsystemsService.Index();
            return PartialView("IndexGrid", measurementsystems.ToList());
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
                IGrid<MeasurementSystems> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MeasurementSystems> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMeasurementSystems.xlsx");
            }
        }

        private IGrid<MeasurementSystems> CreateExportableGrid()
        {
            IGrid<MeasurementSystems> grid = new Grid<MeasurementSystems>(_measurementsystemsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMeasurementSystem).Titled("Measurement System").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MeasurementSystems>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MeasurementSystems/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_measurementsystemsService.Get(id));
        }

        // GET: MeasurementSystems/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MeasurementSystems/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMeasurementSystem,sMeasurementSystem")] MeasurementSystemsPost measurementsystems)
        {
            if (ModelState.IsValid)
            {
                measurementsystems.UserName = User.Identity.Name;
                _measurementsystemsService.Create(measurementsystems);
                return RedirectToAction("Index");
            }

            return View(measurementsystems);
        }

        // GET: MeasurementSystems/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MeasurementSystemsPost measurementsystems = _measurementsystemsService.GetPost(id);
            if (measurementsystems == null)
            {
                return NotFound();
            }

            return View(measurementsystems);
        }

        // POST: MeasurementSystems/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMeasurementSystem,sMeasurementSystem")] MeasurementSystemsPost measurementsystems)
        {
            if (ModelState.IsValid)
            {
                measurementsystems.UserName = User.Identity.Name;
                _measurementsystemsService.Edit(measurementsystems);
                return RedirectToAction("Index");
            }

            return View(measurementsystems);
        }


        // GET: MeasurementSystems/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_measurementsystemsService.Get(id));
        }

        // POST: MeasurementSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MeasurementSystemsPost measurementsystems = _measurementsystemsService.GetPost(id);
            measurementsystems.UserName = User.Identity.Name;
            _measurementsystemsService.Delete(measurementsystems);
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
            string sMeasurementSystem;

            MeasurementSystemsPost measurementsystems;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMeasurementSystem = _measurementsystemsService.Get(nID).sMeasurementSystem;
                            if (!_measurementsystemsService.VerifyMeasurementSystemDeleteOK(nID, sMeasurementSystem).Any())
                            {
                                measurementsystems = _measurementsystemsService.GetPost(nID);
                                measurementsystems.UserName = User.Identity.Name;
                                _measurementsystemsService.Delete(measurementsystems);
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
        public IActionResult VerifyMeasurementSystem(long ixMeasurementSystem, string sMeasurementSystem)
        {
            string validationResponse = "";

            if (!_measurementsystemsService.VerifyMeasurementSystemUnique(ixMeasurementSystem, sMeasurementSystem))
            {
                validationResponse = $"MeasurementSystem {sMeasurementSystem} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

