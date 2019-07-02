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

    public class OutboundCarrierManifestPickupsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundCarrierManifestPickupsService _outboundcarriermanifestpickupsService;

        public OutboundCarrierManifestPickupsController(IOutboundCarrierManifestPickupsService outboundcarriermanifestpickupsService )
        {
            _outboundcarriermanifestpickupsService = outboundcarriermanifestpickupsService;
        }

        // GET: OutboundCarrierManifestPickups
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundcarriermanifestpickups = _outboundcarriermanifestpickupsService.Index();
            return View(outboundcarriermanifestpickups.ToList());
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
            var outboundcarriermanifestpickups = _outboundcarriermanifestpickupsService.Index();
            return PartialView("IndexGrid", outboundcarriermanifestpickups.ToList());
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
                IGrid<OutboundCarrierManifestPickups> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundCarrierManifestPickups> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundCarrierManifestPickups.xlsx");
            }
        }

        private IGrid<OutboundCarrierManifestPickups> CreateExportableGrid()
        {
            IGrid<OutboundCarrierManifestPickups> grid = new Grid<OutboundCarrierManifestPickups>(_outboundcarriermanifestpickupsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundCarrierManifestPickup).Titled("Outbound Carrier Manifest Pickup").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.OutboundCarrierManifests.sOutboundCarrierManifest).Titled("Outbound Carrier Manifest").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundCarrierManifestPickups>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundCarrierManifestPickups/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundcarriermanifestpickupsService.Get(id));
        }

        // GET: OutboundCarrierManifestPickups/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixOutboundCarrierManifest = new SelectList(_outboundcarriermanifestpickupsService.selectOutboundCarrierManifests().Select( x => new { x.ixOutboundCarrierManifest, x.sOutboundCarrierManifest }), "ixOutboundCarrierManifest", "sOutboundCarrierManifest");
			ViewBag.ixStatus = new SelectList(_outboundcarriermanifestpickupsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: OutboundCarrierManifestPickups/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundCarrierManifestPickup,sOutboundCarrierManifestPickup,ixOutboundCarrierManifest,ixStatus")] OutboundCarrierManifestPickupsPost outboundcarriermanifestpickups)
        {
            if (ModelState.IsValid)
            {
                outboundcarriermanifestpickups.UserName = User.Identity.Name;
                _outboundcarriermanifestpickupsService.Create(outboundcarriermanifestpickups);
                return RedirectToAction("Index");
            }
			ViewBag.ixOutboundCarrierManifest = new SelectList(_outboundcarriermanifestpickupsService.selectOutboundCarrierManifests().Select( x => new { x.ixOutboundCarrierManifest, x.sOutboundCarrierManifest }), "ixOutboundCarrierManifest", "sOutboundCarrierManifest");
			ViewBag.ixStatus = new SelectList(_outboundcarriermanifestpickupsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(outboundcarriermanifestpickups);
        }

        // GET: OutboundCarrierManifestPickups/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundCarrierManifestPickupsPost outboundcarriermanifestpickups = _outboundcarriermanifestpickupsService.GetPost(id);
            if (outboundcarriermanifestpickups == null)
            {
                return NotFound();
            }
			ViewBag.ixOutboundCarrierManifest = new SelectList(_outboundcarriermanifestpickupsService.selectOutboundCarrierManifests().Select( x => new { x.ixOutboundCarrierManifest, x.sOutboundCarrierManifest }), "ixOutboundCarrierManifest", "sOutboundCarrierManifest", outboundcarriermanifestpickups.ixOutboundCarrierManifest);
			ViewBag.ixStatus = new SelectList(_outboundcarriermanifestpickupsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundcarriermanifestpickups.ixStatus);

            return View(outboundcarriermanifestpickups);
        }

        // POST: OutboundCarrierManifestPickups/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundCarrierManifestPickup,sOutboundCarrierManifestPickup,ixOutboundCarrierManifest,ixStatus")] OutboundCarrierManifestPickupsPost outboundcarriermanifestpickups)
        {
            if (ModelState.IsValid)
            {
                outboundcarriermanifestpickups.UserName = User.Identity.Name;
                _outboundcarriermanifestpickupsService.Edit(outboundcarriermanifestpickups);
                return RedirectToAction("Index");
            }
			ViewBag.ixOutboundCarrierManifest = new SelectList(_outboundcarriermanifestpickupsService.selectOutboundCarrierManifests().Select( x => new { x.ixOutboundCarrierManifest, x.sOutboundCarrierManifest }), "ixOutboundCarrierManifest", "sOutboundCarrierManifest", outboundcarriermanifestpickups.ixOutboundCarrierManifest);
			ViewBag.ixStatus = new SelectList(_outboundcarriermanifestpickupsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundcarriermanifestpickups.ixStatus);

            return View(outboundcarriermanifestpickups);
        }


        // GET: OutboundCarrierManifestPickups/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundcarriermanifestpickupsService.Get(id));
        }

        // POST: OutboundCarrierManifestPickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundCarrierManifestPickupsPost outboundcarriermanifestpickups = _outboundcarriermanifestpickupsService.GetPost(id);
            outboundcarriermanifestpickups.UserName = User.Identity.Name;
            _outboundcarriermanifestpickupsService.Delete(outboundcarriermanifestpickups);
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
            string sOutboundCarrierManifestPickup;

            OutboundCarrierManifestPickupsPost outboundcarriermanifestpickups;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundCarrierManifestPickup = _outboundcarriermanifestpickupsService.Get(nID).sOutboundCarrierManifestPickup;
                            if (!_outboundcarriermanifestpickupsService.VerifyOutboundCarrierManifestPickupDeleteOK(nID, sOutboundCarrierManifestPickup).Any())
                            {
                                outboundcarriermanifestpickups = _outboundcarriermanifestpickupsService.GetPost(nID);
                                outboundcarriermanifestpickups.UserName = User.Identity.Name;
                                _outboundcarriermanifestpickupsService.Delete(outboundcarriermanifestpickups);
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
        public IActionResult VerifyOutboundCarrierManifestPickup(long ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup)
        {
            string validationResponse = "";

            if (!_outboundcarriermanifestpickupsService.VerifyOutboundCarrierManifestPickupUnique(ixOutboundCarrierManifestPickup, sOutboundCarrierManifestPickup))
            {
                validationResponse = $"OutboundCarrierManifestPickup {sOutboundCarrierManifestPickup} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

