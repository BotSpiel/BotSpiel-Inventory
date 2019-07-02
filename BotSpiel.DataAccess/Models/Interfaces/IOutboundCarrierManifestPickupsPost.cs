using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundCarrierManifestPickupsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixOutboundCarrierManifestPickup { get; set; }
		String sOutboundCarrierManifestPickup { get; set; }
		Int64 ixOutboundCarrierManifest { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

