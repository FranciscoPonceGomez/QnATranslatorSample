using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Newtonsoft.Json.Linq;

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
            string ApiKey = "96ab42c0eca04fe18084c861a1ac06a8";
            string targetLang = "es";

            var answer = result.Answers.First().Answer;
            Activity reply = ((Activity)context.Activity).CreateReply();
            var accessToken = await MessagesController.GetAuthenticationToken(ApiKey);
            reply.Text = await MessagesController.TranslateText(answer, targetLang, accessToken);

            await context.PostAsync(reply);
        }
    }
}