using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BotSpiel.DataAccess.Data;
using BotSpiel.DataAccess.Models;
using BotSpiel.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using NonFactors.Mvc.Grid;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

using Microsoft.AspNetCore.Identity;

namespace BotSpiel
{

    public class BotspielBotMessagesController : Controller
    {

        /*
        -- =============================================
        -- Author:		<BotSpiel>

        -- Description:	<Description>

        This class ....

        */

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _currentUserName;

        private readonly IBotspielBotMessagesService _botspielbotmessagesService;

        private readonly BotSpielBotAdapter _adapter;
        private BotCallbackHandler _callback;

        private readonly IBot _botSpielBot;

        Activity _activityBotResponse;

        public BotspielBotMessagesController(IBotspielBotMessagesService botspielbotmessagesService, BotSpielBotAdapter adapter, IBot botSpielBot, IHttpContextAccessor httpContextAccessor, BotCallbackHandler callback = null)
        {
            _botspielbotmessagesService = botspielbotmessagesService;
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException("httpContextAccessor cannot be null");
            _currentUserName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            _adapter = adapter ?? throw new System.ArgumentNullException("adapter cannot be null");
            _callback = callback;
            _botSpielBot = botSpielBot ?? throw new System.ArgumentNullException("bot cannot be null");

            _adapter.UpdateConversationUser(_currentUserName);

        }

        // GET: BotspielBotMessages
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var botspielbotmessages = _botspielbotmessagesService.Index();
            return View(botspielbotmessages.ToList());
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
            var botspielbotmessages = _botspielbotmessagesService.Index();
            return PartialView("IndexGrid", botspielbotmessages.ToList());
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
                IGrid<BotspielBotMessages> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<BotspielBotMessages> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportBotspielBotMessages.xlsx");
            }
        }

        private IGrid<BotspielBotMessages> CreateExportableGrid()
        {
            IGrid<BotspielBotMessages> grid = new Grid<BotspielBotMessages>(_botspielbotmessagesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sBotspielBotMessage).Titled("Botspiel Bot Message").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sMyMessage).Titled("My Message").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sYourReply).Titled("Your Reply").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<BotspielBotMessages>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: BotspielBotMessages/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_botspielbotmessagesService.Get(id));
        }

        // GET: BotspielBotMessages/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create(string botReply = null)
        {
            BotspielBotMessagesPost botspielbotmessages = new BotspielBotMessagesPost();
            botspielbotmessages.sMyMessage = botReply;

            if (botspielbotmessages.sMyMessage == null)
            {
                botspielbotmessages.sMyMessage = "Hello. Please type something to get us started.";
            }

            return View(botspielbotmessages);
        }

        // POST: BotspielBotMessages/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ixBotspielBotMessage,sBotspielBotMessage,sMyMessage,sYourReply")] BotspielBotMessagesPost botspielbotmessages)
        {
            long ixBotspielBotMessage;
            string sMyMessage = "";

            if (ModelState.IsValid)
            {
                botspielbotmessages.UserName = User.Identity.Name;
                ixBotspielBotMessage = await _botspielbotmessagesService.Create(botspielbotmessages);

                _adapter.SendTextToBotAsync(botspielbotmessages.sYourReply, ixBotspielBotMessage.ToString(),
                        async (turnContext, cancellationToken) => await _botSpielBot.OnTurnAsync(turnContext), default(CancellationToken)).Wait();

                _activityBotResponse = _adapter.GetNextReply();

                while (_activityBotResponse != null)
                {
                    sMyMessage += _activityBotResponse.Text;
                    _activityBotResponse = _adapter.GetNextReply();
                }

                return RedirectToAction("Create", new { botReply = sMyMessage });
            }

            return View(botspielbotmessages);
        }

        // GET: BotspielBotMessages/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            BotspielBotMessagesPost botspielbotmessages = _botspielbotmessagesService.GetPost(id);
            if (botspielbotmessages == null)
            {
                return NotFound();
            }

            return View(botspielbotmessages);
        }

        // POST: BotspielBotMessages/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixBotspielBotMessage,sBotspielBotMessage,sMyMessage,sYourReply")] BotspielBotMessagesPost botspielbotmessages)
        {
            if (ModelState.IsValid)
            {
                botspielbotmessages.UserName = User.Identity.Name;
                _botspielbotmessagesService.Edit(botspielbotmessages);
                return RedirectToAction("Index");
            }

            return View(botspielbotmessages);
        }


        // GET: BotspielBotMessages/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_botspielbotmessagesService.Get(id));
        }

        // POST: BotspielBotMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BotspielBotMessagesPost botspielbotmessages = _botspielbotmessagesService.GetPost(id);
            botspielbotmessages.UserName = User.Identity.Name;
            _botspielbotmessagesService.Delete(botspielbotmessages);
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
            string sBotspielBotMessage;

            BotspielBotMessagesPost botspielbotmessages;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sBotspielBotMessage = _botspielbotmessagesService.Get(nID).sBotspielBotMessage;
                            if (!_botspielbotmessagesService.VerifyBotspielBotMessageDeleteOK(nID, sBotspielBotMessage).Any())
                            {
                                botspielbotmessages = _botspielbotmessagesService.GetPost(nID);
                                botspielbotmessages.UserName = User.Identity.Name;
                                _botspielbotmessagesService.Delete(botspielbotmessages);
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
        public IActionResult VerifyBotspielBotMessage(long ixBotspielBotMessage, string sBotspielBotMessage)
        {
            string validationResponse = "";

            if (!_botspielbotmessagesService.VerifyBotspielBotMessageUnique(ixBotspielBotMessage, sBotspielBotMessage))
            {
                validationResponse = $"BotspielBotMessage {sBotspielBotMessage} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

