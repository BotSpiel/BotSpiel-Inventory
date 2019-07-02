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
    public class CreateFacilityWorkAreasDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateFacilityWorkAreasDialogId = "createFacilityWorkAreasDialog";
       private const string FacilityWorkAreaPromptId = "facilityworkareaPrompt";

        private const string DialogKey = nameof(CreateFacilityWorkAreasDialog);
        private const string DialogKeyOptions = "createFacilityWorkAreasDialogOptions";
        private const string SearchColumnsKey = "CreateFacilityWorkAreasDialogSearchColumns";
        private const string SearchTextKey = "CreateFacilityWorkAreasDialogSearchText";
        private const string EditColumnsKey = "CreateFacilityWorkAreasDialogEditColumns";
        private const string EditTextKey = "CreateFacilityWorkAreasDialogEditText";
        private const string SelectedRecordKey = "CreateFacilityWorkAreasDialogSelectedRecordKey";

        private readonly IFacilityWorkAreasService _facilityworkareasService;
        FacilityWorkAreasPost _facilityworkareasPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityworkareas" };
        string[] edit = { "Edit facilityworkareas" };
        string[] details = { "Display facilityworkareas" };
        string[] delete = { "Delete facilityworkareas" };

        public CreateFacilityWorkAreasDialog(string id, IFacilityWorkAreasService facilityworkareasService, FacilityWorkAreasPost facilityworkareasPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityworkareasService = facilityworkareasService;
            _facilityworkareasPost = facilityworkareasPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> facilityworkareaValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_facilityworkareasService.VerifyFacilityWorkAreaUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The facilityworkarea {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(FacilityWorkAreaPromptId, facilityworkareaValidator));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             FacilityWorkAreaPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> FacilityWorkAreaPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new FacilityWorkAreasPost();

            return await step.PromptAsync(
                FacilityWorkAreaPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityWorkArea:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sFacilityWorkArea = (string)step.Result;
            ((FacilityWorkAreasPost)step.Values[DialogKey]).sFacilityWorkArea = sFacilityWorkArea;


            return await step.EndDialogAsync(
                (FacilityWorkAreasPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


