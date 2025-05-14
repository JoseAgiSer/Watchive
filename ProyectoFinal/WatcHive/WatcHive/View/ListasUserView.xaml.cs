using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WatcHive.Domain;

namespace WatcHive.View
{
    /// <summary>
    /// Lógica de interacción para ListasUserView.xaml
    /// </summary>
    public partial class ListasUserView : UserControl
    {
        private Usuario usuarioLoged;
        List <Pelicula> peliculaList;
        List <Serie> serieList;
        List <Contenido> contenidoPendienteList = new List<Contenido>();
        List<Contenido> contenidoVistoList = new List<Contenido>();
        public ListasUserView(Usuario usuario)
        {
            usuarioLoged = usuario;
            InitializeComponent();
            rellenarListas();
        }

        private void rellenarListas()
        {
            Pendientes p = new Pendientes();
            p.readPendientes(usuarioLoged.username);

            ContenidoVisto cv = new ContenidoVisto();
            cv.readContenidoVisto(usuarioLoged.username);

            foreach (ContenidoVisto vistos in cv.getListContenidoVisto())
            {

                if (new Pelicula().isPelicula(vistos.idContenido))
                {
                    Pelicula pe = new Pelicula();
                    contenidoVistoList.Add(pe.readById(vistos.idContenido));
                }
                else
                {
                    Serie se = new Serie();
                    contenidoVistoList.Add(se.readById(vistos.idContenido));
                }
            }

            foreach (Pendientes pen in p.getListPendientes()) {

                if (new Pelicula().isPelicula(pen.id))
                {
                    Pelicula pe = new Pelicula();
                    contenidoPendienteList.Add(pe.readById(pen.id));
                }
                else {
                    Serie se = new Serie();
                    contenidoPendienteList.Add(se.readById(pen.id));
                }
            }

            rellenarPaneles(contenidoVistoList, panelVistos, false);
            rellenarPaneles(contenidoPendienteList, panelPendientes, true);
        }

        private void rellenarPaneles(List<Contenido> contenidoList, WrapPanel panel, bool addBoton)
        {
            foreach (Contenido contenido in contenidoList)
            {
                var stack = new StackPanel
                {
                    Width = 150,
                    Margin = new Thickness(10)
                };

                Image image = new Image
                {
                    Width = 110,
                    Margin = new Thickness(10),
                    Stretch = Stretch.Uniform
                };

                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(contenido.imagen);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    image.Source = bitmap;
                }
                catch { }

                image.MouseDown += ImageOrTitle_Click;

                TextBlock title = new TextBlock
                {
                    Text = contenido.nombre,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 5, 0, 0),
                };

                title.MouseDown += ImageOrTitle_Click;

                stack.Children.Add(image);
                stack.Children.Add(title);

                if (addBoton)
                {
                    Button vistoButton = new Button
                    {
                        Content = "Ya la he visto",
                        Width = 120,
                        Height = 24,
                        Margin = new Thickness(0, 8, 0, 0),
                        Tag = contenido,
                        Style = (Style)Application.Current.FindResource("LoginButtonStyle"),
                        FontWeight = FontWeights.SemiBold,
                        FontSize = 11
                    };

                    vistoButton.Click += VistoButton_Click;

                    stack.Children.Add(vistoButton);
                }

                panel.Children.Add(stack);
            }
        }

        private void VistoButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Contenido contenido)
            {
                Window parentWindow = Window.GetWindow(this);
                var popup = new AgregarVistoWindow
                {
                    Owner = parentWindow
                };
                Pendientes pendiente = new Pendientes(usuarioLoged.username, contenido.id, true, DateTime.Now.Date, false);
                pendiente.update();

                if (popup.ShowDialog() == true)
                {
                    DateTime fechaSeleccionada = popup.FechaSeleccionada;
                    string emocionSeleccionada = popup.EemocionSeleccionada;
                    int puntuacionSeleccionada = popup.PuntuacionSeleccionada;

                    ContenidoVisto contenidoVisto = new ContenidoVisto
                    {
                        idContenido = contenido.id,
                        nombreUsuario = usuarioLoged.username,
                        fechaVisto = fechaSeleccionada,
                        idEmocion = new Emocion().getIdEmocion(emocionSeleccionada),
                        puntuacion = puntuacionSeleccionada
                    };

                    contenidoVisto.insert();

                    contenidoVistoList = new List<Contenido>();
                    contenidoPendienteList = new List<Contenido>();

                    panelPendientes.Children.Clear();
                    panelVistos.Children.Clear();

                    rellenarListas();
                }

            }
        }

        private void ImageOrTitle_Click(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element?.Tag is Serie serieData)
            {
                var detalleWindow = new DetalleWindow(serieData, usuarioLoged);
                detalleWindow.ShowDialog();
            }
        }
    }
}
