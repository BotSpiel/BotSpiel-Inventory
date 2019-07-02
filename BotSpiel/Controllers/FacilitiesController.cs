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

    public class FacilitiesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IFacilitiesService _facilitiesService;

        public FacilitiesController(IFacilitiesService facilitiesService )
        {
            _facilitiesService = facilitiesService;
        }

        // GET: Facilities
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var facilities = _facilitiesService.Index();
            return View(facilities.ToList());
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
            var facilities = _facilitiesService.Index();
            return PartialView("IndexGrid", facilities.ToList());
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
                IGrid<Facilities> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Facilities> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportFacilities.xlsx");
            }
        }

        private IGrid<Facilities> CreateExportableGrid()
        {
            IGrid<Facilities> grid = new Grid<Facilities>(_facilitiesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sFacility).Titled("Facility").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Addresses.sAddress).Titled("Address").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sLatitude).Titled("Latitude").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sLongitude).Titled("Longitude").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Facilities>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Facilities/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_facilitiesService.Get(id));
        }

        // GET: Facilities/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixAddress = new SelectList(_facilitiesService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress");

            return View();
        }

        // POST: Facilities/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixFacility,sFacility,ixAddress,sLatitude,sLongitude")] FacilitiesPost facilities)
        {
            if (ModelState.IsValid)
            {
                facilities.UserName = User.Identity.Name;
                _facilitiesService.Create(facilities);
                return RedirectToAction("Index");
            }
			ViewBag.ixAddress = new SelectList(_facilitiesService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress");

            return View(facilities);
        }

        // GET: Facilities/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            FacilitiesPost facilities = _facilitiesService.GetPost(id);
            if (facilities == null)
            {
                return NotFound();
            }
			ViewBag.ixAddress = new SelectList(_facilitiesService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress", facilities.ixAddress);

            return View(facilities);
        }

        // POST: Facilities/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixFacility,sFacility,ixAddress,sLatitude,sLongitude")] FacilitiesPost facilities)
        {
            if (ModelState.IsValid)
            {
                facilities.UserName = User.Identity.Name;
                _facilitiesService.Edit(facilities);
                return RedirectToAction("Index");
            }
			ViewBag.ixAddress = new SelectList(_facilitiesService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress", facilities.ixAddress);

            return View(facilities);
        }


        // GET: Facilities/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_facilitiesService.Get(id));
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FacilitiesPost facilities = _facilitiesService.GetPost(id);
            facilities.UserName = User.Identity.Name;
            _facilitiesService.Delete(facilities);
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
            string sFacility;

            FacilitiesPost facilities;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sFacility = _facilitiesService.Get(nID).sFacility;
                            if (!_facilitiesService.VerifyFacilityDeleteOK(nID, sFacility).Any())
                            {
                                facilities = _facilitiesService.GetPost(nID);
                                facilities.UserName = User.Identity.Name;
                                _facilitiesService.Delete(facilities);
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
        public IActionResult VerifyFacility(long ixFacility, string sFacility)
        {
            string validationResponse = "";

            if (!_facilitiesService.VerifyFacilityUnique(ixFacility, sFacility))
            {
                validationResponse = $"Facility {sFacility} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

