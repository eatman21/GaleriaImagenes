using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaleriaImagenes.Entidades
{
    [Table("image_gallery")]
    public class Image_gallery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [MaxLength(16777215)] // MySQL MEDIUMBLOB limit
        public byte[] Image { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Place is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Place must be between 1 and 200 characters")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Column(TypeName = "DATE")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
