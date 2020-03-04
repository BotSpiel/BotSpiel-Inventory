using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Custom Code Start | Added Code Block 
using FluentValidation;
using BotSpiel.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
//Custom Code End

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InboundOrdersPost : IInboundOrdersPost
    {

        /*
        -- =============================================
        -- Author:		<BotSpiel>

        -- Description:	<Description>

        This class ....

        */

        //Custom Code Start | Added Code Block
        public InboundOrdersPost()
        {
            ixStatus = 5;
        }
        //Custom Code End

        [Display(Name = "Inbound Order ID")]
		public virtual Int64 ixInboundOrder { get; set; }
		[Display(Name = "Inbound Order")]
		public virtual String sInboundOrder { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Order Reference")]
		public virtual String sOrderReference { get; set; }
		[Required]
		[Display(Name = "Inbound Order Type ID")]
		public virtual Int64 ixInboundOrderType { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Business Partner ID")]
		public virtual Int64 ixBusinessPartner { get; set; }
		[Display(Name = "Expected At")]
		public virtual DateTime? dtExpectedAt { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }

    //Custom Code Start | Added Code Block 
    public class InboundOrdersPostValidator : AbstractValidator<InboundOrdersPost>
    {

        public InboundOrdersPostValidator()
        {

            RuleFor(x => x.ixBusinessPartner)
            .GreaterThan(0)
            .WithMessage("A business partner is required. Please select one.");
        }

    }
    //Custom Code End


}


