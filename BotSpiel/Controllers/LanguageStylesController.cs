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

    public class LanguageStylesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ILanguageStylesService _languagestylesService;

        public LanguageStylesController(ILanguageStylesService languagestylesService )
        {
            _languagestylesService = languagestylesService;
        }

        // GET: LanguageStyles
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var languagestyles = _languagestylesService.Index();
            return View(languagestyles.ToList());
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
            var languagestyles = _languagestylesService.Index();
            return PartialView("IndexGrid", languagestyles.ToList());
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
                IGrid<LanguageStyles> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<LanguageStyles> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportLanguageStyles.xlsx");
            }
        }

        private IGrid<LanguageStyles> CreateExportableGrid()
        {
            IGrid<LanguageStyles> grid = new Grid<LanguageStyles>(_languagestylesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sLanguageStyle).Titled("Language Style").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<LanguageStyles>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: LanguageStyles/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_languagestylesService.Get(id));
        }

        // GET: LanguageStyles/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: LanguageStyles/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixLanguageStyle,sLanguageStyle")] LanguageStylesPost languagestyles)
        {
            if (ModelState.IsValid)
            {
                languagestyles.UserName = User.Identity.Name;
                _languagestylesService.Create(languagestyles);
                return RedirectToAction("Index");
            }

            return View(languagestyles);
        }

        // GET: LanguageStyles/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            LanguageStylesPost languagestyles = _languagestylesService.GetPost(id);
            if (languagestyles == null)
            {
                return NotFound();
            }

            return View(languagestyles);
        }

        // POST: LanguageStyles/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixLanguageStyle,sLanguageStyle")] LanguageStylesPost languagestyles)
        {
            if (ModelState.IsValid)
            {
                languagestyles.UserName = User.Identity.Name;
                _languagestylesService.Edit(languagestyles);
                return RedirectToAction("Index");
            }

            return View(languagestyles);
        }


        // GET: LanguageStyles/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_languagestylesService.Get(id));
        }

        // POST: LanguageStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LanguageStylesPost languagestyles = _languagestylesService.GetPost(id);
            languagestyles.UserName = User.Identity.Name;
            _languagestylesService.Delete(languagestyles);
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
            string sLanguageStyle;

            LanguageStylesPost languagestyles;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sLanguageStyle = _languagestylesService.Get(nID).sLanguageStyle;
                            if (!_languagestylesService.VerifyLanguageStyleDeleteOK(nID, sLanguageStyle).Any())
                            {
                                languagestyles = _languagestylesService.GetPost(nID);
                                languagestyles.UserName = User.Identity.Name;
                                _languagestylesService.Delete(languagestyles);
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
        public IActionResult VerifyLanguageStyle(long ixLanguageStyle, string sLanguageStyle)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

