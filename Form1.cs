using GaleriaImagenes.AccesoDatos;
using GaleriaImagenes.Entidades;
using GaleriaImagenes.MyContext;
using System.Data.Common;
using System.Drawing.Imaging;

namespace GaleriaImagenes
{
    public partial class Form1 : Form
    {
        private readonly AppDbContext dbContext = new();
        private int buscarId;

        public Form1()
        {
            InitializeComponent();
        }

        

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog Ofdselect = new OpenFileDialog();
            Ofdselect.Filter = "Image|*.jpg; *.png;";
            Ofdselect.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            Ofdselect.Title = "Select Image";

            if (Ofdselect.ShowDialog() == DialogResult.OK)
            {
                PbImage.Image = Image.FromFile(Ofdselect.FileName);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private bool Validar()
        {
            var colorDefecto = Color.FromArgb(140, 173, 187);

            if (RtxtDescripcion.Text == string.Empty)
            {
                lblDescripcion.ForeColor = Color.Red;
                RtxtDescripcion.Focus();
                return false;
            }
            lblDescripcion.ForeColor = colorDefecto;


            if (txtLugar.Text == string.Empty)
            {
                lblLugar.ForeColor = Color.Red;
                txtLugar.Focus();
                return false;
            }
            lblLugar.ForeColor = colorDefecto;

            if (PbImage.Image == null)
            {
                MessageBox.Show("Debe indicar una imagen.");
                return false;
            }
            return true;
        }
        void limpiarCampos()
        {
            RtxtDescripcion.Text = String.Empty;
            txtLugar.Text = String.Empty;
            txtBuscar.Text = String.Empty;
            PbImage.Image = null;

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validar() == true)
            {
                MemoryStream ms = new MemoryStream();
                PbImage.Image.Save(ms, ImageFormat.Jpeg);
                byte[] convertirImagen = ms.ToArray();


                try
                {
                    DateTime date = dateTimePicker1.Value;

                    Image_gallery nuevaImagen = new();
                    nuevaImagen.Image = convertirImagen;
                    nuevaImagen.Description = RtxtDescripcion.Text;
                    nuevaImagen.Place = txtLugar.Text;
                    nuevaImagen.Date = date;

                    Crud nuevoRegistro = new();
                    nuevoRegistro.Create(nuevaImagen);


                    limpiarCampos();

                }
                catch (DbException ex)
                {
                    MessageBox.Show("No se pudo");

                    throw;
                }
            }
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {


            Image_gallery buscarImagenId = new Image_gallery();
            var id = txtBuscar.Text;

            if (txtBuscar.Text == string.Empty)
            {
                MessageBox.Show("Debes buscar un ID" + id);
                txtBuscar.Focus();
            }
            else
            {
                try
                {
                    var traerDato = dbContext.Image_gallery.Select(d => new
                    {
                        Id = d.Id,
                        Image = d.Image,
                        Description = d.Description,
                        Place = d.Place,
                        Date = d.Date
                    }).FirstOrDefault(i => i.Id == Convert.ToInt32(id));

                    if ((traerDato == null) || (traerDato.Id == 0))
                    {
                        MessageBox.Show("No se encontrado un registro en la consulta.");
                        limpiarCampos();
                    }
                    else
                    {
                        txtLugar.Text = traerDato.Place.ToString();
                        RtxtDescripcion.Text = traerDato.Description.ToString();
                        dateTimePicker1.Value = traerDato.Date;
                        try
                        {
                            MemoryStream ms = new MemoryStream((byte[])traerDato.Image);
                            Bitmap bm = new Bitmap(ms);
                            PbImage.Image = bm;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Hubo un problema al recuperar la imagen.\nEl formato debe ser correcto.");
                        }
                        buscarId = int.Parse(traerDato.Id.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido el error: " + ex);

                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            var id = txtBuscar.Text;

            if (Validar() == true)
            {
                MemoryStream ms = new MemoryStream();
                PbImage.Image.Save(ms, ImageFormat.Jpeg);
                byte[] convertirImagen = ms.ToArray();


                try
                {
                    DateTime date = dateTimePicker1.Value;

                    Image_gallery nuevaImagen = new();
                    nuevaImagen.Image = convertirImagen;
                    nuevaImagen.Description = RtxtDescripcion.Text;
                    nuevaImagen.Place = txtLugar.Text;
                    nuevaImagen.Date = date;

                    Crud nuevoRegistro = new();
                    nuevoRegistro.Update(nuevaImagen, Convert.ToInt32(id));


                    limpiarCampos();

                }
                catch (DbException ex)
                {
                    MessageBox.Show("No se pudo");

                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = txtBuscar.Text;

            if (!(id is null))
            {           
                try
                {
                    Crud nuevoRegistro = new();
                    nuevoRegistro.Delete(Convert.ToInt32(id));
                    limpiarCampos();
                }
                catch (DbException ex)
                {
                    MessageBox.Show("No se pudo: "+ex);

                    throw;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblLugar_Click(object sender, EventArgs e)
        {

        }

        private void btnliampia_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void lblclose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}