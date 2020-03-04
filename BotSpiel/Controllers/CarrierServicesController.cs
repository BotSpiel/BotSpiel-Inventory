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

    public class CarrierServicesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICarrierServicesService _carrierservicesService;

        public CarrierServicesController(ICarrierServicesService carrierservicesService )
        {
            _carrierservicesService = carrierservicesService;
        }

        // GET: CarrierServices
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var carrierservices = _carrierservicesService.Index();
            return View(carrierservices.ToList());
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
            var carrierservices = _carrierservicesService.Index();
            return PartialView("IndexGrid", carrierservices.ToList());
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
                IGrid<CarrierServices> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<CarrierServices> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCarrierServices.xlsx");
            }
        }

        private IGrid<CarrierServices> CreateExportableGrid()
        {
            IGrid<CarrierServices> grid = new Grid<CarrierServices>(_carrierservicesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCarrierService).Titled("Carrier Service").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Carriers.sCarrier).Titled("Carrier").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<CarrierServices>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: CarrierServices/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_carrierservicesService.Get(id));
        }

        // GET: CarrierServices/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCarrier = new SelectList(_carrierservicesService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier");

            return View();
        }

        // POST: CarrierServices/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCarrierService,sCarrierService,ixCarrier")] CarrierServicesPost carrierservices)
        {
            if (ModelState.IsValid)
            {
                carrierservices.UserName = User.Identity.Name;
                _carrierservicesService.Create(carrierservices);
                return RedirectToAction("Index");
            }
			ViewBag.ixCarrier = new SelectList(_carrierservicesService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier");

            return View(carrierservices);
        }

        //Custom Code Start | Added Code Block 
        [Authorize]
        [HttpGet]
        public ActionResult CreateWithID(long id)
        {
            ViewBag.ixCarrier = new SelectList(_carrierservicesService.selectCarriers().Where(x => x.ixCarrier == id).Select(x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier");

            return View();
        }

        // POST: CarrierServices/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithID([Bind("ixCarrierService,sCarrierService,ixCarrier")] CarrierServicesPost carrierservices)
        {
            if (ModelState.IsValid)
            {
                carrierservices.UserName = User.Identity.Name;
                _carrierservicesService.Create(carrierservices);
                return RedirectToAction("Edit", "Carriers", new { id = carrierservices.ixCarrier });
            }
            ViewBag.ixCarrier = new SelectList(_carrierservicesService.selectCarriers().Where(x => x.ixCarrier == carrierservices.ixCarrier).Select(x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier");

            return View(carrierservices);
        }
        //Custom Code End


        // GET: CarrierServices/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CarrierServicesPost carrierservices = _carrierservicesService.GetPost(id);
            if (carrierservices == null)
            {
                return NotFound();
            }
			ViewBag.ixCarrier = new SelectList(_carrierservicesService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier", carrierservices.ixCarrier);

            return View(carrierservices);
        }

        // POST: CarrierServices/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCarrierService,sCarrierService,ixCarrier")] CarrierServicesPost carrierservices)
        {
            if (ModelState.IsValid)
            {
                carrierservices.UserName = User.Identity.Name;
                _carrierservicesService.Edit(carrierservices);
                return RedirectToAction("Index");
            }
			ViewBag.ixCarrier = new SelectList(_carrierservicesService.selectCarriers().Select( x => new { x.ixCarrier, x.sCarrier }), "ixCarrier", "sCarrier", carrierservices.ixCarrier);

            return View(carrierservices);
        }


        // GET: CarrierServices/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_carrierservicesService.Get(id));
        }

        // POST: CarrierServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CarrierServicesPost carrierservices = _carrierservicesService.GetPost(id);
            carrierservices.UserName = User.Identity.Name;
            _carrierservicesService.Delete(carrierservices);
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
            string sCarrierService;

            CarrierServicesPost carrierservices;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCarrierService = _carrierservicesService.Get(nID).sCarrierService;
                            if (!_carrierservicesService.VerifyCarrierServiceDeleteOK(nID, sCarrierService).Any())
                            {
                                carrierservices = _carrierservicesService.GetPost(nID);
                                carrierservices.UserName = User.Identity.Name;
                                _carrierservicesService.Delete(carrierservices);
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
        public IActionResult VerifyCarrierService(long ixCarrierService, string sCarrierService)
        {
            string validationResponse = "";

            if (!_carrierservicesService.VerifyCarrierServiceUnique(ixCarrierService, sCarrierService))
            {
                validationResponse = $"CarrierService {sCarrierService} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

