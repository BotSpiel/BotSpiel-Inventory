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

    public class ExecutionModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IExecutionModuleGridsService _executionmodulegridsService;

        public ExecutionModuleGridsController(IExecutionModuleGridsService executionmodulegridsService )
        {
            _executionmodulegridsService = executionmodulegridsService;
        }

        // GET: ExecutionModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var executionmodulegrids = _executionmodulegridsService.Index();
            return View(executionmodulegrids.ToList());
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
            var executionmodulegrids = _executionmodulegridsService.Index();
            return PartialView("IndexGrid", executionmodulegrids.ToList());
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
                IGrid<ExecutionModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<ExecutionModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportExecutionModuleGrids.xlsx");
            }
        }

        private IGrid<ExecutionModuleGrids> CreateExportableGrid()
        {
            IGrid<ExecutionModuleGrids> grid = new Grid<ExecutionModuleGrids>(_executionmodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sExecutionModuleGrid).Titled("Execution Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<ExecutionModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: ExecutionModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var executionmodulegridsconfig = _executionmodulegridsService.Indexconfig();
            return View(executionmodulegridsconfig.ToList());
        }

        // GET: ExecutionModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var executionmodulegridsmd = _executionmodulegridsService.Indexmd();
            return View(executionmodulegridsmd.ToList());
        }

        // GET: ExecutionModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var executionmodulegridstx = _executionmodulegridsService.Indextx();
            return View(executionmodulegridstx.ToList());
        }

        // GET: ExecutionModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var executionmodulegridsanalytics = _executionmodulegridsService.Indexanalytics();
            return View(executionmodulegridsanalytics.ToList());
        }

        // GET: ExecutionModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_executionmodulegridsService.Get(id));
        }

        // GET: ExecutionModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: ExecutionModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixExecutionModuleGrid,sExecutionModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] ExecutionModuleGridsPost executionmodulegrids)
        {
            if (ModelState.IsValid)
            {
                executionmodulegrids.UserName = User.Identity.Name;
                _executionmodulegridsService.Create(executionmodulegrids);
                return RedirectToAction("Index");
            }

            return View(executionmodulegrids);
        }

        // GET: ExecutionModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ExecutionModuleGridsPost executionmodulegrids = _executionmodulegridsService.GetPost(id);
            if (executionmodulegrids == null)
            {
                return NotFound();
            }

            return View(executionmodulegrids);
        }

        // POST: ExecutionModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixExecutionModuleGrid,sExecutionModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] ExecutionModuleGridsPost executionmodulegrids)
        {
            if (ModelState.IsValid)
            {
                executionmodulegrids.UserName = User.Identity.Name;
                _executionmodulegridsService.Edit(executionmodulegrids);
                return RedirectToAction("Index");
            }

            return View(executionmodulegrids);
        }


        // GET: ExecutionModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_executionmodulegridsService.Get(id));
        }

        // POST: ExecutionModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ExecutionModuleGridsPost executionmodulegrids = _executionmodulegridsService.GetPost(id);
            executionmodulegrids.UserName = User.Identity.Name;
            _executionmodulegridsService.Delete(executionmodulegrids);
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
            string sExecutionModuleGrid;

            ExecutionModuleGridsPost executionmodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sExecutionModuleGrid = _executionmodulegridsService.Get(nID).sExecutionModuleGrid;
                            if (!_executionmodulegridsService.VerifyExecutionModuleGridDeleteOK(nID, sExecutionModuleGrid).Any())
                            {
                                executionmodulegrids = _executionmodulegridsService.GetPost(nID);
                                executionmodulegrids.UserName = User.Identity.Name;
                                _executionmodulegridsService.Delete(executionmodulegrids);
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
        public IActionResult VerifyExecutionModuleGrid(long ixExecutionModuleGrid, string sExecutionModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

