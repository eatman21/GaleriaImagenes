using FluentAssertions;
using GaleriaImagenes.Entidades;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GaleriaImagenes.Tests.Entidades
{
    public class Image_gallery_Tests
    {
        private List<ValidationResult> ValidateModel(Image_gallery model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Image_gallery_WithValidData_ShouldPassValidation()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = new byte[] { 1, 2, 3 },
                Description = "A beautiful sunset over the mountains",
                Place = "Rocky Mountains",
                Date = new DateTime(2024, 1, 15)
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void Image_gallery_WithMissingImage_ShouldFailValidation()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = null!,
                Description = "Test Description",
                Place = "Test Place",
                Date = DateTime.Now.Date
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(v => v.ErrorMessage!.Contains("Image"));
        }

        [Fact]
        public void Image_gallery_WithMissingDescription_ShouldFailValidation()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = new byte[] { 1, 2, 3 },
                Description = null!,
                Place = "Test Place",
                Date = DateTime.Now.Date
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(v => v.ErrorMessage!.Contains("Description"));
        }

        [Fact]
        public void Image_gallery_WithEmptyDescription_ShouldFailValidation()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = new byte[] { 1, 2, 3 },
                Description = "",
                Place = "Test Place",
                Date = DateTime.Now.Date
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().NotBeEmpty();
        }

        [Fact]
        public void Image_gallery_WithMissingPlace_ShouldFailValidation()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = new byte[] { 1, 2, 3 },
                Description = "Test Description",
                Place = null!,
                Date = DateTime.Now.Date
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(v => v.ErrorMessage!.Contains("Place"));
        }

        [Fact]
        public void Image_gallery_WithTooLongDescription_ShouldFailValidation()
        {
            // Arrange
            var longDescription = new string('A', 501); // Exceeds max length of 500
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = new byte[] { 1, 2, 3 },
                Description = longDescription,
                Place = "Test Place",
                Date = DateTime.Now.Date
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(v => v.ErrorMessage!.Contains("Description"));
        }

        [Fact]
        public void Image_gallery_WithTooLongPlace_ShouldFailValidation()
        {
            // Arrange
            var longPlace = new string('A', 201); // Exceeds max length of 200
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = new byte[] { 1, 2, 3 },
                Description = "Test Description",
                Place = longPlace,
                Date = DateTime.Now.Date
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(v => v.ErrorMessage!.Contains("Place"));
        }

        [Fact]
        public void Image_gallery_WithMaxLengthDescription_ShouldPassValidation()
        {
            // Arrange
            var maxDescription = new string('A', 500); // Exactly max length
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = new byte[] { 1, 2, 3 },
                Description = maxDescription,
                Place = "Test Place",
                Date = DateTime.Now.Date
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void Image_gallery_WithMaxLengthPlace_ShouldPassValidation()
        {
            // Arrange
            var maxPlace = new string('A', 200); // Exactly max length
            var imageGallery = new Image_gallery
            {
                Id = 1,
                Image = new byte[] { 1, 2, 3 },
                Description = "Test Description",
                Place = maxPlace,
                Date = DateTime.Now.Date
            };

            // Act
            var validationResults = ValidateModel(imageGallery);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void Image_gallery_Properties_ShouldBeSettable()
        {
            // Arrange & Act
            var imageGallery = new Image_gallery
            {
                Id = 42,
                Image = new byte[] { 10, 20, 30 },
                Description = "Sunset view",
                Place = "Beach",
                Date = new DateTime(2024, 6, 15)
            };

            // Assert
            imageGallery.Id.Should().Be(42);
            imageGallery.Image.Should().Equal(new byte[] { 10, 20, 30 });
            imageGallery.Description.Should().Be("Sunset view");
            imageGallery.Place.Should().Be("Beach");
            imageGallery.Date.Should().Be(new DateTime(2024, 6, 15));
        }
    }
}
