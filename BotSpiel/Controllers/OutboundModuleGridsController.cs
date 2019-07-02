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

    public class OutboundModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundModuleGridsService _outboundmodulegridsService;

        public OutboundModuleGridsController(IOutboundModuleGridsService outboundmodulegridsService )
        {
            _outboundmodulegridsService = outboundmodulegridsService;
        }

        // GET: OutboundModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundmodulegrids = _outboundmodulegridsService.Index();
            return View(outboundmodulegrids.ToList());
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
            var outboundmodulegrids = _outboundmodulegridsService.Index();
            return PartialView("IndexGrid", outboundmodulegrids.ToList());
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
                IGrid<OutboundModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundModuleGrids.xlsx");
            }
        }

        private IGrid<OutboundModuleGrids> CreateExportableGrid()
        {
            IGrid<OutboundModuleGrids> grid = new Grid<OutboundModuleGrids>(_outboundmodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundModuleGrid).Titled("Outbound Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<OutboundModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var outboundmodulegridsconfig = _outboundmodulegridsService.Indexconfig();
            return View(outboundmodulegridsconfig.ToList());
        }

        // GET: OutboundModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var outboundmodulegridsmd = _outboundmodulegridsService.Indexmd();
            return View(outboundmodulegridsmd.ToList());
        }

        // GET: OutboundModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var outboundmodulegridstx = _outboundmodulegridsService.Indextx();
            return View(outboundmodulegridstx.ToList());
        }

        // GET: OutboundModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var outboundmodulegridsanalytics = _outboundmodulegridsService.Indexanalytics();
            return View(outboundmodulegridsanalytics.ToList());
        }

        // GET: OutboundModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundmodulegridsService.Get(id));
        }

        // GET: OutboundModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: OutboundModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundModuleGrid,sOutboundModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] OutboundModuleGridsPost outboundmodulegrids)
        {
            if (ModelState.IsValid)
            {
                outboundmodulegrids.UserName = User.Identity.Name;
                _outboundmodulegridsService.Create(outboundmodulegrids);
                return RedirectToAction("Index");
            }

            return View(outboundmodulegrids);
        }

        // GET: OutboundModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundModuleGridsPost outboundmodulegrids = _outboundmodulegridsService.GetPost(id);
            if (outboundmodulegrids == null)
            {
                return NotFound();
            }

            return View(outboundmodulegrids);
        }

        // POST: OutboundModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundModuleGrid,sOutboundModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] OutboundModuleGridsPost outboundmodulegrids)
        {
            if (ModelState.IsValid)
            {
                outboundmodulegrids.UserName = User.Identity.Name;
                _outboundmodulegridsService.Edit(outboundmodulegrids);
                return RedirectToAction("Index");
            }

            return View(outboundmodulegrids);
        }


        // GET: OutboundModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundmodulegridsService.Get(id));
        }

        // POST: OutboundModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundModuleGridsPost outboundmodulegrids = _outboundmodulegridsService.GetPost(id);
            outboundmodulegrids.UserName = User.Identity.Name;
            _outboundmodulegridsService.Delete(outboundmodulegrids);
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
            string sOutboundModuleGrid;

            OutboundModuleGridsPost outboundmodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundModuleGrid = _outboundmodulegridsService.Get(nID).sOutboundModuleGrid;
                            if (!_outboundmodulegridsService.VerifyOutboundModuleGridDeleteOK(nID, sOutboundModuleGrid).Any())
                            {
                                outboundmodulegrids = _outboundmodulegridsService.GetPost(nID);
                                outboundmodulegrids.UserName = User.Identity.Name;
                                _outboundmodulegridsService.Delete(outboundmodulegrids);
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
        public IActionResult VerifyOutboundModuleGrid(long ixOutboundModuleGrid, string sOutboundModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

