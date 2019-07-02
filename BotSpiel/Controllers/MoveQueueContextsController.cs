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

    public class MoveQueueContextsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMoveQueueContextsService _movequeuecontextsService;

        public MoveQueueContextsController(IMoveQueueContextsService movequeuecontextsService )
        {
            _movequeuecontextsService = movequeuecontextsService;
        }

        // GET: MoveQueueContexts
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var movequeuecontexts = _movequeuecontextsService.Index();
            return View(movequeuecontexts.ToList());
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
            var movequeuecontexts = _movequeuecontextsService.Index();
            return PartialView("IndexGrid", movequeuecontexts.ToList());
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
                IGrid<MoveQueueContexts> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MoveQueueContexts> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMoveQueueContexts.xlsx");
            }
        }

        private IGrid<MoveQueueContexts> CreateExportableGrid()
        {
            IGrid<MoveQueueContexts> grid = new Grid<MoveQueueContexts>(_movequeuecontextsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMoveQueueContext).Titled("Move Queue Context").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MoveQueueContexts>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MoveQueueContexts/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_movequeuecontextsService.Get(id));
        }

        // GET: MoveQueueContexts/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MoveQueueContexts/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMoveQueueContext,sMoveQueueContext")] MoveQueueContextsPost movequeuecontexts)
        {
            if (ModelState.IsValid)
            {
                movequeuecontexts.UserName = User.Identity.Name;
                _movequeuecontextsService.Create(movequeuecontexts);
                return RedirectToAction("Index");
            }

            return View(movequeuecontexts);
        }

        // GET: MoveQueueContexts/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MoveQueueContextsPost movequeuecontexts = _movequeuecontextsService.GetPost(id);
            if (movequeuecontexts == null)
            {
                return NotFound();
            }

            return View(movequeuecontexts);
        }

        // POST: MoveQueueContexts/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMoveQueueContext,sMoveQueueContext")] MoveQueueContextsPost movequeuecontexts)
        {
            if (ModelState.IsValid)
            {
                movequeuecontexts.UserName = User.Identity.Name;
                _movequeuecontextsService.Edit(movequeuecontexts);
                return RedirectToAction("Index");
            }

            return View(movequeuecontexts);
        }


        // GET: MoveQueueContexts/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_movequeuecontextsService.Get(id));
        }

        // POST: MoveQueueContexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MoveQueueContextsPost movequeuecontexts = _movequeuecontextsService.GetPost(id);
            movequeuecontexts.UserName = User.Identity.Name;
            _movequeuecontextsService.Delete(movequeuecontexts);
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
            string sMoveQueueContext;

            MoveQueueContextsPost movequeuecontexts;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMoveQueueContext = _movequeuecontextsService.Get(nID).sMoveQueueContext;
                            if (!_movequeuecontextsService.VerifyMoveQueueContextDeleteOK(nID, sMoveQueueContext).Any())
                            {
                                movequeuecontexts = _movequeuecontextsService.GetPost(nID);
                                movequeuecontexts.UserName = User.Identity.Name;
                                _movequeuecontextsService.Delete(movequeuecontexts);
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
        public IActionResult VerifyMoveQueueContext(long ixMoveQueueContext, string sMoveQueueContext)
        {
            string validationResponse = "";

            if (!_movequeuecontextsService.VerifyMoveQueueContextUnique(ixMoveQueueContext, sMoveQueueContext))
            {
                validationResponse = $"MoveQueueContext {sMoveQueueContext} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

