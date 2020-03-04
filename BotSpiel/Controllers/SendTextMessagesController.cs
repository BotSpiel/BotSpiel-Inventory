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

    public class SendTextMessagesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ISendTextMessagesService _sendtextmessagesService;

        public SendTextMessagesController(ISendTextMessagesService sendtextmessagesService )
        {
            _sendtextmessagesService = sendtextmessagesService;
        }

        // GET: SendTextMessages
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var sendtextmessages = _sendtextmessagesService.Index();
            return View(sendtextmessages.ToList());
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
            var sendtextmessages = _sendtextmessagesService.Index();
            return PartialView("IndexGrid", sendtextmessages.ToList());
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
                IGrid<SendTextMessages> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<SendTextMessages> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportSendTextMessages.xlsx");
            }
        }

        private IGrid<SendTextMessages> CreateExportableGrid()
        {
            IGrid<SendTextMessages> grid = new Grid<SendTextMessages>(_sendtextmessagesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sSendTextMessage).Titled("Send Text Message").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.People.sPerson).Titled("Person").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<SendTextMessages>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: SendTextMessages/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_sendtextmessagesService.Get(id));
        }

        // GET: SendTextMessages/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPerson = new SelectList(_sendtextmessagesService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson");

            return View();
        }

        // POST: SendTextMessages/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixSendTextMessage,sSendTextMessage,ixPerson,sContent")] SendTextMessagesPost sendtextmessages)
        {
            if (ModelState.IsValid)
            {
                sendtextmessages.UserName = User.Identity.Name;
                _sendtextmessagesService.Create(sendtextmessages);
                return RedirectToAction("Index");
            }
			ViewBag.ixPerson = new SelectList(_sendtextmessagesService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson");

            return View(sendtextmessages);
        }

        // GET: SendTextMessages/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            SendTextMessagesPost sendtextmessages = _sendtextmessagesService.GetPost(id);
            if (sendtextmessages == null)
            {
                return NotFound();
            }
			ViewBag.ixPerson = new SelectList(_sendtextmessagesService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson", sendtextmessages.ixPerson);

            return View(sendtextmessages);
        }

        // POST: SendTextMessages/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixSendTextMessage,sSendTextMessage,ixPerson,sContent")] SendTextMessagesPost sendtextmessages)
        {
            if (ModelState.IsValid)
            {
                sendtextmessages.UserName = User.Identity.Name;
                _sendtextmessagesService.Edit(sendtextmessages);
                return RedirectToAction("Index");
            }
			ViewBag.ixPerson = new SelectList(_sendtextmessagesService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson", sendtextmessages.ixPerson);

            return View(sendtextmessages);
        }


        // GET: SendTextMessages/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_sendtextmessagesService.Get(id));
        }

        // POST: SendTextMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SendTextMessagesPost sendtextmessages = _sendtextmessagesService.GetPost(id);
            sendtextmessages.UserName = User.Identity.Name;
            _sendtextmessagesService.Delete(sendtextmessages);
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
            string sSendTextMessage;

            SendTextMessagesPost sendtextmessages;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sSendTextMessage = _sendtextmessagesService.Get(nID).sSendTextMessage;
                            if (!_sendtextmessagesService.VerifySendTextMessageDeleteOK(nID, sSendTextMessage).Any())
                            {
                                sendtextmessages = _sendtextmessagesService.GetPost(nID);
                                sendtextmessages.UserName = User.Identity.Name;
                                _sendtextmessagesService.Delete(sendtextmessages);
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
        public IActionResult VerifySendTextMessage(long ixSendTextMessage, string sSendTextMessage)
        {
            string validationResponse = "";

            if (!_sendtextmessagesService.VerifySendTextMessageUnique(ixSendTextMessage, sSendTextMessage))
            {
                validationResponse = $"SendTextMessage {sSendTextMessage} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

