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

    public class InboundModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInboundModuleGridsService _inboundmodulegridsService;

        public InboundModuleGridsController(IInboundModuleGridsService inboundmodulegridsService )
        {
            _inboundmodulegridsService = inboundmodulegridsService;
        }

        // GET: InboundModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inboundmodulegrids = _inboundmodulegridsService.Index();
            return View(inboundmodulegrids.ToList());
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
            var inboundmodulegrids = _inboundmodulegridsService.Index();
            return PartialView("IndexGrid", inboundmodulegrids.ToList());
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
                IGrid<InboundModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InboundModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInboundModuleGrids.xlsx");
            }
        }

        private IGrid<InboundModuleGrids> CreateExportableGrid()
        {
            IGrid<InboundModuleGrids> grid = new Grid<InboundModuleGrids>(_inboundmodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInboundModuleGrid).Titled("Inbound Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<InboundModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InboundModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var inboundmodulegridsconfig = _inboundmodulegridsService.Indexconfig();
            return View(inboundmodulegridsconfig.ToList());
        }

        // GET: InboundModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var inboundmodulegridsmd = _inboundmodulegridsService.Indexmd();
            return View(inboundmodulegridsmd.ToList());
        }

        // GET: InboundModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var inboundmodulegridstx = _inboundmodulegridsService.Indextx();
            return View(inboundmodulegridstx.ToList());
        }

        // GET: InboundModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var inboundmodulegridsanalytics = _inboundmodulegridsService.Indexanalytics();
            return View(inboundmodulegridsanalytics.ToList());
        }

        // GET: InboundModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inboundmodulegridsService.Get(id));
        }

        // GET: InboundModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: InboundModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInboundModuleGrid,sInboundModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] InboundModuleGridsPost inboundmodulegrids)
        {
            if (ModelState.IsValid)
            {
                inboundmodulegrids.UserName = User.Identity.Name;
                _inboundmodulegridsService.Create(inboundmodulegrids);
                return RedirectToAction("Index");
            }

            return View(inboundmodulegrids);
        }

        // GET: InboundModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InboundModuleGridsPost inboundmodulegrids = _inboundmodulegridsService.GetPost(id);
            if (inboundmodulegrids == null)
            {
                return NotFound();
            }

            return View(inboundmodulegrids);
        }

        // POST: InboundModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInboundModuleGrid,sInboundModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] InboundModuleGridsPost inboundmodulegrids)
        {
            if (ModelState.IsValid)
            {
                inboundmodulegrids.UserName = User.Identity.Name;
                _inboundmodulegridsService.Edit(inboundmodulegrids);
                return RedirectToAction("Index");
            }

            return View(inboundmodulegrids);
        }


        // GET: InboundModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inboundmodulegridsService.Get(id));
        }

        // POST: InboundModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InboundModuleGridsPost inboundmodulegrids = _inboundmodulegridsService.GetPost(id);
            inboundmodulegrids.UserName = User.Identity.Name;
            _inboundmodulegridsService.Delete(inboundmodulegrids);
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
            string sInboundModuleGrid;

            InboundModuleGridsPost inboundmodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInboundModuleGrid = _inboundmodulegridsService.Get(nID).sInboundModuleGrid;
                            if (!_inboundmodulegridsService.VerifyInboundModuleGridDeleteOK(nID, sInboundModuleGrid).Any())
                            {
                                inboundmodulegrids = _inboundmodulegridsService.GetPost(nID);
                                inboundmodulegrids.UserName = User.Identity.Name;
                                _inboundmodulegridsService.Delete(inboundmodulegrids);
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
        public IActionResult VerifyInboundModuleGrid(long ixInboundModuleGrid, string sInboundModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

