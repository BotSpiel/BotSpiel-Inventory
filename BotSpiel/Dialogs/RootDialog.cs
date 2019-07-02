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

namespace BotSpiel.Dialogs
{
    public class RootDialog : ComponentDialog
    {
        private const string RootDialogId = "rootDialog";
        private const string ChoicesPromptId = "choicesDialog";
        private const string TextPromptId = "textPrompt";
        private const string DialogKey = nameof(RootDialog);
        private const string DialogKeyOptions = "rootDialogOptions";

        private readonly BotUserEntityContext _botUserEntityContext;
        private readonly NavigationEntityData _navigationEntityData;

        public RootDialog(string id, BotUserEntityContext botUserEntityContext, NavigationEntityData navigationEntityData)
        : base(id)
        {
            InitialDialogId = Id;
            _botUserEntityContext = botUserEntityContext;
            _navigationEntityData = navigationEntityData;

            // Define the prompts used in the RootDialog.
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new ChoicePrompt(ChoicesPromptId));

            // Define the conversation flow for the RootDialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
                modulePrompt,
                entityPrompt,
                entityIntentPrompt,
                donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));

        }

        private async Task<DialogTurnResult> modulePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = _botUserEntityContext;
            DialogTurnResult returnResult = new DialogTurnResult(0);

            if (step.Options == null)
            {
                List<string> modules = new List<string>();
                _navigationEntityData.NavigationModules().ToList()
                    .ForEach(k => modules.Add(k.ToString()));

                returnResult = await step.PromptAsync(
                ChoicesPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("I know about the following areas. Please choose one that I can help with."),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(modules),
                },
                cancellationToken);
            }
            else
            {
                step.Values[DialogKeyOptions] = (BotUserEntityContext)step.Options;
                List<string> branchOptions = _navigationEntityData.BranchEntitiesForEntity(((BotUserEntityContext)step.Options).entity);

                returnResult = await step.PromptAsync(
                ChoicesPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What would like to do next?."),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(branchOptions),
                },
                cancellationToken);

            }
            return returnResult;
        }

        private async Task<DialogTurnResult> entityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);

            if (step.Options == null)
            {
                FoundChoice module = (FoundChoice)step.Result;
                ((BotUserEntityContext)step.Values[DialogKey]).module = module.Value;
                List<string> entities = _navigationEntityData.NavigationEntitiesForModule(module.Value.ToString());

                returnResult = await step.PromptAsync(
                    ChoicesPromptId,
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text($"The {module.Value} area has the following topics that I know about. Please choose one that I can help with."),
                        RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                        Choices = ChoiceFactory.ToChoices(entities),
                    },
                    cancellationToken);
            }
            else
            {
                FoundChoice branchChoice = (FoundChoice)step.Result;
                if (branchChoice.Value == "Choose an area")
                {
                    ((BotUserEntityContext)step.Values[DialogKey]).module = branchChoice.Value;

                    returnResult = await step.EndDialogAsync(
                        (BotUserEntityContext)step.Values[DialogKey],
                        cancellationToken);

                }
                else
                {
                    ((BotUserEntityContext)step.Values[DialogKey]).module = ((BotUserEntityContext)step.Values[DialogKeyOptions]).module;
                    ((BotUserEntityContext)step.Values[DialogKey]).entity = branchChoice.Value;
                    List<string> entityIntents = _navigationEntityData.CrudActionForEntities(branchChoice.Value.ToString());
                    entityIntents.Add("Find");

                    returnResult = await step.PromptAsync(
                        ChoicesPromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"With the {branchChoice.Value} I can do the following things.Please select one."),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(entityIntents),
                        },
                        cancellationToken);
                }

            }
            return returnResult;
        }

        private async Task<DialogTurnResult> entityIntentPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);

            if (step.Options == null)
            {
                FoundChoice entity = (FoundChoice)step.Result;
                ((BotUserEntityContext)step.Values[DialogKey]).entity = entity.Value;
                List<string> entityIntents = _navigationEntityData.CrudActionForEntities(entity.Value.ToString());
                entityIntents.Add("Find");

                returnResult = await step.PromptAsync(
                    ChoicesPromptId,
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text($"With the {entity.Value} I can do the following things.Please select one."),
                        RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                        Choices = ChoiceFactory.ToChoices(entityIntents),
                    },
                    cancellationToken);
            }
            else
            {
                FoundChoice entityintent = (FoundChoice)step.Result;
                ((BotUserEntityContext)step.Values[DialogKey]).entityIntent = entityintent.Value;


                returnResult = await step.EndDialogAsync(
                    (BotUserEntityContext)step.Values[DialogKey],
                    cancellationToken);
            }

            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice entityintent = (FoundChoice)step.Result;
            ((BotUserEntityContext)step.Values[DialogKey]).entityIntent = entityintent.Value;


            return await step.EndDialogAsync(
                (BotUserEntityContext)step.Values[DialogKey],
                cancellationToken);
        }
    }
}



