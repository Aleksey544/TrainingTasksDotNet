using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace PracticalTask2Point2
{
    //Deserializating from .xml, .json, .dat files and outputting deserialized data to the console
    class Deserializator
    {
        public static void XmlDeserializator(string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<string>));

            using (FileStream fs = new FileStream($"{fileName}.xml", FileMode.OpenOrCreate))
            {
                List<string> DeserializedFile = (List<string>)formatter.Deserialize(fs);

                foreach (string item in DeserializedFile)
                    Console.WriteLine(item);

                Console.WriteLine($"[XML INFO]: Directory structure was deserialized from {fileName}.xml succesfully!");
            }
        }

        public static void JsonDeserializator(string fileName)
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<string>));

            using (FileStream fs = new FileStream($"{fileName}.json", FileMode.OpenOrCreate))
            {
                List<string> DeserializedFile = (List<string>)formatter.ReadObject(fs);

                foreach (string item in DeserializedFile)
                    Console.WriteLine(item);

                Console.WriteLine($"[JSON INFO]: Directory structure was deserialized from {fileName}.json succesfully!");
            }
        }

        public static void BinaryDeserializator(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream($"{fileName}.dat", FileMode.OpenOrCreate))
            {
                List<string> DeserializedFile = (List<string>)formatter.Deserialize(fs);

                foreach (string item in DeserializedFile)
                    Console.WriteLine(item);

                Console.WriteLine($"[BINARY INFO]: Directory structure was deserialized from {fileName}.dat succesfully!");
            }
        }
    }
}
