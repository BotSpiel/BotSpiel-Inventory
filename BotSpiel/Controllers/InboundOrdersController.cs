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

    public class InboundOrdersController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInboundOrdersService _inboundordersService;

        public InboundOrdersController(IInboundOrdersService inboundordersService )
        {
            _inboundordersService = inboundordersService;
        }

        // GET: InboundOrders
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inboundorders = _inboundordersService.Index();
            return View(inboundorders.ToList());
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
            var inboundorders = _inboundordersService.Index();
            return PartialView("IndexGrid", inboundorders.ToList());
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
                IGrid<InboundOrders> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InboundOrders> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInboundOrders.xlsx");
            }
        }

        private IGrid<InboundOrders> CreateExportableGrid()
        {
            IGrid<InboundOrders> grid = new Grid<InboundOrders>(_inboundordersService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInboundOrder).Titled("Inbound Order").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sOrderReference).Titled("Order Reference").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.InboundOrderTypes.sInboundOrderType).Titled("Inbound Order Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Facilities.sFacility).Titled("Facility").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Companies.sCompany).Titled("Company").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.BusinessPartners.sBusinessPartner).Titled("Business Partner").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtExpectedAt).Titled("Expected At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InboundOrders>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InboundOrders/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inboundordersService.Get(id));
        }

        // GET: InboundOrders/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixBusinessPartner = new SelectList(_inboundordersService.selectBusinessPartners().Select( x => new { x.ixBusinessPartner, x.sBusinessPartner }), "ixBusinessPartner", "sBusinessPartner");
			ViewBag.ixCompany = new SelectList(_inboundordersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_inboundordersService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixInboundOrderType = new SelectList(_inboundordersService.selectInboundOrderTypes().Select( x => new { x.ixInboundOrderType, x.sInboundOrderType }), "ixInboundOrderType", "sInboundOrderType");
			ViewBag.ixStatus = new SelectList(_inboundordersService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: InboundOrders/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInboundOrder,sInboundOrder,sOrderReference,ixInboundOrderType,ixFacility,ixCompany,ixBusinessPartner,dtExpectedAt,ixStatus")] InboundOrdersPost inboundorders)
        {
            if (ModelState.IsValid)
            {
                inboundorders.UserName = User.Identity.Name;
                _inboundordersService.Create(inboundorders);
                return RedirectToAction("Index");
            }
			ViewBag.ixBusinessPartner = new SelectList(_inboundordersService.selectBusinessPartners().Select( x => new { x.ixBusinessPartner, x.sBusinessPartner }), "ixBusinessPartner", "sBusinessPartner");
			ViewBag.ixCompany = new SelectList(_inboundordersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");
			ViewBag.ixFacility = new SelectList(_inboundordersService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility");
			ViewBag.ixInboundOrderType = new SelectList(_inboundordersService.selectInboundOrderTypes().Select( x => new { x.ixInboundOrderType, x.sInboundOrderType }), "ixInboundOrderType", "sInboundOrderType");
			ViewBag.ixStatus = new SelectList(_inboundordersService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(inboundorders);
        }

        // GET: InboundOrders/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InboundOrdersPost inboundorders = _inboundordersService.GetPost(id);
            if (inboundorders == null)
            {
                return NotFound();
            }
			ViewBag.ixBusinessPartner = new SelectList(_inboundordersService.selectBusinessPartners().Select( x => new { x.ixBusinessPartner, x.sBusinessPartner }), "ixBusinessPartner", "sBusinessPartner", inboundorders.ixBusinessPartner);
			ViewBag.ixCompany = new SelectList(_inboundordersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", inboundorders.ixCompany);
			ViewBag.ixFacility = new SelectList(_inboundordersService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", inboundorders.ixFacility);
			ViewBag.ixInboundOrderType = new SelectList(_inboundordersService.selectInboundOrderTypes().Select( x => new { x.ixInboundOrderType, x.sInboundOrderType }), "ixInboundOrderType", "sInboundOrderType", inboundorders.ixInboundOrderType);
			ViewBag.ixStatus = new SelectList(_inboundordersService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", inboundorders.ixStatus);

            return View(inboundorders);
        }

        // POST: InboundOrders/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInboundOrder,sInboundOrder,sOrderReference,ixInboundOrderType,ixFacility,ixCompany,ixBusinessPartner,dtExpectedAt,ixStatus")] InboundOrdersPost inboundorders)
        {
            if (ModelState.IsValid)
            {
                inboundorders.UserName = User.Identity.Name;
                _inboundordersService.Edit(inboundorders);
                return RedirectToAction("Index");
            }
			ViewBag.ixBusinessPartner = new SelectList(_inboundordersService.selectBusinessPartners().Select( x => new { x.ixBusinessPartner, x.sBusinessPartner }), "ixBusinessPartner", "sBusinessPartner", inboundorders.ixBusinessPartner);
			ViewBag.ixCompany = new SelectList(_inboundordersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", inboundorders.ixCompany);
			ViewBag.ixFacility = new SelectList(_inboundordersService.selectFacilities().Select( x => new { x.ixFacility, x.sFacility }), "ixFacility", "sFacility", inboundorders.ixFacility);
			ViewBag.ixInboundOrderType = new SelectList(_inboundordersService.selectInboundOrderTypes().Select( x => new { x.ixInboundOrderType, x.sInboundOrderType }), "ixInboundOrderType", "sInboundOrderType", inboundorders.ixInboundOrderType);
			ViewBag.ixStatus = new SelectList(_inboundordersService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", inboundorders.ixStatus);

            return View(inboundorders);
        }


        // GET: InboundOrders/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inboundordersService.Get(id));
        }

        // POST: InboundOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InboundOrdersPost inboundorders = _inboundordersService.GetPost(id);
            inboundorders.UserName = User.Identity.Name;
            _inboundordersService.Delete(inboundorders);
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
            string sInboundOrder;

            InboundOrdersPost inboundorders;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInboundOrder = _inboundordersService.Get(nID).sInboundOrder;
                            if (!_inboundordersService.VerifyInboundOrderDeleteOK(nID, sInboundOrder).Any())
                            {
                                inboundorders = _inboundordersService.GetPost(nID);
                                inboundorders.UserName = User.Identity.Name;
                                _inboundordersService.Delete(inboundorders);
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
        public IActionResult VerifyInboundOrder(long ixInboundOrder, string sInboundOrder)
        {
            string validationResponse = "";

            if (!_inboundordersService.VerifyInboundOrderUnique(ixInboundOrder, sInboundOrder))
            {
                validationResponse = $"InboundOrder {sInboundOrder} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

