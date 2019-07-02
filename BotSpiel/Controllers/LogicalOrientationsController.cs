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

    public class LogicalOrientationsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ILogicalOrientationsService _logicalorientationsService;

        public LogicalOrientationsController(ILogicalOrientationsService logicalorientationsService )
        {
            _logicalorientationsService = logicalorientationsService;
        }

        // GET: LogicalOrientations
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var logicalorientations = _logicalorientationsService.Index();
            return View(logicalorientations.ToList());
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
            var logicalorientations = _logicalorientationsService.Index();
            return PartialView("IndexGrid", logicalorientations.ToList());
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
                IGrid<LogicalOrientations> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<LogicalOrientations> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportLogicalOrientations.xlsx");
            }
        }

        private IGrid<LogicalOrientations> CreateExportableGrid()
        {
            IGrid<LogicalOrientations> grid = new Grid<LogicalOrientations>(_logicalorientationsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sLogicalOrientation).Titled("Logical Orientation").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<LogicalOrientations>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: LogicalOrientations/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_logicalorientationsService.Get(id));
        }

        // GET: LogicalOrientations/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: LogicalOrientations/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixLogicalOrientation,sLogicalOrientation")] LogicalOrientationsPost logicalorientations)
        {
            if (ModelState.IsValid)
            {
                logicalorientations.UserName = User.Identity.Name;
                _logicalorientationsService.Create(logicalorientations);
                return RedirectToAction("Index");
            }

            return View(logicalorientations);
        }

        // GET: LogicalOrientations/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            LogicalOrientationsPost logicalorientations = _logicalorientationsService.GetPost(id);
            if (logicalorientations == null)
            {
                return NotFound();
            }

            return View(logicalorientations);
        }

        // POST: LogicalOrientations/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixLogicalOrientation,sLogicalOrientation")] LogicalOrientationsPost logicalorientations)
        {
            if (ModelState.IsValid)
            {
                logicalorientations.UserName = User.Identity.Name;
                _logicalorientationsService.Edit(logicalorientations);
                return RedirectToAction("Index");
            }

            return View(logicalorientations);
        }


        // GET: LogicalOrientations/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_logicalorientationsService.Get(id));
        }

        // POST: LogicalOrientations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LogicalOrientationsPost logicalorientations = _logicalorientationsService.GetPost(id);
            logicalorientations.UserName = User.Identity.Name;
            _logicalorientationsService.Delete(logicalorientations);
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
            string sLogicalOrientation;

            LogicalOrientationsPost logicalorientations;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sLogicalOrientation = _logicalorientationsService.Get(nID).sLogicalOrientation;
                            if (!_logicalorientationsService.VerifyLogicalOrientationDeleteOK(nID, sLogicalOrientation).Any())
                            {
                                logicalorientations = _logicalorientationsService.GetPost(nID);
                                logicalorientations.UserName = User.Identity.Name;
                                _logicalorientationsService.Delete(logicalorientations);
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
        public IActionResult VerifyLogicalOrientation(long ixLogicalOrientation, string sLogicalOrientation)
        {
            string validationResponse = "";

            if (!_logicalorientationsService.VerifyLogicalOrientationUnique(ixLogicalOrientation, sLogicalOrientation))
            {
                validationResponse = $"LogicalOrientation {sLogicalOrientation} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

