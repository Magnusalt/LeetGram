using System;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace LeetGram.Services
{
    public class ImageHandler
    {
        public static async Task<string> Resize(Stream originalImage)
        {
            using var image = await Image.LoadAsync(originalImage);

            var aspectDiff = image.Height - image.Width;

            var cropRectangle = aspectDiff switch
            {
                var d when d > 0 => new Rectangle(0, aspectDiff / 2, image.Width, image.Width),
                var d when d < 0 => new Rectangle(-1 * aspectDiff / 2, 0, image.Height, image.Height),
                var d when d == 0 => new Rectangle(0, 0, image.Width, image.Height),
                _ => throw new InvalidDataException() // to please the compiler...
            };

            var targetSizePx = 400;
            if (cropRectangle.Width < targetSizePx)
                targetSizePx = cropRectangle.Width;

            image.Mutate(im => im.Crop(cropRectangle).Resize(targetSizePx, targetSizePx));

            await using var output = new MemoryStream();
            await image.SaveAsync(output, new JpegEncoder());
            return Convert.ToBase64String(output.ToArray());
        }
    }
}