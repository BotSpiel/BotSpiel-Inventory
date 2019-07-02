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
    public class DeleteCarriersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteCarriersDialogId = "deleteCarriersDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteCarriersDialog);
        private const string DialogKeyOptions = "deleteCarriersDialogOptions";
        private const string SearchColumnsKey = "DeleteCarriersDialogSearchColumns";
        private const string SearchTextKey = "DeleteCarriersDialogSearchText";
        private const string EditColumnsKey = "DeleteCarriersDialogEditColumns";
        private const string EditTextKey = "DeleteCarriersDialogEditText";
        private const string SelectedRecordKey = "DeleteCarriersDialogSelectedRecordKey";

        private readonly ICarriersService _carriersService;
        CarriersPost _carriersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit carriers" };
        string[] edit = { "Edit carriers" };
        string[] details = { "Display carriers" };
        string[] delete = { "Delete carriers" };

        public DeleteCarriersDialog(string id, ICarriersService carriersService, CarriersPost carriersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _carriersService = carriersService;
            _carriersPost = carriersPost;

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
            step.Values[DialogKey] = new CarriersPost();
            step.Values[DialogKeyOptions] = (CarriersPost)step.Options;
            ((CarriersPost)step.Values[DialogKey]).ixCarrier = ((CarriersPost)step.Values[DialogKeyOptions]).ixCarrier;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((CarriersPost)step.Options).sCarrier}:"),
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
                ((CarriersPost)step.Values[DialogKey]).ixCarrier = -1;
            }

            return await step.EndDialogAsync(
                (CarriersPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


