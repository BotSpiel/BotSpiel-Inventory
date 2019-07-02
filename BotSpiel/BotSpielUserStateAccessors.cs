using System;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using BotSpiel.DataAccess.Models;

namespace BotSpiel
{
    public class BotSpielUserStateAccessors
    {
        // The property accessor keys to use.
        public const string DidBotWelcomeUserName = "BotSpiel.DidBotWelcomeUser";
        public const string BotUserDataAccessorName = "BotSpiel.BotUserData";
        public const string DialogStateAccessorName = "BotSpiel.DialogState";

        public BotSpielUserStateAccessors(ConversationState conversationState, UserState userState)
        {
            UserState = userState ?? throw new ArgumentNullException(nameof(userState));
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        }

        public IStatePropertyAccessor<bool> DidBotWelcomeUser { get; set; }
        public IStatePropertyAccessor<BotUserData> BotUserDataAccessor { get; set; }
        public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }
        public ConversationState ConversationState { get; }
        public UserState UserState { get; }

    }
}