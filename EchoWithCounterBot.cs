// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using EchoBotWithCounter.Dialogs;
using EchoBotWithCounter.Helpers;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace Microsoft.BotBuilderSamples
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class EchoWithCounterBot : IBot
    {
        public EchoBotAccessors EchoBotAccessors { get; }

        private readonly DialogSet _dialogSet;
        private readonly ILogger _logger;
        private static CounterState _counterState = new CounterState();
        public QnAMaker QnAMaker { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EchoWithCounterBot"/> class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public EchoWithCounterBot(EchoBotAccessors accessors, QnAMaker qnAMaker ,ILoggerFactory loggerFactory)
        {
      
            QnAMaker = qnAMaker;

          

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<EchoWithCounterBot>();
            _logger.LogTrace("EchoBot turn start.");


            var dialogState = accessors.DialogState;

            //// compose dialogs
            _dialogSet = new DialogSet(dialogState);
            _dialogSet.Add(PrincipalDialog.Instance);
            _dialogSet.Add(OpcionalDialog.Instance);

            //Prompts
            _dialogSet.Add(new ChoicePrompt("choicePrompt"));
            _dialogSet.Add(new TextPrompt("textPrompt"));
            _dialogSet.Add(new NumberPrompt<int>("numberPrompt"));

            EchoBotAccessors = accessors ?? throw new ArgumentNullException(nameof(accessors));
          

        }

        /// <summary>
        /// Every conversation turn for our Echo Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {

           
            
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                _counterState.Id = turnContext.Activity.Conversation.Id;

                turnContext.TurnState.Add("EchoBotAccessors", EchoBotAccessors);
                
           
                var dialogCtx = await _dialogSet.CreateContextAsync(turnContext, cancellationToken);

                if (!turnContext.Responded)
                {
                    var result = await QnAMaker.GetAnswersAsync(turnContext);

                    if (result != null && result.Length > 0)
                    {
                        await turnContext.SendActivityAsync($"{result[0].Answer}", cancellationToken: cancellationToken);
                        if(_counterState.TurnCount == 0)
                        {
                            await dialogCtx.BeginDialogAsync(PrincipalDialog.Id, cancellationToken);
                        }
                      
                    }
                    else
                    {

                        await dialogCtx.BeginDialogAsync(OpcionalDialog.Id, cancellationToken);
                        await dialogCtx.ContinueDialogAsync(cancellationToken);

                    }
                    await dialogCtx.ContinueDialogAsync(cancellationToken);
                }
                _counterState.TurnCount++;
            }





            //else
            //{
            //    await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            //}
        }
    }
}
