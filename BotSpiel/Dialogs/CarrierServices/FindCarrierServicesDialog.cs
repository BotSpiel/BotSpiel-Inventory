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
    public class FindCarrierServicesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditCarrierServicesDialogId = "editCarrierServicesDialog";
        private const string DetailsCarrierServicesDialogId = "detailsCarrierServicesDialog";
        private const string DeleteCarrierServicesDialogId = "deleteCarrierServicesDialog";

        private const string FindCarrierServicesDialogId = "findCarrierServicesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindCarrierServicesDialog);
        private const string DialogKeyOptions = "findCarrierServicesDialogOptions";
        private const string SearchColumnsKey = "FindCarrierServicesDialogSearchColumns";
        private const string SearchTextKey = "FindCarrierServicesDialogSearchText";
        private const string EditColumnsKey = "FindCarrierServicesDialogEditColumns";
        private const string EditTextKey = "FindCarrierServicesDialogEditText";
        private const string SelectedRecordKey = "FindCarrierServicesDialogSelectedRecordKey";

        private readonly ICarrierServicesService _carrierservicesService;
        CarrierServicesPost _carrierservicesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit carrierservices" };
        string[] edit = { "Edit carrierservices" };
        string[] details = { "Display carrierservices" };
        string[] delete = { "Delete carrierservices" };

        public FindCarrierServicesDialog(string id, ICarrierServicesService carrierservicesService, CarrierServicesPost carrierservicesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _carrierservicesService = carrierservicesService;
            _carrierservicesPost = carrierservicesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditCarrierServicesDialog(EditCarrierServicesDialogId, _carrierservicesService, _carrierservicesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteCarrierServicesDialog(DeleteCarrierServicesDialogId, _carrierservicesService, _carrierservicesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new CarrierServicesPost();
            step.Values[SelectedRecordKey] = _carrierservicesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("CarrierServices");

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
            var carrierservicesIndex = _carrierservicesService.Index();
            var recordCountTotal = carrierservicesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "CarrierService":
                    var searchRecordsCarrierService = carrierservicesIndex.Where(o => o.sCarrierService.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sCarrierService).Select(o => o.sCarrierService.ToString());
                    var recordCountCarrierService = searchRecordsCarrierService.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} carrierservices. Your search resulted in {recordCountCarrierService} records. I show the top 15. Please choose a CarrierService or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsCarrierService.Take(15).Union(refine).Union(exit).ToList()),
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
            var carrierservicesIndex = _carrierservicesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit carrierservices"))
            {

                if (selection.Value == "Refine search")
                {
                    ((CarrierServicesPost)step.Values[DialogKey]).ixCarrierService = 0;
                }
                else if (selection.Value == "Exit carrierservices")
                {
                    ((CarrierServicesPost)step.Values[DialogKey]).ixCarrierService = -1;
                }
                returnResult = await step.EndDialogAsync(
                (CarrierServicesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _carrierservicesService.GetPost(carrierservicesIndex.Where(o => o.sCarrierService == selection.Value).Select(o => o.ixCarrierService).First());
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
            var carrierservicesIndex = _carrierservicesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit carrierservices")
            {
                ((CarrierServicesPost)step.Values[DialogKey]).ixCarrierService = -1;
                returnResult = await step.EndDialogAsync(
                (CarrierServicesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit carrierservices") || (selection.Value == "Display carrierservices") || (selection.Value == "Delete carrierservices"))
            {
                currentBotUserData.ixCarrierService = ((CarrierServicesPost)step.Values[SelectedRecordKey]).ixCarrierService;
                switch (selection.Value)
                {
                    case "Edit carrierservices":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditCarrierServicesDialogId, (CarrierServicesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display carrierservices":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete carrierservices":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteCarrierServicesDialogId, (CarrierServicesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (CarrierServicesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


