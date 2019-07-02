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

    public class FacilityFloorsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IFacilityFloorsService _facilityfloorsService;

        public FacilityFloorsController(IFacilityFloorsService facilityfloorsService )
        {
            _facilityfloorsService = facilityfloorsService;
        }

        // GET: FacilityFloors
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var facilityfloors = _facilityfloorsService.Index();
            return View(facilityfloors.ToList());
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
            var facilityfloors = _facilityfloorsService.Index();
            return PartialView("IndexGrid", facilityfloors.ToList());
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
                IGrid<FacilityFloors> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<FacilityFloors> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportFacilityFloors.xlsx");
            }
        }

        private IGrid<FacilityFloors> CreateExportableGrid()
        {
            IGrid<FacilityFloors> grid = new Grid<FacilityFloors>(_facilityfloorsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sFacilityFloor).Titled("Facility Floor").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<FacilityFloors>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: FacilityFloors/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_facilityfloorsService.Get(id));
        }

        // GET: FacilityFloors/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: FacilityFloors/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixFacilityFloor,sFacilityFloor")] FacilityFloorsPost facilityfloors)
        {
            if (ModelState.IsValid)
            {
                facilityfloors.UserName = User.Identity.Name;
                _facilityfloorsService.Create(facilityfloors);
                return RedirectToAction("Index");
            }

            return View(facilityfloors);
        }

        // GET: FacilityFloors/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            FacilityFloorsPost facilityfloors = _facilityfloorsService.GetPost(id);
            if (facilityfloors == null)
            {
                return NotFound();
            }

            return View(facilityfloors);
        }

        // POST: FacilityFloors/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixFacilityFloor,sFacilityFloor")] FacilityFloorsPost facilityfloors)
        {
            if (ModelState.IsValid)
            {
                facilityfloors.UserName = User.Identity.Name;
                _facilityfloorsService.Edit(facilityfloors);
                return RedirectToAction("Index");
            }

            return View(facilityfloors);
        }


        // GET: FacilityFloors/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_facilityfloorsService.Get(id));
        }

        // POST: FacilityFloors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FacilityFloorsPost facilityfloors = _facilityfloorsService.GetPost(id);
            facilityfloors.UserName = User.Identity.Name;
            _facilityfloorsService.Delete(facilityfloors);
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
            string sFacilityFloor;

            FacilityFloorsPost facilityfloors;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sFacilityFloor = _facilityfloorsService.Get(nID).sFacilityFloor;
                            if (!_facilityfloorsService.VerifyFacilityFloorDeleteOK(nID, sFacilityFloor).Any())
                            {
                                facilityfloors = _facilityfloorsService.GetPost(nID);
                                facilityfloors.UserName = User.Identity.Name;
                                _facilityfloorsService.Delete(facilityfloors);
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
        public IActionResult VerifyFacilityFloor(long ixFacilityFloor, string sFacilityFloor)
        {
            string validationResponse = "";

            if (!_facilityfloorsService.VerifyFacilityFloorUnique(ixFacilityFloor, sFacilityFloor))
            {
                validationResponse = $"FacilityFloor {sFacilityFloor} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

