using System;
using System.Collections.Generic;
using System.Linq;
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
using WatcHive.Domain.API;
using WatcHive.Domain;
using WatcHive.Persistence.Manages;

namespace WatcHive.View
{
    /// <summary>
    /// Lógica de interacción para PeliculasView.xaml
    /// </summary>
    public partial class PeliculasView : UserControl
    {
        private List<Pelicula> pelisEnPantalla = new List<Pelicula>();
        private Usuario usuarioLoged;
        public PeliculasView(Usuario usuario)
        {
            InitializeComponent();
            LoadPopularPeliculas();
            usuarioLoged = usuario;
        }

        private async void LoadPopularPeliculas()
        {
            APIManager apiManager = new APIManager();
            var pelis = await apiManager.GetPopularMoviesAsync();

            foreach (var peli in pelis)
            {
                string url = $"https://image.tmdb.org/t/p/w500{peli.poster_path}";

                Pelicula peliculaObj = convertirAPelicula(url,peli);

                var stack = new StackPanel
                {
                    Width = 200,
                    Margin = new Thickness(10),
                    Cursor = Cursors.Hand
                };

                Image image = new Image
                {
                    Width = 150,
                    Height = 225,
                    Margin = new Thickness(10),
                    Stretch = Stretch.UniformToFill,
                    Tag = peliculaObj
                };

                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(url);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    image.Source = bitmap;
                }
                catch
                {
                    // Manejo opcional de errores
                }

                image.MouseDown += ImageOrTitle_Click;

                TextBlock title = new TextBlock
                {
                    Text = peli.title,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 5, 0, 0),
                    Tag = peliculaObj
                };

                title.MouseDown += ImageOrTitle_Click;

                stack.Children.Add(image);
                stack.Children.Add(title);
                PeliculasPanel.Children.Add(stack);
            }
        }
        private Pelicula convertirAPelicula(string url, TMDBMovie peli)
        {
            Pelicula peliAdd = new Pelicula(
                peli.id,
                peli.title,
                DateTime.Parse(peli.releaseDate),
                url,
                peli.Overview,
                peli.VoteAverage,
                peli.generoId
                );

            pelisEnPantalla.Add(peliAdd);
            return peliAdd;
        }

        private void ImageOrTitle_Click(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element?.Tag is Pelicula peliData)
            {
                var detalleWindow = new DetalleWindow(peliData, usuarioLoged);
                detalleWindow.ShowDialog();
            }
        }
    }
}
