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

    public class PeopleController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService )
        {
            _peopleService = peopleService;
        }

        // GET: People
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var people = _peopleService.Index();
            return View(people.ToList());
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
            var people = _peopleService.Index();
            return PartialView("IndexGrid", people.ToList());
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
                IGrid<People> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<People> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPeople.xlsx");
            }
        }

        private IGrid<People> CreateExportableGrid()
        {
            IGrid<People> grid = new Grid<People>(_peopleService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPerson).Titled("Person").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sFirstName).Titled("First Name").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sLastName).Titled("Last Name").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Languages.sLanguage).Titled("Language").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<People>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: People/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_peopleService.Get(id));
        }

        // GET: People/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixLanguage = new SelectList(_peopleService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");

            return View();
        }

        // POST: People/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPerson,sPerson,sFirstName,sLastName,ixLanguage")] PeoplePost people)
        {
            if (ModelState.IsValid)
            {
                people.UserName = User.Identity.Name;
                _peopleService.Create(people);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_peopleService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");

            return View(people);
        }

        // GET: People/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PeoplePost people = _peopleService.GetPost(id);
            if (people == null)
            {
                return NotFound();
            }
			ViewBag.ixLanguage = new SelectList(_peopleService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", people.ixLanguage);

            return View(people);
        }

        // POST: People/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPerson,sPerson,sFirstName,sLastName,ixLanguage")] PeoplePost people)
        {
            if (ModelState.IsValid)
            {
                people.UserName = User.Identity.Name;
                _peopleService.Edit(people);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_peopleService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", people.ixLanguage);

            return View(people);
        }


        // GET: People/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_peopleService.Get(id));
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PeoplePost people = _peopleService.GetPost(id);
            people.UserName = User.Identity.Name;
            _peopleService.Delete(people);
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
            string sPerson;

            PeoplePost people;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPerson = _peopleService.Get(nID).sPerson;
                            if (!_peopleService.VerifyPersonDeleteOK(nID, sPerson).Any())
                            {
                                people = _peopleService.GetPost(nID);
                                people.UserName = User.Identity.Name;
                                _peopleService.Delete(people);
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
        public IActionResult VerifyPerson(long ixPerson, string sPerson)
        {
            string validationResponse = "";

            if (!_peopleService.VerifyPersonUnique(ixPerson, sPerson))
            {
                validationResponse = $"Person {sPerson} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

