using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IGalaxiesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixGalaxy { get; set; }
		String sGalaxy { get; set; }
		Int64 ixUniverse { get; set; }
		String UserName { get; set; }
    }
}
  

