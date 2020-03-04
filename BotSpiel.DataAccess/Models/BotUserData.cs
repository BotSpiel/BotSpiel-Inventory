using System;
using System.Collections.Generic;
using System.Text;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Models
{
    public class BotUserData : IBotUserData
    {
        public BotUserData()
        {
            botUserEntityContext = new BotUserEntityContext();
            bIsInitialSetUpParametersSet = false;
        }

        public BotUserEntityContext botUserEntityContext { get; set; }
        public Int64 ixCompany { get; set; }
        public Int64 ixFacility { get; set; }
        public Int64 ixFacilityWorkArea { get; set; }
        public Int64 ixInventoryUnit { get; set; }
        public Int64 ixPickBatchPick { get; set; }
        public Int64 ixPutAwayHandlingUnit { get; set; }
        public Int64 ixSetUpExecutionParameter { get; set; }
        public Int64 ixDropInventoryUnit { get; set; }
        //Custom Code Start | Added Code Block 
        public Int64 ixInventoryLocation { get; set; }
        public bool bIsInitialSetUpParametersSet { get; set; }
        public String sPutAwaySuggestion { get; set; }
        public Tuple<Int64, double> pickSuggestion { get; set; }
        //Custom Code End

    }
}

