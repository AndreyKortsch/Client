using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

    namespace Приложение_с_нейросетью
    {
        public class EmbeddedResourceHelper
        {
            public string GetFileContent(string fileName)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = $"{assembly.GetName().Name}.Assets.{fileName}";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new Exception($"File '{fileName}' not found in embedded resources.");
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        return content;
                    }
                }
            }
        }
    }
