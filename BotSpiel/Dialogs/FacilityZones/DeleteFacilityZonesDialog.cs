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
    public class DeleteFacilityZonesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteFacilityZonesDialogId = "deleteFacilityZonesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteFacilityZonesDialog);
        private const string DialogKeyOptions = "deleteFacilityZonesDialogOptions";
        private const string SearchColumnsKey = "DeleteFacilityZonesDialogSearchColumns";
        private const string SearchTextKey = "DeleteFacilityZonesDialogSearchText";
        private const string EditColumnsKey = "DeleteFacilityZonesDialogEditColumns";
        private const string EditTextKey = "DeleteFacilityZonesDialogEditText";
        private const string SelectedRecordKey = "DeleteFacilityZonesDialogSelectedRecordKey";

        private readonly IFacilityZonesService _facilityzonesService;
        FacilityZonesPost _facilityzonesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityzones" };
        string[] edit = { "Edit facilityzones" };
        string[] details = { "Display facilityzones" };
        string[] delete = { "Delete facilityzones" };

        public DeleteFacilityZonesDialog(string id, IFacilityZonesService facilityzonesService, FacilityZonesPost facilityzonesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityzonesService = facilityzonesService;
            _facilityzonesPost = facilityzonesPost;

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
            step.Values[DialogKey] = new FacilityZonesPost();
            step.Values[DialogKeyOptions] = (FacilityZonesPost)step.Options;
            ((FacilityZonesPost)step.Values[DialogKey]).ixFacilityZone = ((FacilityZonesPost)step.Values[DialogKeyOptions]).ixFacilityZone;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((FacilityZonesPost)step.Options).sFacilityZone}:"),
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
                ((FacilityZonesPost)step.Values[DialogKey]).ixFacilityZone = -1;
            }

            return await step.EndDialogAsync(
                (FacilityZonesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


