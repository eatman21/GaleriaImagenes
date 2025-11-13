using FluentAssertions;
using GaleriaImagenes.BusinessLogic;
using GaleriaImagenes.Entidades;
using System.Drawing;
using System.Drawing.Imaging;
using Xunit;

namespace GaleriaImagenes.Tests.BusinessLogic
{
    public class ImageGalleryServiceTests
    {
        private readonly ImageGalleryService _service;

        public ImageGalleryServiceTests()
        {
            _service = new ImageGalleryService();
        }

        [Fact]
        public void ValidateImageGallery_WithValidData_ShouldReturnSuccess()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "Valid Description",
                Place = "Valid Place",
                Date = DateTime.Now.Date
            };

            // Act
            var result = _service.ValidateImageGallery(imageGallery);

            // Assert
            result.Success.Should().BeTrue();
        }

        [Fact]
        public void ValidateImageGallery_WithNullObject_ShouldReturnFailure()
        {
            // Act
            var result = _service.ValidateImageGallery(null!);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("cannot be null");
        }

        [Fact]
        public void ValidateImageGallery_WithEmptyDescription_ShouldReturnFailure()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "",
                Place = "Valid Place",
                Date = DateTime.Now.Date
            };

            // Act
            var result = _service.ValidateImageGallery(imageGallery);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Description");
        }

        [Fact]
        public void ValidateImageGallery_WithEmptyPlace_ShouldReturnFailure()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "Valid Description",
                Place = "",
                Date = DateTime.Now.Date
            };

            // Act
            var result = _service.ValidateImageGallery(imageGallery);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Place");
        }

        [Fact]
        public void ConvertImageToByteArray_WithValidImage_ShouldReturnByteArray()
        {
            // Arrange
            using var bitmap = new Bitmap(100, 100);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Red);

            // Act
            var result = _service.ConvertImageToByteArray(bitmap);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void ConvertImageToByteArray_WithNullImage_ShouldReturnFailure()
        {
            // Act
            var result = _service.ConvertImageToByteArray(null!);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("cannot be null");
        }

        [Fact]
        public void ConvertByteArrayToBitmap_WithValidBytes_ShouldReturnBitmap()
        {
            // Arrange
            using var originalBitmap = new Bitmap(100, 100);
            using var ms = new MemoryStream();
            originalBitmap.Save(ms, ImageFormat.Jpeg);
            var imageBytes = ms.ToArray();

            // Act
            var result = _service.ConvertByteArrayToBitmap(imageBytes);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Width.Should().BeGreaterThan(0);
        }

        [Fact]
        public void ConvertByteArrayToBitmap_WithNullBytes_ShouldReturnFailure()
        {
            // Act
            var result = _service.ConvertByteArrayToBitmap(null!);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("cannot be null");
        }

        [Fact]
        public void ConvertByteArrayToBitmap_WithEmptyBytes_ShouldReturnFailure()
        {
            // Act
            var result = _service.ConvertByteArrayToBitmap(Array.Empty<byte>());

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("cannot be null or empty");
        }

        [Fact]
        public void ConvertByteArrayToBitmap_WithInvalidBytes_ShouldReturnFailure()
        {
            // Arrange
            var invalidBytes = new byte[] { 1, 2, 3, 4, 5 };

            // Act
            var result = _service.ConvertByteArrayToBitmap(invalidBytes);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Invalid image format");
        }

        [Fact]
        public void ConvertImageToByteArray_AndBackToBitmap_ShouldPreserveImage()
        {
            // Arrange
            using var originalBitmap = new Bitmap(50, 50);
            using var graphics = Graphics.FromImage(originalBitmap);
            graphics.Clear(Color.Blue);

            // Act
            var byteArrayResult = _service.ConvertImageToByteArray(originalBitmap);
            var bitmapResult = _service.ConvertByteArrayToBitmap(byteArrayResult.Data!);

            // Assert
            byteArrayResult.Success.Should().BeTrue();
            bitmapResult.Success.Should().BeTrue();
            bitmapResult.Data!.Width.Should().Be(originalBitmap.Width);
            bitmapResult.Data.Height.Should().Be(originalBitmap.Height);
        }
    }
}
