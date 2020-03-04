using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;
//Custom Code Start | Added Code Block 
using BotSpiel.Services.Utilities;
//Custom Code End

namespace BotSpiel.Services
{

    public class PickBatchesService : IPickBatchesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPickBatchesRepository _pickbatchesRepository;
        //Custom Code Start | Added Code Block 
        private readonly CommonLookUps _commonLookUps;
        private readonly Shipping _shipping;
        private readonly IOutboundCarrierManifestsService _outboundcarriermanifestsService;
        private readonly IOutboundOrdersRepository _outboundordersRepository;
        private readonly IOutboundShipmentsService _outboundshipmentsService;
        //Custom Code End
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public PickBatchesService(IPickBatchesRepository pickbatchesRepository)
        //Replaced Code Block End
        public PickBatchesService(IPickBatchesRepository pickbatchesRepository
            , CommonLookUps commonLookUps
            , Shipping shipping
            , IOutboundCarrierManifestsService outboundcarriermanifestsService
            , IOutboundOrdersRepository outboundordersRepository
            , IOutboundShipmentsService outboundshipmentsService
            )
        //Custom Code End
        {
            _pickbatchesRepository = pickbatchesRepository;
            //Custom Code Start | Added Code Block 
            _commonLookUps = commonLookUps;
            _shipping = shipping;
            _outboundcarriermanifestsService = outboundcarriermanifestsService;
            _outboundordersRepository = outboundordersRepository;
            _outboundshipmentsService = outboundshipmentsService;
            //Custom Code End
        }

        public PickBatchesPost GetPost(Int64 ixPickBatch) => _pickbatchesRepository.GetPost(ixPickBatch);
        public PickBatches Get(Int64 ixPickBatch) => _pickbatchesRepository.Get(ixPickBatch);
        public IQueryable<PickBatches> Index() => _pickbatchesRepository.Index();
        public IQueryable<PickBatches> IndexDb() => _pickbatchesRepository.IndexDb();
        //Custom Code Start | Added Code Block 
        public IQueryable<PickBatchesPost> IndexDbPost() => _pickbatchesRepository.IndexDbPost();
        //Custom Code End
        public IQueryable<Statuses> selectStatuses() => _pickbatchesRepository.selectStatuses();
        public IQueryable<PickBatchTypes> selectPickBatchTypes() => _pickbatchesRepository.selectPickBatchTypes();
       public IQueryable<Statuses> StatusesDb() => _pickbatchesRepository.StatusesDb();
        public IQueryable<PickBatchTypes> PickBatchTypesDb() => _pickbatchesRepository.PickBatchTypesDb();
        public bool VerifyPickBatchUnique(Int64 ixPickBatch, string sPickBatch) => _pickbatchesRepository.VerifyPickBatchUnique(ixPickBatch, sPickBatch);
        public List<string> VerifyPickBatchDeleteOK(Int64 ixPickBatch, string sPickBatch) => _pickbatchesRepository.VerifyPickBatchDeleteOK(ixPickBatch, sPickBatch);

        public Task<Int64> Create(PickBatchesPost pickbatchesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchesRepository.RegisterCreate(pickbatchesPost);
            try
            {
                this._pickbatchesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(pickbatchesPost.ixPickBatch);

        }
        public Task Edit(PickBatchesPost pickbatchesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchesRepository.RegisterEdit(pickbatchesPost);
            try
            {
                this._pickbatchesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process
            //Custom Code Start | Added Code Block 

            if(pickbatchesPost.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Complete").Select(s => s.ixStatus).FirstOrDefault())
            {
                // We check if the shipment associated with the orders on the batch has been manifested. If not we try and find an active manifest else we create a new one and add the shipment to it
                var outboundShipments = _outboundordersRepository.IndexDb().Where(x => x.ixPickBatch == pickbatchesPost.ixPickBatch).Select(x => x.ixOutboundShipment).ToList();
                var outboundShipmentsNotManifested = _outboundshipmentsService.IndexDb().Where(x => x.ixOutboundCarrierManifest == null)
                    .Join(outboundShipments, os => os.ixOutboundShipment, o => o, (os, o) => new { Os = os, O = o })
                    .Select(s => s.Os.ixOutboundShipment).ToList();

                outboundShipmentsNotManifested.ForEach(x =>
                {
                    var outboundShipment = _outboundshipmentsService.GetPost(x);
                    if (
                    _outboundcarriermanifestsService.IndexDb().Where(m =>
                        m.ixFacility == outboundShipment.ixFacility &&
                        m.ixCarrier == outboundShipment.ixCarrier &&
                        m.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()
                    ).Any())
                    {
                        var ixOutboundCarrierManifest = _outboundcarriermanifestsService.IndexDb().Where(m =>
                        m.ixFacility == outboundShipment.ixFacility &&
                        m.ixCarrier == outboundShipment.ixCarrier &&
                        m.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()
                        ).Select(m => m.ixOutboundCarrierManifest).FirstOrDefault();
                        outboundShipment.ixOutboundCarrierManifest = ixOutboundCarrierManifest;
                        outboundShipment.UserName = pickbatchesPost.UserName;
                        _outboundshipmentsService.Edit(outboundShipment);
                    }
                    else
                    {
                        OutboundCarrierManifestsPost outboundCarrierManifestsPost = new OutboundCarrierManifestsPost();
                        outboundCarrierManifestsPost.ixFacility = outboundShipment.ixFacility;
                        outboundCarrierManifestsPost.ixCarrier = outboundShipment.ixCarrier;
                        outboundCarrierManifestsPost.ixPickupInventoryLocation = _shipping.getTrailerDoorSuggestion(outboundShipment.ixFacility);
                        outboundCarrierManifestsPost.ixStatus = _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault();
                        outboundCarrierManifestsPost.dtScheduledPickupAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + _outboundshipmentsService.CarriersDb().Where(c => c.ixCarrier == outboundShipment.ixCarrier).Select(c => c.dtScheduledPickupTime).FirstOrDefault();
                        outboundCarrierManifestsPost.UserName = pickbatchesPost.UserName;
                        var ixOutboundCarrierManifest = _outboundcarriermanifestsService.Create(outboundCarrierManifestsPost).Result;
                        outboundShipment.ixOutboundCarrierManifest = ixOutboundCarrierManifest;
                        outboundShipment.UserName = pickbatchesPost.UserName;
                        _outboundshipmentsService.Edit(outboundShipment);
                    }
                }
                );

            }
            //Custom Code End



            return Task.CompletedTask;

        }
        public Task Delete(PickBatchesPost pickbatchesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchesRepository.RegisterDelete(pickbatchesPost);
            try
            {
                this._pickbatchesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }



    }
}
  

