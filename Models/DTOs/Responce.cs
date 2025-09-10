namespace SrSur.Models.DTOs
{
    public class Responce<T>
    {
        public bool? succeeded { get; set; }
        public string? message { get; set; }
        public List<Exception>? errors { get; set; }
        public T? data { get; set; }

        public Responce()
        {

        }

        public Responce(T data, string message)
        {
            succeeded = true;
            this.message = message;
            this.data = data;
        }
        public Responce(string message)
        {
            succeeded = false;
            this.message = message;
        }
    }
}
