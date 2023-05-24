namespace OnlineStore.Rsponses
{
    public class ErrorResponse
    {
        public int Code { get; set; } = 500;
        public bool Status { get; set; }
        public string Msg { get; set; }

    }
}
