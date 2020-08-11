namespace xsolla_backend_card.Models.Response
{
    /// <summary>
    /// Класс для ответа сервера
    /// </summary>
    public class Response
    {
        //Сообщение
        public string Message { get; set; }
        
        //Данные
        public object Data { get; set; }
    }
}