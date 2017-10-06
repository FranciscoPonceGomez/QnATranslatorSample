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
    }
}