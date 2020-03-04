using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundOrderLinesInventoryAllocationService : IOutboundOrderLinesInventoryAllocationService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundOrderLinesInventoryAllocationRepository _outboundorderlinesinventoryallocationRepository;

        public OutboundOrderLinesInventoryAllocationService(IOutboundOrderLinesInventoryAllocationRepository outboundorderlinesinventoryallocationRepository)
        {
            _outboundorderlinesinventoryallocationRepository = outboundorderlinesinventoryallocationRepository;
        }

        public OutboundOrderLinesInventoryAllocationPost GetPost(Int64 ixOutboundOrderLineInventoryAllocation) => _outboundorderlinesinventoryallocationRepository.GetPost(ixOutboundOrderLineInventoryAllocation);
        public OutboundOrderLinesInventoryAllocation Get(Int64 ixOutboundOrderLineInventoryAllocation) => _outboundorderlinesinventoryallocationRepository.Get(ixOutboundOrderLineInventoryAllocation);
        public IQueryable<OutboundOrderLinesInventoryAllocation> Index() => _outboundorderlinesinventoryallocationRepository.Index();
        public IQueryable<OutboundOrderLinesInventoryAllocation> IndexDb() => _outboundorderlinesinventoryallocationRepository.IndexDb();
        //Custom Code Start | Added Code Block 
        public IQueryable<OutboundOrderLinesInventoryAllocationPost> IndexDbPost() => _outboundorderlinesinventoryallocationRepository.IndexDbPost();
        //Custom Code End
        public IQueryable<Statuses> selectStatuses() => _outboundorderlinesinventoryallocationRepository.selectStatuses();
        public IQueryable<OutboundOrderLines> selectOutboundOrderLines() => _outboundorderlinesinventoryallocationRepository.selectOutboundOrderLines();
       public IQueryable<Statuses> StatusesDb() => _outboundorderlinesinventoryallocationRepository.StatusesDb();
        public IQueryable<OutboundOrderLines> OutboundOrderLinesDb() => _outboundorderlinesinventoryallocationRepository.OutboundOrderLinesDb();
        public bool VerifyOutboundOrderLineInventoryAllocationUnique(Int64 ixOutboundOrderLineInventoryAllocation, string sOutboundOrderLineInventoryAllocation) => _outboundorderlinesinventoryallocationRepository.VerifyOutboundOrderLineInventoryAllocationUnique(ixOutboundOrderLineInventoryAllocation, sOutboundOrderLineInventoryAllocation);
        public List<string> VerifyOutboundOrderLineInventoryAllocationDeleteOK(Int64 ixOutboundOrderLineInventoryAllocation, string sOutboundOrderLineInventoryAllocation) => _outboundorderlinesinventoryallocationRepository.VerifyOutboundOrderLineInventoryAllocationDeleteOK(ixOutboundOrderLineInventoryAllocation, sOutboundOrderLineInventoryAllocation);

        public Task<Int64> Create(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinesinventoryallocationRepository.RegisterCreate(outboundorderlinesinventoryallocationPost);
            try
            {
                this._outboundorderlinesinventoryallocationRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinesinventoryallocationRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundorderlinesinventoryallocationPost.ixOutboundOrderLineInventoryAllocation);

        }
        public Task Edit(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinesinventoryallocationRepository.RegisterEdit(outboundorderlinesinventoryallocationPost);
            try
            {
                this._outboundorderlinesinventoryallocationRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinesinventoryallocationRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinesinventoryallocationRepository.RegisterDelete(outboundorderlinesinventoryallocationPost);
            try
            {
                this._outboundorderlinesinventoryallocationRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinesinventoryallocationRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

