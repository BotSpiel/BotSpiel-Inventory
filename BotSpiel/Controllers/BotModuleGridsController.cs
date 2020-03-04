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

    public class BotModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IBotModuleGridsService _botmodulegridsService;

        public BotModuleGridsController(IBotModuleGridsService botmodulegridsService )
        {
            _botmodulegridsService = botmodulegridsService;
        }

        // GET: BotModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var botmodulegrids = _botmodulegridsService.Index();
            return View(botmodulegrids.ToList());
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
            var botmodulegrids = _botmodulegridsService.Index();
            return PartialView("IndexGrid", botmodulegrids.ToList());
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
                IGrid<BotModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<BotModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportBotModuleGrids.xlsx");
            }
        }

        private IGrid<BotModuleGrids> CreateExportableGrid()
        {
            IGrid<BotModuleGrids> grid = new Grid<BotModuleGrids>(_botmodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sBotModuleGrid).Titled("Bot Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<BotModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: BotModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var botmodulegridsconfig = _botmodulegridsService.Indexconfig();
            return View(botmodulegridsconfig.ToList());
        }

        // GET: BotModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var botmodulegridsmd = _botmodulegridsService.Indexmd();
            return View(botmodulegridsmd.ToList());
        }

        // GET: BotModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var botmodulegridstx = _botmodulegridsService.Indextx();
            return View(botmodulegridstx.ToList());
        }

        // GET: BotModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var botmodulegridsanalytics = _botmodulegridsService.Indexanalytics();
            return View(botmodulegridsanalytics.ToList());
        }

        // GET: BotModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_botmodulegridsService.Get(id));
        }

        // GET: BotModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: BotModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixBotModuleGrid,sBotModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] BotModuleGridsPost botmodulegrids)
        {
            if (ModelState.IsValid)
            {
                botmodulegrids.UserName = User.Identity.Name;
                _botmodulegridsService.Create(botmodulegrids);
                return RedirectToAction("Index");
            }

            return View(botmodulegrids);
        }

        // GET: BotModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            BotModuleGridsPost botmodulegrids = _botmodulegridsService.GetPost(id);
            if (botmodulegrids == null)
            {
                return NotFound();
            }

            return View(botmodulegrids);
        }

        // POST: BotModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixBotModuleGrid,sBotModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] BotModuleGridsPost botmodulegrids)
        {
            if (ModelState.IsValid)
            {
                botmodulegrids.UserName = User.Identity.Name;
                _botmodulegridsService.Edit(botmodulegrids);
                return RedirectToAction("Index");
            }

            return View(botmodulegrids);
        }


        // GET: BotModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_botmodulegridsService.Get(id));
        }

        // POST: BotModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BotModuleGridsPost botmodulegrids = _botmodulegridsService.GetPost(id);
            botmodulegrids.UserName = User.Identity.Name;
            _botmodulegridsService.Delete(botmodulegrids);
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
            string sBotModuleGrid;

            BotModuleGridsPost botmodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sBotModuleGrid = _botmodulegridsService.Get(nID).sBotModuleGrid;
                            if (!_botmodulegridsService.VerifyBotModuleGridDeleteOK(nID, sBotModuleGrid).Any())
                            {
                                botmodulegrids = _botmodulegridsService.GetPost(nID);
                                botmodulegrids.UserName = User.Identity.Name;
                                _botmodulegridsService.Delete(botmodulegrids);
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
        public IActionResult VerifyBotModuleGrid(long ixBotModuleGrid, string sBotModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

