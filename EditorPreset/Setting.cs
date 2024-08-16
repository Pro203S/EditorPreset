using System.IO;
using System.Xml.Serialization;
using static UnityModManagerNet.UnityModManager;

namespace EditorPreset
{
    public class Setting : ModSettings
    {
        public override void Save(ModEntry modEntry)
        {
            var filepath = GetPath(modEntry);
            try
            {
                using (var writer = new StreamWriter(filepath))
                {
                    var serializer = new XmlSerializer(GetType());
                    serializer.Serialize(writer, this);
                }
            }
            catch
            {
            }
        }

        public override string GetPath(ModEntry modEntry)
        {
            return Path.Combine(modEntry.Path, GetType().Name + ".xml");
        }
    }
}
