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

    public class MoveQueueTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMoveQueueTypesService _movequeuetypesService;

        public MoveQueueTypesController(IMoveQueueTypesService movequeuetypesService )
        {
            _movequeuetypesService = movequeuetypesService;
        }

        // GET: MoveQueueTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var movequeuetypes = _movequeuetypesService.Index();
            return View(movequeuetypes.ToList());
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
            var movequeuetypes = _movequeuetypesService.Index();
            return PartialView("IndexGrid", movequeuetypes.ToList());
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
                IGrid<MoveQueueTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MoveQueueTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMoveQueueTypes.xlsx");
            }
        }

        private IGrid<MoveQueueTypes> CreateExportableGrid()
        {
            IGrid<MoveQueueTypes> grid = new Grid<MoveQueueTypes>(_movequeuetypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMoveQueueType).Titled("Move Queue Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MoveQueueTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MoveQueueTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_movequeuetypesService.Get(id));
        }

        // GET: MoveQueueTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MoveQueueTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMoveQueueType,sMoveQueueType")] MoveQueueTypesPost movequeuetypes)
        {
            if (ModelState.IsValid)
            {
                movequeuetypes.UserName = User.Identity.Name;
                _movequeuetypesService.Create(movequeuetypes);
                return RedirectToAction("Index");
            }

            return View(movequeuetypes);
        }

        // GET: MoveQueueTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MoveQueueTypesPost movequeuetypes = _movequeuetypesService.GetPost(id);
            if (movequeuetypes == null)
            {
                return NotFound();
            }

            return View(movequeuetypes);
        }

        // POST: MoveQueueTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMoveQueueType,sMoveQueueType")] MoveQueueTypesPost movequeuetypes)
        {
            if (ModelState.IsValid)
            {
                movequeuetypes.UserName = User.Identity.Name;
                _movequeuetypesService.Edit(movequeuetypes);
                return RedirectToAction("Index");
            }

            return View(movequeuetypes);
        }


        // GET: MoveQueueTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_movequeuetypesService.Get(id));
        }

        // POST: MoveQueueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MoveQueueTypesPost movequeuetypes = _movequeuetypesService.GetPost(id);
            movequeuetypes.UserName = User.Identity.Name;
            _movequeuetypesService.Delete(movequeuetypes);
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
            string sMoveQueueType;

            MoveQueueTypesPost movequeuetypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMoveQueueType = _movequeuetypesService.Get(nID).sMoveQueueType;
                            if (!_movequeuetypesService.VerifyMoveQueueTypeDeleteOK(nID, sMoveQueueType).Any())
                            {
                                movequeuetypes = _movequeuetypesService.GetPost(nID);
                                movequeuetypes.UserName = User.Identity.Name;
                                _movequeuetypesService.Delete(movequeuetypes);
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
        public IActionResult VerifyMoveQueueType(long ixMoveQueueType, string sMoveQueueType)
        {
            string validationResponse = "";

            if (!_movequeuetypesService.VerifyMoveQueueTypeUnique(ixMoveQueueType, sMoveQueueType))
            {
                validationResponse = $"MoveQueueType {sMoveQueueType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

