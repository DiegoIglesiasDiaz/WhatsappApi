using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace WhatsappApi.Services
{
    public class WhatsappCloudSendMessage : IWhatsappCloudSendMessage
    {
        public async Task<bool> Execute(object model)
        {
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string endpoint = "https://graph.facebook.com";
                string version = "v18.0";
                string phoneNumberId = "217487694777630";
                string accessToken = "EAAZAfwYy2OY4BOy3bFXHPHC4jqp1sf9kFr1ZBzaB5PKSPGjRZC0ctB103tAML0kWwnnJaU6OUKiA6Ph84zBvNcTrwvAkFLzEphE3boJYh9SLSoYZASZAvtJKW3z6ZCBW9zrthy4J5vY44rgbH9Eg474VJ05ZC0eeSjP18ARbCFMX49WcnoZB8ulZBSvTuEUNFZBD82";
                string uri = $"{endpoint}/{version}/{phoneNumberId}/messages";

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
