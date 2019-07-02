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

    public class MoveQueuesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMoveQueuesService _movequeuesService;

        public MoveQueuesController(IMoveQueuesService movequeuesService )
        {
            _movequeuesService = movequeuesService;
        }

        // GET: MoveQueues
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var movequeues = _movequeuesService.Index();
            return View(movequeues.ToList());
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
            var movequeues = _movequeuesService.Index();
            return PartialView("IndexGrid", movequeues.ToList());
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
                IGrid<MoveQueues> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MoveQueues> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMoveQueues.xlsx");
            }
        }

        private IGrid<MoveQueues> CreateExportableGrid()
        {
            IGrid<MoveQueues> grid = new Grid<MoveQueues>(_movequeuesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMoveQueue).Titled("Move Queue").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.MoveQueueTypes.sMoveQueueType).Titled("Move Queue Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.MoveQueueContexts.sMoveQueueContext).Titled("Move Queue Context").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sPreferredResource).Titled("Preferred Resource").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantity).Titled("Base Unit Quantity").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtStartBy).Titled("Start By").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCompleteBy).Titled("Complete By").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtStartedAt).Titled("Started At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCompletedAt).Titled("Completed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MoveQueues>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MoveQueues/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_movequeuesService.Get(id));
        }

        // GET: MoveQueues/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixInboundOrderLine = new SelectList(_movequeuesService.selectInboundOrderLines().Select( x => new { x.ixInboundOrderLine, x.sInboundOrderLine }), "ixInboundOrderLine", "sInboundOrderLine");
			ViewBag.ixMoveQueueContext = new SelectList(_movequeuesService.selectMoveQueueContexts().Select( x => new { x.ixMoveQueueContext, x.sMoveQueueContext }), "ixMoveQueueContext", "sMoveQueueContext");
			ViewBag.ixMoveQueueType = new SelectList(_movequeuesService.selectMoveQueueTypes().Select( x => new { x.ixMoveQueueType, x.sMoveQueueType }), "ixMoveQueueType", "sMoveQueueType");
			ViewBag.ixOutboundOrderLine = new SelectList(_movequeuesService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine");
			ViewBag.ixPickBatch = new SelectList(_movequeuesService.selectPickBatches().Select( x => new { x.ixPickBatch, x.sPickBatch }), "ixPickBatch", "sPickBatch");
			ViewBag.ixSourceHandlingUnit = new SelectList(_movequeuesService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixSourceInventoryLocation = new SelectList(_movequeuesService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixSourceInventoryUnit = new SelectList(_movequeuesService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit");
			ViewBag.ixStatus = new SelectList(_movequeuesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");
			ViewBag.ixTargetHandlingUnit = new SelectList(_movequeuesService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixTargetInventoryLocation = new SelectList(_movequeuesService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixTargetInventoryUnit = new SelectList(_movequeuesService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit");

            return View();
        }

        // POST: MoveQueues/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMoveQueue,sMoveQueue,ixMoveQueueType,ixMoveQueueContext,ixSourceInventoryUnit,ixTargetInventoryUnit,ixSourceInventoryLocation,ixTargetInventoryLocation,ixSourceHandlingUnit,ixTargetHandlingUnit,sPreferredResource,nBaseUnitQuantity,dtStartBy,dtCompleteBy,dtStartedAt,dtCompletedAt,ixInboundOrderLine,ixOutboundOrderLine,ixPickBatch,ixStatus")] MoveQueuesPost movequeues)
        {
            if (ModelState.IsValid)
            {
                movequeues.UserName = User.Identity.Name;
                _movequeuesService.Create(movequeues);
                return RedirectToAction("Index");
            }
			ViewBag.ixInboundOrderLine = new SelectList(_movequeuesService.selectInboundOrderLines().Select( x => new { x.ixInboundOrderLine, x.sInboundOrderLine }), "ixInboundOrderLine", "sInboundOrderLine");
			ViewBag.ixMoveQueueContext = new SelectList(_movequeuesService.selectMoveQueueContexts().Select( x => new { x.ixMoveQueueContext, x.sMoveQueueContext }), "ixMoveQueueContext", "sMoveQueueContext");
			ViewBag.ixMoveQueueType = new SelectList(_movequeuesService.selectMoveQueueTypes().Select( x => new { x.ixMoveQueueType, x.sMoveQueueType }), "ixMoveQueueType", "sMoveQueueType");
			ViewBag.ixOutboundOrderLine = new SelectList(_movequeuesService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine");
			ViewBag.ixPickBatch = new SelectList(_movequeuesService.selectPickBatches().Select( x => new { x.ixPickBatch, x.sPickBatch }), "ixPickBatch", "sPickBatch");
			ViewBag.ixSourceHandlingUnit = new SelectList(_movequeuesService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixSourceInventoryLocation = new SelectList(_movequeuesService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixSourceInventoryUnit = new SelectList(_movequeuesService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit");
			ViewBag.ixStatus = new SelectList(_movequeuesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");
			ViewBag.ixTargetHandlingUnit = new SelectList(_movequeuesService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixTargetInventoryLocation = new SelectList(_movequeuesService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixTargetInventoryUnit = new SelectList(_movequeuesService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit");

            return View(movequeues);
        }

        // GET: MoveQueues/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MoveQueuesPost movequeues = _movequeuesService.GetPost(id);
            if (movequeues == null)
            {
                return NotFound();
            }
			ViewBag.ixInboundOrderLine = new SelectList(_movequeuesService.selectInboundOrderLinesNullable().Select( x => new { ixInboundOrderLine = x.Key, sInboundOrderLine = x.Value }), "ixInboundOrderLine", "sInboundOrderLine", movequeues.ixInboundOrderLine);
			ViewBag.ixMoveQueueContext = new SelectList(_movequeuesService.selectMoveQueueContexts().Select( x => new { x.ixMoveQueueContext, x.sMoveQueueContext }), "ixMoveQueueContext", "sMoveQueueContext", movequeues.ixMoveQueueContext);
			ViewBag.ixMoveQueueType = new SelectList(_movequeuesService.selectMoveQueueTypes().Select( x => new { x.ixMoveQueueType, x.sMoveQueueType }), "ixMoveQueueType", "sMoveQueueType", movequeues.ixMoveQueueType);
			ViewBag.ixOutboundOrderLine = new SelectList(_movequeuesService.selectOutboundOrderLinesNullable().Select( x => new { ixOutboundOrderLine = x.Key, sOutboundOrderLine = x.Value }), "ixOutboundOrderLine", "sOutboundOrderLine", movequeues.ixOutboundOrderLine);
			ViewBag.ixPickBatch = new SelectList(_movequeuesService.selectPickBatchesNullable().Select( x => new { ixPickBatch = x.Key, sPickBatch = x.Value }), "ixPickBatch", "sPickBatch", movequeues.ixPickBatch);
			ViewBag.ixSourceHandlingUnit = new SelectList(_movequeuesService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", movequeues.ixSourceHandlingUnit);
			ViewBag.ixSourceInventoryLocation = new SelectList(_movequeuesService.selectInventoryLocationsNullable().Select( x => new { ixInventoryLocation = x.Key, sInventoryLocation = x.Value }), "ixInventoryLocation", "sInventoryLocation", movequeues.ixSourceInventoryLocation);
			ViewBag.ixSourceInventoryUnit = new SelectList(_movequeuesService.selectInventoryUnitsNullable().Select( x => new { ixInventoryUnit = x.Key, sInventoryUnit = x.Value }), "ixInventoryUnit", "sInventoryUnit", movequeues.ixSourceInventoryUnit);
			ViewBag.ixStatus = new SelectList(_movequeuesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", movequeues.ixStatus);
			ViewBag.ixTargetHandlingUnit = new SelectList(_movequeuesService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", movequeues.ixTargetHandlingUnit);
			ViewBag.ixTargetInventoryLocation = new SelectList(_movequeuesService.selectInventoryLocationsNullable().Select( x => new { ixInventoryLocation = x.Key, sInventoryLocation = x.Value }), "ixInventoryLocation", "sInventoryLocation", movequeues.ixTargetInventoryLocation);
			ViewBag.ixTargetInventoryUnit = new SelectList(_movequeuesService.selectInventoryUnitsNullable().Select( x => new { ixInventoryUnit = x.Key, sInventoryUnit = x.Value }), "ixInventoryUnit", "sInventoryUnit", movequeues.ixTargetInventoryUnit);

            return View(movequeues);
        }

        // POST: MoveQueues/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMoveQueue,sMoveQueue,ixMoveQueueType,ixMoveQueueContext,ixSourceInventoryUnit,ixTargetInventoryUnit,ixSourceInventoryLocation,ixTargetInventoryLocation,ixSourceHandlingUnit,ixTargetHandlingUnit,sPreferredResource,nBaseUnitQuantity,dtStartBy,dtCompleteBy,dtStartedAt,dtCompletedAt,ixInboundOrderLine,ixOutboundOrderLine,ixPickBatch,ixStatus")] MoveQueuesPost movequeues)
        {
            if (ModelState.IsValid)
            {
                movequeues.UserName = User.Identity.Name;
                _movequeuesService.Edit(movequeues);
                return RedirectToAction("Index");
            }
			ViewBag.ixInboundOrderLine = new SelectList(_movequeuesService.selectInboundOrderLinesNullable().Select( x => new { ixInboundOrderLine = x.Key, sInboundOrderLine = x.Value }), "ixInboundOrderLine", "sInboundOrderLine", movequeues.ixInboundOrderLine);
			ViewBag.ixMoveQueueContext = new SelectList(_movequeuesService.selectMoveQueueContexts().Select( x => new { x.ixMoveQueueContext, x.sMoveQueueContext }), "ixMoveQueueContext", "sMoveQueueContext", movequeues.ixMoveQueueContext);
			ViewBag.ixMoveQueueType = new SelectList(_movequeuesService.selectMoveQueueTypes().Select( x => new { x.ixMoveQueueType, x.sMoveQueueType }), "ixMoveQueueType", "sMoveQueueType", movequeues.ixMoveQueueType);
			ViewBag.ixOutboundOrderLine = new SelectList(_movequeuesService.selectOutboundOrderLinesNullable().Select( x => new { ixOutboundOrderLine = x.Key, sOutboundOrderLine = x.Value }), "ixOutboundOrderLine", "sOutboundOrderLine", movequeues.ixOutboundOrderLine);
			ViewBag.ixPickBatch = new SelectList(_movequeuesService.selectPickBatchesNullable().Select( x => new { ixPickBatch = x.Key, sPickBatch = x.Value }), "ixPickBatch", "sPickBatch", movequeues.ixPickBatch);
			ViewBag.ixSourceHandlingUnit = new SelectList(_movequeuesService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", movequeues.ixSourceHandlingUnit);
			ViewBag.ixSourceInventoryLocation = new SelectList(_movequeuesService.selectInventoryLocationsNullable().Select( x => new { ixInventoryLocation = x.Key, sInventoryLocation = x.Value }), "ixInventoryLocation", "sInventoryLocation", movequeues.ixSourceInventoryLocation);
			ViewBag.ixSourceInventoryUnit = new SelectList(_movequeuesService.selectInventoryUnitsNullable().Select( x => new { ixInventoryUnit = x.Key, sInventoryUnit = x.Value }), "ixInventoryUnit", "sInventoryUnit", movequeues.ixSourceInventoryUnit);
			ViewBag.ixStatus = new SelectList(_movequeuesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", movequeues.ixStatus);
			ViewBag.ixTargetHandlingUnit = new SelectList(_movequeuesService.selectHandlingUnitsNullable().Select( x => new { ixHandlingUnit = x.Key, sHandlingUnit = x.Value }), "ixHandlingUnit", "sHandlingUnit", movequeues.ixTargetHandlingUnit);
			ViewBag.ixTargetInventoryLocation = new SelectList(_movequeuesService.selectInventoryLocationsNullable().Select( x => new { ixInventoryLocation = x.Key, sInventoryLocation = x.Value }), "ixInventoryLocation", "sInventoryLocation", movequeues.ixTargetInventoryLocation);
			ViewBag.ixTargetInventoryUnit = new SelectList(_movequeuesService.selectInventoryUnitsNullable().Select( x => new { ixInventoryUnit = x.Key, sInventoryUnit = x.Value }), "ixInventoryUnit", "sInventoryUnit", movequeues.ixTargetInventoryUnit);

            return View(movequeues);
        }


        // GET: MoveQueues/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_movequeuesService.Get(id));
        }

        // POST: MoveQueues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MoveQueuesPost movequeues = _movequeuesService.GetPost(id);
            movequeues.UserName = User.Identity.Name;
            _movequeuesService.Delete(movequeues);
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
            string sMoveQueue;

            MoveQueuesPost movequeues;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMoveQueue = _movequeuesService.Get(nID).sMoveQueue;
                            if (!_movequeuesService.VerifyMoveQueueDeleteOK(nID, sMoveQueue).Any())
                            {
                                movequeues = _movequeuesService.GetPost(nID);
                                movequeues.UserName = User.Identity.Name;
                                _movequeuesService.Delete(movequeues);
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
        public IActionResult VerifyMoveQueue(long ixMoveQueue, string sMoveQueue)
        {
            string validationResponse = "";

            if (!_movequeuesService.VerifyMoveQueueUnique(ixMoveQueue, sMoveQueue))
            {
                validationResponse = $"MoveQueue {sMoveQueue} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

