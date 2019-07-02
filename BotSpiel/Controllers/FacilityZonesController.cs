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

    public class FacilityZonesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IFacilityZonesService _facilityzonesService;

        public FacilityZonesController(IFacilityZonesService facilityzonesService )
        {
            _facilityzonesService = facilityzonesService;
        }

        // GET: FacilityZones
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var facilityzones = _facilityzonesService.Index();
            return View(facilityzones.ToList());
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
            var facilityzones = _facilityzonesService.Index();
            return PartialView("IndexGrid", facilityzones.ToList());
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
                IGrid<FacilityZones> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<FacilityZones> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportFacilityZones.xlsx");
            }
        }

        private IGrid<FacilityZones> CreateExportableGrid()
        {
            IGrid<FacilityZones> grid = new Grid<FacilityZones>(_facilityzonesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sFacilityZone).Titled("Facility Zone").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<FacilityZones>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: FacilityZones/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_facilityzonesService.Get(id));
        }

        // GET: FacilityZones/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: FacilityZones/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixFacilityZone,sFacilityZone")] FacilityZonesPost facilityzones)
        {
            if (ModelState.IsValid)
            {
                facilityzones.UserName = User.Identity.Name;
                _facilityzonesService.Create(facilityzones);
                return RedirectToAction("Index");
            }

            return View(facilityzones);
        }

        // GET: FacilityZones/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            FacilityZonesPost facilityzones = _facilityzonesService.GetPost(id);
            if (facilityzones == null)
            {
                return NotFound();
            }

            return View(facilityzones);
        }

        // POST: FacilityZones/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixFacilityZone,sFacilityZone")] FacilityZonesPost facilityzones)
        {
            if (ModelState.IsValid)
            {
                facilityzones.UserName = User.Identity.Name;
                _facilityzonesService.Edit(facilityzones);
                return RedirectToAction("Index");
            }

            return View(facilityzones);
        }


        // GET: FacilityZones/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_facilityzonesService.Get(id));
        }

        // POST: FacilityZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FacilityZonesPost facilityzones = _facilityzonesService.GetPost(id);
            facilityzones.UserName = User.Identity.Name;
            _facilityzonesService.Delete(facilityzones);
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
            string sFacilityZone;

            FacilityZonesPost facilityzones;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sFacilityZone = _facilityzonesService.Get(nID).sFacilityZone;
                            if (!_facilityzonesService.VerifyFacilityZoneDeleteOK(nID, sFacilityZone).Any())
                            {
                                facilityzones = _facilityzonesService.GetPost(nID);
                                facilityzones.UserName = User.Identity.Name;
                                _facilityzonesService.Delete(facilityzones);
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
        public IActionResult VerifyFacilityZone(long ixFacilityZone, string sFacilityZone)
        {
            string validationResponse = "";

            if (!_facilityzonesService.VerifyFacilityZoneUnique(ixFacilityZone, sFacilityZone))
            {
                validationResponse = $"FacilityZone {sFacilityZone} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

