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

    public class FacilityWorkAreasController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IFacilityWorkAreasService _facilityworkareasService;

        public FacilityWorkAreasController(IFacilityWorkAreasService facilityworkareasService )
        {
            _facilityworkareasService = facilityworkareasService;
        }

        // GET: FacilityWorkAreas
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var facilityworkareas = _facilityworkareasService.Index();
            return View(facilityworkareas.ToList());
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
            var facilityworkareas = _facilityworkareasService.Index();
            return PartialView("IndexGrid", facilityworkareas.ToList());
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
                IGrid<FacilityWorkAreas> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<FacilityWorkAreas> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportFacilityWorkAreas.xlsx");
            }
        }

        private IGrid<FacilityWorkAreas> CreateExportableGrid()
        {
            IGrid<FacilityWorkAreas> grid = new Grid<FacilityWorkAreas>(_facilityworkareasService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sFacilityWorkArea).Titled("Facility Work Area").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<FacilityWorkAreas>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: FacilityWorkAreas/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_facilityworkareasService.Get(id));
        }

        // GET: FacilityWorkAreas/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: FacilityWorkAreas/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixFacilityWorkArea,sFacilityWorkArea")] FacilityWorkAreasPost facilityworkareas)
        {
            if (ModelState.IsValid)
            {
                facilityworkareas.UserName = User.Identity.Name;
                _facilityworkareasService.Create(facilityworkareas);
                return RedirectToAction("Index");
            }

            return View(facilityworkareas);
        }

        // GET: FacilityWorkAreas/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            FacilityWorkAreasPost facilityworkareas = _facilityworkareasService.GetPost(id);
            if (facilityworkareas == null)
            {
                return NotFound();
            }

            return View(facilityworkareas);
        }

        // POST: FacilityWorkAreas/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixFacilityWorkArea,sFacilityWorkArea")] FacilityWorkAreasPost facilityworkareas)
        {
            if (ModelState.IsValid)
            {
                facilityworkareas.UserName = User.Identity.Name;
                _facilityworkareasService.Edit(facilityworkareas);
                return RedirectToAction("Index");
            }

            return View(facilityworkareas);
        }


        // GET: FacilityWorkAreas/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_facilityworkareasService.Get(id));
        }

        // POST: FacilityWorkAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FacilityWorkAreasPost facilityworkareas = _facilityworkareasService.GetPost(id);
            facilityworkareas.UserName = User.Identity.Name;
            _facilityworkareasService.Delete(facilityworkareas);
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
            string sFacilityWorkArea;

            FacilityWorkAreasPost facilityworkareas;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sFacilityWorkArea = _facilityworkareasService.Get(nID).sFacilityWorkArea;
                            if (!_facilityworkareasService.VerifyFacilityWorkAreaDeleteOK(nID, sFacilityWorkArea).Any())
                            {
                                facilityworkareas = _facilityworkareasService.GetPost(nID);
                                facilityworkareas.UserName = User.Identity.Name;
                                _facilityworkareasService.Delete(facilityworkareas);
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
        public IActionResult VerifyFacilityWorkArea(long ixFacilityWorkArea, string sFacilityWorkArea)
        {
            string validationResponse = "";

            if (!_facilityworkareasService.VerifyFacilityWorkAreaUnique(ixFacilityWorkArea, sFacilityWorkArea))
            {
                validationResponse = $"FacilityWorkArea {sFacilityWorkArea} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

