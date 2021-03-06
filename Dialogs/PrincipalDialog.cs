﻿using System;
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
                        Prompt = stepContext.Context.Activity.CreateReply($"¿En qué te puedo ayudar 😊?"),
                        Choices = new[] { new Choice { Value = "Convalidaciones" }, new Choice { Value = "Resultados" }, new Choice { Value = "Convenios" }, new Choice { Value = "Matrícula" }, new Choice { Value = "Test vocacional" }, new Choice { Value = "Examen Admisión" }, new Choice { Value = "Pregunta abierta" }, new Choice { Value = "Feedback" } },
                    });
            });
        }

        public static new string Id => "mainDialog";

        public static PrincipalDialog Instance { get; } = new PrincipalDialog(Id);
    }
}
