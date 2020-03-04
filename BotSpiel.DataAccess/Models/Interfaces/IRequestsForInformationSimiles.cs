using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IRequestsForInformationSimiles
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixRequestsForInformationSimile { get; set; }
		Int64 ixRequestsForInformationSimileEdit { get; set; }
		String sRequestsForInformationSimile { get; set; }
		Int64 ixRequestForInformation { get; set; }
		String sRequestsForInformationSimileText { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		RequestsForInformation RequestsForInformation { get; set; }
    }
}
  

