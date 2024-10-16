using Exiled.API.Interfaces;
using Org.BouncyCastle.Utilities.Encoders;
using UnityEngine;

namespace ScpList
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float Showtime { get; set; } = 30f;
        public int Showduration { get; set; } = 5;
        public string FirstColorHex { get; set; } = "#FFFFFF";
        public string SecondColorHex { get; set; } = "#ff0000";
        public string MainColorHex { get; set; } = "#ff0000";
        public string TextShow { get; set; } = "Alive SCPs:";

    }
}
