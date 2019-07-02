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
    public class DeleteBusinessPartnersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteBusinessPartnersDialogId = "deleteBusinessPartnersDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteBusinessPartnersDialog);
        private const string DialogKeyOptions = "deleteBusinessPartnersDialogOptions";
        private const string SearchColumnsKey = "DeleteBusinessPartnersDialogSearchColumns";
        private const string SearchTextKey = "DeleteBusinessPartnersDialogSearchText";
        private const string EditColumnsKey = "DeleteBusinessPartnersDialogEditColumns";
        private const string EditTextKey = "DeleteBusinessPartnersDialogEditText";
        private const string SelectedRecordKey = "DeleteBusinessPartnersDialogSelectedRecordKey";

        private readonly IBusinessPartnersService _businesspartnersService;
        BusinessPartnersPost _businesspartnersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit businesspartners" };
        string[] edit = { "Edit businesspartners" };
        string[] details = { "Display businesspartners" };
        string[] delete = { "Delete businesspartners" };

        public DeleteBusinessPartnersDialog(string id, IBusinessPartnersService businesspartnersService, BusinessPartnersPost businesspartnersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _businesspartnersService = businesspartnersService;
            _businesspartnersPost = businesspartnersPost;

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
            step.Values[DialogKey] = new BusinessPartnersPost();
            step.Values[DialogKeyOptions] = (BusinessPartnersPost)step.Options;
            ((BusinessPartnersPost)step.Values[DialogKey]).ixBusinessPartner = ((BusinessPartnersPost)step.Values[DialogKeyOptions]).ixBusinessPartner;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((BusinessPartnersPost)step.Options).sBusinessPartner}:"),
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
                ((BusinessPartnersPost)step.Values[DialogKey]).ixBusinessPartner = -1;
            }

            return await step.EndDialogAsync(
                (BusinessPartnersPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


