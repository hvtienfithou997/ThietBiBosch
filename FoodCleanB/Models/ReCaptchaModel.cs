using Newtonsoft.Json;
using System.Collections.Generic;

namespace FoodCleanB.Models
{
    public class ReCaptchaModel
    {
        public string Validate(string encodedResponse)
        {
            const string privateKey = "6LcCh94UAAAAAMczVBzMNU5vdhp_eBq5uGuhkPgW";
            string googleReply;

            using (var client = new System.Net.WebClient())
            {
                googleReply = 
                    client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={privateKey}&response={encodedResponse}");
            }

            var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaModel>(googleReply);
            Success = captchaResponse.Success;
            ErrorCodes = captchaResponse.ErrorCodes;

            return Success.ToLower();
        }

        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}