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

    public class PickBatchPickingController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPickBatchPickingService _pickbatchpickingService;

        public PickBatchPickingController(IPickBatchPickingService pickbatchpickingService )
        {
            _pickbatchpickingService = pickbatchpickingService;
        }

        // GET: PickBatchPicking
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var pickbatchpicking = _pickbatchpickingService.Index();
            return View(pickbatchpicking.ToList());
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
            var pickbatchpicking = _pickbatchpickingService.Index();
            return PartialView("IndexGrid", pickbatchpicking.ToList());
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
                IGrid<PickBatchPicking> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PickBatchPicking> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPickBatchPicking.xlsx");
            }
        }

        private IGrid<PickBatchPicking> CreateExportableGrid()
        {
            IGrid<PickBatchPicking> grid = new Grid<PickBatchPicking>(_pickbatchpickingService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPickBatchPick).Titled("Pick Batch Pick").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.PickBatches.sPickBatch).Titled("Pick Batch").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InventoryUnits.sInventoryUnit).Titled("Inventory Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityPicked).Titled("Base Unit Quantity Picked").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sPackToHandlingUnit).Titled("Pack To Handling Unit").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.HandlingUnits.sHandlingUnit).Titled("Handling Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PickBatchPicking>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PickBatchPicking/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_pickbatchpickingService.Get(id));
        }

        // GET: PickBatchPicking/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnit = new SelectList(_pickbatchpickingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixInventoryUnit = new SelectList(_pickbatchpickingService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit");
			ViewBag.ixPickBatch = new SelectList(_pickbatchpickingService.selectPickBatches().Select( x => new { x.ixPickBatch, x.sPickBatch }), "ixPickBatch", "sPickBatch");

            return View();
        }

        // POST: PickBatchPicking/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPickBatchPick,sPickBatchPick,ixPickBatch,ixInventoryUnit,nBaseUnitQuantityPicked,sPackToHandlingUnit,ixHandlingUnit")] PickBatchPickingPost pickbatchpicking)
        {
            if (ModelState.IsValid)
            {
                pickbatchpicking.UserName = User.Identity.Name;
                _pickbatchpickingService.Create(pickbatchpicking);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_pickbatchpickingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixInventoryUnit = new SelectList(_pickbatchpickingService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit");
			ViewBag.ixPickBatch = new SelectList(_pickbatchpickingService.selectPickBatches().Select( x => new { x.ixPickBatch, x.sPickBatch }), "ixPickBatch", "sPickBatch");

            return View(pickbatchpicking);
        }

        // GET: PickBatchPicking/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PickBatchPickingPost pickbatchpicking = _pickbatchpickingService.GetPost(id);
            if (pickbatchpicking == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnit = new SelectList(_pickbatchpickingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", pickbatchpicking.ixHandlingUnit);
			ViewBag.ixInventoryUnit = new SelectList(_pickbatchpickingService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit", pickbatchpicking.ixInventoryUnit);
			ViewBag.ixPickBatch = new SelectList(_pickbatchpickingService.selectPickBatches().Select( x => new { x.ixPickBatch, x.sPickBatch }), "ixPickBatch", "sPickBatch", pickbatchpicking.ixPickBatch);

            return View(pickbatchpicking);
        }

        // POST: PickBatchPicking/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPickBatchPick,sPickBatchPick,ixPickBatch,ixInventoryUnit,nBaseUnitQuantityPicked,sPackToHandlingUnit,ixHandlingUnit")] PickBatchPickingPost pickbatchpicking)
        {
            if (ModelState.IsValid)
            {
                pickbatchpicking.UserName = User.Identity.Name;
                _pickbatchpickingService.Edit(pickbatchpicking);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_pickbatchpickingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", pickbatchpicking.ixHandlingUnit);
			ViewBag.ixInventoryUnit = new SelectList(_pickbatchpickingService.selectInventoryUnits().Select( x => new { x.ixInventoryUnit, x.sInventoryUnit }), "ixInventoryUnit", "sInventoryUnit", pickbatchpicking.ixInventoryUnit);
			ViewBag.ixPickBatch = new SelectList(_pickbatchpickingService.selectPickBatches().Select( x => new { x.ixPickBatch, x.sPickBatch }), "ixPickBatch", "sPickBatch", pickbatchpicking.ixPickBatch);

            return View(pickbatchpicking);
        }


        // GET: PickBatchPicking/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_pickbatchpickingService.Get(id));
        }

        // POST: PickBatchPicking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PickBatchPickingPost pickbatchpicking = _pickbatchpickingService.GetPost(id);
            pickbatchpicking.UserName = User.Identity.Name;
            _pickbatchpickingService.Delete(pickbatchpicking);
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
            string sPickBatchPick;

            PickBatchPickingPost pickbatchpicking;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPickBatchPick = _pickbatchpickingService.Get(nID).sPickBatchPick;
                            if (!_pickbatchpickingService.VerifyPickBatchPickDeleteOK(nID, sPickBatchPick).Any())
                            {
                                pickbatchpicking = _pickbatchpickingService.GetPost(nID);
                                pickbatchpicking.UserName = User.Identity.Name;
                                _pickbatchpickingService.Delete(pickbatchpicking);
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
        public IActionResult VerifyPickBatchPick(long ixPickBatchPick, string sPickBatchPick)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

