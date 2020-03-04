using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IQuestionSimiles
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixQuestionSimile { get; set; }
		Int64 ixQuestionSimileEdit { get; set; }
		String sQuestionSimile { get; set; }
		Int64 ixQuestion { get; set; }
		String sQuestionSimileText { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Questions Questions { get; set; }
    }
}
  

