using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinectPhotobooth.Models
{
    public sealed class ImageModel:IDisposable
    {
        public bool ShowTrails { get; set; }
        public bool PersonFill { get; set; }
        public int DepthHeight { get; set; }
        public int DepthWidth {get; set;}
        
        public int ColorHeight {get;set;}
        public int ColorWidth {get;set;}

        public byte[] DisplayPixels { get; set; }
        public byte[] ColorFrameData { get; set; }
        public byte[] BodyIndexFrameData { get; set; }

        public Body[] Bodies { get; set; }
        public ColorSpacePoint[] ColorPoints { get; set; }
        public int BytesPerPixel { get; set; }
        public PixelFormat PixelFormat { get; set; }
        public void Dispose()
        {
            
            GC.SuppressFinalize(this);
        }
    }
}
