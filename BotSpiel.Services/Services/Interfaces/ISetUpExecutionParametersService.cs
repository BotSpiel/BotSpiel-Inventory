using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ISetUpExecutionParametersService
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

        Task<Int64> Create(SetUpExecutionParametersPost setupexecutionparametersPost);
        Task Edit(SetUpExecutionParametersPost setupexecutionparametersPost);
        Task Delete(SetUpExecutionParametersPost setupexecutionparametersPost);
    }
}
  

