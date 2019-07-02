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
    public class EditCarriersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditCarriersDialogId = "editCarriersDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string CarrierPromptId = "carrierPrompt";
        private const string CarrierTypePromptId = "carriertypePrompt";
        private const string StandardCarrierAlphaCodePromptId = "standardcarrieralphacodePrompt";
        private const string CarrierConsignmentNumberPrefixPromptId = "carrierconsignmentnumberprefixPrompt";
        private const string CarrierConsignmentNumberStartPromptId = "carrierconsignmentnumberstartPrompt";
        private const string CarrierConsignmentNumberLastUsedPromptId = "carrierconsignmentnumberlastusedPrompt";
        private const string ScheduledPickupTimePromptId = "scheduledpickuptimePrompt";

        private const string DialogKey = nameof(EditCarriersDialog);
        private const string DialogKeyOptions = "editCarriersDialogOptions";
        private const string SearchColumnsKey = "EditCarriersDialogSearchColumns";
        private const string SearchTextKey = "EditCarriersDialogSearchText";
        private const string EditColumnsKey = "EditCarriersDialogEditColumns";
        private const string EditTextKey = "EditCarriersDialogEditText";
        private const string SelectedRecordKey = "EditCarriersDialogSelectedRecordKey";

        private readonly ICarriersService _carriersService;
        CarriersPost _carriersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit carriers" };
        string[] edit = { "Edit carriers" };
        string[] details = { "Display carriers" };
        string[] delete = { "Delete carriers" };

        public EditCarriersDialog(string id, ICarriersService carriersService, CarriersPost carriersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _carriersService = carriersService;
            _carriersPost = carriersPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> carrierValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_carriersService.VerifyCarrierUnique(_carriersPost.ixCarrier, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The carrier {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(CarrierPromptId, carrierValidator));
            AddDialog(new ChoicePrompt(CarrierTypePromptId));
            AddDialog(new TextPrompt(StandardCarrierAlphaCodePromptId));
            AddDialog(new TextPrompt(CarrierConsignmentNumberPrefixPromptId));
            AddDialog(new NumberPrompt<Int64>(CarrierConsignmentNumberStartPromptId));
            AddDialog(new NumberPrompt<Int64>(CarrierConsignmentNumberLastUsedPromptId));
            AddDialog(new DateTimePrompt(ScheduledPickupTimePromptId));

            AddDialog(new ChoicePrompt(ChoicePromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              chooseEditColumnPrompt,
              editColumnPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> chooseEditColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string editColumn = "";
            string editText = "";

            step.Values[DialogKey] = new CarriersPost();
            step.Values[DialogKeyOptions] = (CarriersPost)step.Options;
            step.Values[DialogKey] = _carriersService.GetPost(((CarriersPost)step.Options).ixCarrier);
            _carriersPost = _carriersService.GetPost(((CarriersPost)step.Options).ixCarrier);
            step.Values[SelectedRecordKey] = _carriersPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("Carriers");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose an attribute to change:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }



        private async Task<DialogTurnResult> editColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice editColumn = (FoundChoice)step.Result;
            step.Values[EditColumnsKey] = editColumn.Value;
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[EditColumnsKey])
            {
                case "Carrier":
					returnResult = await step.PromptAsync(
						CarrierPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Carrier:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "CarrierType":
					returnResult = await step.PromptAsync(
						CarrierTypePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CarrierType:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_carriersService.selectCarrierTypes().Select(ct => ct.sCarrierType).ToList()),
						},
						cancellationToken);
                    break;
                case "StandardCarrierAlphaCode":
					returnResult = await step.PromptAsync(
						StandardCarrierAlphaCodePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a StandardCarrierAlphaCode:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "CarrierConsignmentNumberPrefix":
					returnResult = await step.PromptAsync(
						CarrierConsignmentNumberPrefixPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CarrierConsignmentNumberPrefix:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "CarrierConsignmentNumberStart":
					returnResult = await step.PromptAsync(
						CarrierConsignmentNumberStartPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CarrierConsignmentNumberStart:"),
							RetryPrompt = MessageFactory.Text("Please enter an integer."),
						},
						cancellationToken);
                    break;
                case "CarrierConsignmentNumberLastUsed":
					returnResult = await step.PromptAsync(
						CarrierConsignmentNumberLastUsedPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CarrierConsignmentNumberLastUsed:"),
							RetryPrompt = MessageFactory.Text("Please enter an integer."),
						},
						cancellationToken);
                    break;
                case "ScheduledPickupTime":
					returnResult = await step.PromptAsync(
						ScheduledPickupTimePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ScheduledPickupTime:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            switch (step.Values[EditColumnsKey])
            {
                case "Carrier":
					var sCarrier = (string)step.Result;
					((CarriersPost)step.Values[DialogKey]).sCarrier = sCarrier;
                    break;
                case "CarrierType":
					FoundChoice _CarrierType = (FoundChoice)step.Result;
					var ixCarrierType = _carriersService.selectCarrierTypes().Where(ct => ct.sCarrierType == _CarrierType.Value).Select(ct => ct.ixCarrierType).First();
					((CarriersPost)step.Values[DialogKey]).ixCarrierType = ixCarrierType;
                    break;
                case "StandardCarrierAlphaCode":
					var sStandardCarrierAlphaCode = (string)step.Result;
					((CarriersPost)step.Values[DialogKey]).sStandardCarrierAlphaCode = sStandardCarrierAlphaCode;
                    break;
                case "CarrierConsignmentNumberPrefix":
					var sCarrierConsignmentNumberPrefix = (string)step.Result;
					((CarriersPost)step.Values[DialogKey]).sCarrierConsignmentNumberPrefix = sCarrierConsignmentNumberPrefix;
                    break;
                case "CarrierConsignmentNumberStart":
					var nCarrierConsignmentNumberStart = (Int64)step.Result;
					((CarriersPost)step.Values[DialogKey]).nCarrierConsignmentNumberStart = nCarrierConsignmentNumberStart;
                    break;
                case "CarrierConsignmentNumberLastUsed":
					var nCarrierConsignmentNumberLastUsed = (Int64)step.Result;
					((CarriersPost)step.Values[DialogKey]).nCarrierConsignmentNumberLastUsed = nCarrierConsignmentNumberLastUsed;
                    break;
                case "ScheduledPickupTime":
					var dtScheduledPickupTime = ((IList<DateTimeResolution>)step.Result).First();
					((CarriersPost)step.Values[DialogKey]).dtScheduledPickupTime = TimeSpan.Parse(dtScheduledPickupTime.Value);
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (CarriersPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


