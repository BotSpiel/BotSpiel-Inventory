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
    public class CreateOutboundShipmentsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateOutboundShipmentsDialogId = "createOutboundShipmentsDialog";
       private const string FacilityPromptId = "facilityPrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string CarrierPromptId = "carrierPrompt";
        private const string CarrierConsignmentNumberPromptId = "carrierconsignmentnumberPrompt";
        private const string StatusPromptId = "statusPrompt";
        private const string AddressPromptId = "addressPrompt";
        private const string OutboundCarrierManifestPromptId = "outboundcarriermanifestPrompt";

        private const string DialogKey = nameof(CreateOutboundShipmentsDialog);
        private const string DialogKeyOptions = "createOutboundShipmentsDialogOptions";
        private const string SearchColumnsKey = "CreateOutboundShipmentsDialogSearchColumns";
        private const string SearchTextKey = "CreateOutboundShipmentsDialogSearchText";
        private const string EditColumnsKey = "CreateOutboundShipmentsDialogEditColumns";
        private const string EditTextKey = "CreateOutboundShipmentsDialogEditText";
        private const string SelectedRecordKey = "CreateOutboundShipmentsDialogSelectedRecordKey";

        private readonly IOutboundShipmentsService _outboundshipmentsService;
        OutboundShipmentsPost _outboundshipmentsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundshipments" };
        string[] edit = { "Edit outboundshipments" };
        string[] details = { "Display outboundshipments" };
        string[] delete = { "Delete outboundshipments" };

        public CreateOutboundShipmentsDialog(string id, IOutboundShipmentsService outboundshipmentsService, OutboundShipmentsPost outboundshipmentsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundshipmentsService = outboundshipmentsService;
            _outboundshipmentsPost = outboundshipmentsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> outboundshipmentValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_outboundshipmentsService.VerifyOutboundShipmentUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The outboundshipment {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(FacilityPromptId));
            AddDialog(new ChoicePrompt(CompanyPromptId));
            AddDialog(new ChoicePrompt(CarrierPromptId));
            AddDialog(new TextPrompt(CarrierConsignmentNumberPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));
            AddDialog(new ChoicePrompt(AddressPromptId));
            AddDialog(new ChoicePrompt(OutboundCarrierManifestPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             FacilityPrompt,
              CompanyPrompt,
              CarrierPrompt,
              CarrierConsignmentNumberPrompt,
              StatusPrompt,
              AddressPrompt,
              OutboundCarrierManifestPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> FacilityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new OutboundShipmentsPost();

            return await step.PromptAsync(
                FacilityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Facility:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectFacilities().Select(ct => ct.sFacility).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompanyPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Facility = (FoundChoice)step.Result;
            var ixFacility = _outboundshipmentsService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
            ((OutboundShipmentsPost)step.Values[DialogKey]).ixFacility = ixFacility;

            return await step.PromptAsync(
                CompanyPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Company:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectCompanies().Select(ct => ct.sCompany).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CarrierPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Company = (FoundChoice)step.Result;
            var ixCompany = _outboundshipmentsService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
            ((OutboundShipmentsPost)step.Values[DialogKey]).ixCompany = ixCompany;

            return await step.PromptAsync(
                CarrierPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Carrier:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectCarriers().Select(ct => ct.sCarrier).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CarrierConsignmentNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Carrier = (FoundChoice)step.Result;
            var ixCarrier = _outboundshipmentsService.selectCarriers().Where(ct => ct.sCarrier == _Carrier.Value).Select(ct => ct.ixCarrier).First();
            ((OutboundShipmentsPost)step.Values[DialogKey]).ixCarrier = ixCarrier;

            return await step.PromptAsync(
                CarrierConsignmentNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CarrierConsignmentNumber:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sCarrierConsignmentNumber = (string)step.Result;
            ((OutboundShipmentsPost)step.Values[DialogKey]).sCarrierConsignmentNumber = sCarrierConsignmentNumber;

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> AddressPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Status = (FoundChoice)step.Result;
            var ixStatus = _outboundshipmentsService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
            ((OutboundShipmentsPost)step.Values[DialogKey]).ixStatus = ixStatus;

            return await step.PromptAsync(
                AddressPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Address:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectAddresses().Select(ct => ct.sAddress).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> OutboundCarrierManifestPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Address = (FoundChoice)step.Result;
            var ixAddress = _outboundshipmentsService.selectAddresses().Where(ct => ct.sAddress == _Address.Value).Select(ct => ct.ixAddress).First();
            ((OutboundShipmentsPost)step.Values[DialogKey]).ixAddress = ixAddress;

            return await step.PromptAsync(
                OutboundCarrierManifestPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OutboundCarrierManifest:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectOutboundCarrierManifests().Select(ct => ct.sOutboundCarrierManifest).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixOutboundCarrierManifest = (Int64)step.Result;
            ((OutboundShipmentsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = ixOutboundCarrierManifest;


            return await step.EndDialogAsync(
                (OutboundShipmentsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


