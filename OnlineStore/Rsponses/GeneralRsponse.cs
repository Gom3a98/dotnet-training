namespace OnlineStore.Rsponses
{
    public class GeneralRsponse<T>
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }

        public List<T> Data = new List<T>();
    }
}
