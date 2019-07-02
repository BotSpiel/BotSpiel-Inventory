using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICompaniesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CompaniesPost GetPost(Int64 ixCompany);        
		Companies Get(Int64 ixCompany);
        IQueryable<Companies> Index();
        bool VerifyCompanyUnique(Int64 ixCompany, string sCompany);
        List<string> VerifyCompanyDeleteOK(Int64 ixCompany, string sCompany);

        Task<Int64> Create(CompaniesPost companiesPost);
        Task Edit(CompaniesPost companiesPost);
        Task Delete(CompaniesPost companiesPost);
    }
}
  

