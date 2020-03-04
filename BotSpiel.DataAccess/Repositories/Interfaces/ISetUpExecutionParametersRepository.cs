using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ISetUpExecutionParametersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        SetUpExecutionParametersPost GetPost(Int64 ixSetUpExecutionParameter);        
		SetUpExecutionParameters Get(Int64 ixSetUpExecutionParameter);
        IQueryable<SetUpExecutionParameters> Index();
        IQueryable<SetUpExecutionParameters> IndexDb();
       IQueryable<Companies> selectCompanies();
        IQueryable<Facilities> selectFacilities();
        IQueryable<FacilityWorkAreas> selectFacilityWorkAreas();
       IQueryable<Companies> CompaniesDb();
        IQueryable<Facilities> FacilitiesDb();
        IQueryable<FacilityWorkAreas> FacilityWorkAreasDb();
        bool VerifySetUpExecutionParameterUnique(Int64 ixSetUpExecutionParameter, string sSetUpExecutionParameter);
        List<string> VerifySetUpExecutionParameterDeleteOK(Int64 ixSetUpExecutionParameter, string sSetUpExecutionParameter);
        void RegisterCreate(SetUpExecutionParametersPost setupexecutionparametersPost);
        void RegisterEdit(SetUpExecutionParametersPost setupexecutionparametersPost);
        void RegisterDelete(SetUpExecutionParametersPost setupexecutionparametersPost);
        void Rollback();
        void Commit();
    }
}
  

