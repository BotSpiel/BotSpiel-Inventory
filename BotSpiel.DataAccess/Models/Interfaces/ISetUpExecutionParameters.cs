using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ISetUpExecutionParameters
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixSetUpExecutionParameter { get; set; }
		Int64 ixSetUpExecutionParameterEdit { get; set; }
		String sSetUpExecutionParameter { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixFacilityWorkArea { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Facilities Facilities { get; set; }
		Companies Companies { get; set; }
		FacilityWorkAreas FacilityWorkAreas { get; set; }
    }
}
  

