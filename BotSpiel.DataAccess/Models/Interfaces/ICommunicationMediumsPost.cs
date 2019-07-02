using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICommunicationMediumsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixCommunicationMedium { get; set; }
		String sCommunicationMedium { get; set; }
		String sCommunicationMediumCode { get; set; }
		String UserName { get; set; }
    }
}
  

