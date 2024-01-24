using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatsappApi.Models;
using WhatsappApi.Services;
using WhatsappApi.Util;


//PUBLICAR APLICACION PARA PODER RECIBIR MENSAJES DEL USUARIO
namespace WhatsappApi.Controllers
{
    [ApiController]
    [Route("whatsapp")]
    public class WhatsappController : Controller
    {
        private readonly IWhatsappCloudSendMessage whatsappCloudSendMessage;
        private readonly IUtil util;
        public WhatsappController(IWhatsappCloudSendMessage _whatsappCloudSendMessage, IUtil _util)
        {
            whatsappCloudSendMessage = _whatsappCloudSendMessage;
            util = _util;
        }

        [HttpGet("test")]
        public async Task<ActionResult> Example()
        {
            var data = new
            {
                messaging_product = "whatsapp",
                to = "34634542109",
                type = "text",
                text = new
                {
                    body = "Esto es un mensaje de prueba"
                }
            };

            var result = await whatsappCloudSendMessage.Execute(data);

            return Ok("Example");
        }
        [HttpGet]

        public ActionResult VerifyToken()
        {
            string AccessToken = "EAAZAfwYy2OY4BOy3bFXHPHC4jqp1sf9kFr1ZBzaB5PKSPGjRZC0ctB103tAML0kWwnnJaU6OUKiA6Ph84zBvNcTrwvAkFLzEphE3boJYh9SLSoYZASZAvtJKW3z6ZCBW9zrthy4J5vY44rgbH9Eg474VJ05ZC0eeSjP18ARbCFMX49WcnoZB8ulZBSvTuEUNFZBD82";
            var token = Request.Query["hub.verify_token"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();

            if (challenge != null && token != null && token.Equals(AccessToken))
            {
                return Ok(challenge);
            }
            return BadRequest("Bad Token");

        }

        [HttpPost]

        public async Task<ActionResult> ReceivedMessage([FromBody] WhatsAppCloudModel body)
        {
            try
            {
                var message = body.Entry[0]?.Changes[0]?.Value?.Messages[0];
                if (message != null)
                {
                    var userNumber = message.From;
                    var userText = GetUserText(message);

                    object objectMessage;

                    if (userText.ToUpper().Contains("HOLA"))
                    {
                        objectMessage = util.TextMessage("Hola ¿Como te puedo ayudar? 😊", userNumber);
                    }
                    else if (userText.ToUpper().Contains("ADIOS"))
                    {
                        objectMessage = util.TextMessage("Adiós que tengas un buen día 😊", userNumber);
                    }
                    else
                    {
                        objectMessage = util.TextMessage("Lo siento, no puedo entenderte", userNumber);
                    }
                   
                    await whatsappCloudSendMessage.Execute(objectMessage);
                }

                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex)
            {
                return Ok("EVENT_RECEIVED");
            }
        }

        private string GetUserText(Message message)
        {
            string TypeMessage = message.Type;

            switch (TypeMessage.ToUpper())
            {
                case "TEXT":
                    {
                        return message.Text.Body;
                    }
                case "INTERACTIVE":
                    {
                        string interctiveType = message.Interactive.Type;
                        if (interctiveType.ToUpper() == "LIST_REPLY")
                        {
                            return message.Interactive.List_Reply.Title;
                        }
                        else if (interctiveType.ToUpper() == "Button_REPLY")
                        {
                            return message.Interactive.Button_Reply.Title;
                        }

                        return string.Empty;
                    }
                default:
                    {
                        return string.Empty;
                    }

            }
        }
    }

}