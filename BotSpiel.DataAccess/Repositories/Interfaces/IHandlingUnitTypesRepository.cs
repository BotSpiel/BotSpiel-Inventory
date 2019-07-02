using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IHandlingUnitTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        HandlingUnitTypesPost GetPost(Int64 ixHandlingUnitType);        
		HandlingUnitTypes Get(Int64 ixHandlingUnitType);
        IQueryable<HandlingUnitTypes> Index();
        bool VerifyHandlingUnitTypeUnique(Int64 ixHandlingUnitType, string sHandlingUnitType);
        List<string> VerifyHandlingUnitTypeDeleteOK(Int64 ixHandlingUnitType, string sHandlingUnitType);
        void RegisterCreate(HandlingUnitTypesPost handlingunittypesPost);
        void RegisterEdit(HandlingUnitTypesPost handlingunittypesPost);
        void RegisterDelete(HandlingUnitTypesPost handlingunittypesPost);
        void Rollback();
        void Commit();
    }
}
  

