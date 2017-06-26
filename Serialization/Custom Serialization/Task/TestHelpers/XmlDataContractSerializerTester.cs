namespace Task.TestHelpers
{
    using System.IO;
    using System.Runtime.Serialization;

    public class XmlDataContractSerializerTester<T> : SerializationTester<T, XmlObjectSerializer>
    {
        public XmlDataContractSerializerTester(XmlObjectSerializer serializer, bool showResult = false)
            : base(serializer, showResult)
        {
        }

        internal override T Deserialization(MemoryStream stream)
        {
            return (T)this.serializer.ReadObject(stream);
        }

        internal override void Serialization(T data, MemoryStream stream)
        {
            this.serializer.WriteObject(stream, data);
        }
    }
}
