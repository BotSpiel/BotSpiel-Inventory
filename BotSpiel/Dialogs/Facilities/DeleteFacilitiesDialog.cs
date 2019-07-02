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

namespace BotSpiel.Dialogs
{
    public class DeleteFacilitiesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteFacilitiesDialogId = "deleteFacilitiesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteFacilitiesDialog);
        private const string DialogKeyOptions = "deleteFacilitiesDialogOptions";
        private const string SearchColumnsKey = "DeleteFacilitiesDialogSearchColumns";
        private const string SearchTextKey = "DeleteFacilitiesDialogSearchText";
        private const string EditColumnsKey = "DeleteFacilitiesDialogEditColumns";
        private const string EditTextKey = "DeleteFacilitiesDialogEditText";
        private const string SelectedRecordKey = "DeleteFacilitiesDialogSelectedRecordKey";

        private readonly IFacilitiesService _facilitiesService;
        FacilitiesPost _facilitiesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilities" };
        string[] edit = { "Edit facilities" };
        string[] details = { "Display facilities" };
        string[] delete = { "Delete facilities" };

        public DeleteFacilitiesDialog(string id, IFacilitiesService facilitiesService, FacilitiesPost facilitiesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilitiesService = facilitiesService;
            _facilitiesPost = facilitiesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              confirmDeletePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> confirmDeletePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new FacilitiesPost();
            step.Values[DialogKeyOptions] = (FacilitiesPost)step.Options;
            ((FacilitiesPost)step.Values[DialogKey]).ixFacility = ((FacilitiesPost)step.Values[DialogKeyOptions]).ixFacility;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((FacilitiesPost)step.Options).sFacility}:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var yesNo = (bool)step.Result;

            if (!yesNo)
            {
                ((FacilitiesPost)step.Values[DialogKey]).ixFacility = -1;
            }

            return await step.EndDialogAsync(
                (FacilitiesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


