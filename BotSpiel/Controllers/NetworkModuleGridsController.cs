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

    public class NetworkModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly INetworkModuleGridsService _networkmodulegridsService;

        public NetworkModuleGridsController(INetworkModuleGridsService networkmodulegridsService )
        {
            _networkmodulegridsService = networkmodulegridsService;
        }

        // GET: NetworkModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var networkmodulegrids = _networkmodulegridsService.Index();
            return View(networkmodulegrids.ToList());
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
            var networkmodulegrids = _networkmodulegridsService.Index();
            return PartialView("IndexGrid", networkmodulegrids.ToList());
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
                IGrid<NetworkModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<NetworkModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportNetworkModuleGrids.xlsx");
            }
        }

        private IGrid<NetworkModuleGrids> CreateExportableGrid()
        {
            IGrid<NetworkModuleGrids> grid = new Grid<NetworkModuleGrids>(_networkmodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sNetworkModuleGrid).Titled("Network Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<NetworkModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: NetworkModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var networkmodulegridsconfig = _networkmodulegridsService.Indexconfig();
            return View(networkmodulegridsconfig.ToList());
        }

        // GET: NetworkModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var networkmodulegridsmd = _networkmodulegridsService.Indexmd();
            return View(networkmodulegridsmd.ToList());
        }

        // GET: NetworkModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var networkmodulegridstx = _networkmodulegridsService.Indextx();
            return View(networkmodulegridstx.ToList());
        }

        // GET: NetworkModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var networkmodulegridsanalytics = _networkmodulegridsService.Indexanalytics();
            return View(networkmodulegridsanalytics.ToList());
        }

        // GET: NetworkModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_networkmodulegridsService.Get(id));
        }

        // GET: NetworkModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: NetworkModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixNetworkModuleGrid,sNetworkModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] NetworkModuleGridsPost networkmodulegrids)
        {
            if (ModelState.IsValid)
            {
                networkmodulegrids.UserName = User.Identity.Name;
                _networkmodulegridsService.Create(networkmodulegrids);
                return RedirectToAction("Index");
            }

            return View(networkmodulegrids);
        }

        // GET: NetworkModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            NetworkModuleGridsPost networkmodulegrids = _networkmodulegridsService.GetPost(id);
            if (networkmodulegrids == null)
            {
                return NotFound();
            }

            return View(networkmodulegrids);
        }

        // POST: NetworkModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixNetworkModuleGrid,sNetworkModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] NetworkModuleGridsPost networkmodulegrids)
        {
            if (ModelState.IsValid)
            {
                networkmodulegrids.UserName = User.Identity.Name;
                _networkmodulegridsService.Edit(networkmodulegrids);
                return RedirectToAction("Index");
            }

            return View(networkmodulegrids);
        }


        // GET: NetworkModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_networkmodulegridsService.Get(id));
        }

        // POST: NetworkModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            NetworkModuleGridsPost networkmodulegrids = _networkmodulegridsService.GetPost(id);
            networkmodulegrids.UserName = User.Identity.Name;
            _networkmodulegridsService.Delete(networkmodulegrids);
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
            string sNetworkModuleGrid;

            NetworkModuleGridsPost networkmodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sNetworkModuleGrid = _networkmodulegridsService.Get(nID).sNetworkModuleGrid;
                            if (!_networkmodulegridsService.VerifyNetworkModuleGridDeleteOK(nID, sNetworkModuleGrid).Any())
                            {
                                networkmodulegrids = _networkmodulegridsService.GetPost(nID);
                                networkmodulegrids.UserName = User.Identity.Name;
                                _networkmodulegridsService.Delete(networkmodulegrids);
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
        public IActionResult VerifyNetworkModuleGrid(long ixNetworkModuleGrid, string sNetworkModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

