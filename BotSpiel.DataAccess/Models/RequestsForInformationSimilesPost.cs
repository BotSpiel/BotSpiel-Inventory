using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class RequestsForInformationSimilesPost : IRequestsForInformationSimilesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Requests For Information Simile ID")]
		public virtual Int64 ixRequestsForInformationSimile { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Requests For Information Simile")]
		public virtual String sRequestsForInformationSimile { get; set; }
		[Required]
		[Display(Name = "Request For Information ID")]
		public virtual Int64 ixRequestForInformation { get; set; }
		[Required]
		[Display(Name = "Requests For Information Simile Text")]
		public virtual String sRequestsForInformationSimileText { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

