using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace korp_1
{
    public class SerializeBasic<Type>
    {
        private string pathToFile;
        private string FileContent;

        public SerializeBasic(string path)
        {
            this.pathToFile = path;
        }

        public Type Deserialize()
        {
            using (StreamReader file = new StreamReader(pathToFile))
            {
                string jsonString = file.ReadToEnd();
                return JsonSerializer.Deserialize<Type>(jsonString);
            }
        }


        public void WriteToJson(Type content)
        {
            string newJson = JsonSerializer.Serialize(content);
            using (StreamWriter file = new StreamWriter(pathToFile))
            {
                file.Write(newJson);
            }
        }
    }
}
