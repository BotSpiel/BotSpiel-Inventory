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

    public class OutboundCarrierManifestsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundCarrierManifestsService _outboundcarriermanifestsService;

        public OutboundCarrierManifestsController(IOutboundCarrierManifestsService outboundcarriermanifestsService )
        {
            _outboundcarriermanifestsService = outboundcarriermanifestsService;
        }

        // GET: OutboundCarrierManifests
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundcarriermanifests = _outboundcarriermanifestsService.Index();
            return View(outboundcarriermanifests.ToList());
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
            var outboundcarriermanifests = _outboundcarriermanifestsService.Index();
            return PartialView("IndexGrid", outboundcarriermanifests.ToList());
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
                IGrid<OutboundCarrierManifests> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundCarrierManifests> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundCarrierManifests.xlsx");
            }
        }

        private IGrid<OutboundCarrierManifests> CreateExportableGrid()
        {
            IGrid<OutboundCarrierManifests> grid = new Grid<OutboundCarrierManifests>(_outboundcarriermanifestsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundCarrierManifest).Titled("Outbound Carrier Manifest").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Facilities.sFacility).Titled("Facility").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Carriers.sCarrier).Titled("Carrier").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtScheduledPickupAt).Titled("Scheduled Pickup At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundCarrierManifests>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundCarrierManifests/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundcarriermanifestsService.Get(id));
        }

        // GET: OutboundCarrierManifests/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCarrier = new SelectList(_outboundcarriermanifestsService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier");
			ViewBag.ixFacility = new SelectList(_outboundcarriermanifestsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixPickupInventoryLocation = new SelectList(_outboundcarriermanifestsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixStatus = new SelectList(_outboundcarriermanifestsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: OutboundCarrierManifests/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundCarrierManifest,sOutboundCarrierManifest,ixFacility,ixCarrier,ixPickupInventoryLocation,dtScheduledPickupAt,ixStatus")] OutboundCarrierManifestsPost outboundcarriermanifests)
        {
            if (ModelState.IsValid)
            {
                outboundcarriermanifests.UserName = User.Identity.Name;
                _outboundcarriermanifestsService.Create(outboundcarriermanifests);
                return RedirectToAction("Index");
            }
			ViewBag.ixCarrier = new SelectList(_outboundcarriermanifestsService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier");
			ViewBag.ixFacility = new SelectList(_outboundcarriermanifestsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixPickupInventoryLocation = new SelectList(_outboundcarriermanifestsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixStatus = new SelectList(_outboundcarriermanifestsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(outboundcarriermanifests);
        }

        // GET: OutboundCarrierManifests/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundCarrierManifestsPost outboundcarriermanifests = _outboundcarriermanifestsService.GetPost(id);
            if (outboundcarriermanifests == null)
            {
                return NotFound();
            }
			ViewBag.ixCarrier = new SelectList(_outboundcarriermanifestsService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier", outboundcarriermanifests.ixCarrier);
			ViewBag.ixFacility = new SelectList(_outboundcarriermanifestsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", outboundcarriermanifests.ixFacility);
			ViewBag.ixPickupInventoryLocation = new SelectList(_outboundcarriermanifestsService.selectInventoryLocationsNullable().Select( x => new { ixInventoryLocation = x.Key, sInventoryLocation = x.Value }), "ixInventoryLocation", "sInventoryLocation", outboundcarriermanifests.ixPickupInventoryLocation);
			ViewBag.ixStatus = new SelectList(_outboundcarriermanifestsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundcarriermanifests.ixStatus);

            return View(outboundcarriermanifests);
        }

        // POST: OutboundCarrierManifests/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundCarrierManifest,sOutboundCarrierManifest,ixFacility,ixCarrier,ixPickupInventoryLocation,dtScheduledPickupAt,ixStatus")] OutboundCarrierManifestsPost outboundcarriermanifests)
        {
            if (ModelState.IsValid)
            {
                outboundcarriermanifests.UserName = User.Identity.Name;
                _outboundcarriermanifestsService.Edit(outboundcarriermanifests);
                return RedirectToAction("Index");
            }
			ViewBag.ixCarrier = new SelectList(_outboundcarriermanifestsService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier", outboundcarriermanifests.ixCarrier);
			ViewBag.ixFacility = new SelectList(_outboundcarriermanifestsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", outboundcarriermanifests.ixFacility);
			ViewBag.ixPickupInventoryLocation = new SelectList(_outboundcarriermanifestsService.selectInventoryLocationsNullable().Select( x => new { ixInventoryLocation = x.Key, sInventoryLocation = x.Value }), "ixInventoryLocation", "sInventoryLocation", outboundcarriermanifests.ixPickupInventoryLocation);
			ViewBag.ixStatus = new SelectList(_outboundcarriermanifestsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundcarriermanifests.ixStatus);

            return View(outboundcarriermanifests);
        }


        // GET: OutboundCarrierManifests/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundcarriermanifestsService.Get(id));
        }

        // POST: OutboundCarrierManifests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundCarrierManifestsPost outboundcarriermanifests = _outboundcarriermanifestsService.GetPost(id);
            outboundcarriermanifests.UserName = User.Identity.Name;
            _outboundcarriermanifestsService.Delete(outboundcarriermanifests);
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
            string sOutboundCarrierManifest;

            OutboundCarrierManifestsPost outboundcarriermanifests;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundCarrierManifest = _outboundcarriermanifestsService.Get(nID).sOutboundCarrierManifest;
                            if (!_outboundcarriermanifestsService.VerifyOutboundCarrierManifestDeleteOK(nID, sOutboundCarrierManifest).Any())
                            {
                                outboundcarriermanifests = _outboundcarriermanifestsService.GetPost(nID);
                                outboundcarriermanifests.UserName = User.Identity.Name;
                                _outboundcarriermanifestsService.Delete(outboundcarriermanifests);
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
        public IActionResult VerifyOutboundCarrierManifest(long ixOutboundCarrierManifest, string sOutboundCarrierManifest)
        {
            string validationResponse = "";

            if (!_outboundcarriermanifestsService.VerifyOutboundCarrierManifestUnique(ixOutboundCarrierManifest, sOutboundCarrierManifest))
            {
                validationResponse = $"OutboundCarrierManifest {sOutboundCarrierManifest} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

