namespace Codachin.API.Controllers
{
    internal class Response<T>
    {
        public Response()
        {
        }

        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public T Data { get; set; }

        public string Message { get; set; }

    }
}