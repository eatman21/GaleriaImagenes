using GaleriaImagenes.AccesoDatos;
using GaleriaImagenes.Entidades;
using GaleriaImagenes.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;

namespace GaleriaImagenes.BusinessLogic
{
    public class ImageGalleryService
    {
        private readonly Crud _crud;

        public ImageGalleryService(Crud crud)
        {
            _crud = crud ?? throw new ArgumentNullException(nameof(crud));
        }

        public ImageGalleryService()
        {
            _crud = new Crud();
        }

        /// <summary>
        /// Validates the image gallery entity
        /// </summary>
        public OperationResult ValidateImageGallery(Image_gallery imageGallery)
        {
            if (imageGallery == null)
            {
                return OperationResult.FailureResult("Image gallery object cannot be null");
            }

            var validationContext = new ValidationContext(imageGallery);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(imageGallery, validationContext, validationResults, true);

            if (!isValid)
            {
                var errors = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                return OperationResult.FailureResult($"Validation failed: {errors}");
            }

            return OperationResult.SuccessResult("Validation successful");
        }

        /// <summary>
        /// Converts an Image to byte array
        /// </summary>
        public OperationResult<byte[]> ConvertImageToByteArray(Image image)
        {
            if (image == null)
            {
                return OperationResult<byte[]>.FailureResult("Image cannot be null");
            }

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Jpeg);
                    byte[] imageBytes = ms.ToArray();
                    return OperationResult<byte[]>.SuccessResult(imageBytes, "Image converted successfully");
                }
            }
            catch (Exception ex)
            {
                return OperationResult<byte[]>.FailureResult($"Error converting image: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Converts byte array to Bitmap
        /// </summary>
        public OperationResult<Bitmap> ConvertByteArrayToBitmap(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return OperationResult<Bitmap>.FailureResult("Image bytes cannot be null or empty");
            }

            try
            {
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    return OperationResult<Bitmap>.SuccessResult(bitmap, "Bitmap created successfully");
                }
            }
            catch (ArgumentException ex)
            {
                return OperationResult<Bitmap>.FailureResult($"Invalid image format: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                return OperationResult<Bitmap>.FailureResult($"Error converting to bitmap: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Creates a new image gallery entry
        /// </summary>
        public async Task<OperationResult> CreateImageGalleryAsync(Image image, string description, string place, DateTime date)
        {
            // Convert image to byte array
            var imageConversionResult = ConvertImageToByteArray(image);
            if (!imageConversionResult.Success)
            {
                return OperationResult.FailureResult(imageConversionResult.Message);
            }

            // Create entity
            var imageGallery = new Image_gallery
            {
                Image = imageConversionResult.Data!,
                Description = description,
                Place = place,
                Date = date
            };

            // Validate entity
            var validationResult = ValidateImageGallery(imageGallery);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            // Save to database
            return await _crud.CreateAsync(imageGallery);
        }

        /// <summary>
        /// Updates an existing image gallery entry
        /// </summary>
        public async Task<OperationResult> UpdateImageGalleryAsync(int id, Image image, string description, string place, DateTime date)
        {
            if (id <= 0)
            {
                return OperationResult.FailureResult("Invalid ID provided");
            }

            // Convert image to byte array
            var imageConversionResult = ConvertImageToByteArray(image);
            if (!imageConversionResult.Success)
            {
                return OperationResult.FailureResult(imageConversionResult.Message);
            }

            // Create entity with updated data
            var imageGallery = new Image_gallery
            {
                Id = id,
                Image = imageConversionResult.Data!,
                Description = description,
                Place = place,
                Date = date
            };

            // Validate entity
            var validationResult = ValidateImageGallery(imageGallery);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            // Update in database
            return await _crud.UpdateAsync(imageGallery, id);
        }

        /// <summary>
        /// Deletes an image gallery entry
        /// </summary>
        public async Task<OperationResult> DeleteImageGalleryAsync(int id)
        {
            if (id <= 0)
            {
                return OperationResult.FailureResult("Invalid ID provided");
            }

            return await _crud.DeleteAsync(id);
        }

        /// <summary>
        /// Gets an image gallery entry by ID
        /// </summary>
        public async Task<OperationResult<Image_gallery>> GetImageGalleryByIdAsync(int id)
        {
            if (id <= 0)
            {
                return OperationResult<Image_gallery>.FailureResult("Invalid ID provided");
            }

            return await _crud.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets all image gallery entries
        /// </summary>
        public async Task<OperationResult<List<Image_gallery>>> GetAllImageGalleriesAsync()
        {
            return await _crud.GetAllAsync();
        }
    }
}
