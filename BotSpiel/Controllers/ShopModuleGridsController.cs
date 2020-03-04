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

    public class ShopModuleGridsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IShopModuleGridsService _shopmodulegridsService;

        public ShopModuleGridsController(IShopModuleGridsService shopmodulegridsService )
        {
            _shopmodulegridsService = shopmodulegridsService;
        }

        // GET: ShopModuleGrids
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var shopmodulegrids = _shopmodulegridsService.Index();
            return View(shopmodulegrids.ToList());
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
            var shopmodulegrids = _shopmodulegridsService.Index();
            return PartialView("IndexGrid", shopmodulegrids.ToList());
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
                IGrid<ShopModuleGrids> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<ShopModuleGrids> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportShopModuleGrids.xlsx");
            }
        }

        private IGrid<ShopModuleGrids> CreateExportableGrid()
        {
            IGrid<ShopModuleGrids> grid = new Grid<ShopModuleGrids>(_shopmodulegridsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sShopModuleGrid).Titled("Shop Module Grid").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sShortDescription).Titled("Short Description").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDataEntityType).Titled("Data Entity Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.bCanCreate).Titled("Can Create").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanEdit).Titled("Can Edit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bCanDelete).Titled("Can Delete").Sortable(true).Filterable(true);

            grid.Pager = new GridPager<ShopModuleGrids>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: ShopModuleGridsconfig
        [Authorize]
        [HttpGet]
        public ActionResult Indexconfig()
        {
            var shopmodulegridsconfig = _shopmodulegridsService.Indexconfig();
            return View(shopmodulegridsconfig.ToList());
        }

        // GET: ShopModuleGridsmd
        [Authorize]
        [HttpGet]
        public ActionResult Indexmd()
        {
            var shopmodulegridsmd = _shopmodulegridsService.Indexmd();
            return View(shopmodulegridsmd.ToList());
        }

        // GET: ShopModuleGridstx
        [Authorize]
        [HttpGet]
        public ActionResult Indextx()
        {
            var shopmodulegridstx = _shopmodulegridsService.Indextx();
            return View(shopmodulegridstx.ToList());
        }

        // GET: ShopModuleGridsanalytics
        [Authorize]
        [HttpGet]
        public ActionResult Indexanalytics()
        {
            var shopmodulegridsanalytics = _shopmodulegridsService.Indexanalytics();
            return View(shopmodulegridsanalytics.ToList());
        }

        // GET: ShopModuleGrids/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_shopmodulegridsService.Get(id));
        }

        // GET: ShopModuleGrids/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: ShopModuleGrids/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixShopModuleGrid,sShopModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] ShopModuleGridsPost shopmodulegrids)
        {
            if (ModelState.IsValid)
            {
                shopmodulegrids.UserName = User.Identity.Name;
                _shopmodulegridsService.Create(shopmodulegrids);
                return RedirectToAction("Index");
            }

            return View(shopmodulegrids);
        }

        // GET: ShopModuleGrids/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ShopModuleGridsPost shopmodulegrids = _shopmodulegridsService.GetPost(id);
            if (shopmodulegrids == null)
            {
                return NotFound();
            }

            return View(shopmodulegrids);
        }

        // POST: ShopModuleGrids/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixShopModuleGrid,sShopModuleGrid,sShortDescription,sDataEntityType,bCanCreate,bCanEdit,bCanDelete")] ShopModuleGridsPost shopmodulegrids)
        {
            if (ModelState.IsValid)
            {
                shopmodulegrids.UserName = User.Identity.Name;
                _shopmodulegridsService.Edit(shopmodulegrids);
                return RedirectToAction("Index");
            }

            return View(shopmodulegrids);
        }


        // GET: ShopModuleGrids/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_shopmodulegridsService.Get(id));
        }

        // POST: ShopModuleGrids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ShopModuleGridsPost shopmodulegrids = _shopmodulegridsService.GetPost(id);
            shopmodulegrids.UserName = User.Identity.Name;
            _shopmodulegridsService.Delete(shopmodulegrids);
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
            string sShopModuleGrid;

            ShopModuleGridsPost shopmodulegrids;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sShopModuleGrid = _shopmodulegridsService.Get(nID).sShopModuleGrid;
                            if (!_shopmodulegridsService.VerifyShopModuleGridDeleteOK(nID, sShopModuleGrid).Any())
                            {
                                shopmodulegrids = _shopmodulegridsService.GetPost(nID);
                                shopmodulegrids.UserName = User.Identity.Name;
                                _shopmodulegridsService.Delete(shopmodulegrids);
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
        public IActionResult VerifyShopModuleGrid(long ixShopModuleGrid, string sShopModuleGrid)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

