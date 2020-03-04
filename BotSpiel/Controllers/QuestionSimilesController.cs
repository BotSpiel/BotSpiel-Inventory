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

    public class QuestionSimilesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IQuestionSimilesService _questionsimilesService;

        public QuestionSimilesController(IQuestionSimilesService questionsimilesService )
        {
            _questionsimilesService = questionsimilesService;
        }

        // GET: QuestionSimiles
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var questionsimiles = _questionsimilesService.Index();
            return View(questionsimiles.ToList());
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
            var questionsimiles = _questionsimilesService.Index();
            return PartialView("IndexGrid", questionsimiles.ToList());
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
                IGrid<QuestionSimiles> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<QuestionSimiles> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportQuestionSimiles.xlsx");
            }
        }

        private IGrid<QuestionSimiles> CreateExportableGrid()
        {
            IGrid<QuestionSimiles> grid = new Grid<QuestionSimiles>(_questionsimilesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sQuestionSimile).Titled("Question Simile").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Questions.sQuestion).Titled("Question").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<QuestionSimiles>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: QuestionSimiles/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_questionsimilesService.Get(id));
        }

        // GET: QuestionSimiles/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixQuestion = new SelectList(_questionsimilesService.selectQuestions().Select( x => new { x.ixQuestion, x.sQuestion }), "ixQuestion", "sQuestion");

            return View();
        }

        // POST: QuestionSimiles/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixQuestionSimile,sQuestionSimile,ixQuestion,sQuestionSimileText")] QuestionSimilesPost questionsimiles)
        {
            if (ModelState.IsValid)
            {
                questionsimiles.UserName = User.Identity.Name;
                _questionsimilesService.Create(questionsimiles);
                return RedirectToAction("Index");
            }
			ViewBag.ixQuestion = new SelectList(_questionsimilesService.selectQuestions().Select( x => new { x.ixQuestion, x.sQuestion }), "ixQuestion", "sQuestion");

            return View(questionsimiles);
        }

        // GET: QuestionSimiles/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            QuestionSimilesPost questionsimiles = _questionsimilesService.GetPost(id);
            if (questionsimiles == null)
            {
                return NotFound();
            }
			ViewBag.ixQuestion = new SelectList(_questionsimilesService.selectQuestions().Select( x => new { x.ixQuestion, x.sQuestion }), "ixQuestion", "sQuestion", questionsimiles.ixQuestion);

            return View(questionsimiles);
        }

        // POST: QuestionSimiles/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixQuestionSimile,sQuestionSimile,ixQuestion,sQuestionSimileText")] QuestionSimilesPost questionsimiles)
        {
            if (ModelState.IsValid)
            {
                questionsimiles.UserName = User.Identity.Name;
                _questionsimilesService.Edit(questionsimiles);
                return RedirectToAction("Index");
            }
			ViewBag.ixQuestion = new SelectList(_questionsimilesService.selectQuestions().Select( x => new { x.ixQuestion, x.sQuestion }), "ixQuestion", "sQuestion", questionsimiles.ixQuestion);

            return View(questionsimiles);
        }


        // GET: QuestionSimiles/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_questionsimilesService.Get(id));
        }

        // POST: QuestionSimiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            QuestionSimilesPost questionsimiles = _questionsimilesService.GetPost(id);
            questionsimiles.UserName = User.Identity.Name;
            _questionsimilesService.Delete(questionsimiles);
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
            string sQuestionSimile;

            QuestionSimilesPost questionsimiles;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sQuestionSimile = _questionsimilesService.Get(nID).sQuestionSimile;
                            if (!_questionsimilesService.VerifyQuestionSimileDeleteOK(nID, sQuestionSimile).Any())
                            {
                                questionsimiles = _questionsimilesService.GetPost(nID);
                                questionsimiles.UserName = User.Identity.Name;
                                _questionsimilesService.Delete(questionsimiles);
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
        public IActionResult VerifyQuestionSimile(long ixQuestionSimile, string sQuestionSimile)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

