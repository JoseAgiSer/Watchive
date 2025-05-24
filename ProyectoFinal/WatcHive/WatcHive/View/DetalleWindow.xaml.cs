using Google.Protobuf.Collections;
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
        private bool botones;

        public DetalleWindow(Serie serieData, Usuario usuarioLoged)
        {
            this.usuarioLoged = usuarioLoged;
            this.serie = serieData;
            this.contenido = serieData;
            this.botones = true;
            InitializeComponent();
            cargarDatos(serieData);

        }

        public DetalleWindow(Pelicula peliData, Usuario usuarioLoged)
        {
            this.usuarioLoged = usuarioLoged;
            this.pelicula = peliData;
            this.contenido = peliData;
            this.botones = true;
            InitializeComponent();
            cargarDatos(peliData);


        }

        public DetalleWindow(Serie serieData, Usuario usuarioLoged,bool boton)
        {
            this.usuarioLoged = usuarioLoged;
            this.serie = serieData;
            this.contenido = serieData;
            this.botones = boton;
            InitializeComponent();
            cargarDatos(serieData);
            btnAgreagarVistos.Visibility = Visibility.Collapsed;
            btnAgregarPendiente.Visibility = Visibility.Collapsed;
        }

        public DetalleWindow(Pelicula peliData, Usuario usuarioLoged, bool boton)
        {
            this.usuarioLoged = usuarioLoged;
            this.pelicula = peliData;
            this.contenido = peliData;
            this.botones = boton;
            InitializeComponent();
            cargarDatos(peliData);
            btnAgreagarVistos.Visibility = Visibility.Collapsed;
            btnAgregarPendiente.Visibility = Visibility.Collapsed;
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
            string nombreGenero = "Genero no definido";
            Genero g = new Genero();
            if (contenidoData.listaGeneros != null) { 
                foreach (int genero in contenidoData.listaGeneros) {
                    if (cont != 0)
                    {
                        nombreGenero = g.readGeneroById(genero);
                        txtGeneros.Text = txtGeneros.Text + " - " + nombreGenero;
                    }
                    else {
                        nombreGenero = g.readGeneroById(genero);
                        txtGeneros.Text = "Genero:  " + nombreGenero;
                        cont++;
                    }
                }
            }
            if (contenidoData is Serie serie)
            {
                txtNotaOrTemporadas.Text = "Número de temporadas: " + serie.numTemporadas.ToString();
            }
            else if (contenidoData is Pelicula pelicula){ 
                txtNotaOrTemporadas.Text = "Nota media: " + pelicula.nota.ToString();
            }

            if (!botones)
            {
                ContenidoVisto visto = new ContenidoVisto();
                string fechaVisto = visto.readContenidoVistoByuser(usuarioLoged.username, contenidoData.id);
                string emocion = visto.readEmocionByUserandID(usuarioLoged.username, contenidoData.id);
                txtNotaOrTemporadas.Text = txtNotaOrTemporadas.Text + "\n\nFecha Visto: " + fechaVisto + "\nTe sentias: " + emocion;
            }


            //if (botones)
            //{
            txtDescripcion.Text = contenidoData.descripcion;
            //}
            //else {
            //    ContenidoVisto visto = new ContenidoVisto();
            //    string fechaVisto = visto.readContenidoVistoByuser(usuarioLoged.username, contenidoData.id);
            //    string emocion = visto.readEmocionByUserandID(usuarioLoged.username, contenidoData.id);
            //    txtDescripcion.Text = contenidoData.descripcion+"\n\nFecha Visto: "+fechaVisto+"\nTe sentias: "+emocion;
            //}
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

        private void btnAgregarPendiente_Click(object sender, RoutedEventArgs e)
        {
            bool yaExisteContenido = false;
            bool yaEnLista = false;
            bool yaVisto = false;
            if (contenido.nombre.Contains("'"))
            {
                contenido.nombre = contenido.nombre.Replace("'", "");
            }
            else if (contenido.descripcion.Contains("'")) { 
                contenido.descripcion = contenido.descripcion.Replace("'", "");
            }

            if (contenido is Serie serieData)
            {
                yaExisteContenido = serieData.exists();
                if (!yaExisteContenido)
                {
                    serieData.insert();
                    int id = serieData.id;
                    List<int> generos = serieData.listaGeneros;
                    ContenidoGenero cg = new ContenidoGenero(id, generos);
                    cg.insert();

                }
                yaEnLista = usuarioLoged.contenidoEnListaPendiente(serieData.id);
                

                if (yaEnLista)
                {
                    MessageBox.Show("Este contenido ya se encuentra en tu lista de 'Pendientes'.");
                } else if (usuarioLoged.contenidoYaVisto(serieData.id)) {
                    MessageBox.Show("Este contenido ya se encuentra en tu lista de 'Vistos'.");
                }
                else
                {
                    Pendientes pendiente = new Pendientes(usuarioLoged.username, serieData.id, false, DateTime.Now.Date, false);
                    pendiente.insert();
                    MessageBox.Show("Has añadido '" + serieData.nombre + "' a tu lista de 'Pendientes'");
                }
            }
            else if (contenido is Pelicula peliData)
            {
                yaExisteContenido = peliData.exists();
                if (!yaExisteContenido)
                {
                    peliData.insert();
                    int id = peliData.id;
                    List<int> generos = peliData.listaGeneros;
                    ContenidoGenero cg = new ContenidoGenero(id, generos);
                    cg.insert();
                }

                yaEnLista = usuarioLoged.contenidoEnListaPendiente(peliData.id);
                if (yaEnLista && !usuarioLoged.contenidoYaVisto(peliData.id))
                {
                    MessageBox.Show("EEste contenido ya se encuentra en tu lista de 'Pendientes'");
                }
                else if (usuarioLoged.contenidoYaVisto(peliData.id))
                {
                    MessageBox.Show("Este contenido ya se encuentra en tu lista de 'Vistos'.");
                }
                else
                {
                    Pendientes pendiente = new Pendientes(usuarioLoged.username, peliData.id, false, DateTime.Now.Date, false);
                    pendiente.insert();
                    MessageBox.Show("Has añadido '" + peliData.nombre + "' a tu lista de pendientes");
                }
            }

            this.Close();
        }

        private void btnAgreagarVistos_Click(object sender, RoutedEventArgs e)
        {
            var popup = new AgregarVistoWindow
            {
                Owner = this
            };

            if (popup.ShowDialog() == true)
            {
                DateTime fecha = popup.FechaSeleccionada;
                string emocion = popup.EemocionSeleccionada;
                int puntuacion = popup.PuntuacionSeleccionada;

                bool yaExisteContenido = false;
                bool yaEnLista = false;
                if (contenido.nombre.Contains("'"))
                {
                    contenido.nombre = contenido.nombre.Replace("'", "");
                }
                else if (contenido.descripcion.Contains("'"))
                {
                    contenido.descripcion = contenido.descripcion.Replace("'", "");
                }
                if (contenido is Serie serieData)
                {
                    yaExisteContenido = serieData.exists();
                    if (!yaExisteContenido)
                    {
                        serieData.insert();
                        int id = serieData.id;
                        List<int> generos = serieData.listaGeneros;
                        ContenidoGenero cg = new ContenidoGenero(id, generos);
                        cg.insert();

                    }
                    yaEnLista = usuarioLoged.contenidoYaVisto(serieData.id);

                    if (yaEnLista)
                    {
                        MessageBox.Show("Este contenido ya se encuentra en tu lista de 'Vistos'.");
                    }
                    else
                    {
                        if (usuarioLoged.contenidoEnListaPendiente(serieData.id)) { 
                            Pendientes pendiente = new Pendientes(usuarioLoged.username, serieData.id, true, DateTime.Now.Date, false);
                            pendiente.update();
                        }
                        int idEmocion = new Emocion().getIdEmocion(emocion);
                        ContenidoVisto visto = new ContenidoVisto(usuarioLoged.username, serieData.id, idEmocion , DateTime.Now.Date, puntuacion);
                        visto.insert();
                        MessageBox.Show("Has añadido " + contenido.nombre + " \ncomo visto el " + fecha.ToShortDateString() + "\nsintiéndote " + emocion.ToLower() + ", \ncon una puntuación de " + puntuacion + "/5");
                    }
                }
                else if (contenido is Pelicula peliData)
                {
                    yaExisteContenido = peliData.exists();
                    if (!yaExisteContenido)
                    {
                        peliData.insert();
                        int id = peliData.id;
                        List<int> generos = peliData.listaGeneros;
                        ContenidoGenero cg = new ContenidoGenero(id, generos);
                        cg.insert();
                    }

                    yaEnLista = usuarioLoged.contenidoYaVisto(peliData.id);
                    if (yaEnLista)
                    {
                        MessageBox.Show("Este contenido ya se encuentra en tu lista de 'Vistos'");
                    }
                    else
                    {
                        if (usuarioLoged.contenidoEnListaPendiente(peliData.id))
                        {
                            Pendientes pendiente = new Pendientes(usuarioLoged.username, peliData.id, true, DateTime.Now.Date, false);
                            pendiente.update();
                        }
                        int idEmocion = new Emocion().getIdEmocion(emocion);
                        ContenidoVisto visto = new ContenidoVisto(usuarioLoged.username, peliData.id, idEmocion, DateTime.Now.Date, puntuacion);
                        visto.insert();
                        MessageBox.Show("Has añadido " + contenido.nombre + " \ncomo visto el " + fecha.ToShortDateString() + "\nsintiéndote " + emocion.ToLower() + ", \ncon una puntuación de " + puntuacion + "/5");
                    }
                }
                this.Close();
            }
        }
    }
}
