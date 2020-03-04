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
    //Custom Code Start
    public delegate void ShipInventoryForManifestPointer(Int64 ixOutboundCarrierManifest, string UserName);
    //Custom Code End
    public class OutboundCarrierManifestPickupsService : IOutboundCarrierManifestPickupsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundCarrierManifestPickupsRepository _outboundcarriermanifestpickupsRepository;
        //Custom Code Start | Added Code Block 
        private readonly Shipping _shipping;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public OutboundCarrierManifestPickupsService(IOutboundCarrierManifestPickupsRepository outboundcarriermanifestpickupsRepository)
        //Replaced Code Block End
        public OutboundCarrierManifestPickupsService(IOutboundCarrierManifestPickupsRepository outboundcarriermanifestpickupsRepository, Shipping shipping)
        //Custom Code End
        {
            _outboundcarriermanifestpickupsRepository = outboundcarriermanifestpickupsRepository;
            //Custom Code Start | Added Code Block 
            _shipping = shipping;
            //Custom Code End
        }

        public OutboundCarrierManifestPickupsPost GetPost(Int64 ixOutboundCarrierManifestPickup) => _outboundcarriermanifestpickupsRepository.GetPost(ixOutboundCarrierManifestPickup);
        public OutboundCarrierManifestPickups Get(Int64 ixOutboundCarrierManifestPickup) => _outboundcarriermanifestpickupsRepository.Get(ixOutboundCarrierManifestPickup);
        public IQueryable<OutboundCarrierManifestPickups> Index() => _outboundcarriermanifestpickupsRepository.Index();
        public IQueryable<OutboundCarrierManifestPickups> IndexDb() => _outboundcarriermanifestpickupsRepository.IndexDb();
       public IQueryable<Statuses> selectStatuses() => _outboundcarriermanifestpickupsRepository.selectStatuses();
        public IQueryable<OutboundCarrierManifests> selectOutboundCarrierManifests() => _outboundcarriermanifestpickupsRepository.selectOutboundCarrierManifests();
       public IQueryable<Statuses> StatusesDb() => _outboundcarriermanifestpickupsRepository.StatusesDb();
        public IQueryable<OutboundCarrierManifests> OutboundCarrierManifestsDb() => _outboundcarriermanifestpickupsRepository.OutboundCarrierManifestsDb();
        public bool VerifyOutboundCarrierManifestPickupUnique(Int64 ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup) => _outboundcarriermanifestpickupsRepository.VerifyOutboundCarrierManifestPickupUnique(ixOutboundCarrierManifestPickup, sOutboundCarrierManifestPickup);
        public List<string> VerifyOutboundCarrierManifestPickupDeleteOK(Int64 ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup) => _outboundcarriermanifestpickupsRepository.VerifyOutboundCarrierManifestPickupDeleteOK(ixOutboundCarrierManifestPickup, sOutboundCarrierManifestPickup);

        public Task<Int64> Create(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestpickupsRepository.RegisterCreate(outboundcarriermanifestpickupsPost);
            try
            {
                this._outboundcarriermanifestpickupsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestpickupsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process
            //Custom Code Start | Added Code Block 
            //_shipping.shipInventoryForManifest(outboundcarriermanifestpickupsPost.ixOutboundCarrierManifest, outboundcarriermanifestpickupsPost.UserName);
            ShipInventoryForManifestPointer shipInventoryForManifestPointer = new ShipInventoryForManifestPointer(_shipping.shipInventoryForManifest);
            var taskShipInventoryForManifest = new Task(() => shipInventoryForManifestPointer(outboundcarriermanifestpickupsPost.ixOutboundCarrierManifest, outboundcarriermanifestpickupsPost.UserName), TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness);
            taskShipInventoryForManifest.Start();
            //Custom Code End


            return Task.FromResult(outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup);

        }
        public Task Edit(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestpickupsRepository.RegisterEdit(outboundcarriermanifestpickupsPost);
            try
            {
                this._outboundcarriermanifestpickupsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestpickupsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestpickupsRepository.RegisterDelete(outboundcarriermanifestpickupsPost);
            try
            {
                this._outboundcarriermanifestpickupsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestpickupsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

