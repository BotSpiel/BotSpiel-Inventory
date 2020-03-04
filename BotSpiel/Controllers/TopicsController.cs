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

    public class TopicsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ITopicsService _topicsService;

        public TopicsController(ITopicsService topicsService )
        {
            _topicsService = topicsService;
        }

        // GET: Topics
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var topics = _topicsService.Index();
            return View(topics.ToList());
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
            var topics = _topicsService.Index();
            return PartialView("IndexGrid", topics.ToList());
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
                IGrid<Topics> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Topics> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportTopics.xlsx");
            }
        }

        private IGrid<Topics> CreateExportableGrid()
        {
            IGrid<Topics> grid = new Grid<Topics>(_topicsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sTopic).Titled("Topic").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Topics>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Topics/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_topicsService.Get(id));
        }

        // GET: Topics/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Topics/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixTopic,sTopic")] TopicsPost topics)
        {
            if (ModelState.IsValid)
            {
                topics.UserName = User.Identity.Name;
                _topicsService.Create(topics);
                return RedirectToAction("Index");
            }

            return View(topics);
        }

        // GET: Topics/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            TopicsPost topics = _topicsService.GetPost(id);
            if (topics == null)
            {
                return NotFound();
            }

            return View(topics);
        }

        // POST: Topics/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixTopic,sTopic")] TopicsPost topics)
        {
            if (ModelState.IsValid)
            {
                topics.UserName = User.Identity.Name;
                _topicsService.Edit(topics);
                return RedirectToAction("Index");
            }

            return View(topics);
        }


        // GET: Topics/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_topicsService.Get(id));
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TopicsPost topics = _topicsService.GetPost(id);
            topics.UserName = User.Identity.Name;
            _topicsService.Delete(topics);
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
            string sTopic;

            TopicsPost topics;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sTopic = _topicsService.Get(nID).sTopic;
                            if (!_topicsService.VerifyTopicDeleteOK(nID, sTopic).Any())
                            {
                                topics = _topicsService.GetPost(nID);
                                topics.UserName = User.Identity.Name;
                                _topicsService.Delete(topics);
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
        public IActionResult VerifyTopic(long ixTopic, string sTopic)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

