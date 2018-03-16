using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;

namespace HadoopTrip
{
    [DataContract]
    public class AnObject
    {
        [DataMember]
        public decimal d;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program demonstrates a bug in Microsoft.Hadoop.Avro:");
            var serializer = Microsoft.Hadoop.Avro.AvroSerializer.Create<AnObject>();
            var theObjectWithADecimal = new AnObject() { d = 10.0m };

            Console.WriteLine("First serialize an object with decimal with Norwegian culture set.");
            var ms = new MemoryStream();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nn-NO");
            serializer.Serialize(ms, theObjectWithADecimal);

            Console.WriteLine("Then switch to English culture and deserialize.");
            ms.Position = 0;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var roudTripped = serializer.Deserialize(ms);

            Console.WriteLine($"\nThe result: {theObjectWithADecimal.d} became {roudTripped.d} after a roundtrip in the serializer.");
            Console.WriteLine(theObjectWithADecimal.d == roudTripped.d ? ":-)" : ":-(");
            Console.WriteLine("Hit any key...");
            Console.ReadKey();
        }
    }

}
