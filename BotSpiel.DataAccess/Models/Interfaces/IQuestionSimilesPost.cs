using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IQuestionSimilesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixQuestionSimile { get; set; }
		String sQuestionSimile { get; set; }
		Int64 ixQuestion { get; set; }
		String sQuestionSimileText { get; set; }
		String UserName { get; set; }
    }
}
  

