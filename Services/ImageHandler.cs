using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;

namespace ImageResizer.Services
{
    public class ImageHandler
    {
        public async Task<string> Resize(Stream originalImage)
        {
            using var image = await Image.LoadAsync(originalImage);

            var aspectDiff = image.Height - image.Width;

            var cropRectangle = aspectDiff switch
            {
                var d when d > 0 => new Rectangle(0, aspectDiff / 2, image.Width, image.Width),
                var d when d < 0 => new Rectangle((-1 * aspectDiff / 2), 0, image.Height, image.Height),
                var d when d == 0 => new Rectangle(0, 0, image.Width, image.Height),
                _ => throw new InvalidDataException()
            };

            var targetSizePx = 400;

            if (cropRectangle.Width < targetSizePx)
            {
                targetSizePx = cropRectangle.Width;
            }

            image.Mutate(im => im.Crop(cropRectangle).Resize(targetSizePx, targetSizePx));

            using var output = new MemoryStream();

            await image.SaveAsync(output, new JpegEncoder());

            return Convert.ToBase64String(output.ToArray());
        }
    }
}