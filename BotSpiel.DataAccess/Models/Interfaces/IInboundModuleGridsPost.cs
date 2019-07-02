using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInboundModuleGridsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInboundModuleGrid { get; set; }
		String sInboundModuleGrid { get; set; }
		String sShortDescription { get; set; }
		String sDataEntityType { get; set; }
		Boolean bCanCreate { get; set; }
		Boolean bCanEdit { get; set; }
		Boolean bCanDelete { get; set; }
		String UserName { get; set; }
    }
}
  

