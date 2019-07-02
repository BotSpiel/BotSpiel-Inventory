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

    public class OutboundOrderPackingController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundOrderPackingService _outboundorderpackingService;

        public OutboundOrderPackingController(IOutboundOrderPackingService outboundorderpackingService )
        {
            _outboundorderpackingService = outboundorderpackingService;
        }

        // GET: OutboundOrderPacking
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundorderpacking = _outboundorderpackingService.Index();
            return View(outboundorderpacking.ToList());
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
            var outboundorderpacking = _outboundorderpackingService.Index();
            return PartialView("IndexGrid", outboundorderpacking.ToList());
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
                IGrid<OutboundOrderPacking> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundOrderPacking> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundOrderPacking.xlsx");
            }
        }

        private IGrid<OutboundOrderPacking> CreateExportableGrid()
        {
            IGrid<OutboundOrderPacking> grid = new Grid<OutboundOrderPacking>(_outboundorderpackingService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundOrderPack).Titled("Outbound Order Pack").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.OutboundOrderLines.sOutboundOrderLine).Titled("Outbound Order Line").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.HandlingUnits.sHandlingUnit).Titled("Handling Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundOrderPacking>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundOrderPacking/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundorderpackingService.Get(id));
        }

        // GET: OutboundOrderPacking/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnit = new SelectList(_outboundorderpackingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderpackingService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine");
			ViewBag.ixStatus = new SelectList(_outboundorderpackingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: OutboundOrderPacking/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundOrderPack,sOutboundOrderPack,ixOutboundOrderLine,ixHandlingUnit,ixStatus")] OutboundOrderPackingPost outboundorderpacking)
        {
            if (ModelState.IsValid)
            {
                outboundorderpacking.UserName = User.Identity.Name;
                _outboundorderpackingService.Create(outboundorderpacking);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_outboundorderpackingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderpackingService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine");
			ViewBag.ixStatus = new SelectList(_outboundorderpackingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(outboundorderpacking);
        }

        // GET: OutboundOrderPacking/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundOrderPackingPost outboundorderpacking = _outboundorderpackingService.GetPost(id);
            if (outboundorderpacking == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnit = new SelectList(_outboundorderpackingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", outboundorderpacking.ixHandlingUnit);
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderpackingService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine", outboundorderpacking.ixOutboundOrderLine);
			ViewBag.ixStatus = new SelectList(_outboundorderpackingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorderpacking.ixStatus);

            return View(outboundorderpacking);
        }

        // POST: OutboundOrderPacking/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundOrderPack,sOutboundOrderPack,ixOutboundOrderLine,ixHandlingUnit,ixStatus")] OutboundOrderPackingPost outboundorderpacking)
        {
            if (ModelState.IsValid)
            {
                outboundorderpacking.UserName = User.Identity.Name;
                _outboundorderpackingService.Edit(outboundorderpacking);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_outboundorderpackingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", outboundorderpacking.ixHandlingUnit);
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderpackingService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine", outboundorderpacking.ixOutboundOrderLine);
			ViewBag.ixStatus = new SelectList(_outboundorderpackingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorderpacking.ixStatus);

            return View(outboundorderpacking);
        }


        // GET: OutboundOrderPacking/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundorderpackingService.Get(id));
        }

        // POST: OutboundOrderPacking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundOrderPackingPost outboundorderpacking = _outboundorderpackingService.GetPost(id);
            outboundorderpacking.UserName = User.Identity.Name;
            _outboundorderpackingService.Delete(outboundorderpacking);
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
            string sOutboundOrderPack;

            OutboundOrderPackingPost outboundorderpacking;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundOrderPack = _outboundorderpackingService.Get(nID).sOutboundOrderPack;
                            if (!_outboundorderpackingService.VerifyOutboundOrderPackDeleteOK(nID, sOutboundOrderPack).Any())
                            {
                                outboundorderpacking = _outboundorderpackingService.GetPost(nID);
                                outboundorderpacking.UserName = User.Identity.Name;
                                _outboundorderpackingService.Delete(outboundorderpacking);
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
        public IActionResult VerifyOutboundOrderPack(long ixOutboundOrderPack, string sOutboundOrderPack)
        {
            string validationResponse = "";

            if (!_outboundorderpackingService.VerifyOutboundOrderPackUnique(ixOutboundOrderPack, sOutboundOrderPack))
            {
                validationResponse = $"OutboundOrderPack {sOutboundOrderPack} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

