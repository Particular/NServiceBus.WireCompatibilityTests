
namespace CommonMessages
{
    public class DataBusResponseMessage
    {
        public byte[] PropertyDataBus { get; set; }
        public string Sender { get; set; }
        public string EncryptedProperty { get; set; }
    }
}