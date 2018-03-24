namespace Task.TestHelpers
{
    using System;
    using System.IO;

    public abstract class SerializationTester<TData, TSerializer>
    {
        protected TSerializer serializer;
        private bool showResult;

        public SerializationTester(TSerializer serializer, bool showResult = false)
        {
            this.serializer = serializer;
            this.showResult = showResult;
        }

        public TData SerializeAndDeserialize(TData data)
        {
            var stream = new MemoryStream();

            Console.WriteLine("Start serialization");
            this.Serialization(data, stream);
            Console.WriteLine("Serialization finished");

            if (this.showResult)
            {
                var r = Console.OutputEncoding.GetString(stream.GetBuffer(), 0, (int)stream.Length);
                Console.WriteLine(r);
            }

            stream.Seek(0, SeekOrigin.Begin);
            Console.WriteLine("Start deserialization");
            TData result = this.Deserialization(stream);
            Console.WriteLine("Deserialization finished");

            return result;
        }

        internal abstract TData Deserialization(MemoryStream stream);

        internal abstract void Serialization(TData data, MemoryStream stream);
    }
}
