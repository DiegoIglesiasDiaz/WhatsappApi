namespace WhatsappApi.Util
{
    public class Util : IUtil
    {
        public object TextMessage(string message, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message
                }
            };
        }
        public object ImageMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "image",
                image = new
                {
                    link = url
                }
            };
        }
        public object AudioMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
        }
        public object VideoMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "video",
                video = new
                {
                    link = url
                }
            };
        }
        public object DocumentMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "document",
                document = new
                {
                    link = url
                }
            };
        }
        public object LocationMessage(string latitude, string longitude,string? name, string? address, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "location",
                location = new
                {
                   latitude = latitude,
                   longitude = longitude,
                   name = name,
                   address = address
                }
            };
        }
        public object ButtonMessage(string text,string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "interactive",
                interactive = new
                {
                   type = "button",
                   body = new
                   {
                       text = text
                   },
                   action = new
                   {
                       buttons = new List<object>
                       {
                           new
                           {
                               type = "reply",
                               reply = new
                               {
                                   id = "01",
                                   title = "Si"
                               }
                           },
                           new
                           {
                               type = "reply",
                               reply = new
                               {
                                   id = "02",
                                   title = "No"
                               }
                           }
                       }
                   }
                }
            };
        }
    }
}
