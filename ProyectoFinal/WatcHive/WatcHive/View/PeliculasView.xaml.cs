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
using Newtonsoft.Json;

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

        public PeliculasView(Usuario usuario, string filtro, string busqueda)
        {
            InitializeComponent();
            if (busqueda.Equals("TITULO"))
            {
                BuscarPorTitulo(filtro);
            }
            else if (busqueda.Equals("GENERO"))
            {
                BuscarPorGenero(filtro);
            }
            else if (busqueda.Equals("PLATAFORMA"))
            {
                BuscarPorPlataforma(filtro);
            }
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

                PeliculasPanel.Children.Add(CrearElementoVisual(peliculaObj));
            }
        }
        private Pelicula convertirAPelicula(string url, TMDBMovie peli)
        {
            DateTime fechaEstreno = DateTime.MinValue;

            if (!string.IsNullOrWhiteSpace(peli.releaseDate) &&
                DateTime.TryParse(peli.releaseDate, out DateTime fechaParseada))
            {
                fechaEstreno = fechaParseada;
            }

            Pelicula peliAdd = new Pelicula(
                peli.id,
                peli.title,
                fechaEstreno,
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

        private UIElement CrearElementoVisual(Contenido contenido)
        {
            var stack = new StackPanel
            {
                Width = 200,
                Margin = new Thickness(10),
                Cursor = Cursors.Hand
            };

            var image = new Image
            {
                Width = 150,
                Height = 225,
                Margin = new Thickness(10),
                Stretch = Stretch.UniformToFill,
                Tag = contenido,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            string urlImagen = contenido.imagen?.Replace("https://image.tmdb.org/t/p/w500", "").Trim();
            bool imagenValida = !string.IsNullOrWhiteSpace(urlImagen);
            if(imagenValida)
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(contenido.imagen);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    image.Source = bitmap;
                }
                catch
                {
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/imagendefault.jpg"));
                }
            }
            else
            {
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/imagendefault.jpg"));
            }

            image.MouseDown += ImageOrTitle_Click;

            var title = new TextBlock
            {
                Text = contenido.nombre,
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 5, 0, 0),
                Tag = contenido
            };

            title.MouseDown += ImageOrTitle_Click;

            stack.Children.Add(image);
            stack.Children.Add(title);
            return stack;
        }
        private async void BuscarPorTitulo(string titulo)
        {
            APIManager api = new APIManager();

            List<TMDBMovie> resultados = await api.SearchMoviesByTitleAsync(titulo);

            if (resultados != null && resultados.Count != 0)
            {
                foreach (var peli in resultados)
                {
                    string url = $"https://image.tmdb.org/t/p/w500{peli.poster_path}";

                    Pelicula peliculaObj = convertirAPelicula(url, peli);

                    PeliculasPanel.Children.Add(CrearElementoVisual(peliculaObj));
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para el titulo '"+titulo+"'");
            }
        }
        private async void BuscarPorPlataforma(string plataforma)
        {
            APIManager api = new APIManager();
            Dictionary<string, int> plataformas = await api.GetProvidersAsync();

            if (!plataformas.TryGetValue(plataforma, out int providerId))
            {
                MessageBox.Show("No se encontró el proveedor especificado.");
                return;
            }

            List<TMDBMovie> resultados = await api.GetMoviesByProviderAsync(providerId);

            if (resultados != null)
            {
                foreach (var peli in resultados)
                {
                    string url = $"https://image.tmdb.org/t/p/w500{peli.poster_path}";

                    Pelicula peliculaObj = convertirAPelicula(url, peli);

                    PeliculasPanel.Children.Add(CrearElementoVisual(peliculaObj));
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados o hubo un error con la API.");
            }
        }

        private async void BuscarPorGenero(string genero)
        {
            APIManager api = new APIManager();

            Genero gen = new Genero();

            int idgenero = gen.getIdByName(genero);

            List<TMDBMovie> resultados = await api.GetMoviesByGenreAsync(idgenero);

            if (resultados != null)
            {
                foreach (var serie in resultados)
                {
                    string url = $"https://image.tmdb.org/t/p/w500{serie.poster_path}";

                    Pelicula peliculaObj = convertirAPelicula(url, serie);

                    PeliculasPanel.Children.Add(CrearElementoVisual(peliculaObj));
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados o hubo un error con la API.");
            }
        }
    }
}
