using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IAisleFaceStorageTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixAisleFaceStorageType { get; set; }
		String sAisleFaceStorageType { get; set; }
		String UserName { get; set; }
    }
}
  

