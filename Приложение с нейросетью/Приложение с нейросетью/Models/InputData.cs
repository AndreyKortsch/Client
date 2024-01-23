using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.ML.Transforms.Image;
using SixLabors.ImageSharp.Processing;

namespace Network.Models
{
    public class InputData
    {
        [ImageType(100, 100)]
        [ColumnName("Image")]
        public byte[] Image { get; set; }
    }
}
