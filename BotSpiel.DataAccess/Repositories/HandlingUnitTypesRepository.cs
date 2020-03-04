using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class HandlingUnitTypesRepository : IHandlingUnitTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly HandlingUnitTypesDB _context;
       private readonly HandlingUnitsDB _contextHandlingUnits;
        private readonly InboundOrderLinesDB _contextInboundOrderLines;
        private readonly MaterialHandlingUnitConfigurationsDB _contextMaterialHandlingUnitConfigurations;
        private readonly ReceivingDB _contextReceiving;
  
        public HandlingUnitTypesRepository(HandlingUnitTypesDB context, HandlingUnitsDB contextHandlingUnits, InboundOrderLinesDB contextInboundOrderLines, MaterialHandlingUnitConfigurationsDB contextMaterialHandlingUnitConfigurations, ReceivingDB contextReceiving)
        {
            _context = context;
           _contextHandlingUnits = contextHandlingUnits;
            _contextInboundOrderLines = contextInboundOrderLines;
            _contextMaterialHandlingUnitConfigurations = contextMaterialHandlingUnitConfigurations;
            _contextReceiving = contextReceiving;
  
        }

        public HandlingUnitTypesPost GetPost(Int64 ixHandlingUnitType) => _context.HandlingUnitTypesPost.AsNoTracking().Where(x => x.ixHandlingUnitType == ixHandlingUnitType).First();
         
		public HandlingUnitTypes Get(Int64 ixHandlingUnitType)
        {
            HandlingUnitTypes handlingunittypes = _context.HandlingUnitTypes.AsNoTracking().Where(x => x.ixHandlingUnitType == ixHandlingUnitType).First();
            return handlingunittypes;
        }

        public IQueryable<HandlingUnitTypes> Index()
        {
            var handlingunittypes = _context.HandlingUnitTypes.AsNoTracking(); 
            return handlingunittypes;
        }

        public IQueryable<HandlingUnitTypes> IndexDb()
        {
            var handlingunittypes = _context.HandlingUnitTypes.AsNoTracking(); 
            return handlingunittypes;
        }
        public bool VerifyHandlingUnitTypeUnique(Int64 ixHandlingUnitType, string sHandlingUnitType)
        {
            if (_context.HandlingUnitTypes.AsNoTracking().Where(x => x.sHandlingUnitType == sHandlingUnitType).Any() && ixHandlingUnitType == 0L) return false;
            else if (_context.HandlingUnitTypes.AsNoTracking().Where(x => x.sHandlingUnitType == sHandlingUnitType && x.ixHandlingUnitType != ixHandlingUnitType).Any() && ixHandlingUnitType != 0L) return false;
            else return true;
        }

        public List<string> VerifyHandlingUnitTypeDeleteOK(Int64 ixHandlingUnitType, string sHandlingUnitType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixHandlingUnitType == ixHandlingUnitType).Any()) existInEntities.Add("HandlingUnits");
            if (_contextInboundOrderLines.InboundOrderLines.AsNoTracking().Where(x => x.ixHandlingUnitType == ixHandlingUnitType).Any()) existInEntities.Add("InboundOrderLines");
            if (_contextMaterialHandlingUnitConfigurations.MaterialHandlingUnitConfigurations.AsNoTracking().Where(x => x.ixHandlingUnitType == ixHandlingUnitType).Any()) existInEntities.Add("MaterialHandlingUnitConfigurations");
            if (_contextReceiving.Receiving.AsNoTracking().Where(x => x.ixHandlingUnitType == ixHandlingUnitType).Any()) existInEntities.Add("Receiving");

            return existInEntities;
        }


        public void RegisterCreate(HandlingUnitTypesPost handlingunittypesPost)
		{
            _context.HandlingUnitTypesPost.Add(handlingunittypesPost); 
        }

        public void RegisterEdit(HandlingUnitTypesPost handlingunittypesPost)
        {
            _context.Entry(handlingunittypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(HandlingUnitTypesPost handlingunittypesPost)
        {
            _context.HandlingUnitTypesPost.Remove(handlingunittypesPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

