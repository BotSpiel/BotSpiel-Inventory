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

    public class BusinessPartnerTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IBusinessPartnerTypesService _businesspartnertypesService;

        public BusinessPartnerTypesController(IBusinessPartnerTypesService businesspartnertypesService )
        {
            _businesspartnertypesService = businesspartnertypesService;
        }

        // GET: BusinessPartnerTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var businesspartnertypes = _businesspartnertypesService.Index();
            return View(businesspartnertypes.ToList());
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
            var businesspartnertypes = _businesspartnertypesService.Index();
            return PartialView("IndexGrid", businesspartnertypes.ToList());
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
                IGrid<BusinessPartnerTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<BusinessPartnerTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportBusinessPartnerTypes.xlsx");
            }
        }

        private IGrid<BusinessPartnerTypes> CreateExportableGrid()
        {
            IGrid<BusinessPartnerTypes> grid = new Grid<BusinessPartnerTypes>(_businesspartnertypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sBusinessPartnerType).Titled("Business Partner Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<BusinessPartnerTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: BusinessPartnerTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_businesspartnertypesService.Get(id));
        }

        // GET: BusinessPartnerTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: BusinessPartnerTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixBusinessPartnerType,sBusinessPartnerType")] BusinessPartnerTypesPost businesspartnertypes)
        {
            if (ModelState.IsValid)
            {
                businesspartnertypes.UserName = User.Identity.Name;
                _businesspartnertypesService.Create(businesspartnertypes);
                return RedirectToAction("Index");
            }

            return View(businesspartnertypes);
        }

        // GET: BusinessPartnerTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            BusinessPartnerTypesPost businesspartnertypes = _businesspartnertypesService.GetPost(id);
            if (businesspartnertypes == null)
            {
                return NotFound();
            }

            return View(businesspartnertypes);
        }

        // POST: BusinessPartnerTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixBusinessPartnerType,sBusinessPartnerType")] BusinessPartnerTypesPost businesspartnertypes)
        {
            if (ModelState.IsValid)
            {
                businesspartnertypes.UserName = User.Identity.Name;
                _businesspartnertypesService.Edit(businesspartnertypes);
                return RedirectToAction("Index");
            }

            return View(businesspartnertypes);
        }


        // GET: BusinessPartnerTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_businesspartnertypesService.Get(id));
        }

        // POST: BusinessPartnerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BusinessPartnerTypesPost businesspartnertypes = _businesspartnertypesService.GetPost(id);
            businesspartnertypes.UserName = User.Identity.Name;
            _businesspartnertypesService.Delete(businesspartnertypes);
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
            string sBusinessPartnerType;

            BusinessPartnerTypesPost businesspartnertypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sBusinessPartnerType = _businesspartnertypesService.Get(nID).sBusinessPartnerType;
                            if (!_businesspartnertypesService.VerifyBusinessPartnerTypeDeleteOK(nID, sBusinessPartnerType).Any())
                            {
                                businesspartnertypes = _businesspartnertypesService.GetPost(nID);
                                businesspartnertypes.UserName = User.Identity.Name;
                                _businesspartnertypesService.Delete(businesspartnertypes);
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
        public IActionResult VerifyBusinessPartnerType(long ixBusinessPartnerType, string sBusinessPartnerType)
        {
            string validationResponse = "";

            if (!_businesspartnertypesService.VerifyBusinessPartnerTypeUnique(ixBusinessPartnerType, sBusinessPartnerType))
            {
                validationResponse = $"BusinessPartnerType {sBusinessPartnerType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

