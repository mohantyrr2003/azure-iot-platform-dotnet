using System;
using System.IO;
using System.Reflection;

namespace Mmm.Platform.IoT.Config.Services.Models
{
    public class Logo
    {
        public const string NAME_HEADER = "Name";
        public const string IS_DEFAULT_HEADER = "IsDefault";
        public static readonly Logo Default;
        private const string SVG_TYPE = "image/svg+xml";
        private const string DEFAULT_LOGO_NAME = "Default Logo";

        static Logo()
        {
            var folder = Path.GetDirectoryName(typeof(Logo).GetTypeInfo().Assembly.Location);
            var path = $@"{folder}/Content/DefaultLogo.svg";
            var bytes = File.ReadAllBytes(path);
            Default = new Logo
            {
                Image = Convert.ToBase64String(bytes),
                Type = Logo.SVG_TYPE,
                Name = Logo.DEFAULT_LOGO_NAME,
                IsDefault = true
            };
        }

        public string Image { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        /* Converts image from base 64 string to byte array */
        public byte[] ConvertImageToBytes()
        {
            return Convert.FromBase64String(this.Image);
        }

        /* Converts given byte array to base 64 string and saves as image */
        public void SetImageFromBytes(byte[] imageBytes)
        {
            this.Image = Convert.ToBase64String(imageBytes);
        }
    }
}
