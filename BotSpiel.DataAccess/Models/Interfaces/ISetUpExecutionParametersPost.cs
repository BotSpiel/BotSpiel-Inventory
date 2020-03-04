using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ISetUpExecutionParametersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixSetUpExecutionParameter { get; set; }
		String sSetUpExecutionParameter { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixFacilityWorkArea { get; set; }
		String UserName { get; set; }
    }
}
  

