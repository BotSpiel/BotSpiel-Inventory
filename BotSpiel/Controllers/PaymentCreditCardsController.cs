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

    public class PaymentCreditCardsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPaymentCreditCardsService _paymentcreditcardsService;

        public PaymentCreditCardsController(IPaymentCreditCardsService paymentcreditcardsService )
        {
            _paymentcreditcardsService = paymentcreditcardsService;
        }

        // GET: PaymentCreditCards
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var paymentcreditcards = _paymentcreditcardsService.Index();
            return View(paymentcreditcards.ToList());
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
            var paymentcreditcards = _paymentcreditcardsService.Index();
            return PartialView("IndexGrid", paymentcreditcards.ToList());
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
                IGrid<PaymentCreditCards> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PaymentCreditCards> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPaymentCreditCards.xlsx");
            }
        }

        private IGrid<PaymentCreditCards> CreateExportableGrid()
        {
            IGrid<PaymentCreditCards> grid = new Grid<PaymentCreditCards>(_paymentcreditcardsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPaymentCreditCard).Titled("Payment Credit Card").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sCreditCardType).Titled("Credit Card Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sFirstName).Titled("First Name").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sLastName).Titled("Last Name").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.nExpireMonth).Titled("Expire Month").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nExpireYear).Titled("Expire Year").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCvvTwo).Titled("Cvv Two").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PaymentCreditCards>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PaymentCreditCards/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_paymentcreditcardsService.Get(id));
        }

        // GET: PaymentCreditCards/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: PaymentCreditCards/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPaymentCreditCard,sPaymentCreditCard,sCreditCardType,sFirstName,sLastName,nExpireMonth,nExpireYear,sCvvTwo")] PaymentCreditCardsPost paymentcreditcards)
        {
            if (ModelState.IsValid)
            {
                paymentcreditcards.UserName = User.Identity.Name;
                _paymentcreditcardsService.Create(paymentcreditcards);
                return RedirectToAction("Index");
            }

            return View(paymentcreditcards);
        }

        // GET: PaymentCreditCards/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PaymentCreditCardsPost paymentcreditcards = _paymentcreditcardsService.GetPost(id);
            if (paymentcreditcards == null)
            {
                return NotFound();
            }

            return View(paymentcreditcards);
        }

        // POST: PaymentCreditCards/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPaymentCreditCard,sPaymentCreditCard,sCreditCardType,sFirstName,sLastName,nExpireMonth,nExpireYear,sCvvTwo")] PaymentCreditCardsPost paymentcreditcards)
        {
            if (ModelState.IsValid)
            {
                paymentcreditcards.UserName = User.Identity.Name;
                _paymentcreditcardsService.Edit(paymentcreditcards);
                return RedirectToAction("Index");
            }

            return View(paymentcreditcards);
        }


        // GET: PaymentCreditCards/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_paymentcreditcardsService.Get(id));
        }

        // POST: PaymentCreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PaymentCreditCardsPost paymentcreditcards = _paymentcreditcardsService.GetPost(id);
            paymentcreditcards.UserName = User.Identity.Name;
            _paymentcreditcardsService.Delete(paymentcreditcards);
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
            string sPaymentCreditCard;

            PaymentCreditCardsPost paymentcreditcards;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPaymentCreditCard = _paymentcreditcardsService.Get(nID).sPaymentCreditCard;
                            if (!_paymentcreditcardsService.VerifyPaymentCreditCardDeleteOK(nID, sPaymentCreditCard).Any())
                            {
                                paymentcreditcards = _paymentcreditcardsService.GetPost(nID);
                                paymentcreditcards.UserName = User.Identity.Name;
                                _paymentcreditcardsService.Delete(paymentcreditcards);
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
        public IActionResult VerifyPaymentCreditCard(long ixPaymentCreditCard, string sPaymentCreditCard)
        {
            string validationResponse = "";

            if (!_paymentcreditcardsService.VerifyPaymentCreditCardUnique(ixPaymentCreditCard, sPaymentCreditCard))
            {
                validationResponse = $"PaymentCreditCard {sPaymentCreditCard} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

