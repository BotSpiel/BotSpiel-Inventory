using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IComplementsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        ComplementsPost GetPost(Int64 ixComplement);        
		Complements Get(Int64 ixComplement);
        IQueryable<Complements> Index();
        IQueryable<Complements> IndexDb();
       IQueryable<Languages> selectLanguages();
        IQueryable<LanguageStyles> selectLanguageStyles();
        IQueryable<ResponseTypes> selectResponseTypes();
       IQueryable<Languages> LanguagesDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        IQueryable<ResponseTypes> ResponseTypesDb();
        List<string> VerifyComplementDeleteOK(Int64 ixComplement, string sComplement);

        Task<Int64> Create(ComplementsPost complementsPost);
        Task Edit(ComplementsPost complementsPost);
        Task Delete(ComplementsPost complementsPost);
    }
}
  

