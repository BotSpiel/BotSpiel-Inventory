using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IUnitOfMeasurementConversionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixUnitOfMeasurementConversion { get; set; }
		String sUnitOfMeasurementConversion { get; set; }
		Int64 ixUnitOfMeasurementFrom { get; set; }
		Int64 ixUnitOfMeasurementTo { get; set; }
		Double nMultiplier { get; set; }
		String UserName { get; set; }
    }
}
  

