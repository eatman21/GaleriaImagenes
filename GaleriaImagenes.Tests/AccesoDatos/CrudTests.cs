using FluentAssertions;
using GaleriaImagenes.AccesoDatos;
using GaleriaImagenes.Entidades;
using GaleriaImagenes.MyContext;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GaleriaImagenes.Tests.AccesoDatos
{
    public class CrudTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly Crud _crud;

        public CrudTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _crud = new Crud(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateAsync_WithValidImageGallery_ShouldReturnSuccess()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "Test Description",
                Place = "Test Place",
                Date = DateTime.Now.Date
            };

            // Act
            var result = await _crud.CreateAsync(imageGallery);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Contain("successfully");

            var savedItem = await _context.Image_gallery.FirstOrDefaultAsync();
            savedItem.Should().NotBeNull();
            savedItem!.Description.Should().Be("Test Description");
        }

        [Fact]
        public async Task CreateAsync_WithNullImageGallery_ShouldReturnFailure()
        {
            // Act
            var result = await _crud.CreateAsync(null!);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("cannot be null");
        }

        [Fact]
        public async Task UpdateAsync_WithValidData_ShouldUpdateRecord()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "Original Description",
                Place = "Original Place",
                Date = DateTime.Now.Date
            };

            await _context.Image_gallery.AddAsync(imageGallery);
            await _context.SaveChangesAsync();

            var updatedGallery = new Image_gallery
            {
                Image = new byte[] { 4, 5, 6 },
                Description = "Updated Description",
                Place = "Updated Place",
                Date = DateTime.Now.Date.AddDays(1)
            };

            // Act
            var result = await _crud.UpdateAsync(updatedGallery, imageGallery.Id);

            // Assert
            result.Success.Should().BeTrue();

            var updated = await _context.Image_gallery.FindAsync(imageGallery.Id);
            updated!.Description.Should().Be("Updated Description");
            updated.Place.Should().Be("Updated Place");
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidId_ShouldReturnFailure()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "Test",
                Place = "Test",
                Date = DateTime.Now.Date
            };

            // Act
            var result = await _crud.UpdateAsync(imageGallery, 0);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Invalid ID");
        }

        [Fact]
        public async Task UpdateAsync_WithNonExistentId_ShouldReturnFailure()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "Test",
                Place = "Test",
                Date = DateTime.Now.Date
            };

            // Act
            var result = await _crud.UpdateAsync(imageGallery, 999);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("not found");
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ShouldDeleteRecord()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "Test Description",
                Place = "Test Place",
                Date = DateTime.Now.Date
            };

            await _context.Image_gallery.AddAsync(imageGallery);
            await _context.SaveChangesAsync();

            // Act
            var result = await _crud.DeleteAsync(imageGallery.Id);

            // Assert
            result.Success.Should().BeTrue();

            var deleted = await _context.Image_gallery.FindAsync(imageGallery.Id);
            deleted.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ShouldReturnFailure()
        {
            // Act
            var result = await _crud.DeleteAsync(0);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Invalid ID");
        }

        [Fact]
        public async Task DeleteAsync_WithNonExistentId_ShouldReturnFailure()
        {
            // Act
            var result = await _crud.DeleteAsync(999);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("not found");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnRecord()
        {
            // Arrange
            var imageGallery = new Image_gallery
            {
                Image = new byte[] { 1, 2, 3 },
                Description = "Test Description",
                Place = "Test Place",
                Date = DateTime.Now.Date
            };

            await _context.Image_gallery.AddAsync(imageGallery);
            await _context.SaveChangesAsync();

            // Act
            var result = await _crud.GetByIdAsync(imageGallery.Id);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Description.Should().Be("Test Description");
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnFailure()
        {
            // Act
            var result = await _crud.GetByIdAsync(0);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Invalid ID");
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRecords()
        {
            // Arrange
            var galleries = new[]
            {
                new Image_gallery { Image = new byte[] { 1 }, Description = "First", Place = "Place1", Date = DateTime.Now.Date },
                new Image_gallery { Image = new byte[] { 2 }, Description = "Second", Place = "Place2", Date = DateTime.Now.Date },
                new Image_gallery { Image = new byte[] { 3 }, Description = "Third", Place = "Place3", Date = DateTime.Now.Date }
            };

            await _context.Image_gallery.AddRangeAsync(galleries);
            await _context.SaveChangesAsync();

            // Act
            var result = await _crud.GetAllAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Count.Should().Be(3);
        }

        [Fact]
        public async Task GetAllAsync_WithEmptyDatabase_ShouldReturnEmptyList()
        {
            // Act
            var result = await _crud.GetAllAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Count.Should().Be(0);
        }
    }
}
