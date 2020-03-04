using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IRequestsForInformationSimilesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixRequestsForInformationSimile { get; set; }
		String sRequestsForInformationSimile { get; set; }
		Int64 ixRequestForInformation { get; set; }
		String sRequestsForInformationSimileText { get; set; }
		String UserName { get; set; }
    }
}
  

