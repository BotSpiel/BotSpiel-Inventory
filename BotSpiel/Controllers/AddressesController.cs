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

    public class AddressesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IAddressesService _addressesService;

        public AddressesController(IAddressesService addressesService )
        {
            _addressesService = addressesService;
        }

        // GET: Addresses
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var addresses = _addressesService.Index();
            return View(addresses.ToList());
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
            var addresses = _addressesService.Index();
            return PartialView("IndexGrid", addresses.ToList());
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
                IGrid<Addresses> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Addresses> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportAddresses.xlsx");
            }
        }

        private IGrid<Addresses> CreateExportableGrid()
        {
            IGrid<Addresses> grid = new Grid<Addresses>(_addressesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sAddress).Titled("Address").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sStreetAndNumberOrPostOfficeBoxOne).Titled("Street And Number Or Post Office Box One").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sStreetAndNumberOrPostOfficeBoxTwo).Titled("Street And Number Or Post Office Box Two").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sStreetAndNumberOrPostOfficeBoxThree).Titled("Street And Number Or Post Office Box Three").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sCityOrSuburb).Titled("City Or Suburb").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sZipOrPostCode).Titled("Zip Or Post Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.CountrySubDivisionsFKDiffStateOrProvince.sCountrySubDivision).Titled("State Or Province").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Countries.sCountry).Titled("Country").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Addresses>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Addresses/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_addressesService.Get(id));
        }

        // GET: Addresses/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCountry = new SelectList(_addressesService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry");
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //ViewBag.ixStateOrProvince = new SelectList(_addressesService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision");
            //Replaced Code Block End
            ViewBag.ixStateOrProvince = new SelectList(_addressesService.selectEmptyCountrySubDivisionsDropdown().Select(x => new { ixCountrySubDivision = x.Key, sCountrySubDivision = x.Value }), "ixCountrySubDivision", "sCountrySubDivision");
            //Custom Code End
            return View();
        }

        // POST: Addresses/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixAddress,sAddress,sStreetAndNumberOrPostOfficeBoxOne,sStreetAndNumberOrPostOfficeBoxTwo,sStreetAndNumberOrPostOfficeBoxThree,sCityOrSuburb,sZipOrPostCode,ixStateOrProvince,ixCountry")] AddressesPost addresses)
        {
            if (ModelState.IsValid)
            {
                addresses.UserName = User.Identity.Name;
                _addressesService.Create(addresses);
                return RedirectToAction("Index");
            }
			ViewBag.ixCountry = new SelectList(_addressesService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry");
			ViewBag.ixStateOrProvince = new SelectList(_addressesService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision");

            return View(addresses);
        }

        // GET: Addresses/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            AddressesPost addresses = _addressesService.GetPost(id);
            if (addresses == null)
            {
                return NotFound();
            }
			ViewBag.ixCountry = new SelectList(_addressesService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry", addresses.ixCountry);
			ViewBag.ixStateOrProvince = new SelectList(_addressesService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision", addresses.ixStateOrProvince);

            return View(addresses);
        }

        // POST: Addresses/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixAddress,sAddress,sStreetAndNumberOrPostOfficeBoxOne,sStreetAndNumberOrPostOfficeBoxTwo,sStreetAndNumberOrPostOfficeBoxThree,sCityOrSuburb,sZipOrPostCode,ixStateOrProvince,ixCountry")] AddressesPost addresses)
        {
            if (ModelState.IsValid)
            {
                addresses.UserName = User.Identity.Name;
                _addressesService.Edit(addresses);
                return RedirectToAction("Index");
            }
			ViewBag.ixCountry = new SelectList(_addressesService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry", addresses.ixCountry);
			ViewBag.ixStateOrProvince = new SelectList(_addressesService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision", addresses.ixStateOrProvince);

            return View(addresses);
        }


        // GET: Addresses/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_addressesService.Get(id));
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AddressesPost addresses = _addressesService.GetPost(id);
            addresses.UserName = User.Identity.Name;
            _addressesService.Delete(addresses);
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
            string sAddress;

            AddressesPost addresses;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sAddress = _addressesService.Get(nID).sAddress;
                            if (!_addressesService.VerifyAddressDeleteOK(nID, sAddress).Any())
                            {
                                addresses = _addressesService.GetPost(nID);
                                addresses.UserName = User.Identity.Name;
                                _addressesService.Delete(addresses);
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
        public IActionResult VerifyAddress(long ixAddress, string sAddress)
        {
            string validationResponse = "";

            if (!_addressesService.VerifyAddressUnique(ixAddress, sAddress))
            {
                validationResponse = $"Address {sAddress} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }

        //Custom Code Start | Added Code Block 
        [AcceptVerbs("Get", "Post")]
        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult getCountrySubDivisionsForCountries(string Id)
        {

            var countrySubDivisionsForCountries = _addressesService.selectCountrySubDivisions().Where(x => x.ixCountry == Convert.ToInt64(Id)).OrderBy(x => x.sCountrySubDivision)
                .Select(x => new { x.ixCountrySubDivision, x.sCountrySubDivision }).Distinct();

            return Json(countrySubDivisionsForCountries);

        }
        //Custom Code End


    }
}
 
