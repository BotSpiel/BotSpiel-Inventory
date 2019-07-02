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

    public class FoundationModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IFoundationModuleGridsService _foundationmodulegridsService;

        public FoundationModuleGridsController(IFoundationModuleGridsService foundationmodulegridsService )
        {
            _foundationmodulegridsService = foundationmodulegridsService;
        }

        // GET: FoundationModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var foundationmodulegrids = _foundationmodulegridsService.Index();
            return View(foundationmodulegrids.ToList());
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
            var foundationmodulegrids = _foundationmodulegridsService.Index();
            return PartialView("IndexGrid", foundationmodulegrids.ToList());
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
                IGrid<FoundationModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<FoundationModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportFoundationModuleGrids.xlsx");
            }
        }

        private IGrid<FoundationModuleGrids> CreateExportableGrid()
        {
            IGrid<FoundationModuleGrids> grid = new Grid<FoundationModuleGrids>(_foundationmodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sFoundationModuleGrid).Titled("Foundation Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<FoundationModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: FoundationModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var foundationmodulegridsconfig = _foundationmodulegridsService.Indexconfig();
            return View(foundationmodulegridsconfig.ToList());
        }

        // GET: FoundationModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var foundationmodulegridsmd = _foundationmodulegridsService.Indexmd();
            return View(foundationmodulegridsmd.ToList());
        }

        // GET: FoundationModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var foundationmodulegridstx = _foundationmodulegridsService.Indextx();
            return View(foundationmodulegridstx.ToList());
        }

        // GET: FoundationModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var foundationmodulegridsanalytics = _foundationmodulegridsService.Indexanalytics();
            return View(foundationmodulegridsanalytics.ToList());
        }

        // GET: FoundationModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_foundationmodulegridsService.Get(id));
        }

        // GET: FoundationModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: FoundationModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixFoundationModuleGrid,sFoundationModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] FoundationModuleGridsPost foundationmodulegrids)
        {
            if (ModelState.IsValid)
            {
                foundationmodulegrids.UserName = User.Identity.Name;
                _foundationmodulegridsService.Create(foundationmodulegrids);
                return RedirectToAction("Index");
            }

            return View(foundationmodulegrids);
        }

        // GET: FoundationModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            FoundationModuleGridsPost foundationmodulegrids = _foundationmodulegridsService.GetPost(id);
            if (foundationmodulegrids == null)
            {
                return NotFound();
            }

            return View(foundationmodulegrids);
        }

        // POST: FoundationModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixFoundationModuleGrid,sFoundationModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] FoundationModuleGridsPost foundationmodulegrids)
        {
            if (ModelState.IsValid)
            {
                foundationmodulegrids.UserName = User.Identity.Name;
                _foundationmodulegridsService.Edit(foundationmodulegrids);
                return RedirectToAction("Index");
            }

            return View(foundationmodulegrids);
        }


        // GET: FoundationModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_foundationmodulegridsService.Get(id));
        }

        // POST: FoundationModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FoundationModuleGridsPost foundationmodulegrids = _foundationmodulegridsService.GetPost(id);
            foundationmodulegrids.UserName = User.Identity.Name;
            _foundationmodulegridsService.Delete(foundationmodulegrids);
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
            string sFoundationModuleGrid;

            FoundationModuleGridsPost foundationmodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sFoundationModuleGrid = _foundationmodulegridsService.Get(nID).sFoundationModuleGrid;
                            if (!_foundationmodulegridsService.VerifyFoundationModuleGridDeleteOK(nID, sFoundationModuleGrid).Any())
                            {
                                foundationmodulegrids = _foundationmodulegridsService.GetPost(nID);
                                foundationmodulegrids.UserName = User.Identity.Name;
                                _foundationmodulegridsService.Delete(foundationmodulegrids);
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
        public IActionResult VerifyFoundationModuleGrid(long ixFoundationModuleGrid, string sFoundationModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

