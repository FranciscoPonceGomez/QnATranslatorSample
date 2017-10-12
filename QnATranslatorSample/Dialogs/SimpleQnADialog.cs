using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Newtonsoft.Json.Linq;
using QnATranslatorSample.Utils;

namespace QnATranslatorSample.Dialogs
{
    [Serializable]
    [QnAMakerSettings]
    public class SimpleQnADialog : QnAMakerDialog
    {
        [Serializable]
        public class QnAMakerSettingsAttribute : QnAMakerAttribute
        {
            public QnAMakerSettingsAttribute()
                : base(ConfigurationManager.AppSettings["QnASubscriptionKey"], ConfigurationManager.AppSettings["QnAKbId"])
            {
            }
        }
        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            var answer = result.Answers.First().Answer;
            Activity reply = ((Activity)context.Activity).CreateReply();
            var accessToken = await Translator.GetAuthenticationToken(ConfigurationManager.AppSettings["TranslatorApiKey"]).ConfigureAwait(false);
            string targetLang = await Translator.DetectLanguage(answer, accessToken).ConfigureAwait(false);
            reply.Text = await Translator.TranslateText(answer, Translator.originLanguage, accessToken).ConfigureAwait(false);

            await context.PostAsync(reply);
        }
    }
}