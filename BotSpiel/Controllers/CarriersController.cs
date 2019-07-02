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

    public class CarriersController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICarriersService _carriersService;

        public CarriersController(ICarriersService carriersService )
        {
            _carriersService = carriersService;
        }

        // GET: Carriers
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var carriers = _carriersService.Index();
            return View(carriers.ToList());
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
            var carriers = _carriersService.Index();
            return PartialView("IndexGrid", carriers.ToList());
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
                IGrid<Carriers> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Carriers> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCarriers.xlsx");
            }
        }

        private IGrid<Carriers> CreateExportableGrid()
        {
            IGrid<Carriers> grid = new Grid<Carriers>(_carriersService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCarrier).Titled("Carrier").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.CarrierTypes.sCarrierType).Titled("Carrier Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sStandardCarrierAlphaCode).Titled("Standard Carrier Alpha Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sCarrierConsignmentNumberPrefix).Titled("Carrier Consignment Number Prefix").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.nCarrierConsignmentNumberStart).Titled("Carrier Consignment Number Start").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nCarrierConsignmentNumberLastUsed).Titled("Carrier Consignment Number Last Used").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtScheduledPickupTime).Titled("Scheduled Pickup Time").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Carriers>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Carriers/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_carriersService.Get(id));
        }

        // GET: Carriers/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCarrierType = new SelectList(_carriersService.selectCarrierTypes().Select( x => new { x.ixCarrierType, x.sCarrierType }), "ixCarrierType", "sCarrierType");

            return View();
        }

        // POST: Carriers/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCarrier,sCarrier,ixCarrierType,sStandardCarrierAlphaCode,sCarrierConsignmentNumberPrefix,nCarrierConsignmentNumberStart,nCarrierConsignmentNumberLastUsed,dtScheduledPickupTime")] CarriersPost carriers)
        {
            if (ModelState.IsValid)
            {
                carriers.UserName = User.Identity.Name;
                _carriersService.Create(carriers);
                return RedirectToAction("Index");
            }
			ViewBag.ixCarrierType = new SelectList(_carriersService.selectCarrierTypes().Select( x => new { x.ixCarrierType, x.sCarrierType }), "ixCarrierType", "sCarrierType");

            return View(carriers);
        }

        // GET: Carriers/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CarriersPost carriers = _carriersService.GetPost(id);
            if (carriers == null)
            {
                return NotFound();
            }
			ViewBag.ixCarrierType = new SelectList(_carriersService.selectCarrierTypes().Select( x => new { x.ixCarrierType, x.sCarrierType }), "ixCarrierType", "sCarrierType", carriers.ixCarrierType);

            return View(carriers);
        }

        // POST: Carriers/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCarrier,sCarrier,ixCarrierType,sStandardCarrierAlphaCode,sCarrierConsignmentNumberPrefix,nCarrierConsignmentNumberStart,nCarrierConsignmentNumberLastUsed,dtScheduledPickupTime")] CarriersPost carriers)
        {
            if (ModelState.IsValid)
            {
                carriers.UserName = User.Identity.Name;
                _carriersService.Edit(carriers);
                return RedirectToAction("Index");
            }
			ViewBag.ixCarrierType = new SelectList(_carriersService.selectCarrierTypes().Select( x => new { x.ixCarrierType, x.sCarrierType }), "ixCarrierType", "sCarrierType", carriers.ixCarrierType);

            return View(carriers);
        }


        // GET: Carriers/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_carriersService.Get(id));
        }

        // POST: Carriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CarriersPost carriers = _carriersService.GetPost(id);
            carriers.UserName = User.Identity.Name;
            _carriersService.Delete(carriers);
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
            string sCarrier;

            CarriersPost carriers;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCarrier = _carriersService.Get(nID).sCarrier;
                            if (!_carriersService.VerifyCarrierDeleteOK(nID, sCarrier).Any())
                            {
                                carriers = _carriersService.GetPost(nID);
                                carriers.UserName = User.Identity.Name;
                                _carriersService.Delete(carriers);
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
        public IActionResult VerifyCarrier(long ixCarrier, string sCarrier)
        {
            string validationResponse = "";

            if (!_carriersService.VerifyCarrierUnique(ixCarrier, sCarrier))
            {
                validationResponse = $"Carrier {sCarrier} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

