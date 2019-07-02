using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CarrierServicesService : ICarrierServicesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICarrierServicesRepository _carrierservicesRepository;

        public CarrierServicesService(ICarrierServicesRepository carrierservicesRepository)
        {
            _carrierservicesRepository = carrierservicesRepository;
        }

        public CarrierServicesPost GetPost(Int64 ixCarrierService) => _carrierservicesRepository.GetPost(ixCarrierService);
        public CarrierServices Get(Int64 ixCarrierService) => _carrierservicesRepository.Get(ixCarrierService);
        public IQueryable<CarrierServices> Index() => _carrierservicesRepository.Index();
       public IQueryable<Carriers> selectCarriers() => _carrierservicesRepository.selectCarriers();
        public bool VerifyCarrierServiceUnique(Int64 ixCarrierService, string sCarrierService) => _carrierservicesRepository.VerifyCarrierServiceUnique(ixCarrierService, sCarrierService);
        public List<string> VerifyCarrierServiceDeleteOK(Int64 ixCarrierService, string sCarrierService) => _carrierservicesRepository.VerifyCarrierServiceDeleteOK(ixCarrierService, sCarrierService);

        public Task<Int64> Create(CarrierServicesPost carrierservicesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carrierservicesRepository.RegisterCreate(carrierservicesPost);
            try
            {
                this._carrierservicesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carrierservicesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(carrierservicesPost.ixCarrierService);

        }
        public Task Edit(CarrierServicesPost carrierservicesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carrierservicesRepository.RegisterEdit(carrierservicesPost);
            try
            {
                this._carrierservicesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carrierservicesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CarrierServicesPost carrierservicesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carrierservicesRepository.RegisterDelete(carrierservicesPost);
            try
            {
                this._carrierservicesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carrierservicesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

