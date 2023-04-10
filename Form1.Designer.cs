namespace GaleriaImagenes
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(components);
            btnseleccionar = new Button();
            btnGuardar = new Button();
            btnliampia = new Button();
            btBuscar = new Button();
            openFileDialog1 = new OpenFileDialog();
            PbImage = new PictureBox();
            lblFecha = new Label();
            lblDescripcion = new Label();
            lblLugar = new Label();
            txtLugar = new TextBox();
            RtxtDescripcion = new RichTextBox();
            txtBuscar = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            btnActualizar = new Button();
            ((System.ComponentModel.ISupportInitialize)PbImage).BeginInit();
            SuspendLayout();
            // 
            // bunifuDragControl1
            // 
            bunifuDragControl1.Fixed = true;
            bunifuDragControl1.Horizontal = true;
            bunifuDragControl1.TargetControl = this;
            bunifuDragControl1.Vertical = true;
            // 
            // btnseleccionar
            // 
            btnseleccionar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnseleccionar.Location = new Point(531, 335);
            btnseleccionar.Name = "btnseleccionar";
            btnseleccionar.Size = new Size(78, 23);
            btnseleccionar.TabIndex = 0;
            btnseleccionar.Text = "Selccionar";
            btnseleccionar.UseVisualStyleBackColor = true;
            btnseleccionar.Click += btnseleccionar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnGuardar.Location = new Point(301, 335);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnliampia
            // 
            btnliampia.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnliampia.Location = new Point(426, 335);
            btnliampia.Name = "btnliampia";
            btnliampia.Size = new Size(75, 23);
            btnliampia.TabIndex = 2;
            btnliampia.Text = "Limpiar";
            btnliampia.UseVisualStyleBackColor = true;
            // 
            // btBuscar
            // 
            btBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btBuscar.Location = new Point(12, 308);
            btBuscar.Name = "btBuscar";
            btBuscar.Size = new Size(75, 23);
            btBuscar.TabIndex = 3;
            btBuscar.Text = "Buscar";
            btBuscar.UseVisualStyleBackColor = true;
            btBuscar.Click += btBuscar_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // PbImage
            // 
            PbImage.Location = new Point(254, 41);
            PbImage.Name = "PbImage";
            PbImage.Size = new Size(420, 250);
            PbImage.TabIndex = 5;
            PbImage.TabStop = false;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblFecha.Location = new Point(29, 76);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(43, 17);
            lblFecha.TabIndex = 6;
            lblFecha.Text = "Fecha";
            lblFecha.Click += label1_Click;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDescripcion.Location = new Point(12, 199);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(80, 17);
            lblDescripcion.TabIndex = 8;
            lblDescripcion.Text = "Descripcion";
            // 
            // lblLugar
            // 
            lblLugar.AutoSize = true;
            lblLugar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblLugar.Location = new Point(29, 121);
            lblLugar.Name = "lblLugar";
            lblLugar.Size = new Size(43, 17);
            lblLugar.TabIndex = 9;
            lblLugar.Text = "Lugar";
            // 
            // txtLugar
            // 
            txtLugar.Location = new Point(87, 121);
            txtLugar.Name = "txtLugar";
            txtLugar.Size = new Size(100, 23);
            txtLugar.TabIndex = 11;
            // 
            // RtxtDescripcion
            // 
            RtxtDescripcion.Location = new Point(98, 180);
            RtxtDescripcion.Name = "RtxtDescripcion";
            RtxtDescripcion.Size = new Size(118, 61);
            RtxtDescripcion.TabIndex = 12;
            RtxtDescripcion.Text = "";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(116, 310);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(100, 23);
            txtBuscar.TabIndex = 13;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(84, 79);
            dateTimePicker1.MaxDate = new DateTime(2025, 12, 31, 0, 0, 0, 0);
            dateTimePicker1.MinDate = new DateTime(1953, 1, 1, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(132, 23);
            dateTimePicker1.TabIndex = 14;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(425, 383);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(75, 23);
            btnActualizar.TabIndex = 15;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnActualizar);
            Controls.Add(dateTimePicker1);
            Controls.Add(txtBuscar);
            Controls.Add(RtxtDescripcion);
            Controls.Add(txtLugar);
            Controls.Add(lblLugar);
            Controls.Add(lblDescripcion);
            Controls.Add(lblFecha);
            Controls.Add(PbImage);
            Controls.Add(btBuscar);
            Controls.Add(btnliampia);
            Controls.Add(btnGuardar);
            Controls.Add(btnseleccionar);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)PbImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Button btBuscar;
        private Button btnliampia;
        private Button btnGuardar;
        private Button btnseleccionar;
        private OpenFileDialog openFileDialog1;
        private PictureBox PbImage;
        private Label lblFecha;
        private TextBox txtLugar;
        private Label lblLugar;
        private Label lblDescripcion;
        private TextBox txtBuscar;
        private RichTextBox RtxtDescripcion;
        private DateTimePicker dateTimePicker1;
        private Button btnActualizar;
    }
}