using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WpfApp2
{
    public struct LauncherApplicationInformation
    {
        public long LastTimeApplicationStartUpUnixTimestamp;
        public long LastTimeApplicationUpdateGameFilesUnixTimestamp;
        
        public static void SaveToXml(LauncherApplicationInformation launcherApplicationInformation)
        {
            // ReSharper disable once HeapView.BoxingAllocation
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var writer = new XmlSerializer(launcherApplicationInformation.GetType());
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var file = new StreamWriter("LauncherApplicationInformation.xml");
            // ReSharper disable once HeapView.BoxingAllocation
            writer.Serialize(file, launcherApplicationInformation);
            file.Close();
        }
        
        public static void SaveToJson(LauncherApplicationInformation launcherApplicationInformation)
        {
            
            // ReSharper disable once HeapView.BoxingAllocation
            var json = JsonConvert.SerializeObject(launcherApplicationInformation);
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var file = new StreamWriter("LauncherApplicationInformation.json");
            file.Write(json);
            file.Close();
        }
        
    }
}