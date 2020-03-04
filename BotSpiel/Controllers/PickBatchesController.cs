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
//Custom Code Start | Added Code Block 
using BotSpiel.Services.Utilities;
//Custom Code End

namespace BotSpiel
{

    public class PickBatchesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPickBatchesService _pickbatchesService;
        //Custom Code Start | Added Code Block 
        private readonly Inventory _inventory;
        private readonly Picking _picking;
        private readonly IStatusesService _statusesService;
        private readonly IOutboundOrdersService _outboundordersService;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public PickBatchesController(IPickBatchesService pickbatchesService)
        //Replaced Code Block End
        public PickBatchesController(IPickBatchesService pickbatchesService, Inventory inventory, IStatusesService statusesService, IOutboundOrdersService outboundordersService, Picking picking)
        //Custom Code End
        {
            _pickbatchesService = pickbatchesService;
            _inventory = inventory;
            _statusesService = statusesService;
            _outboundordersService = outboundordersService;
            _picking = picking;
        }

        // GET: PickBatches
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var pickbatches = _pickbatchesService.Index();
            return View(pickbatches.ToList());
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
            var pickbatches = _pickbatchesService.Index();
            return PartialView("IndexGrid", pickbatches.ToList());
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
                IGrid<PickBatches> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PickBatches> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPickBatches.xlsx");
            }
        }

        private IGrid<PickBatches> CreateExportableGrid()
        {
            IGrid<PickBatches> grid = new Grid<PickBatches>(_pickbatchesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPickBatch).Titled("Pick Batch").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.PickBatchTypes.sPickBatchType).Titled("Pick Batch Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bMultiResource).Titled("Multi Resource").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtStartBy).Titled("Start By").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCompleteBy).Titled("Complete By").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PickBatches>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PickBatches/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_pickbatchesService.Get(id));
        }

        // GET: PickBatches/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPickBatchType = new SelectList(_pickbatchesService.selectPickBatchTypes().Select( x => new { x.ixPickBatchType, x.sPickBatchType }), "ixPickBatchType", "sPickBatchType");
			ViewBag.ixStatus = new SelectList(_pickbatchesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: PickBatches/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPickBatch,sPickBatch,ixPickBatchType,bMultiResource,dtStartBy,dtCompleteBy,ixStatus")] PickBatchesPost pickbatches)
        {
            if (ModelState.IsValid)
            {
                pickbatches.UserName = User.Identity.Name;
                _pickbatchesService.Create(pickbatches);
                return RedirectToAction("Index");
            }
			ViewBag.ixPickBatchType = new SelectList(_pickbatchesService.selectPickBatchTypes().Select( x => new { x.ixPickBatchType, x.sPickBatchType }), "ixPickBatchType", "sPickBatchType");
			ViewBag.ixStatus = new SelectList(_pickbatchesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(pickbatches);
        }

        // GET: PickBatches/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PickBatchesPost pickbatches = _pickbatchesService.GetPost(id);
            if (pickbatches == null)
            {
                return NotFound();
            }
			ViewBag.ixPickBatchType = new SelectList(_pickbatchesService.selectPickBatchTypes().Select( x => new { x.ixPickBatchType, x.sPickBatchType }), "ixPickBatchType", "sPickBatchType", pickbatches.ixPickBatchType);
			ViewBag.ixStatus = new SelectList(_pickbatchesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", pickbatches.ixStatus);

            return View(pickbatches);
        }

        // POST: PickBatches/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPickBatch,sPickBatch,ixPickBatchType,bMultiResource,dtStartBy,dtCompleteBy,ixStatus")] PickBatchesPost pickbatches)
        {
            if (ModelState.IsValid)
            {
                pickbatches.UserName = User.Identity.Name;
                _pickbatchesService.Edit(pickbatches);
                return RedirectToAction("Index");
            }
			ViewBag.ixPickBatchType = new SelectList(_pickbatchesService.selectPickBatchTypes().Select( x => new { x.ixPickBatchType, x.sPickBatchType }), "ixPickBatchType", "sPickBatchType", pickbatches.ixPickBatchType);
			ViewBag.ixStatus = new SelectList(_pickbatchesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", pickbatches.ixStatus);

            return View(pickbatches);
        }


        // GET: PickBatches/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_pickbatchesService.Get(id));
        }

        // POST: PickBatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PickBatchesPost pickbatches = _pickbatchesService.GetPost(id);
            pickbatches.UserName = User.Identity.Name;
            _pickbatchesService.Delete(pickbatches);
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
            string sPickBatch;

            PickBatchesPost pickbatches;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPickBatch = _pickbatchesService.Get(nID).sPickBatch;
                            if (!_pickbatchesService.VerifyPickBatchDeleteOK(nID, sPickBatch).Any())
                            {
                                pickbatches = _pickbatchesService.GetPost(nID);
                                pickbatches.UserName = User.Identity.Name;
                                _pickbatchesService.Delete(pickbatches);
                                iDs.Add(nID);
                            }
                        }
                    }
                );

            iDs.ForEach(n => validationResponse = validationResponse + ", " + n.ToString());

            return Json(validationResponse);
        }

        //Custom Code Start | Added Code Block 

        [AcceptVerbs("Get", "Post")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult MultiRowPickBatchAllocate(string Ids)
        {
            string validationResponse = "";
            string[] sIDs = Ids.Split("|");
            List<long> iDs = new List<long>();
            long nID;
            string sPickBatch;

            PickBatchesPost pickbatches;

            sIDs.ToList()
                .ForEach(s =>
                {
                    if (long.TryParse(s, out nID))
                    {
                        _inventory.allocateBatch(nID, User.Identity.Name);
                        iDs.Add(nID);
                    }
                }
                );

            iDs.ForEach(n => validationResponse = validationResponse + ", " + n.ToString());

            return Json(validationResponse);
        }

        [AcceptVerbs("Get", "Post")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult MultiRowPickBatchActivate(string Ids)
        {
            string validationResponse = "";
            string[] sIDs = Ids.Split("|");
            List<long> iDs = new List<long>();
            long nID;
            string sPickBatch;
            bool bIsAllocated = true;

            PickBatchesPost pickbatches;

            sIDs.ToList()
                .ForEach(s =>
                {
                    if (long.TryParse(s, out nID))
                    {
                        //First check if activation OK
                        bIsAllocated = true;
                        var pickBatch = _pickbatchesService.GetPost(nID);
                        //We need to check if the batch has been allocated and that the batch is Inactive
                        _outboundordersService.IndexDb().Where(x => x.ixPickBatch == nID).Select(x => x.ixOutboundOrder).ToList()
                        .ForEach(o =>
                        {
                            if (_picking.getOrderAllocationStatus(o) == "Not Allocated")
                            {
                                bIsAllocated = false;
                            }
                        });

                        if (bIsAllocated)
                        {
                            pickBatch.ixStatus = _statusesService.IndexDb().Where(x => x.sStatus == "Active").Select(x => x.ixStatus).FirstOrDefault();
                            pickBatch.UserName = User.Identity.Name;
                            _pickbatchesService.Edit(pickBatch);
                            iDs.Add(nID);
                        }
                    }
                }
                );

            iDs.ForEach(n => validationResponse = validationResponse + ", " + n.ToString());

            return Json(validationResponse);
        }

        //Custom Code End



        [AcceptVerbs("Get","Post")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult VerifyPickBatch(long ixPickBatch, string sPickBatch)
        {
            string validationResponse = "";

            if (!_pickbatchesService.VerifyPickBatchUnique(ixPickBatch, sPickBatch))
            {
                validationResponse = $"PickBatch {sPickBatch} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

