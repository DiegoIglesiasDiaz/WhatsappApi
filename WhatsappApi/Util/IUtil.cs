namespace WhatsappApi.Util
{
    public interface IUtil
    {
        object TextMessage(string message, string number);
        object ImageMessage(string url, string number);
        object AudioMessage(string url, string number);
        object VideoMessage(string url, string number);
        object DocumentMessage(string url, string number);
        object LocationMessage(string latitude, string longitude, string? name, string? address, string number);
        object ButtonMessage(string text, string number);

    }
}
