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

    public class AssemblyModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IAssemblyModuleGridsService _assemblymodulegridsService;

        public AssemblyModuleGridsController(IAssemblyModuleGridsService assemblymodulegridsService )
        {
            _assemblymodulegridsService = assemblymodulegridsService;
        }

        // GET: AssemblyModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var assemblymodulegrids = _assemblymodulegridsService.Index();
            return View(assemblymodulegrids.ToList());
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
            var assemblymodulegrids = _assemblymodulegridsService.Index();
            return PartialView("IndexGrid", assemblymodulegrids.ToList());
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
                IGrid<AssemblyModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<AssemblyModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportAssemblyModuleGrids.xlsx");
            }
        }

        private IGrid<AssemblyModuleGrids> CreateExportableGrid()
        {
            IGrid<AssemblyModuleGrids> grid = new Grid<AssemblyModuleGrids>(_assemblymodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sAssemblyModuleGrid).Titled("Assembly Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<AssemblyModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: AssemblyModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var assemblymodulegridsconfig = _assemblymodulegridsService.Indexconfig();
            return View(assemblymodulegridsconfig.ToList());
        }

        // GET: AssemblyModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var assemblymodulegridsmd = _assemblymodulegridsService.Indexmd();
            return View(assemblymodulegridsmd.ToList());
        }

        // GET: AssemblyModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var assemblymodulegridstx = _assemblymodulegridsService.Indextx();
            return View(assemblymodulegridstx.ToList());
        }

        // GET: AssemblyModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var assemblymodulegridsanalytics = _assemblymodulegridsService.Indexanalytics();
            return View(assemblymodulegridsanalytics.ToList());
        }

        // GET: AssemblyModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_assemblymodulegridsService.Get(id));
        }

        // GET: AssemblyModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: AssemblyModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixAssemblyModuleGrid,sAssemblyModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] AssemblyModuleGridsPost assemblymodulegrids)
        {
            if (ModelState.IsValid)
            {
                assemblymodulegrids.UserName = User.Identity.Name;
                _assemblymodulegridsService.Create(assemblymodulegrids);
                return RedirectToAction("Index");
            }

            return View(assemblymodulegrids);
        }

        // GET: AssemblyModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            AssemblyModuleGridsPost assemblymodulegrids = _assemblymodulegridsService.GetPost(id);
            if (assemblymodulegrids == null)
            {
                return NotFound();
            }

            return View(assemblymodulegrids);
        }

        // POST: AssemblyModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixAssemblyModuleGrid,sAssemblyModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] AssemblyModuleGridsPost assemblymodulegrids)
        {
            if (ModelState.IsValid)
            {
                assemblymodulegrids.UserName = User.Identity.Name;
                _assemblymodulegridsService.Edit(assemblymodulegrids);
                return RedirectToAction("Index");
            }

            return View(assemblymodulegrids);
        }


        // GET: AssemblyModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_assemblymodulegridsService.Get(id));
        }

        // POST: AssemblyModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AssemblyModuleGridsPost assemblymodulegrids = _assemblymodulegridsService.GetPost(id);
            assemblymodulegrids.UserName = User.Identity.Name;
            _assemblymodulegridsService.Delete(assemblymodulegrids);
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
            string sAssemblyModuleGrid;

            AssemblyModuleGridsPost assemblymodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sAssemblyModuleGrid = _assemblymodulegridsService.Get(nID).sAssemblyModuleGrid;
                            if (!_assemblymodulegridsService.VerifyAssemblyModuleGridDeleteOK(nID, sAssemblyModuleGrid).Any())
                            {
                                assemblymodulegrids = _assemblymodulegridsService.GetPost(nID);
                                assemblymodulegrids.UserName = User.Identity.Name;
                                _assemblymodulegridsService.Delete(assemblymodulegrids);
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
        public IActionResult VerifyAssemblyModuleGrid(long ixAssemblyModuleGrid, string sAssemblyModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

