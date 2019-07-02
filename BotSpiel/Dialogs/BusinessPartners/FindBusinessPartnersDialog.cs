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
    public class FindBusinessPartnersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditBusinessPartnersDialogId = "editBusinessPartnersDialog";
        private const string DetailsBusinessPartnersDialogId = "detailsBusinessPartnersDialog";
        private const string DeleteBusinessPartnersDialogId = "deleteBusinessPartnersDialog";

        private const string FindBusinessPartnersDialogId = "findBusinessPartnersDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindBusinessPartnersDialog);
        private const string DialogKeyOptions = "findBusinessPartnersDialogOptions";
        private const string SearchColumnsKey = "FindBusinessPartnersDialogSearchColumns";
        private const string SearchTextKey = "FindBusinessPartnersDialogSearchText";
        private const string EditColumnsKey = "FindBusinessPartnersDialogEditColumns";
        private const string EditTextKey = "FindBusinessPartnersDialogEditText";
        private const string SelectedRecordKey = "FindBusinessPartnersDialogSelectedRecordKey";

        private readonly IBusinessPartnersService _businesspartnersService;
        BusinessPartnersPost _businesspartnersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit businesspartners" };
        string[] edit = { "Edit businesspartners" };
        string[] details = { "Display businesspartners" };
        string[] delete = { "Delete businesspartners" };

        public FindBusinessPartnersDialog(string id, IBusinessPartnersService businesspartnersService, BusinessPartnersPost businesspartnersPost, BotSpielUserStateAccessors statePropertyAccessor)
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

            AddDialog(new EditBusinessPartnersDialog(EditBusinessPartnersDialogId, _businesspartnersService, _businesspartnersPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteBusinessPartnersDialog(DeleteBusinessPartnersDialogId, _businesspartnersService, _businesspartnersPost, _botSpielUserStateAccessors));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             chooseSearchColumnPrompt,
             enterSearchTextPrompt,
             selectFromResultPrompt,
             editDeleteDetailsPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }

        private async Task<DialogTurnResult> chooseSearchColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string searchColumn = "";
            string searchText = "";

            step.Values[DialogKey] = new BusinessPartnersPost();
            step.Values[SelectedRecordKey] = _businesspartnersPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("BusinessPartners");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose a column to search on:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> enterSearchTextPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice searchColumn = (FoundChoice)step.Result;
            step.Values[SearchColumnsKey] = searchColumn.Value;

            return await step.PromptAsync(
                TextPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter text to search for in {step.Values[SearchColumnsKey]}:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }


        private async Task<DialogTurnResult> selectFromResultPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[SearchTextKey] = (string)step.Result;
            var businesspartnersIndex = _businesspartnersService.Index();
            var recordCountTotal = businesspartnersIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "BusinessPartner":
                    var searchRecordsBusinessPartner = businesspartnersIndex.Where(o => o.sBusinessPartner.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sBusinessPartner).Select(o => o.sBusinessPartner.ToString());
                    var recordCountBusinessPartner = searchRecordsBusinessPartner.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} businesspartners. Your search resulted in {recordCountBusinessPartner} records. I show the top 15. Please choose a BusinessPartner or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsBusinessPartner.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }



        private async Task<DialogTurnResult> editDeleteDetailsPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);
            var businesspartnersIndex = _businesspartnersService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit businesspartners"))
            {

                if (selection.Value == "Refine search")
                {
                    ((BusinessPartnersPost)step.Values[DialogKey]).ixBusinessPartner = 0;
                }
                else if (selection.Value == "Exit businesspartners")
                {
                    ((BusinessPartnersPost)step.Values[DialogKey]).ixBusinessPartner = -1;
                }
                returnResult = await step.EndDialogAsync(
                (BusinessPartnersPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _businesspartnersService.GetPost(businesspartnersIndex.Where(o => o.sBusinessPartner == selection.Value).Select(o => o.ixBusinessPartner).First());
                returnResult = await step.PromptAsync(
                    ChoicePromptId,
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text($"Would you like to edit, display or delete {selection.Value}. Please choose an option or exit to continue."),
                        RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                        Choices = ChoiceFactory.ToChoices(edit.Union(details).Union(delete).Union(exit).ToList())
                    },
                    cancellationToken);
            }
            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);

            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            var businesspartnersIndex = _businesspartnersService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit businesspartners")
            {
                ((BusinessPartnersPost)step.Values[DialogKey]).ixBusinessPartner = -1;
                returnResult = await step.EndDialogAsync(
                (BusinessPartnersPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit businesspartners") || (selection.Value == "Display businesspartners") || (selection.Value == "Delete businesspartners"))
            {
                currentBotUserData.ixBusinessPartner = ((BusinessPartnersPost)step.Values[SelectedRecordKey]).ixBusinessPartner;
                switch (selection.Value)
                {
                    case "Edit businesspartners":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditBusinessPartnersDialogId, (BusinessPartnersPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display businesspartners":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete businesspartners":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteBusinessPartnersDialogId, (BusinessPartnersPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (BusinessPartnersPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


