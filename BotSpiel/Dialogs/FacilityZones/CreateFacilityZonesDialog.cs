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
    public class CreateFacilityZonesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateFacilityZonesDialogId = "createFacilityZonesDialog";
       private const string FacilityZonePromptId = "facilityzonePrompt";

        private const string DialogKey = nameof(CreateFacilityZonesDialog);
        private const string DialogKeyOptions = "createFacilityZonesDialogOptions";
        private const string SearchColumnsKey = "CreateFacilityZonesDialogSearchColumns";
        private const string SearchTextKey = "CreateFacilityZonesDialogSearchText";
        private const string EditColumnsKey = "CreateFacilityZonesDialogEditColumns";
        private const string EditTextKey = "CreateFacilityZonesDialogEditText";
        private const string SelectedRecordKey = "CreateFacilityZonesDialogSelectedRecordKey";

        private readonly IFacilityZonesService _facilityzonesService;
        FacilityZonesPost _facilityzonesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityzones" };
        string[] edit = { "Edit facilityzones" };
        string[] details = { "Display facilityzones" };
        string[] delete = { "Delete facilityzones" };

        public CreateFacilityZonesDialog(string id, IFacilityZonesService facilityzonesService, FacilityZonesPost facilityzonesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityzonesService = facilityzonesService;
            _facilityzonesPost = facilityzonesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> facilityzoneValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_facilityzonesService.VerifyFacilityZoneUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The facilityzone {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(FacilityZonePromptId, facilityzoneValidator));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             FacilityZonePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> FacilityZonePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new FacilityZonesPost();

            return await step.PromptAsync(
                FacilityZonePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityZone:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sFacilityZone = (string)step.Result;
            ((FacilityZonesPost)step.Values[DialogKey]).sFacilityZone = sFacilityZone;


            return await step.EndDialogAsync(
                (FacilityZonesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


