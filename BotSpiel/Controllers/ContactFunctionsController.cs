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

    public class ContactFunctionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IContactFunctionsService _contactfunctionsService;

        public ContactFunctionsController(IContactFunctionsService contactfunctionsService )
        {
            _contactfunctionsService = contactfunctionsService;
        }

        // GET: ContactFunctions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var contactfunctions = _contactfunctionsService.Index();
            return View(contactfunctions.ToList());
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
            var contactfunctions = _contactfunctionsService.Index();
            return PartialView("IndexGrid", contactfunctions.ToList());
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
                IGrid<ContactFunctions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<ContactFunctions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportContactFunctions.xlsx");
            }
        }

        private IGrid<ContactFunctions> CreateExportableGrid()
        {
            IGrid<ContactFunctions> grid = new Grid<ContactFunctions>(_contactfunctionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sContactFunction).Titled("Contact Function").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sContactFunctionCode).Titled("Contact Function Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<ContactFunctions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: ContactFunctions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_contactfunctionsService.Get(id));
        }

        // GET: ContactFunctions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: ContactFunctions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixContactFunction,sContactFunction,sContactFunctionCode")] ContactFunctionsPost contactfunctions)
        {
            if (ModelState.IsValid)
            {
                contactfunctions.UserName = User.Identity.Name;
                _contactfunctionsService.Create(contactfunctions);
                return RedirectToAction("Index");
            }

            return View(contactfunctions);
        }

        // GET: ContactFunctions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ContactFunctionsPost contactfunctions = _contactfunctionsService.GetPost(id);
            if (contactfunctions == null)
            {
                return NotFound();
            }

            return View(contactfunctions);
        }

        // POST: ContactFunctions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixContactFunction,sContactFunction,sContactFunctionCode")] ContactFunctionsPost contactfunctions)
        {
            if (ModelState.IsValid)
            {
                contactfunctions.UserName = User.Identity.Name;
                _contactfunctionsService.Edit(contactfunctions);
                return RedirectToAction("Index");
            }

            return View(contactfunctions);
        }


        // GET: ContactFunctions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_contactfunctionsService.Get(id));
        }

        // POST: ContactFunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ContactFunctionsPost contactfunctions = _contactfunctionsService.GetPost(id);
            contactfunctions.UserName = User.Identity.Name;
            _contactfunctionsService.Delete(contactfunctions);
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
            string sContactFunction;

            ContactFunctionsPost contactfunctions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sContactFunction = _contactfunctionsService.Get(nID).sContactFunction;
                            if (!_contactfunctionsService.VerifyContactFunctionDeleteOK(nID, sContactFunction).Any())
                            {
                                contactfunctions = _contactfunctionsService.GetPost(nID);
                                contactfunctions.UserName = User.Identity.Name;
                                _contactfunctionsService.Delete(contactfunctions);
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
        public IActionResult VerifyContactFunction(long ixContactFunction, string sContactFunction)
        {
            string validationResponse = "";

            if (!_contactfunctionsService.VerifyContactFunctionUnique(ixContactFunction, sContactFunction))
            {
                validationResponse = $"ContactFunction {sContactFunction} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

