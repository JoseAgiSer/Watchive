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

        public Dictionary<string, List<int>> emocionGenerospelis = new Dictionary<string, List<int>>
            {
                { "Feliz", new List<int> { 35, 10751 } },         // Comedia, Familia
                { "Triste", new List<int> { 18, 10749 } },        // Drama, Romance
                { "Enfadado/a", new List<int> { 28, 53 } },         // Accion, Thriller
                { "Ansioso/a", new List<int> { 9648, 27 } },        // Misterio, Terror
                { "Aburrido/a", new List<int> { 12, 14 } },         // Aventura, Fantasa
                { "Relajado/a", new List<int> { 16, 10402 } },      // Animacion, Musica
                { "Motivado/a", new List<int> { 99, 80 } },         // Documental, Crimen
                { "Indefinida", new List<int> { 99, 80 } },
            };

        public Dictionary<string, List<int>> emocionGenerosseries = new Dictionary<string, List<int>>
            {
                { "Feliz", new List<int> { 35, 10751 } },         // Comedia, Familia
                { "Triste", new List<int> { 18, 10749 } },        // Drama, Romance
                { "Enfadado/a", new List<int> { 10759, 9648 } },         // Accion, Thriller
                { "Ansioso/a", new List<int> { 9648, 18 } },        // Misterio, Terror
                { "Aburrido/a", new List<int> { 10765, 10759 } },         // Aventura, Fantasa
                { "Relajado/a", new List<int> { 16, 10751 } },      // Animacion, Musica
                { "Motivado/a", new List<int> { 99, 80 } },         // Documental, Crimen
                { "Indefinida", new List<int> { 99, 80 } },
            };


        private Usuario usuarioLoged;
        public RecomendacionesView(Usuario usuarioLoged)
        {
            InitializeComponent();
            this.usuarioLoged = usuarioLoged;
        }

        private async void BtnRecomendar_Click(object sender, RoutedEventArgs e)
        {
            string emocion = (comboEmociones.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(emocion))
            {
                MessageBox.Show("Por favor, selecciona una emoción.");
                return;
            }

            bool incluirPeliculas = radioPeliculas.IsChecked == true;
            bool incluirSeries = radioSeries.IsChecked == true;

            wrapRecomendaciones.Children.Clear();

            if (!emocionGenerospelis.ContainsKey(emocion))
            {
                MessageBox.Show("Por favor, selecciona una emoción válida.");
                return;
            }
            if (emocion.Equals("No lo se"))
            {
                emocion = "Indefinida";
            }

            APIManager api = new APIManager();

            List<int> generos = recomendacionPersonalizada(emocion, incluirPeliculas);

            if (incluirPeliculas)
            {
                
                if (generos.Count != 0 || emocion.Equals("Indefinida"))
                {
                    //Personalizada
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
                else
                {
                    //Defaulr
                    var peliculas = await api.GetMoviesByGenresAsync(emocionGenerospelis[emocion]);
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
            }

            if (incluirSeries)
            {
                if(generos.Count != 0 || emocion.Equals("Indefinida"))
                {
                    var series = await api.GetSeriesByGenresAsync(generos);
                    foreach (var serie in series)
                    {
                        DateTime fechaEstreno;
                        if (!string.IsNullOrEmpty(serie.first_air_date))
                        {
                            DateTime.TryParse(serie.first_air_date, out fechaEstreno);
                        }
                        else
                        {
                            fechaEstreno = new DateTime(1900, 1, 1); // o la que consideres por defecto
                        }
                        Serie serieaux = new Serie
                        {
                            id = serie.id,
                            nombre = serie.name,
                            imagen = $"https://image.tmdb.org/t/p/w500{serie.poster_path}",
                            fechaEstreno = fechaEstreno,
                            descripcion = serie.overview,
                            numTemporadas = serie.number_of_seasons.HasValue ? serie.number_of_seasons.Value : 1,
                            listaGeneros = serie.genre_ids
                        };
                        wrapRecomendaciones.Children.Add(CrearElementoVisual(serieaux));
                    }
                }
                else
                {
                    var series = await api.GetSeriesByGenresAsync(emocionGenerosseries[emocion]);
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
        }

        private List<int> recomendacionPersonalizada(string emocion, bool pelicula)
        {
            List<int> gustosGneros = new List<int>();
            if (pelicula)
            {
                Emocion emo = new Emocion();
                emo.getIdEmocion(emocion);

                ContenidoVisto c = new ContenidoVisto();
                gustosGneros = c.getRecomendacionUsuario(usuarioLoged.username, emocion,true);
            }
            else {
                ContenidoVisto c = new ContenidoVisto();
                gustosGneros = c.getRecomendacionUsuario(usuarioLoged.username, emocion,false);
            }
            return gustosGneros;
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

            string urlImagen = contenido.imagen?.Replace("https://image.tmdb.org/t/p/w500", "").Trim();
            bool imagenValida = !string.IsNullOrWhiteSpace(urlImagen);
            if (imagenValida)
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
                catch {
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
        private void ImageOrTitle_Click(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element?.Tag is Serie serieData)
            {
                var detalleWindow = new DetalleWindow(serieData, usuarioLoged);
                detalleWindow.ShowDialog();
            }
            if (element?.Tag is Pelicula peliData)
            {
                var detalleWindow = new DetalleWindow(peliData, usuarioLoged);
                detalleWindow.ShowDialog();
            }
        }
    }
}
