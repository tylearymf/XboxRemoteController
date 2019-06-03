using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGlass
{

    public class MediaState
    {
        public uint TitleId { get; set; }
        public string AumId { get; set; }
        public string AssetId { get; set; }
        public MediaType MediaType { get; set; }
        public MediaSoundLevel SoundLevel { get; set; }
        public MediaControlCommands EnabledCommands { get; set; }
        public MediaPlaybackStatus PlaybackStatus { get; set; }
        public float PlaybackRate { get; set; }
        public TimeSpan Position { get; set; }
        public TimeSpan MediaStart { get; set; }
        public TimeSpan MediaEnd { get; set; }
        public TimeSpan MinimumSeek { get; set; }
        public TimeSpan MaximumSeek { get; set; }

        public IReadOnlyDictionary<string, string> Metadata { get; set; }

    }
}
