using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICompaniesRepository
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
        void RegisterCreate(CompaniesPost companiesPost);
        void RegisterEdit(CompaniesPost companiesPost);
        void RegisterDelete(CompaniesPost companiesPost);
        void Rollback();
        void Commit();
    }
}
  

