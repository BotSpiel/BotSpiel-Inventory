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
    public class EditCarrierServicesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditCarrierServicesDialogId = "editCarrierServicesDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string CarrierServicePromptId = "carrierservicePrompt";
        private const string CarrierPromptId = "carrierPrompt";

        private const string DialogKey = nameof(EditCarrierServicesDialog);
        private const string DialogKeyOptions = "editCarrierServicesDialogOptions";
        private const string SearchColumnsKey = "EditCarrierServicesDialogSearchColumns";
        private const string SearchTextKey = "EditCarrierServicesDialogSearchText";
        private const string EditColumnsKey = "EditCarrierServicesDialogEditColumns";
        private const string EditTextKey = "EditCarrierServicesDialogEditText";
        private const string SelectedRecordKey = "EditCarrierServicesDialogSelectedRecordKey";

        private readonly ICarrierServicesService _carrierservicesService;
        CarrierServicesPost _carrierservicesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit carrierservices" };
        string[] edit = { "Edit carrierservices" };
        string[] details = { "Display carrierservices" };
        string[] delete = { "Delete carrierservices" };

        public EditCarrierServicesDialog(string id, ICarrierServicesService carrierservicesService, CarrierServicesPost carrierservicesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _carrierservicesService = carrierservicesService;
            _carrierservicesPost = carrierservicesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> carrierserviceValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_carrierservicesService.VerifyCarrierServiceUnique(_carrierservicesPost.ixCarrierService, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The carrierservice {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(CarrierServicePromptId, carrierserviceValidator));
            AddDialog(new ChoicePrompt(CarrierPromptId));

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

            step.Values[DialogKey] = new CarrierServicesPost();
            step.Values[DialogKeyOptions] = (CarrierServicesPost)step.Options;
            step.Values[DialogKey] = _carrierservicesService.GetPost(((CarrierServicesPost)step.Options).ixCarrierService);
            _carrierservicesPost = _carrierservicesService.GetPost(((CarrierServicesPost)step.Options).ixCarrierService);
            step.Values[SelectedRecordKey] = _carrierservicesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("CarrierServices");

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
                case "CarrierService":
					returnResult = await step.PromptAsync(
						CarrierServicePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CarrierService:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Carrier":
					returnResult = await step.PromptAsync(
						CarrierPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Carrier:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_carrierservicesService.selectCarriers().Select(ct => ct.sCarrier).ToList()),
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
                case "CarrierService":
					var sCarrierService = (string)step.Result;
					((CarrierServicesPost)step.Values[DialogKey]).sCarrierService = sCarrierService;
                    break;
                case "Carrier":
					FoundChoice _Carrier = (FoundChoice)step.Result;
					var ixCarrier = _carrierservicesService.selectCarriers().Where(ct => ct.sCarrier == _Carrier.Value).Select(ct => ct.ixCarrier).First();
					((CarrierServicesPost)step.Values[DialogKey]).ixCarrier = ixCarrier;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (CarrierServicesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


