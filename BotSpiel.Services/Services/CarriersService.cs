using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CarriersService : ICarriersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICarriersRepository _carriersRepository;

        public CarriersService(ICarriersRepository carriersRepository)
        {
            _carriersRepository = carriersRepository;
        }

        public CarriersPost GetPost(Int64 ixCarrier) => _carriersRepository.GetPost(ixCarrier);
        public Carriers Get(Int64 ixCarrier) => _carriersRepository.Get(ixCarrier);
        public IQueryable<Carriers> Index() => _carriersRepository.Index();
       public IQueryable<CarrierTypes> selectCarrierTypes() => _carriersRepository.selectCarrierTypes();
        public bool VerifyCarrierUnique(Int64 ixCarrier, string sCarrier) => _carriersRepository.VerifyCarrierUnique(ixCarrier, sCarrier);
        public List<string> VerifyCarrierDeleteOK(Int64 ixCarrier, string sCarrier) => _carriersRepository.VerifyCarrierDeleteOK(ixCarrier, sCarrier);

        public Task<Int64> Create(CarriersPost carriersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carriersRepository.RegisterCreate(carriersPost);
            try
            {
                this._carriersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carriersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(carriersPost.ixCarrier);

        }
        public Task Edit(CarriersPost carriersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carriersRepository.RegisterEdit(carriersPost);
            try
            {
                this._carriersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carriersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CarriersPost carriersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carriersRepository.RegisterDelete(carriersPost);
            try
            {
                this._carriersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carriersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

