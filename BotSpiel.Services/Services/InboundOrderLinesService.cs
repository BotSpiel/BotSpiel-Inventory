using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InboundOrderLinesService : IInboundOrderLinesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInboundOrderLinesRepository _inboundorderlinesRepository;

        public InboundOrderLinesService(IInboundOrderLinesRepository inboundorderlinesRepository)
        {
            _inboundorderlinesRepository = inboundorderlinesRepository;
        }

        public InboundOrderLinesPost GetPost(Int64 ixInboundOrderLine) => _inboundorderlinesRepository.GetPost(ixInboundOrderLine);
        public InboundOrderLines Get(Int64 ixInboundOrderLine) => _inboundorderlinesRepository.Get(ixInboundOrderLine);
        public IQueryable<InboundOrderLines> Index() => _inboundorderlinesRepository.Index();
        public IQueryable<InboundOrderLines> IndexDb() => _inboundorderlinesRepository.IndexDb();
       public IQueryable<Statuses> selectStatuses() => _inboundorderlinesRepository.selectStatuses();
        public IQueryable<Materials> selectMaterials() => _inboundorderlinesRepository.selectMaterials();
        public IQueryable<HandlingUnitTypes> selectHandlingUnitTypes() => _inboundorderlinesRepository.selectHandlingUnitTypes();
        public IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations() => _inboundorderlinesRepository.selectMaterialHandlingUnitConfigurations();
        public IQueryable<InboundOrders> selectInboundOrders() => _inboundorderlinesRepository.selectInboundOrders();
       public IQueryable<Statuses> StatusesDb() => _inboundorderlinesRepository.StatusesDb();
        public IQueryable<Materials> MaterialsDb() => _inboundorderlinesRepository.MaterialsDb();
        public IQueryable<HandlingUnitTypes> HandlingUnitTypesDb() => _inboundorderlinesRepository.HandlingUnitTypesDb();
        public IQueryable<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurationsDb() => _inboundorderlinesRepository.MaterialHandlingUnitConfigurationsDb();
        public IQueryable<InboundOrders> InboundOrdersDb() => _inboundorderlinesRepository.InboundOrdersDb();
       public List<KeyValuePair<Int64?, string>> selectHandlingUnitTypesNullable() => _inboundorderlinesRepository.selectHandlingUnitTypesNullable();
        public List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable() => _inboundorderlinesRepository.selectMaterialHandlingUnitConfigurationsNullable();
        public bool VerifyInboundOrderLineUnique(Int64 ixInboundOrderLine, string sInboundOrderLine) => _inboundorderlinesRepository.VerifyInboundOrderLineUnique(ixInboundOrderLine, sInboundOrderLine);
        public List<string> VerifyInboundOrderLineDeleteOK(Int64 ixInboundOrderLine, string sInboundOrderLine) => _inboundorderlinesRepository.VerifyInboundOrderLineDeleteOK(ixInboundOrderLine, sInboundOrderLine);

        public Task<Int64> Create(InboundOrderLinesPost inboundorderlinesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundorderlinesRepository.RegisterCreate(inboundorderlinesPost);
            try
            {
                this._inboundorderlinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundorderlinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inboundorderlinesPost.ixInboundOrderLine);

        }
        public Task Edit(InboundOrderLinesPost inboundorderlinesPost)
        {
            // Additional validations

            // Pre-process

            //Custom Code Start | Added Code Block 
            var inboundorderline = GetPost(inboundorderlinesPost.ixInboundOrderLine);
            inboundorderlinesPost.nBaseUnitQuantityReceived += inboundorderline.nBaseUnitQuantityReceived;
            //inboundorderlinesPost.ixStatus = inboundorderline.ixStatus;
            //Custom Code End

            // Process
            this._inboundorderlinesRepository.RegisterEdit(inboundorderlinesPost);
            try
            {
                this._inboundorderlinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundorderlinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InboundOrderLinesPost inboundorderlinesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundorderlinesRepository.RegisterDelete(inboundorderlinesPost);
            try
            {
                this._inboundorderlinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundorderlinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

