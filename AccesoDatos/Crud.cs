using GaleriaImagenes.Entidades;
using GaleriaImagenes.MyContext;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaImagenes.AccesoDatos
{
    public class Crud
    {
        private readonly AppDbContext _contextoDatos = new AppDbContext();

        public Crud(AppDbContext context)
        {
            _contextoDatos = context;
        }
        public Crud()
        {
            
        }
        public void Create(Image_gallery creaImagen)
        {
            try
            {
                _contextoDatos.Image_gallery.Add(creaImagen);
                _contextoDatos.SaveChanges();
                MessageBox.Show("Datos creados");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("No se guardo: " + ex.Message.ToString());
                ex.Message.ToString();
            }
        }
        public void Update(Image_gallery actualizar, int id)
        {
            var buscarRegistro = _contextoDatos.Image_gallery.FirstOrDefault(d => d.Id == id);
            try
            {
                if (!(buscarRegistro is null))
                {
                    if (id == buscarRegistro.Id)
                    {
                        buscarRegistro.Description = actualizar.Description;
                        buscarRegistro.Place = actualizar.Place;
                        buscarRegistro.Image = actualizar.Image;
                        buscarRegistro.Date = actualizar.Date;

                        _contextoDatos.Image_gallery.Update(buscarRegistro);
                        _contextoDatos.SaveChangesAsync();

                        MessageBox.Show("Actualizamos el registro:" + buscarRegistro);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ha ocurrido un error al actualizar." + ex);
            }
            MessageBox.Show("Se ha actualizado un registro.");
        }
        public void Delete(int id)
        {
            var buscarRegistro = _contextoDatos.Image_gallery.FirstOrDefault(elimina=> elimina.Id == id);

            try
            {

                if (!(buscarRegistro is null))
                {
                    if (id == buscarRegistro.Id)
                    {
                        _contextoDatos.Image_gallery.Remove(buscarRegistro);
                        _contextoDatos.SaveChanges();

                        MessageBox.Show("Eliminado el registro:" + buscarRegistro);

                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el registro:" + buscarRegistro);

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ha ocurrido un error al actualizar." + ex);
            }
            MessageBox.Show("Se ha eliminado un registro.");

        }
    }
}
