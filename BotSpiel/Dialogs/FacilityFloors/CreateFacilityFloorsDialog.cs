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
    public class CreateFacilityFloorsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateFacilityFloorsDialogId = "createFacilityFloorsDialog";
       private const string FacilityFloorPromptId = "facilityfloorPrompt";

        private const string DialogKey = nameof(CreateFacilityFloorsDialog);
        private const string DialogKeyOptions = "createFacilityFloorsDialogOptions";
        private const string SearchColumnsKey = "CreateFacilityFloorsDialogSearchColumns";
        private const string SearchTextKey = "CreateFacilityFloorsDialogSearchText";
        private const string EditColumnsKey = "CreateFacilityFloorsDialogEditColumns";
        private const string EditTextKey = "CreateFacilityFloorsDialogEditText";
        private const string SelectedRecordKey = "CreateFacilityFloorsDialogSelectedRecordKey";

        private readonly IFacilityFloorsService _facilityfloorsService;
        FacilityFloorsPost _facilityfloorsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityfloors" };
        string[] edit = { "Edit facilityfloors" };
        string[] details = { "Display facilityfloors" };
        string[] delete = { "Delete facilityfloors" };

        public CreateFacilityFloorsDialog(string id, IFacilityFloorsService facilityfloorsService, FacilityFloorsPost facilityfloorsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityfloorsService = facilityfloorsService;
            _facilityfloorsPost = facilityfloorsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> facilityfloorValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_facilityfloorsService.VerifyFacilityFloorUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The facilityfloor {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(FacilityFloorPromptId, facilityfloorValidator));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             FacilityFloorPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> FacilityFloorPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new FacilityFloorsPost();

            return await step.PromptAsync(
                FacilityFloorPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityFloor:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sFacilityFloor = (string)step.Result;
            ((FacilityFloorsPost)step.Values[DialogKey]).sFacilityFloor = sFacilityFloor;


            return await step.EndDialogAsync(
                (FacilityFloorsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


