using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundOrdersService : IOutboundOrdersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundOrdersRepository _outboundordersRepository;
        //Custom Code Start | Added Code Block 
        private readonly IPickBatchesService _pickbatchesService;
        private readonly IOutboundShipmentsService _outboundshipmentsService;
        private readonly ICarriersService _carriersService;
        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        //Custom Code End
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public OutboundOrdersService(IOutboundOrdersRepository outboundordersRepository)
        //Replaced Code Block End
        public OutboundOrdersService(IOutboundOrdersRepository outboundordersRepository, IPickBatchesService pickbatchesService, IOutboundShipmentsService outboundshipmentsService, ICarriersService carriersService, IOutboundOrderLinesService outboundorderlinesService)
        //Custom Code End
        {
            _outboundordersRepository = outboundordersRepository;
            //Custom Code Start | Added Code Block 
            _pickbatchesService = pickbatchesService;
            _outboundshipmentsService = outboundshipmentsService;
            _carriersService = carriersService;
            _outboundorderlinesService = outboundorderlinesService;
            //Custom Code End
        }

        public OutboundOrdersPost GetPost(Int64 ixOutboundOrder) => _outboundordersRepository.GetPost(ixOutboundOrder);
        public OutboundOrders Get(Int64 ixOutboundOrder) => _outboundordersRepository.Get(ixOutboundOrder);

        public IQueryable<OutboundOrders> Index() => _outboundordersRepository.Index();
        public IQueryable<OutboundOrders> IndexDb() => _outboundordersRepository.IndexDb();
        //Custom Code Start | Added Code Block 
        public IQueryable<OutboundOrdersPost> IndexDbPost() => _outboundordersRepository.IndexDbPost();
        //Custom Code End

        public IQueryable<Statuses> selectStatuses() => _outboundordersRepository.selectStatuses();
        public IQueryable<Companies> selectCompanies() => _outboundordersRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _outboundordersRepository.selectFacilities();
        public IQueryable<BusinessPartners> selectBusinessPartners() => _outboundordersRepository.selectBusinessPartners();
        public IQueryable<OutboundOrderTypes> selectOutboundOrderTypes() => _outboundordersRepository.selectOutboundOrderTypes();
        public IQueryable<CarrierServices> selectCarrierServices() => _outboundordersRepository.selectCarrierServices();
        public IQueryable<OutboundShipments> selectOutboundShipments() => _outboundordersRepository.selectOutboundShipments();
        public IQueryable<PickBatches> selectPickBatches() => _outboundordersRepository.selectPickBatches();
       public IQueryable<Statuses> StatusesDb() => _outboundordersRepository.StatusesDb();
        public IQueryable<Companies> CompaniesDb() => _outboundordersRepository.CompaniesDb();
        public IQueryable<Facilities> FacilitiesDb() => _outboundordersRepository.FacilitiesDb();
        public IQueryable<BusinessPartners> BusinessPartnersDb() => _outboundordersRepository.BusinessPartnersDb();
        public IQueryable<OutboundOrderTypes> OutboundOrderTypesDb() => _outboundordersRepository.OutboundOrderTypesDb();
        public IQueryable<CarrierServices> CarrierServicesDb() => _outboundordersRepository.CarrierServicesDb();
        public IQueryable<OutboundShipments> OutboundShipmentsDb() => _outboundordersRepository.OutboundShipmentsDb();
        public IQueryable<PickBatches> PickBatchesDb() => _outboundordersRepository.PickBatchesDb();
       public List<KeyValuePair<Int64?, string>> selectOutboundShipmentsNullable() => _outboundordersRepository.selectOutboundShipmentsNullable();
        public List<KeyValuePair<Int64?, string>> selectPickBatchesNullable() => _outboundordersRepository.selectPickBatchesNullable();
        public bool VerifyOutboundOrderUnique(Int64 ixOutboundOrder, string sOutboundOrder) => _outboundordersRepository.VerifyOutboundOrderUnique(ixOutboundOrder, sOutboundOrder);
        public List<string> VerifyOutboundOrderDeleteOK(Int64 ixOutboundOrder, string sOutboundOrder) => _outboundordersRepository.VerifyOutboundOrderDeleteOK(ixOutboundOrder, sOutboundOrder);

        public Task<Int64> Create(OutboundOrdersPost outboundordersPost)
        {
            // Additional validations

            // Pre-process
            //Custom Code Start | Added Code Block 

            //We batch and consign the order
            //As a simple placeholder we use Discrete order picking - single picker
            PickBatchesPost pickBatch = new PickBatchesPost();
            pickBatch.ixPickBatchType = _pickbatchesService.PickBatchTypesDb().Where(x => x.sPickBatchType == "Discrete order picking - single picker").Select(x => x.ixPickBatchType).FirstOrDefault();
            pickBatch.bMultiResource = false;
            pickBatch.dtStartBy = DateTime.Now;
            pickBatch.dtCompleteBy = pickBatch.dtStartBy.Add(new TimeSpan(1, 0, 0, 0));
            pickBatch.ixStatus = _pickbatchesService.StatusesDb().Where(x => x.sStatus == "Inactive").Select(x => x.ixStatus).FirstOrDefault();
            pickBatch.UserName = outboundordersPost.UserName;
            var ixPickBatch = _pickbatchesService.Create(pickBatch).Result;
            outboundordersPost.ixPickBatch = ixPickBatch;

            //We start by trying to find an existing active outboundShipment that matches out order 
            if (_outboundshipmentsService.IndexDb().Where(x =>
                 x.ixFacility == outboundordersPost.ixFacility &&
                 x.ixCompany == outboundordersPost.ixCompany &&
                 x.ixCarrier == _outboundordersRepository.CarrierServicesDb().Where(c => c.ixCarrierService == outboundordersPost.ixCarrierService).Select(c => c.Carriers.ixCarrier).FirstOrDefault() &&
                 x.ixAddress == _outboundordersRepository.BusinessPartnersDb().Where(b => b.ixBusinessPartner == outboundordersPost.ixBusinessPartner).Select(b => b.ixAddress).FirstOrDefault() &&
                 x.ixStatus == _outboundordersRepository.StatusesDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()
                ).Select(x => x.ixOutboundShipment).Any())
            {
                var ixOutboundShipment = _outboundshipmentsService.IndexDb().Where(x =>
                                  x.ixFacility == outboundordersPost.ixFacility &&
                                  x.ixCompany == outboundordersPost.ixCompany &&
                                  x.ixCarrier == _outboundordersRepository.CarrierServicesDb().Where(c => c.ixCarrier == outboundordersPost.ixCarrierService).Select(c => c.Carriers.ixCarrier).FirstOrDefault() &&
                                  x.ixAddress == _outboundordersRepository.BusinessPartnersDb().Where(b => b.ixBusinessPartner == outboundordersPost.ixBusinessPartner).Select(b => b.ixAddress).FirstOrDefault() &&
                                  x.ixStatus == _outboundordersRepository.StatusesDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()
                                ).Select(x => x.ixOutboundShipment).FirstOrDefault();
                outboundordersPost.ixOutboundShipment = ixOutboundShipment;
            }
            else
            {
                OutboundShipmentsPost outboundShipment = new OutboundShipmentsPost();
                outboundShipment.ixFacility = outboundordersPost.ixFacility;
                outboundShipment.ixCompany = outboundordersPost.ixCompany;
                outboundShipment.ixCarrier = _outboundordersRepository.CarrierServicesDb().Where(x => x.ixCarrier == outboundordersPost.ixCarrierService).Select(x => x.Carriers.ixCarrier).FirstOrDefault();
                var carrier = _carriersService.GetPost(outboundShipment.ixCarrier);
                if (carrier.nCarrierConsignmentNumberLastUsed == null || carrier.nCarrierConsignmentNumberLastUsed < carrier.nCarrierConsignmentNumberStart)
                {
                    outboundShipment.sCarrierConsignmentNumber = carrier.sCarrierConsignmentNumberPrefix + carrier.nCarrierConsignmentNumberStart.ToString();
                    carrier.nCarrierConsignmentNumberLastUsed = carrier.nCarrierConsignmentNumberStart;
                }
                else
                {
                    outboundShipment.sCarrierConsignmentNumber = carrier.sCarrierConsignmentNumberPrefix + (carrier.nCarrierConsignmentNumberLastUsed + 1).ToString();
                    carrier.nCarrierConsignmentNumberLastUsed += 1;
                }
                _carriersService.Edit(carrier);
                outboundShipment.ixStatus = _outboundordersRepository.StatusesDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault();
                outboundShipment.ixAddress = _outboundordersRepository.BusinessPartnersDb().Where(b => b.ixBusinessPartner == outboundordersPost.ixBusinessPartner).Select(b => b.ixAddress).FirstOrDefault();
                outboundShipment.UserName = outboundordersPost.UserName;
                var ixOutboundShipment =_outboundshipmentsService.Create(outboundShipment).Result;
                outboundordersPost.ixOutboundShipment = ixOutboundShipment;
            }
            //Custom Code End


            // Process
            this._outboundordersRepository.RegisterCreate(outboundordersPost);
            try
            {
                this._outboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundordersPost.ixOutboundOrder);

        }
        public Task Edit(OutboundOrdersPost outboundordersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundordersRepository.RegisterEdit(outboundordersPost);
            try
            {
                this._outboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundOrdersPost outboundordersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundordersRepository.RegisterDelete(outboundordersPost);
            try
            {
                this._outboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

