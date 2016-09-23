namespace CommonMessages
{
    public class DataBusSendMessage
    {
        public byte[] PropertyDataBus { get; set; }
        public string SentFrom { get; set; }
        public string EncryptedProperty { get; set; }
    }
}