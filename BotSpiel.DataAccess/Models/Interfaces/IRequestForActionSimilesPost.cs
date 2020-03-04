using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IRequestForActionSimilesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixRequestForActionSimile { get; set; }
		String sRequestForActionSimile { get; set; }
		Int64 ixRequestForAction { get; set; }
		String sRequestForActionSimileText { get; set; }
		String UserName { get; set; }
    }
}
  

