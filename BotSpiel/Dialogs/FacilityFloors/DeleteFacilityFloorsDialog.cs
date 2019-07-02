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
    public class DeleteFacilityFloorsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteFacilityFloorsDialogId = "deleteFacilityFloorsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteFacilityFloorsDialog);
        private const string DialogKeyOptions = "deleteFacilityFloorsDialogOptions";
        private const string SearchColumnsKey = "DeleteFacilityFloorsDialogSearchColumns";
        private const string SearchTextKey = "DeleteFacilityFloorsDialogSearchText";
        private const string EditColumnsKey = "DeleteFacilityFloorsDialogEditColumns";
        private const string EditTextKey = "DeleteFacilityFloorsDialogEditText";
        private const string SelectedRecordKey = "DeleteFacilityFloorsDialogSelectedRecordKey";

        private readonly IFacilityFloorsService _facilityfloorsService;
        FacilityFloorsPost _facilityfloorsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityfloors" };
        string[] edit = { "Edit facilityfloors" };
        string[] details = { "Display facilityfloors" };
        string[] delete = { "Delete facilityfloors" };

        public DeleteFacilityFloorsDialog(string id, IFacilityFloorsService facilityfloorsService, FacilityFloorsPost facilityfloorsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityfloorsService = facilityfloorsService;
            _facilityfloorsPost = facilityfloorsPost;

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
            step.Values[DialogKey] = new FacilityFloorsPost();
            step.Values[DialogKeyOptions] = (FacilityFloorsPost)step.Options;
            ((FacilityFloorsPost)step.Values[DialogKey]).ixFacilityFloor = ((FacilityFloorsPost)step.Values[DialogKeyOptions]).ixFacilityFloor;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((FacilityFloorsPost)step.Options).sFacilityFloor}:"),
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
                ((FacilityFloorsPost)step.Values[DialogKey]).ixFacilityFloor = -1;
            }

            return await step.EndDialogAsync(
                (FacilityFloorsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


