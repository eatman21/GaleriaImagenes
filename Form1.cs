using GaleriaImagenes.BusinessLogic;

namespace GaleriaImagenes
{
    public partial class Form1 : Form
    {
        private readonly ImageGalleryService _imageGalleryService;
        private int buscarId;

        public Form1()
        {
            InitializeComponent();
            _imageGalleryService = new ImageGalleryService();
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
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validar() == true)
            {
                try
                {
                    DateTime date = dateTimePicker1.Value;

                    var result = await _imageGalleryService.CreateImageGalleryAsync(
                        PbImage.Image,
                        RtxtDescripcion.Text,
                        txtLugar.Text,
                        date
                    );

                    if (result.Success)
                    {
                        MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btBuscar_Click(object sender, EventArgs e)
        {
            var id = txtBuscar.Text;

            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Debes buscar un ID", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscar.Focus();
                return;
            }

            if (!int.TryParse(id, out int imageId))
            {
                MessageBox.Show("ID debe ser un número válido", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscar.Focus();
                return;
            }

            try
            {
                var result = await _imageGalleryService.GetImageGalleryByIdAsync(imageId);

                if (!result.Success || result.Data == null)
                {
                    MessageBox.Show(result.Message, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarCampos();
                }
                else
                {
                    var traerDato = result.Data;
                    txtLugar.Text = traerDato.Place;
                    RtxtDescripcion.Text = traerDato.Description;
                    dateTimePicker1.Value = traerDato.Date;

                    var bitmapResult = _imageGalleryService.ConvertByteArrayToBitmap(traerDato.Image);
                    if (bitmapResult.Success)
                    {
                        PbImage.Image = bitmapResult.Data;
                    }
                    else
                    {
                        MessageBox.Show(bitmapResult.Message, "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    buscarId = traerDato.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido el error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            var id = txtBuscar.Text;

            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Debes indicar un ID para actualizar", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscar.Focus();
                return;
            }

            if (!int.TryParse(id, out int imageId))
            {
                MessageBox.Show("ID debe ser un número válido", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscar.Focus();
                return;
            }

            if (Validar() == true)
            {
                try
                {
                    DateTime date = dateTimePicker1.Value;

                    var result = await _imageGalleryService.UpdateImageGalleryAsync(
                        imageId,
                        PbImage.Image,
                        RtxtDescripcion.Text,
                        txtLugar.Text,
                        date
                    );

                    if (result.Success)
                    {
                        MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var id = txtBuscar.Text;

            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Debes indicar un ID para eliminar", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscar.Focus();
                return;
            }

            if (!int.TryParse(id, out int imageId))
            {
                MessageBox.Show("ID debe ser un número válido", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscar.Focus();
                return;
            }

            var confirmResult = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este registro?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var result = await _imageGalleryService.DeleteImageGalleryAsync(imageId);

                    if (result.Success)
                    {
                        MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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