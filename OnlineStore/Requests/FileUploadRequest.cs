namespace OnlineStore.Requests
{
    public class FileUploadRequest
    {
        public int? BookId { get; set; }
        public IFormFile? Files { get; set; }
        public int ImageType { get; set; }
    }
}
