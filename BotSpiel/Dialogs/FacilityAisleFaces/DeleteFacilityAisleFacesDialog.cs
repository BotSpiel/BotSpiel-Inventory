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
    public class DeleteFacilityAisleFacesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteFacilityAisleFacesDialogId = "deleteFacilityAisleFacesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteFacilityAisleFacesDialog);
        private const string DialogKeyOptions = "deleteFacilityAisleFacesDialogOptions";
        private const string SearchColumnsKey = "DeleteFacilityAisleFacesDialogSearchColumns";
        private const string SearchTextKey = "DeleteFacilityAisleFacesDialogSearchText";
        private const string EditColumnsKey = "DeleteFacilityAisleFacesDialogEditColumns";
        private const string EditTextKey = "DeleteFacilityAisleFacesDialogEditText";
        private const string SelectedRecordKey = "DeleteFacilityAisleFacesDialogSelectedRecordKey";

        private readonly IFacilityAisleFacesService _facilityaislefacesService;
        FacilityAisleFacesPost _facilityaislefacesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityaislefaces" };
        string[] edit = { "Edit facilityaislefaces" };
        string[] details = { "Display facilityaislefaces" };
        string[] delete = { "Delete facilityaislefaces" };

        public DeleteFacilityAisleFacesDialog(string id, IFacilityAisleFacesService facilityaislefacesService, FacilityAisleFacesPost facilityaislefacesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityaislefacesService = facilityaislefacesService;
            _facilityaislefacesPost = facilityaislefacesPost;

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
            step.Values[DialogKey] = new FacilityAisleFacesPost();
            step.Values[DialogKeyOptions] = (FacilityAisleFacesPost)step.Options;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixFacilityAisleFace = ((FacilityAisleFacesPost)step.Values[DialogKeyOptions]).ixFacilityAisleFace;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((FacilityAisleFacesPost)step.Options).sFacilityAisleFace}:"),
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
                ((FacilityAisleFacesPost)step.Values[DialogKey]).ixFacilityAisleFace = -1;
            }

            return await step.EndDialogAsync(
                (FacilityAisleFacesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


