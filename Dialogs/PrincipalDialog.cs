using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace EchoBotWithCounter.Dialogs
{
    public class PrincipalDialog : WaterfallDialog
    {
        public PrincipalDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                return await stepContext.PromptAsync(
                    "choicePrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply($"🤖: ¡Bienvenido \n ¿En qué te puedo ayudar?"),
                        Choices = new[] { new Choice { Value = "Mi Matricula" }, new Choice { Value = "Mis Cursos" }, new Choice { Value = "Realizar Pago" }, new Choice { Value = "Inscripción" }, new Choice { Value = "Otros" } },
                    });
            });
        }

        public static new string Id => "mainDialog";

        public static PrincipalDialog Instance { get; } = new PrincipalDialog(Id);
    }
}
