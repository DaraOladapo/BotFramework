using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantBot.CognitiveServices
{
    public class CheckSpellingResponse
    {
        public string _type { get; set; }
        public List<FlaggedToken> flaggedTokens { get; set; }
    }

    public class Suggestion
    {
        public string suggestion { get; set; }
        public int score { get; set; }
    }

    public class FlaggedToken
    {
        public int offset { get; set; }
        public string token { get; set; }
        public string type { get; set; }
        public List<Suggestion> suggestions { get; set; }
    }
}