using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class BaySequenceTypesPost : IBaySequenceTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Bay Sequence Type ID")]
		public virtual Int64 ixBaySequenceType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyBaySequenceType", controller: "BaySequenceTypes", AdditionalFields = nameof(ixBaySequenceType))]
		[Display(Name = "Bay Sequence Type")]
		public virtual String sBaySequenceType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

