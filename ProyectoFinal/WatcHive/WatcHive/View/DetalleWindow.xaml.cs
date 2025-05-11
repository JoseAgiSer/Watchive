using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WatcHive.Domain;

namespace WatcHive.View
{
    /// <summary>
    /// Lógica de interacción para DetalleWindow.xaml
    /// </summary>
    public partial class DetalleWindow : Window
    {
        private Usuario usuarioLoged;
        private Serie serie;
        private Pelicula pelicula;
        private Contenido contenido;
        public DetalleWindow(Serie serieData, Usuario usuarioLoged)
        {
            InitializeComponent();
            cargarDatos(serieData);
            this.usuarioLoged = usuarioLoged;
            this.serie = serieData;
            this.contenido = serieData;
        }

        public DetalleWindow(Pelicula peliData, Usuario usuarioLoged)
        {
            InitializeComponent();
            cargarDatos(peliData);
            this.usuarioLoged = usuarioLoged;
            this.pelicula = peliData;
            this.contenido = peliData;

        }

        private void cargarDatos(Contenido contenidoData)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(contenidoData.imagen);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            imgPoster.Source = bitmap;

            txtTitulo.Text = contenidoData.nombre;
            int cont = 0;
            foreach (string genero in contenidoData.listaGeneros) {
                if (cont != 0)
                {
                    txtGeneros.Text = txtGeneros.Text + " - " + genero;
                }
                else {
                    txtGeneros.Text = genero;
                }
            }
            txtDescripcion.Text = contenidoData.descripcion;
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool yaExisteContenido = false;
            bool yaEnLista = false;
            if (contenido is Serie serieData)
            {
                yaExisteContenido = serieData.exists();
                if (!yaExisteContenido)
                    serieData.insert();

                yaEnLista = usuarioLoged.contenidoEnListas(serieData.id);

                if (yaEnLista)
                {
                    MessageBox.Show("Este contenido ya se encuentra en tu lista de vistos o pendientes.");
                }
                else
                {
                    Pendientes pendiente = new Pendientes(usuarioLoged.username, serieData.id, false, DateTime.Now.Date, false);
                    pendiente.insert();
                    MessageBox.Show("Has añadido '" + serieData.nombre + "' a tu lista de pendientes");
                }
            }
            else if (contenido is Pelicula peliData)
            {
                //IMPLEMENTAR LO DE ARRIBA AQUI TAMBIEN
                Pendientes pendiente = new Pendientes(usuarioLoged.username, peliData.id, false, DateTime.Now.Date, false);
                peliData.insert();
                pendiente.insert();
                MessageBox.Show("Has añadido '" + peliData.nombre + "' a tu lista de pendientes");
            }

            this.Close();
        }
    }
}
