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

    public class CompaniesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService )
        {
            _companiesService = companiesService;
        }

        // GET: Companies
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var companies = _companiesService.Index();
            return View(companies.ToList());
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
            var companies = _companiesService.Index();
            return PartialView("IndexGrid", companies.ToList());
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
                IGrid<Companies> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Companies> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCompanies.xlsx");
            }
        }

        private IGrid<Companies> CreateExportableGrid()
        {
            IGrid<Companies> grid = new Grid<Companies>(_companiesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCompany).Titled("Company").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Companies>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Companies/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_companiesService.Get(id));
        }

        // GET: Companies/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Companies/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCompany,sCompany")] CompaniesPost companies)
        {
            if (ModelState.IsValid)
            {
                companies.UserName = User.Identity.Name;
                _companiesService.Create(companies);
                return RedirectToAction("Index");
            }

            return View(companies);
        }

        // GET: Companies/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CompaniesPost companies = _companiesService.GetPost(id);
            if (companies == null)
            {
                return NotFound();
            }

            return View(companies);
        }

        // POST: Companies/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCompany,sCompany")] CompaniesPost companies)
        {
            if (ModelState.IsValid)
            {
                companies.UserName = User.Identity.Name;
                _companiesService.Edit(companies);
                return RedirectToAction("Index");
            }

            return View(companies);
        }


        // GET: Companies/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_companiesService.Get(id));
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CompaniesPost companies = _companiesService.GetPost(id);
            companies.UserName = User.Identity.Name;
            _companiesService.Delete(companies);
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
            string sCompany;

            CompaniesPost companies;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCompany = _companiesService.Get(nID).sCompany;
                            if (!_companiesService.VerifyCompanyDeleteOK(nID, sCompany).Any())
                            {
                                companies = _companiesService.GetPost(nID);
                                companies.UserName = User.Identity.Name;
                                _companiesService.Delete(companies);
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
        public IActionResult VerifyCompany(long ixCompany, string sCompany)
        {
            string validationResponse = "";

            if (!_companiesService.VerifyCompanyUnique(ixCompany, sCompany))
            {
                validationResponse = $"Company {sCompany} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

