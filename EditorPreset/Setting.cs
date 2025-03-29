using System.IO;
using System.Xml.Serialization;
using static UnityModManagerNet.UnityModManager;

namespace EditorPreset
{
    public class Setting : ModSettings
    {
        public float cameraZoom = 100;
        public float cameraPosX = 0;
        public float cameraPosY = 0;
        public TrackAnimationType trackAnimation = TrackAnimationType.None;
        public TrackAnimationType trackDisappearAnimation = TrackAnimationType.None;
        public float trackBeatsAhead = 9;
        public float trackBeatsBehind = 0;
        public string levelAuthor = "만든이";
        public int songVolume = 100;

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
            return Path.Combine(modEntry.Path, GetType().Name + ".settings");
        }
    }
}
