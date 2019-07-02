using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IGalaxiesService
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
       IQueryable<Universes> selectUniverses();
        bool VerifyGalaxyUnique(Int64 ixGalaxy, string sGalaxy);
        List<string> VerifyGalaxyDeleteOK(Int64 ixGalaxy, string sGalaxy);

        Task<Int64> Create(GalaxiesPost galaxiesPost);
        Task Edit(GalaxiesPost galaxiesPost);
        Task Delete(GalaxiesPost galaxiesPost);
    }
}
  

