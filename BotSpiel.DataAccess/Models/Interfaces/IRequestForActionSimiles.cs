using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IRequestForActionSimiles
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixRequestForActionSimile { get; set; }
		Int64 ixRequestForActionSimileEdit { get; set; }
		String sRequestForActionSimile { get; set; }
		Int64 ixRequestForAction { get; set; }
		String sRequestForActionSimileText { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		RequestsForAction RequestsForAction { get; set; }
    }
}
  

