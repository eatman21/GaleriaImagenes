using GaleriaImagenes.Entidades;
using GaleriaImagenes.Models;
using GaleriaImagenes.MyContext;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace GaleriaImagenes.AccesoDatos
{
    public class Crud
    {
        private readonly AppDbContext _contextoDatos;

        public Crud(AppDbContext context)
        {
            _contextoDatos = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Crud()
        {
            _contextoDatos = new AppDbContext();
        }

        /// <summary>
        /// Creates a new image gallery entry in the database
        /// </summary>
        public async Task<OperationResult> CreateAsync(Image_gallery creaImagen)
        {
            if (creaImagen == null)
            {
                return OperationResult.FailureResult("Image gallery object cannot be null");
            }

            try
            {
                await _contextoDatos.Image_gallery.AddAsync(creaImagen);
                await _contextoDatos.SaveChangesAsync();
                return OperationResult.SuccessResult("Image gallery entry created successfully");
            }
            catch (MySqlException ex)
            {
                return OperationResult.FailureResult($"Database error while creating entry: {ex.Message}", ex);
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.FailureResult($"Error saving changes: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                return OperationResult.FailureResult($"Unexpected error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates an existing image gallery entry
        /// </summary>
        public async Task<OperationResult> UpdateAsync(Image_gallery actualizar, int id)
        {
            if (actualizar == null)
            {
                return OperationResult.FailureResult("Image gallery object cannot be null");
            }

            if (id <= 0)
            {
                return OperationResult.FailureResult("Invalid ID provided");
            }

            try
            {
                var buscarRegistro = await _contextoDatos.Image_gallery.FirstOrDefaultAsync(d => d.Id == id);

                if (buscarRegistro == null)
                {
                    return OperationResult.FailureResult($"Record with ID {id} not found");
                }

                // Update properties
                buscarRegistro.Description = actualizar.Description;
                buscarRegistro.Place = actualizar.Place;
                buscarRegistro.Image = actualizar.Image;
                buscarRegistro.Date = actualizar.Date;

                _contextoDatos.Image_gallery.Update(buscarRegistro);
                await _contextoDatos.SaveChangesAsync();

                return OperationResult.SuccessResult($"Record with ID {id} updated successfully");
            }
            catch (MySqlException ex)
            {
                return OperationResult.FailureResult($"Database error while updating: {ex.Message}", ex);
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.FailureResult($"Error saving changes: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                return OperationResult.FailureResult($"Unexpected error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deletes an image gallery entry by ID
        /// </summary>
        public async Task<OperationResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return OperationResult.FailureResult("Invalid ID provided");
            }

            try
            {
                var buscarRegistro = await _contextoDatos.Image_gallery.FirstOrDefaultAsync(elimina => elimina.Id == id);

                if (buscarRegistro == null)
                {
                    return OperationResult.FailureResult($"Record with ID {id} not found");
                }

                _contextoDatos.Image_gallery.Remove(buscarRegistro);
                await _contextoDatos.SaveChangesAsync();

                return OperationResult.SuccessResult($"Record with ID {id} deleted successfully");
            }
            catch (MySqlException ex)
            {
                return OperationResult.FailureResult($"Database error while deleting: {ex.Message}", ex);
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.FailureResult($"Error saving changes: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                return OperationResult.FailureResult($"Unexpected error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets an image gallery entry by ID
        /// </summary>
        public async Task<OperationResult<Image_gallery>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return OperationResult<Image_gallery>.FailureResult("Invalid ID provided");
            }

            try
            {
                var registro = await _contextoDatos.Image_gallery.FirstOrDefaultAsync(i => i.Id == id);

                if (registro == null)
                {
                    return OperationResult<Image_gallery>.FailureResult($"Record with ID {id} not found");
                }

                return OperationResult<Image_gallery>.SuccessResult(registro, "Record retrieved successfully");
            }
            catch (MySqlException ex)
            {
                return OperationResult<Image_gallery>.FailureResult($"Database error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                return OperationResult<Image_gallery>.FailureResult($"Unexpected error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets all image gallery entries
        /// </summary>
        public async Task<OperationResult<List<Image_gallery>>> GetAllAsync()
        {
            try
            {
                var registros = await _contextoDatos.Image_gallery.ToListAsync();
                return OperationResult<List<Image_gallery>>.SuccessResult(registros, $"Retrieved {registros.Count} records");
            }
            catch (MySqlException ex)
            {
                return OperationResult<List<Image_gallery>>.FailureResult($"Database error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                return OperationResult<List<Image_gallery>>.FailureResult($"Unexpected error: {ex.Message}", ex);
            }
        }
    }
}
