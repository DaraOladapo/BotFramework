using AssistantBot.CognitiveServices;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AssistantBot
{
    [Serializable]
    public class AssistantBotDialog : IDialog<object>
    {
        CognitiveClient _cognitiveClient;
        public AssistantBotDialog()
        {
            _cognitiveClient = new CognitiveClient();
        }
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            //Pass sentence to Spelling API and get reponse:
            var checkSpellingResponse = await _cognitiveClient.CheckSpelling(message.Text);
            StringBuilder stringBuilder = new StringBuilder();
            var standardReponse = message.From.Name + ", I checked your sentence, ";
            if (checkSpellingResponse.flaggedTokens.Count != 0)
            {
                checkSpellingResponse.flaggedTokens.ForEach(flag => flag.suggestions.ForEach(suggestion => stringBuilder.AppendLine(suggestion.suggestion)));
                standardReponse = standardReponse + "here is correct spelling: " + stringBuilder.ToString();
            }
            else
                standardReponse = standardReponse + "it is correct!";

            await context.PostAsync(standardReponse);
            context.Wait(MessageReceivedAsync);
        }
    }
}