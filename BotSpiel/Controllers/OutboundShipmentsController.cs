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

    public class OutboundShipmentsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundShipmentsService _outboundshipmentsService;

        public OutboundShipmentsController(IOutboundShipmentsService outboundshipmentsService )
        {
            _outboundshipmentsService = outboundshipmentsService;
        }

        // GET: OutboundShipments
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundshipments = _outboundshipmentsService.Index();
            return View(outboundshipments.ToList());
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
            var outboundshipments = _outboundshipmentsService.Index();
            return PartialView("IndexGrid", outboundshipments.ToList());
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
                IGrid<OutboundShipments> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundShipments> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundShipments.xlsx");
            }
        }

        private IGrid<OutboundShipments> CreateExportableGrid()
        {
            IGrid<OutboundShipments> grid = new Grid<OutboundShipments>(_outboundshipmentsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundShipment).Titled("Outbound Shipment").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Facilities.sFacility).Titled("Facility").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Companies.sCompany).Titled("Company").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Carriers.sCarrier).Titled("Carrier").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCarrierConsignmentNumber).Titled("Carrier Consignment Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Addresses.sAddress).Titled("Address").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundShipments>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundShipments/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundshipmentsService.Get(id));
        }

        // GET: OutboundShipments/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixAddress = new SelectList(_outboundshipmentsService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress");
			ViewBag.ixCarrier = new SelectList(_outboundshipmentsService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier");
			ViewBag.ixCompany = new SelectList(_outboundshipmentsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_outboundshipmentsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixOutboundCarrierManifest = new SelectList(_outboundshipmentsService.selectOutboundCarrierManifests().Select( x => new { x.ixOutboundCarrierManifest, x.sOutboundCarrierManifest }), "ixOutboundCarrierManifest", "sOutboundCarrierManifest");
			ViewBag.ixStatus = new SelectList(_outboundshipmentsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: OutboundShipments/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundShipment,sOutboundShipment,ixFacility,ixCompany,ixCarrier,sCarrierConsignmentNumber,ixStatus,ixAddress,ixOutboundCarrierManifest")] OutboundShipmentsPost outboundshipments)
        {
            if (ModelState.IsValid)
            {
                outboundshipments.UserName = User.Identity.Name;
                _outboundshipmentsService.Create(outboundshipments);
                return RedirectToAction("Index");
            }
			ViewBag.ixAddress = new SelectList(_outboundshipmentsService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress");
			ViewBag.ixCarrier = new SelectList(_outboundshipmentsService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier");
			ViewBag.ixCompany = new SelectList(_outboundshipmentsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_outboundshipmentsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixOutboundCarrierManifest = new SelectList(_outboundshipmentsService.selectOutboundCarrierManifests().Select( x => new { x.ixOutboundCarrierManifest, x.sOutboundCarrierManifest }), "ixOutboundCarrierManifest", "sOutboundCarrierManifest");
			ViewBag.ixStatus = new SelectList(_outboundshipmentsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(outboundshipments);
        }

        // GET: OutboundShipments/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundShipmentsPost outboundshipments = _outboundshipmentsService.GetPost(id);
            if (outboundshipments == null)
            {
                return NotFound();
            }
			ViewBag.ixAddress = new SelectList(_outboundshipmentsService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress", outboundshipments.ixAddress);
			ViewBag.ixCarrier = new SelectList(_outboundshipmentsService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier", outboundshipments.ixCarrier);
			ViewBag.ixCompany = new SelectList(_outboundshipmentsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", outboundshipments.ixCompany);
			ViewBag.ixFacility = new SelectList(_outboundshipmentsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", outboundshipments.ixFacility);
			ViewBag.ixOutboundCarrierManifest = new SelectList(_outboundshipmentsService.selectOutboundCarrierManifestsNullable().Select( x => new { ixOutboundCarrierManifest = x.Key, sOutboundCarrierManifest = x.Value }), "ixOutboundCarrierManifest", "sOutboundCarrierManifest", outboundshipments.ixOutboundCarrierManifest);
			ViewBag.ixStatus = new SelectList(_outboundshipmentsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundshipments.ixStatus);

            return View(outboundshipments);
        }

        // POST: OutboundShipments/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundShipment,sOutboundShipment,ixFacility,ixCompany,ixCarrier,sCarrierConsignmentNumber,ixStatus,ixAddress,ixOutboundCarrierManifest")] OutboundShipmentsPost outboundshipments)
        {
            if (ModelState.IsValid)
            {
                outboundshipments.UserName = User.Identity.Name;
                _outboundshipmentsService.Edit(outboundshipments);
                return RedirectToAction("Index");
            }
			ViewBag.ixAddress = new SelectList(_outboundshipmentsService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress", outboundshipments.ixAddress);
			ViewBag.ixCarrier = new SelectList(_outboundshipmentsService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier", outboundshipments.ixCarrier);
			ViewBag.ixCompany = new SelectList(_outboundshipmentsService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", outboundshipments.ixCompany);
			ViewBag.ixFacility = new SelectList(_outboundshipmentsService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", outboundshipments.ixFacility);
			ViewBag.ixOutboundCarrierManifest = new SelectList(_outboundshipmentsService.selectOutboundCarrierManifestsNullable().Select( x => new { ixOutboundCarrierManifest = x.Key, sOutboundCarrierManifest = x.Value }), "ixOutboundCarrierManifest", "sOutboundCarrierManifest", outboundshipments.ixOutboundCarrierManifest);
			ViewBag.ixStatus = new SelectList(_outboundshipmentsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundshipments.ixStatus);

            return View(outboundshipments);
        }


        // GET: OutboundShipments/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundshipmentsService.Get(id));
        }

        // POST: OutboundShipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundShipmentsPost outboundshipments = _outboundshipmentsService.GetPost(id);
            outboundshipments.UserName = User.Identity.Name;
            _outboundshipmentsService.Delete(outboundshipments);
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
            string sOutboundShipment;

            OutboundShipmentsPost outboundshipments;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundShipment = _outboundshipmentsService.Get(nID).sOutboundShipment;
                            if (!_outboundshipmentsService.VerifyOutboundShipmentDeleteOK(nID, sOutboundShipment).Any())
                            {
                                outboundshipments = _outboundshipmentsService.GetPost(nID);
                                outboundshipments.UserName = User.Identity.Name;
                                _outboundshipmentsService.Delete(outboundshipments);
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
        public IActionResult VerifyOutboundShipment(long ixOutboundShipment, string sOutboundShipment)
        {
            string validationResponse = "";

            if (!_outboundshipmentsService.VerifyOutboundShipmentUnique(ixOutboundShipment, sOutboundShipment))
            {
                validationResponse = $"OutboundShipment {sOutboundShipment} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

