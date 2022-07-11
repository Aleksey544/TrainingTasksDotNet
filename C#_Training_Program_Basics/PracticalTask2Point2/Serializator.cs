using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace PracticalTask2Point2
{
    //Building a file system tree and serealizating it into .xml, .json and .dat files
    class Serializator
    {
        public static bool DisplayFileSystem(string path, ref List<string> FileSystemList)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                FileSystemList.Add($"{dirInfo.FullName}");

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    FileSystemList.Add($"\t{file.Name} -> size: {file.Length} bytes, " +
                    $"creation time: {file.CreationTime}, attributes: {file.Attributes}");
                }

                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                    DisplayFileSystem(dir.FullName, ref FileSystemList);
            }
            catch (Exception) //uncorrect path
            {
                FileSystemList.Clear();
                return true;
            }

            return false;
        }

        public static void XmlSerializator(List<string> FileSystemList, string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<string>));

            using (FileStream fs = new FileStream($"{fileName}.xml", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fs, FileSystemList);
                Console.WriteLine($"[XML INFO]: Directory structure was serialized into {fileName}.xml succesfully!");
            }
        }

        public static void JsonSerializator(List<string> FileSystemList, string fileName)
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<string>));

            using (FileStream fs = new FileStream($"{fileName}.json", FileMode.Create, FileAccess.Write))
            {
                formatter.WriteObject(fs, FileSystemList);
                Console.WriteLine($"[JSON INFO]: Directory structure was serialized into {fileName}.json succesfully!");
            }
        }

        public static void BinarySerializator(List<string> FileSystemList, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream($"{fileName}.dat", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fs, FileSystemList);
                Console.WriteLine($"[BINARY INFO]: Directory structure was serialized into {fileName}.dat succesfully!");
            }
        }
    }
}
