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

    public class OutboundOrdersController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundOrdersService _outboundordersService;

        public OutboundOrdersController(IOutboundOrdersService outboundordersService )
        {
            _outboundordersService = outboundordersService;
        }

        // GET: OutboundOrders
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundorders = _outboundordersService.Index();
            return View(outboundorders.ToList());
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
            var outboundorders = _outboundordersService.Index();
            return PartialView("IndexGrid", outboundorders.ToList());
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
                IGrid<OutboundOrders> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundOrders> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundOrders.xlsx");
            }
        }

        private IGrid<OutboundOrders> CreateExportableGrid()
        {
            IGrid<OutboundOrders> grid = new Grid<OutboundOrders>(_outboundordersService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundOrder).Titled("Outbound Order").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sOrderReference).Titled("Order Reference").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.OutboundOrderTypes.sOutboundOrderType).Titled("Outbound Order Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Facilities.sFacility).Titled("Facility").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Companies.sCompany).Titled("Company").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.BusinessPartners.sBusinessPartner).Titled("Business Partner").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtDeliverEarliest).Titled("Deliver Earliest").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtDeliverLatest).Titled("Deliver Latest").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.CarrierServices.sCarrierService).Titled("Carrier Service").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundOrders>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundOrders/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundordersService.Get(id));
        }

        // GET: OutboundOrders/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixBusinessPartner = new SelectList(_outboundordersService.selectBusinessPartners().Select( x => new { x.ixBusinessPartner, x.sBusinessPartner }), "ixBusinessPartner", "sBusinessPartner");
			ViewBag.ixCarrierService = new SelectList(_outboundordersService.selectCarrierServices().Select( x => new { x.ixCarrierService, x.sCarrierService }), "ixCarrierService", "sCarrierService");
			ViewBag.ixCompany = new SelectList(_outboundordersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_outboundordersService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixOutboundOrderType = new SelectList(_outboundordersService.selectOutboundOrderTypes().Select( x => new { x.ixOutboundOrderType, x.sOutboundOrderType }), "ixOutboundOrderType", "sOutboundOrderType");
			ViewBag.ixOutboundShipment = new SelectList(_outboundordersService.selectOutboundShipments().Select( x => new { x.ixOutboundShipment, x.sOutboundShipment }), "ixOutboundShipment", "sOutboundShipment");
			ViewBag.ixPickBatch = new SelectList(_outboundordersService.selectPickBatches().Select( x => new { x.ixPickBatch, x.sPickBatch }), "ixPickBatch", "sPickBatch");
			ViewBag.ixStatus = new SelectList(_outboundordersService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: OutboundOrders/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundOrder,sOutboundOrder,sOrderReference,ixOutboundOrderType,ixFacility,ixCompany,ixBusinessPartner,dtDeliverEarliest,dtDeliverLatest,ixCarrierService,ixStatus,ixPickBatch,ixOutboundShipment")] OutboundOrdersPost outboundorders)
        {
            if (ModelState.IsValid)
            {
                outboundorders.UserName = User.Identity.Name;
                _outboundordersService.Create(outboundorders);
                return RedirectToAction("Index");
            }
			ViewBag.ixBusinessPartner = new SelectList(_outboundordersService.selectBusinessPartners().Select( x => new { x.ixBusinessPartner, x.sBusinessPartner }), "ixBusinessPartner", "sBusinessPartner");
			ViewBag.ixCarrierService = new SelectList(_outboundordersService.selectCarrierServices().Select( x => new { x.ixCarrierService, x.sCarrierService }), "ixCarrierService", "sCarrierService");
			ViewBag.ixCompany = new SelectList(_outboundordersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_outboundordersService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixOutboundOrderType = new SelectList(_outboundordersService.selectOutboundOrderTypes().Select( x => new { x.ixOutboundOrderType, x.sOutboundOrderType }), "ixOutboundOrderType", "sOutboundOrderType");
			ViewBag.ixOutboundShipment = new SelectList(_outboundordersService.selectOutboundShipments().Select( x => new { x.ixOutboundShipment, x.sOutboundShipment }), "ixOutboundShipment", "sOutboundShipment");
			ViewBag.ixPickBatch = new SelectList(_outboundordersService.selectPickBatches().Select( x => new { x.ixPickBatch, x.sPickBatch }), "ixPickBatch", "sPickBatch");
			ViewBag.ixStatus = new SelectList(_outboundordersService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(outboundorders);
        }

        // GET: OutboundOrders/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundOrdersPost outboundorders = _outboundordersService.GetPost(id);
            if (outboundorders == null)
            {
                return NotFound();
            }
			ViewBag.ixBusinessPartner = new SelectList(_outboundordersService.selectBusinessPartners().Select( x => new { x.ixBusinessPartner, x.sBusinessPartner }), "ixBusinessPartner", "sBusinessPartner", outboundorders.ixBusinessPartner);
			ViewBag.ixCarrierService = new SelectList(_outboundordersService.selectCarrierServices().Select( x => new { x.ixCarrierService, x.sCarrierService }), "ixCarrierService", "sCarrierService", outboundorders.ixCarrierService);
			ViewBag.ixCompany = new SelectList(_outboundordersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", outboundorders.ixCompany);
			ViewBag.ixFacility = new SelectList(_outboundordersService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", outboundorders.ixFacility);
			ViewBag.ixOutboundOrderType = new SelectList(_outboundordersService.selectOutboundOrderTypes().Select( x => new { x.ixOutboundOrderType, x.sOutboundOrderType }), "ixOutboundOrderType", "sOutboundOrderType", outboundorders.ixOutboundOrderType);
			ViewBag.ixOutboundShipment = new SelectList(_outboundordersService.selectOutboundShipmentsNullable().Select( x => new { ixOutboundShipment = x.Key, sOutboundShipment = x.Value }), "ixOutboundShipment", "sOutboundShipment", outboundorders.ixOutboundShipment);
			ViewBag.ixPickBatch = new SelectList(_outboundordersService.selectPickBatchesNullable().Select( x => new { ixPickBatch = x.Key, sPickBatch = x.Value }), "ixPickBatch", "sPickBatch", outboundorders.ixPickBatch);
			ViewBag.ixStatus = new SelectList(_outboundordersService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorders.ixStatus);

            return View(outboundorders);
        }

        // POST: OutboundOrders/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundOrder,sOutboundOrder,sOrderReference,ixOutboundOrderType,ixFacility,ixCompany,ixBusinessPartner,dtDeliverEarliest,dtDeliverLatest,ixCarrierService,ixStatus,ixPickBatch,ixOutboundShipment")] OutboundOrdersPost outboundorders)
        {
            if (ModelState.IsValid)
            {
                outboundorders.UserName = User.Identity.Name;
                _outboundordersService.Edit(outboundorders);
                return RedirectToAction("Index");
            }
			ViewBag.ixBusinessPartner = new SelectList(_outboundordersService.selectBusinessPartners().Select( x => new { x.ixBusinessPartner, x.sBusinessPartner }), "ixBusinessPartner", "sBusinessPartner", outboundorders.ixBusinessPartner);
			ViewBag.ixCarrierService = new SelectList(_outboundordersService.selectCarrierServices().Select( x => new { x.ixCarrierService, x.sCarrierService }), "ixCarrierService", "sCarrierService", outboundorders.ixCarrierService);
			ViewBag.ixCompany = new SelectList(_outboundordersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", outboundorders.ixCompany);
			ViewBag.ixFacility = new SelectList(_outboundordersService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", outboundorders.ixFacility);
			ViewBag.ixOutboundOrderType = new SelectList(_outboundordersService.selectOutboundOrderTypes().Select( x => new { x.ixOutboundOrderType, x.sOutboundOrderType }), "ixOutboundOrderType", "sOutboundOrderType", outboundorders.ixOutboundOrderType);
			ViewBag.ixOutboundShipment = new SelectList(_outboundordersService.selectOutboundShipmentsNullable().Select( x => new { ixOutboundShipment = x.Key, sOutboundShipment = x.Value }), "ixOutboundShipment", "sOutboundShipment", outboundorders.ixOutboundShipment);
			ViewBag.ixPickBatch = new SelectList(_outboundordersService.selectPickBatchesNullable().Select( x => new { ixPickBatch = x.Key, sPickBatch = x.Value }), "ixPickBatch", "sPickBatch", outboundorders.ixPickBatch);
			ViewBag.ixStatus = new SelectList(_outboundordersService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorders.ixStatus);

            return View(outboundorders);
        }


        // GET: OutboundOrders/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundordersService.Get(id));
        }

        // POST: OutboundOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundOrdersPost outboundorders = _outboundordersService.GetPost(id);
            outboundorders.UserName = User.Identity.Name;
            _outboundordersService.Delete(outboundorders);
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
            string sOutboundOrder;

            OutboundOrdersPost outboundorders;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundOrder = _outboundordersService.Get(nID).sOutboundOrder;
                            if (!_outboundordersService.VerifyOutboundOrderDeleteOK(nID, sOutboundOrder).Any())
                            {
                                outboundorders = _outboundordersService.GetPost(nID);
                                outboundorders.UserName = User.Identity.Name;
                                _outboundordersService.Delete(outboundorders);
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
        public IActionResult VerifyOutboundOrder(long ixOutboundOrder, string sOutboundOrder)
        {
            string validationResponse = "";

            if (!_outboundordersService.VerifyOutboundOrderUnique(ixOutboundOrder, sOutboundOrder))
            {
                validationResponse = $"OutboundOrder {sOutboundOrder} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

