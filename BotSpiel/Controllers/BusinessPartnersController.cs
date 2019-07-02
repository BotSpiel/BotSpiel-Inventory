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

    public class BusinessPartnersController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IBusinessPartnersService _businesspartnersService;

        public BusinessPartnersController(IBusinessPartnersService businesspartnersService )
        {
            _businesspartnersService = businesspartnersService;
        }

        // GET: BusinessPartners
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var businesspartners = _businesspartnersService.Index();
            return View(businesspartners.ToList());
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
            var businesspartners = _businesspartnersService.Index();
            return PartialView("IndexGrid", businesspartners.ToList());
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
                IGrid<BusinessPartners> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<BusinessPartners> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportBusinessPartners.xlsx");
            }
        }

        private IGrid<BusinessPartners> CreateExportableGrid()
        {
            IGrid<BusinessPartners> grid = new Grid<BusinessPartners>(_businesspartnersService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sBusinessPartner).Titled("Business Partner").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.BusinessPartnerTypes.sBusinessPartnerType).Titled("Business Partner Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Companies.sCompany).Titled("Company").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Addresses.sAddress).Titled("Address").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<BusinessPartners>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: BusinessPartners/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_businesspartnersService.Get(id));
        }

        // GET: BusinessPartners/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixAddress = new SelectList(_businesspartnersService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress");
			ViewBag.ixBusinessPartnerType = new SelectList(_businesspartnersService.selectBusinessPartnerTypes().Select( x => new { x.ixBusinessPartnerType, x.sBusinessPartnerType }), "ixBusinessPartnerType", "sBusinessPartnerType");
			ViewBag.ixCompany = new SelectList(_businesspartnersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");

            return View();
        }

        // POST: BusinessPartners/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixBusinessPartner,sBusinessPartner,ixBusinessPartnerType,ixCompany,ixAddress")] BusinessPartnersPost businesspartners)
        {
            if (ModelState.IsValid)
            {
                businesspartners.UserName = User.Identity.Name;
                _businesspartnersService.Create(businesspartners);
                return RedirectToAction("Index");
            }
			ViewBag.ixAddress = new SelectList(_businesspartnersService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress");
			ViewBag.ixBusinessPartnerType = new SelectList(_businesspartnersService.selectBusinessPartnerTypes().Select( x => new { x.ixBusinessPartnerType, x.sBusinessPartnerType }), "ixBusinessPartnerType", "sBusinessPartnerType");
			ViewBag.ixCompany = new SelectList(_businesspartnersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany");

            return View(businesspartners);
        }

        // GET: BusinessPartners/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            BusinessPartnersPost businesspartners = _businesspartnersService.GetPost(id);
            if (businesspartners == null)
            {
                return NotFound();
            }
			ViewBag.ixAddress = new SelectList(_businesspartnersService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress", businesspartners.ixAddress);
			ViewBag.ixBusinessPartnerType = new SelectList(_businesspartnersService.selectBusinessPartnerTypes().Select( x => new { x.ixBusinessPartnerType, x.sBusinessPartnerType }), "ixBusinessPartnerType", "sBusinessPartnerType", businesspartners.ixBusinessPartnerType);
			ViewBag.ixCompany = new SelectList(_businesspartnersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", businesspartners.ixCompany);

            return View(businesspartners);
        }

        // POST: BusinessPartners/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixBusinessPartner,sBusinessPartner,ixBusinessPartnerType,ixCompany,ixAddress")] BusinessPartnersPost businesspartners)
        {
            if (ModelState.IsValid)
            {
                businesspartners.UserName = User.Identity.Name;
                _businesspartnersService.Edit(businesspartners);
                return RedirectToAction("Index");
            }
			ViewBag.ixAddress = new SelectList(_businesspartnersService.selectAddresses().Select( x => new { x.ixAddress, x.sAddress }), "ixAddress", "sAddress", businesspartners.ixAddress);
			ViewBag.ixBusinessPartnerType = new SelectList(_businesspartnersService.selectBusinessPartnerTypes().Select( x => new { x.ixBusinessPartnerType, x.sBusinessPartnerType }), "ixBusinessPartnerType", "sBusinessPartnerType", businesspartners.ixBusinessPartnerType);
			ViewBag.ixCompany = new SelectList(_businesspartnersService.selectCompanies().Select( x => new { x.ixCompany, x.sCompany }), "ixCompany", "sCompany", businesspartners.ixCompany);

            return View(businesspartners);
        }


        // GET: BusinessPartners/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_businesspartnersService.Get(id));
        }

        // POST: BusinessPartners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BusinessPartnersPost businesspartners = _businesspartnersService.GetPost(id);
            businesspartners.UserName = User.Identity.Name;
            _businesspartnersService.Delete(businesspartners);
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
            string sBusinessPartner;

            BusinessPartnersPost businesspartners;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sBusinessPartner = _businesspartnersService.Get(nID).sBusinessPartner;
                            if (!_businesspartnersService.VerifyBusinessPartnerDeleteOK(nID, sBusinessPartner).Any())
                            {
                                businesspartners = _businesspartnersService.GetPost(nID);
                                businesspartners.UserName = User.Identity.Name;
                                _businesspartnersService.Delete(businesspartners);
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
        public IActionResult VerifyBusinessPartner(long ixBusinessPartner, string sBusinessPartner)
        {
            string validationResponse = "";

            if (!_businesspartnersService.VerifyBusinessPartnerUnique(ixBusinessPartner, sBusinessPartner))
            {
                validationResponse = $"BusinessPartner {sBusinessPartner} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

