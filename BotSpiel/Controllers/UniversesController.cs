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

    public class UniversesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IUniversesService _universesService;

        public UniversesController(IUniversesService universesService )
        {
            _universesService = universesService;
        }

        // GET: Universes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var universes = _universesService.Index();
            return View(universes.ToList());
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
            var universes = _universesService.Index();
            return PartialView("IndexGrid", universes.ToList());
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
                IGrid<Universes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Universes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportUniverses.xlsx");
            }
        }

        private IGrid<Universes> CreateExportableGrid()
        {
            IGrid<Universes> grid = new Grid<Universes>(_universesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sUniverse).Titled("Universe").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Universes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Universes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_universesService.Get(id));
        }

        // GET: Universes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Universes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixUniverse,sUniverse")] UniversesPost universes)
        {
            if (ModelState.IsValid)
            {
                universes.UserName = User.Identity.Name;
                _universesService.Create(universes);
                return RedirectToAction("Index");
            }

            return View(universes);
        }

        // GET: Universes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            UniversesPost universes = _universesService.GetPost(id);
            if (universes == null)
            {
                return NotFound();
            }

            return View(universes);
        }

        // POST: Universes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixUniverse,sUniverse")] UniversesPost universes)
        {
            if (ModelState.IsValid)
            {
                universes.UserName = User.Identity.Name;
                _universesService.Edit(universes);
                return RedirectToAction("Index");
            }

            return View(universes);
        }


        // GET: Universes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_universesService.Get(id));
        }

        // POST: Universes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            UniversesPost universes = _universesService.GetPost(id);
            universes.UserName = User.Identity.Name;
            _universesService.Delete(universes);
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
            string sUniverse;

            UniversesPost universes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sUniverse = _universesService.Get(nID).sUniverse;
                            if (!_universesService.VerifyUniverseDeleteOK(nID, sUniverse).Any())
                            {
                                universes = _universesService.GetPost(nID);
                                universes.UserName = User.Identity.Name;
                                _universesService.Delete(universes);
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
        public IActionResult VerifyUniverse(long ixUniverse, string sUniverse)
        {
            string validationResponse = "";

            if (!_universesService.VerifyUniverseUnique(ixUniverse, sUniverse))
            {
                validationResponse = $"Universe {sUniverse} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

