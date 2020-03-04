using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
using BotSpiel.Services;
//Custom Code Start | Added Code Block 
using BotSpiel.Services.Utilities;
using BotSpiel.DataAccess.Utilities;
//Custom Code End

namespace BotSpiel.Dialogs
{
    public class CreateGetPickBatchesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateGetPickBatchesDialogId = "createGetPickBatchesDialog";
        private const string GetPickBatchPromptId = "getpickbatchPrompt";

        private const string DialogKey = nameof(CreateGetPickBatchesDialog);
        private const string DialogKeyOptions = "createGetPickBatchesDialogOptions";
        private const string SearchColumnsKey = "CreateGetPickBatchesDialogSearchColumns";
        private const string SearchTextKey = "CreateGetPickBatchesDialogSearchText";
        private const string EditColumnsKey = "CreateGetPickBatchesDialogEditColumns";
        private const string EditTextKey = "CreateGetPickBatchesDialogEditText";
        private const string SelectedRecordKey = "CreateGetPickBatchesDialogSelectedRecordKey";
        //Custom Code Start | Removed Block 
        //private readonly IGetPickBatchesService _getpickbatchesService;
        //Custom Code End			
        GetPickBatchesPost _getpickbatchesPost;

        //Custom Code Start | Added Code Block 
        private readonly IPickBatchesService _pickbatchesService;
        private readonly CommonLookUps _commonLookUps;
        //Custom Code End

        string[] refine = { "Refine search" };
        string[] exit = { "Exit getpickbatches" };
        string[] edit = { "Edit getpickbatches" };
        string[] details = { "Display getpickbatches" };
        string[] delete = { "Delete getpickbatches" };

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public CreateGetPickBatchesDialog(string id, IGetPickBatchesService getpickbatchesService, GetPickBatchesPost getpickbatchesPost, BotSpielUserStateAccessors statePropertyAccessor)
        //Replaced Code Block End
        public CreateGetPickBatchesDialog(string id, GetPickBatchesPost getpickbatchesPost, BotSpielUserStateAccessors statePropertyAccessor
            , IPickBatchesService pickbatchesService
            , CommonLookUps commonLookUps
            )
        //Custom Code End
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            //Custom Code Start | Removed Block 
            //_getpickbatchesService = getpickbatchesService;
            //Custom Code End			

            _getpickbatchesPost = getpickbatchesPost;


            //Custom Code Start | Added Code Block 

            _pickbatchesService = pickbatchesService;
            _commonLookUps = commonLookUps;

            PromptValidator<string> pickBatchPickValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value.Trim().ToLower();
                if (!_pickbatchesService.IndexDb().Where(x => x.sPickBatch.Trim().ToLower() == value && x.ixStatus != _commonLookUps.getStatuses().Where(s => s.sStatus == "Complete").Select(s => s.ixStatus).FirstOrDefault()).Any())
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The pick batch {value} does not exist or is already complete. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    if (_pickbatchesService.IndexDb().Where(x => x.sPickBatch.Trim().ToLower() == value && x.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Started").Select(s => s.ixStatus).FirstOrDefault() && !x.bMultiResource).Any())
                    {
                        await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The pick batch {value} has already been started and is flagged for a single picker. Please enter a different value or exit."), cancellationToken);
                        return false;
                    }
                    else if (_pickbatchesService.IndexDb().Where(x => x.sPickBatch.Trim().ToLower() == value && x.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Inactive").Select(s => s.ixStatus).FirstOrDefault()).Any())
                    {
                        await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The pick batch {value} has not been activated. Please activate or enter a different value or exit."), cancellationToken);
                        return false;
                    }
                    return true;
                }
            };
            //Custom Code End


            // Define the prompts used in the Dialog.
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //AddDialog(new TextPrompt(GetPickBatchPromptId));
            //Replaced Code Block End
            AddDialog(new TextPrompt(GetPickBatchPromptId, pickBatchPickValidator));
            //Custom Code End


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             GetPickBatchPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> GetPickBatchPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new GetPickBatchesPost();

            return await step.PromptAsync(
                GetPickBatchPromptId,
                new PromptOptions
                {

                    //Custom Code Start | Replaced Code Block
                    //Replaced Code Block Start
                    //Prompt = MessageFactory.Text($"Please enter a GetPickBatch:"),
                    //Replaced Code Block End
                    Prompt = MessageFactory.Text($"Please enter a Pick Batch to pick:"),
                    //Custom Code End
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sGetPickBatch = (string)step.Result;
            ((GetPickBatchesPost)step.Values[DialogKey]).sGetPickBatch = sGetPickBatch;


            return await step.EndDialogAsync(
                (GetPickBatchesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


