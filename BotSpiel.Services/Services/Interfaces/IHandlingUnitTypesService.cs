using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IHandlingUnitTypesService
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

        Task<Int64> Create(HandlingUnitTypesPost handlingunittypesPost);
        Task Edit(HandlingUnitTypesPost handlingunittypesPost);
        Task Delete(HandlingUnitTypesPost handlingunittypesPost);
    }
}
  

