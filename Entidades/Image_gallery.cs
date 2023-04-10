using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaImagenes.Entidades
{
    [Table("image_gallery")]
    public class Image_gallery
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime Date { get; set; }
    }

}
