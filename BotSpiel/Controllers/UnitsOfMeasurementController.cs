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

    public class UnitsOfMeasurementController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IUnitsOfMeasurementService _unitsofmeasurementService;

        public UnitsOfMeasurementController(IUnitsOfMeasurementService unitsofmeasurementService )
        {
            _unitsofmeasurementService = unitsofmeasurementService;
        }

        // GET: UnitsOfMeasurement
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var unitsofmeasurement = _unitsofmeasurementService.Index();
            return View(unitsofmeasurement.ToList());
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
            var unitsofmeasurement = _unitsofmeasurementService.Index();
            return PartialView("IndexGrid", unitsofmeasurement.ToList());
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
                IGrid<UnitsOfMeasurement> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<UnitsOfMeasurement> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportUnitsOfMeasurement.xlsx");
            }
        }

        private IGrid<UnitsOfMeasurement> CreateExportableGrid()
        {
            IGrid<UnitsOfMeasurement> grid = new Grid<UnitsOfMeasurement>(_unitsofmeasurementService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sUnitOfMeasurement).Titled("Unit Of Measurement").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.MeasurementUnitsOf.sMeasurementUnitOf).Titled("Measurement Unit Of").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.MeasurementSystems.sMeasurementSystem).Titled("Measurement System").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sSymbol).Titled("Symbol").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<UnitsOfMeasurement>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: UnitsOfMeasurement/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_unitsofmeasurementService.Get(id));
        }

        // GET: UnitsOfMeasurement/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixMeasurementSystem = new SelectList(_unitsofmeasurementService.selectMeasurementSystems().Select( x => new { x.ixMeasurementSystem, x.sMeasurementSystem }), "ixMeasurementSystem", "sMeasurementSystem");
			ViewBag.ixMeasurementUnitOf = new SelectList(_unitsofmeasurementService.selectMeasurementUnitsOf().Select( x => new { x.ixMeasurementUnitOf, x.sMeasurementUnitOf }), "ixMeasurementUnitOf", "sMeasurementUnitOf");

            return View();
        }

        // POST: UnitsOfMeasurement/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixUnitOfMeasurement,sUnitOfMeasurement,ixMeasurementUnitOf,ixMeasurementSystem,sSymbol")] UnitsOfMeasurementPost unitsofmeasurement)
        {
            if (ModelState.IsValid)
            {
                unitsofmeasurement.UserName = User.Identity.Name;
                _unitsofmeasurementService.Create(unitsofmeasurement);
                return RedirectToAction("Index");
            }
			ViewBag.ixMeasurementSystem = new SelectList(_unitsofmeasurementService.selectMeasurementSystems().Select( x => new { x.ixMeasurementSystem, x.sMeasurementSystem }), "ixMeasurementSystem", "sMeasurementSystem");
			ViewBag.ixMeasurementUnitOf = new SelectList(_unitsofmeasurementService.selectMeasurementUnitsOf().Select( x => new { x.ixMeasurementUnitOf, x.sMeasurementUnitOf }), "ixMeasurementUnitOf", "sMeasurementUnitOf");

            return View(unitsofmeasurement);
        }

        // GET: UnitsOfMeasurement/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            UnitsOfMeasurementPost unitsofmeasurement = _unitsofmeasurementService.GetPost(id);
            if (unitsofmeasurement == null)
            {
                return NotFound();
            }
			ViewBag.ixMeasurementSystem = new SelectList(_unitsofmeasurementService.selectMeasurementSystems().Select( x => new { x.ixMeasurementSystem, x.sMeasurementSystem }), "ixMeasurementSystem", "sMeasurementSystem", unitsofmeasurement.ixMeasurementSystem);
			ViewBag.ixMeasurementUnitOf = new SelectList(_unitsofmeasurementService.selectMeasurementUnitsOf().Select( x => new { x.ixMeasurementUnitOf, x.sMeasurementUnitOf }), "ixMeasurementUnitOf", "sMeasurementUnitOf", unitsofmeasurement.ixMeasurementUnitOf);

            return View(unitsofmeasurement);
        }

        // POST: UnitsOfMeasurement/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixUnitOfMeasurement,sUnitOfMeasurement,ixMeasurementUnitOf,ixMeasurementSystem,sSymbol")] UnitsOfMeasurementPost unitsofmeasurement)
        {
            if (ModelState.IsValid)
            {
                unitsofmeasurement.UserName = User.Identity.Name;
                _unitsofmeasurementService.Edit(unitsofmeasurement);
                return RedirectToAction("Index");
            }
			ViewBag.ixMeasurementSystem = new SelectList(_unitsofmeasurementService.selectMeasurementSystems().Select( x => new { x.ixMeasurementSystem, x.sMeasurementSystem }), "ixMeasurementSystem", "sMeasurementSystem", unitsofmeasurement.ixMeasurementSystem);
			ViewBag.ixMeasurementUnitOf = new SelectList(_unitsofmeasurementService.selectMeasurementUnitsOf().Select( x => new { x.ixMeasurementUnitOf, x.sMeasurementUnitOf }), "ixMeasurementUnitOf", "sMeasurementUnitOf", unitsofmeasurement.ixMeasurementUnitOf);

            return View(unitsofmeasurement);
        }


        // GET: UnitsOfMeasurement/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_unitsofmeasurementService.Get(id));
        }

        // POST: UnitsOfMeasurement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            UnitsOfMeasurementPost unitsofmeasurement = _unitsofmeasurementService.GetPost(id);
            unitsofmeasurement.UserName = User.Identity.Name;
            _unitsofmeasurementService.Delete(unitsofmeasurement);
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
            string sUnitOfMeasurement;

            UnitsOfMeasurementPost unitsofmeasurement;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sUnitOfMeasurement = _unitsofmeasurementService.Get(nID).sUnitOfMeasurement;
                            if (!_unitsofmeasurementService.VerifyUnitOfMeasurementDeleteOK(nID, sUnitOfMeasurement).Any())
                            {
                                unitsofmeasurement = _unitsofmeasurementService.GetPost(nID);
                                unitsofmeasurement.UserName = User.Identity.Name;
                                _unitsofmeasurementService.Delete(unitsofmeasurement);
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
        public IActionResult VerifyUnitOfMeasurement(long ixUnitOfMeasurement, string sUnitOfMeasurement)
        {
            string validationResponse = "";

            if (!_unitsofmeasurementService.VerifyUnitOfMeasurementUnique(ixUnitOfMeasurement, sUnitOfMeasurement))
            {
                validationResponse = $"UnitOfMeasurement {sUnitOfMeasurement} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

