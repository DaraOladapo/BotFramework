using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using Microsoft.ProjectOxford.Text.Sentiment;

namespace FirstBot
{
    public class AssistantBotDialog : IDialog<object>
    {
        Microsoft.ProjectOxford.Text.Sentiment.SentimentClient _cognitiveClient;
        public AssistantBotDialog()
        {
            _cognitiveClient = new Microsoft.ProjectOxford.Text.Sentiment.SentimentClient(ConfigurationManager.AppSettings["CognitiveServicesApiToken"]);
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var document = new SentimentDocument()
            {
                Id = "123",
                Text = message.Text,
                Language = "en"
            };
            var request = new SentimentRequest();
            request.Documents.Add(document);

            var response = await _cognitiveClient.GetSentimentAsync(request);
            var retrievedDocument = response.Documents.FirstOrDefault();
            var sentimentScore = retrievedDocument.Score;

            await context.PostAsync(message.From.Name + " sentiment for your sentence is: " + sentimentScore + "%");
            context.Wait(MessageReceivedAsync);
        }

    }
}