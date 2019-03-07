using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace EchoBotWithCounter.Dialogs
{
    public class OpcionalDialog : WaterfallDialog
    {
        public OpcionalDialog(string dialogId, IEnumerable<WaterfallStep> steps = null) : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                return await stepContext.PromptAsync(
                    "choicePrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply($"¡No entendí tu pregunta, tengo estas opciones que te pueden ayudar 😊"),
                        Choices = new[] { new Choice { Value = "Convalidaciones" }, new Choice { Value = "Resultados" }, new Choice { Value = "Convenios" }, new Choice { Value = "Matrícula" }, new Choice { Value = "Test vocacional" }, new Choice { Value = "Examen Admisión" }, new Choice { Value = "Pregunta abierta" }, new Choice { Value = "Feedback" } },

                    });
            });
        }
        public static new string Id => "opcionalDialog";

        public static OpcionalDialog Instance { get; } = new OpcionalDialog(Id);
    }
}
