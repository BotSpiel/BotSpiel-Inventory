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
    public class DeleteFacilityWorkAreasDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteFacilityWorkAreasDialogId = "deleteFacilityWorkAreasDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteFacilityWorkAreasDialog);
        private const string DialogKeyOptions = "deleteFacilityWorkAreasDialogOptions";
        private const string SearchColumnsKey = "DeleteFacilityWorkAreasDialogSearchColumns";
        private const string SearchTextKey = "DeleteFacilityWorkAreasDialogSearchText";
        private const string EditColumnsKey = "DeleteFacilityWorkAreasDialogEditColumns";
        private const string EditTextKey = "DeleteFacilityWorkAreasDialogEditText";
        private const string SelectedRecordKey = "DeleteFacilityWorkAreasDialogSelectedRecordKey";

        private readonly IFacilityWorkAreasService _facilityworkareasService;
        FacilityWorkAreasPost _facilityworkareasPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityworkareas" };
        string[] edit = { "Edit facilityworkareas" };
        string[] details = { "Display facilityworkareas" };
        string[] delete = { "Delete facilityworkareas" };

        public DeleteFacilityWorkAreasDialog(string id, IFacilityWorkAreasService facilityworkareasService, FacilityWorkAreasPost facilityworkareasPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityworkareasService = facilityworkareasService;
            _facilityworkareasPost = facilityworkareasPost;

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
            step.Values[DialogKey] = new FacilityWorkAreasPost();
            step.Values[DialogKeyOptions] = (FacilityWorkAreasPost)step.Options;
            ((FacilityWorkAreasPost)step.Values[DialogKey]).ixFacilityWorkArea = ((FacilityWorkAreasPost)step.Values[DialogKeyOptions]).ixFacilityWorkArea;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((FacilityWorkAreasPost)step.Options).sFacilityWorkArea}:"),
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
                ((FacilityWorkAreasPost)step.Values[DialogKey]).ixFacilityWorkArea = -1;
            }

            return await step.EndDialogAsync(
                (FacilityWorkAreasPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


