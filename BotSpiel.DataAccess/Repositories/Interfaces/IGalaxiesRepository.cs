using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IGalaxiesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        GalaxiesPost GetPost(Int64 ixGalaxy);        
		Galaxies Get(Int64 ixGalaxy);
        IQueryable<Galaxies> Index();
        IQueryable<Galaxies> IndexDb();
       IQueryable<Universes> selectUniverses();
       IQueryable<Universes> UniversesDb();
        bool VerifyGalaxyUnique(Int64 ixGalaxy, string sGalaxy);
        List<string> VerifyGalaxyDeleteOK(Int64 ixGalaxy, string sGalaxy);
        void RegisterCreate(GalaxiesPost galaxiesPost);
        void RegisterEdit(GalaxiesPost galaxiesPost);
        void RegisterDelete(GalaxiesPost galaxiesPost);
        void Rollback();
        void Commit();
    }
}
  

