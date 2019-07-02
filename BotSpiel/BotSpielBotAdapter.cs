using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using BotSpiel.DataAccess.Data;
using BotSpiel.DataAccess.Models;
using BotSpiel.Services;

namespace BotSpiel
{
    public class BotSpielBotAdapter : BotAdapter
    {

        private readonly bool _sendTraceActivity;
        private object _conversationLock = new object();
        private object _activeQueueLock = new object();

        private int _nextId = 0;
        string _messageId;
        long _nMessageId;

        private readonly IBotspielBotMessagesService _botspielbotmessagesService;
        BotspielBotMessagesPost botspielbotmessages;

        public BotSpielBotAdapter(IBotspielBotMessagesService botspielbotmessagesService, ConversationReference conversation = null, bool sendTraceActivity = false)
            : base()
        {
            _botspielbotmessagesService = botspielbotmessagesService ?? throw new System.ArgumentNullException("botspielbotmessagesService cannot be null"); ;
            _sendTraceActivity = sendTraceActivity;
            if (conversation != null)
            {
                Conversation = conversation;
            }
            else
            {
                Conversation = new ConversationReference
                {
                    ChannelId = "BotSpielwebApp",
                    ServiceUrl = "http://localhost:3978",
                };

                Conversation.User = new ChannelAccount(id: "user", name: "User");
                Conversation.Bot = new ChannelAccount(id: "bot", name: "Bot");
                Conversation.Conversation = new ConversationAccount(false, "convo", "webAppConversation");
            }

        }

        public ConversationReference Conversation { get; set; }
        public Queue<Activity> ActiveQueue { get; } = new Queue<Activity>();

        public new BotSpielBotAdapter Use(IMiddleware middleware)
        {
            base.Use(middleware);
            return this;
        }

        public Task UpdateConversationUser(string currentUserName)
        {
            Conversation.User.Id = currentUserName;
            Conversation.User.Name = currentUserName;
            Conversation.Conversation.Id = currentUserName;

            return null;
        }

        public async Task ProcessActivityAsync(Activity activity, BotCallbackHandler callback, CancellationToken cancellationToken = default(CancellationToken))
        {

            lock (_conversationLock)
            {
                if (activity.Type == null)
                {
                    activity.Type = ActivityTypes.Message;
                }

                activity.ChannelId = Conversation.ChannelId;
                activity.From = Conversation.User;
                activity.Recipient = Conversation.Bot;
                activity.Conversation = Conversation.Conversation;
                activity.ServiceUrl = Conversation.ServiceUrl;

                var id = activity.Id = (_nextId++).ToString();
            }

            if (activity.Timestamp == null || activity.Timestamp == default(DateTimeOffset))
            {
                activity.Timestamp = DateTime.UtcNow;
            }

            using (var context = new TurnContext(this, activity))
            {
                await RunPipelineAsync(context, callback, cancellationToken).ConfigureAwait(false);
            }
        }

        public async override Task<ResourceResponse[]> SendActivitiesAsync(ITurnContext turnContext, Activity[] activities, CancellationToken cancellationToken)
        {
            if (turnContext == null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            if (activities == null)
            {
                throw new ArgumentNullException(nameof(activities));
            }

            if (activities.Length == 0)
            {
                throw new ArgumentException("Expecting one or more activities, but the array was empty.", nameof(activities));
            }

            var responses = new ResourceResponse[activities.Length];

            for (var index = 0; index < activities.Length; index++)
            {
                var activity = activities[index];

                if (string.IsNullOrEmpty(activity.Id))
                {
                    activity.Id = Guid.NewGuid().ToString("n");
                }

                if (activity.Timestamp == null)
                {
                    activity.Timestamp = DateTime.UtcNow;
                }

                if (activity.Type == ActivityTypesEx.Delay)
                {

                    var delayMs = (int)activity.Value;

                    await Task.Delay(delayMs).ConfigureAwait(false);
                }
                else if (activity.Type == ActivityTypes.Trace)
                {
                    if (_sendTraceActivity)
                    {
                        lock (_activeQueueLock)
                        {
                            ActiveQueue.Enqueue(activity);
                        }
                    }
                }
                else
                {
                    lock (_activeQueueLock)
                    {
                        ActiveQueue.Enqueue(activity);
                    }
                }

                responses[index] = new ResourceResponse(activity.Id);
            }

            return responses;
        }

        public override Task<ResourceResponse> UpdateActivityAsync(ITurnContext turnContext, Activity activity, CancellationToken cancellationToken)
        {
            lock (_activeQueueLock)
            {
                var replies = ActiveQueue.ToList();
                for (int i = 0; i < ActiveQueue.Count; i++)
                {
                    if (replies[i].Id == activity.Id)
                    {
                        replies[i] = activity;
                        ActiveQueue.Clear();
                        foreach (var item in replies)
                        {
                            ActiveQueue.Enqueue(item);
                        }

                        return Task.FromResult(new ResourceResponse(activity.Id));
                    }
                }
            }

            return Task.FromResult(new ResourceResponse());
        }

        // Deletes an existing activity in the ActiveQueue.
        public override Task DeleteActivityAsync(ITurnContext turnContext, ConversationReference reference, CancellationToken cancellationToken)
        {
            lock (_activeQueueLock)
            {
                var replies = ActiveQueue.ToList();
                for (int i = 0; i < ActiveQueue.Count; i++)
                {
                    if (replies[i].Id == reference.ActivityId)
                    {
                        replies.RemoveAt(i);
                        ActiveQueue.Clear();
                        foreach (var item in replies)
                        {
                            ActiveQueue.Enqueue(item);
                        }

                        break;
                    }
                }
            }

            return Task.CompletedTask;
        }

        public Task CreateConversationAsync(string channelId, BotCallbackHandler callback, CancellationToken cancellationToken)
        {
            ActiveQueue.Clear();
            var update = Activity.CreateConversationUpdateActivity();
            update.Conversation = new ConversationAccount() { Id = Guid.NewGuid().ToString("n") };
            var context = new TurnContext(this, (Activity)update);
            return callback(context, cancellationToken);
        }

        public Activity GetNextReply()
        {
            lock (_activeQueueLock)
            {
                if (ActiveQueue.Count > 0)
                {
                    return ActiveQueue.Dequeue();
                }
            }

            return null;
        }

        public Activity MakeActivity(string text = null, string messageId = null)
        {
            Activity activity = new Activity
            {
                Type = ActivityTypes.Message,
                From = Conversation.User,
                Recipient = Conversation.Bot,
                Conversation = Conversation.Conversation,
                ServiceUrl = Conversation.ServiceUrl,
                Id = messageId ?? (_nextId++).ToString(),
                Text = text,
            };

            return activity;
        }

        public Task SendTextToBotAsync(string userSays, string messageId, BotCallbackHandler callback, CancellationToken cancellationToken)
        {
            _messageId = messageId;
            return ProcessActivityAsync(MakeActivity(userSays, messageId), callback, cancellationToken);
        }

    }
}
