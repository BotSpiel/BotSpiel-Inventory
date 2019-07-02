using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ILogicalOrientationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixLogicalOrientation { get; set; }
		String sLogicalOrientation { get; set; }
		String UserName { get; set; }
    }
}
  

