using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CommunicationMediumsPost : ICommunicationMediumsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Communication Medium ID")]
		public virtual Int64 ixCommunicationMedium { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCommunicationMedium", controller: "CommunicationMediums", AdditionalFields = nameof(ixCommunicationMedium))]
		[Display(Name = "Communication Medium")]
		public virtual String sCommunicationMedium { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Communication Medium Code")]
		public virtual String sCommunicationMediumCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

