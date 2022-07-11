using System;
using System.Collections.Generic;

namespace PracticalTask2Point2
{
    //Handling entered command line arguments
    class CommandLineHandler
    {
        public static void Handler(string[] args)
        {
            if (args.Length == 3)
            {
                List<string> FileSystemList = new List<string>();
                string path = args[0];
                bool uncorrectPath = Serializator.DisplayFileSystem($@"{path}", ref FileSystemList);

                string fileName = args[1];
                string fileType = args[2];

                if (uncorrectPath)
                    Console.WriteLine("Entered path is uncorrect!");
                else if (fileType == "xml")
                    Serializator.XmlSerializator(FileSystemList, fileName);
                else if (fileType == "json")
                    Serializator.JsonSerializator(FileSystemList, fileName);
                else if (fileType == "dat")
                    Serializator.BinarySerializator(FileSystemList, fileName);
                else
                    Console.WriteLine("Entered file type is uncorrect!");
            }
            else if (args.Length == 2)
            {
                string fileName = args[0];
                string fileType = args[1];

                if (fileType == "xml")
                    Deserializator.XmlDeserializator(fileName);
                else if (fileType == "json")
                    Deserializator.JsonDeserializator(fileName);
                else if (fileType == "dat")
                    Deserializator.BinaryDeserializator(fileName);
                else
                    Console.WriteLine("Entered file type is uncorrect!");
            }
            else
            {
                Console.WriteLine("To serialize directory structure amount of command line arguments must be 3:\n" +
                "path to directory (E:\\ExampleDirectory), your file name (ExampleName), file type (xml, json, dat).\n" +
                "\nTo deserialize file and output deserialized data to console amount of command line arguments must be 2:\n" +
                "file name (it must be located in the same folder as the executable program file), file type (xml, json, dat).");
            }
        }
    }
}
