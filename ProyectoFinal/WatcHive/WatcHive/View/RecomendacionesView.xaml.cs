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
using WatcHive.Domain;
using WatcHive.Persistence.Manages;

namespace WatcHive.View
{
    /// <summary>
    /// Lógica de interacción para RecomendacionesView.xaml
    /// </summary>
    public partial class RecomendacionesView : UserControl
    {
        public RecomendacionesView(Usuario usuarioLoged)
        {
            InitializeComponent();
        }

        private async void BtnRecomendar_Click(object sender, RoutedEventArgs e)
        {
            string emocion = (comboEmociones.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(emocion))
            {
                MessageBox.Show("Por favor, selecciona una emoción.");
                return;
            }
            bool incluirPeliculas = checkPeliculas.IsChecked == true;
            bool incluirSeries = checkSeries.IsChecked == true;

            wrapRecomendaciones.Children.Clear();

            Dictionary<string, List<int>> emocionGeneros = new Dictionary<string, List<int>>
            {
                { "Feliz", new List<int> { 35, 10751 } },         // Comedia, Familia
                { "Triste", new List<int> { 18, 10749 } },        // Drama, Romance
                { "Enfadado", new List<int> { 28, 53 } },         // Accion, Thriller
                { "Ansioso", new List<int> { 9648, 27 } },        // Misterio, Terror
                { "Aburrido", new List<int> { 12, 14 } },         // Aventura, Fantasa
                { "Relajado", new List<int> { 16, 10402 } },      // Animacion, Musica
                { "Motivado", new List<int> { 99, 80 } },         // Documental, Crimen
            };

            if (!emocionGeneros.ContainsKey(emocion))
            {
                MessageBox.Show("Por favor, selecciona una emoción válida.");
                return;
            }

            APIManager api = new APIManager();
            List<int> generos = emocionGeneros[emocion];

            if (incluirPeliculas)
            {
                var peliculas = await api.GetMoviesByGenresAsync(generos);
                foreach (var peli in peliculas)
                {
                    Pelicula pelicula = new Pelicula
                    {
                        id = peli.id,
                        nombre = peli.title,
                        imagen = $"https://image.tmdb.org/t/p/w500{peli.poster_path}",
                        fechaEstreno = DateTime.Parse(peli.releaseDate),
                        descripcion = peli.Overview,
                        nota = peli.VoteAverage,
                        listaGeneros = peli.generoId
                    };

                    wrapRecomendaciones.Children.Add(CrearElementoVisual(pelicula));
                }
            }

            if (incluirSeries)
            {
                var series = await api.GetSeriesByGenresAsync(generos);
                foreach (var serie in series)
                {
                    Serie serieaux = new Serie
                    {
                        id = serie.id,
                        nombre = serie.name,
                        imagen = $"https://image.tmdb.org/t/p/w500{serie.poster_path}",
                        fechaEstreno = DateTime.Parse(serie.first_air_date),
                        descripcion = serie.overview,
                        numTemporadas = serie.number_of_seasons.HasValue ? serie.number_of_seasons.Value : 1,
                        listaGeneros = serie.genre_ids
                    };
                    wrapRecomendaciones.Children.Add(CrearElementoVisual(serieaux));
                }
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
                Tag = contenido
            };

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(contenido.imagen);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                image.Source = bitmap;
            }
            catch {
            
            }

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

            stack.Children.Add(image);
            stack.Children.Add(title);
            return stack;
        }
    }
}
