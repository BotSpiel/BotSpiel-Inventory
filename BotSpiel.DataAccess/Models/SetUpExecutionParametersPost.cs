using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class SetUpExecutionParametersPost : ISetUpExecutionParametersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Set Up Execution Parameter ID")]
		public virtual Int64 ixSetUpExecutionParameter { get; set; }
		[Display(Name = "Set Up Execution Parameter")]
		public virtual String sSetUpExecutionParameter { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Facility Work Area ID")]
		public virtual Int64 ixFacilityWorkArea { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

