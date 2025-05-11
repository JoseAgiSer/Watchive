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
        List <Contenido> contenidosList = new List<Contenido>();
        public ListasUserView(Usuario usuario)
        {
            usuarioLoged = usuario;
            InitializeComponent();
            rellenarPaneles();
        }

        private void rellenarPaneles()
        {
            Pendientes p = new Pendientes();
            p.readPendientes(usuarioLoged.username);

            foreach (Pendientes pen in p.getListPendientes()) {

                if (new Pelicula().isPelicula(pen.id))
                {
                    //Es una pelicula
                    Pelicula pe = new Pelicula();
                    //peliculaList.Add(pe.readById(pen.id));
                    contenidosList.Add(pe.readById(pen.id));
                }
                else {
                    // no es una peli, por lo tanto una serie
                    Serie se = new Serie();
                    //serieList.Add(se.readById(pen.id));
                    contenidosList.Add(se.readById(pen.id));
                }
            }

            foreach (Contenido contenido in contenidosList) {
                var stack = new StackPanel
                {
                    Width = 150,
                    Margin = new Thickness(10),
                    Cursor = Cursors.Hand
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
                panelPendientes.Children.Add(stack);
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
