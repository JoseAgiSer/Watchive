using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using WatcHive.Domain.API;
using WatcHive.Persistence.Manages;

namespace WatcHive.View
{
    /// <summary>
    /// Lógica de interacción para SeriesView.xaml
    /// </summary>
    public partial class SeriesView : UserControl
    {
        private List<Serie> seriesEnPantalla = new List<Serie>();
        private Usuario usuarioLoged;
        public SeriesView(Usuario usuario)
        {
            InitializeComponent();
            LoadPopularSeries();
            usuarioLoged = usuario;
        }

        public SeriesView(Usuario usuario, string titulo)
        {
            InitializeComponent();
            BuscarPorTitulo(titulo);
            usuarioLoged = usuario;
        }

        private async void LoadPopularSeries()
        {
            APIManager apiManager = new APIManager();
            var series = await apiManager.GetPopularSeriesAsync();

            foreach (var serie in series)
            {
                string url = $"https://image.tmdb.org/t/p/w500{serie.poster_path}";

                Serie serieObj = convertirASerie(url,serie);

               
                SeriesPanel.Children.Add(CrearElementoVisual(serieObj));
            }
        }
        private Serie convertirASerie(string url, TVShowDTO serie)
        {

            DateTime fechaEstreno = DateTime.MinValue;

            if (!string.IsNullOrWhiteSpace(serie.first_air_date) &&
                DateTime.TryParse(serie.first_air_date, out DateTime fechaParseada))
            {
                fechaEstreno = fechaParseada;
            }

            Serie serieAdd = new Serie(
                serie.id, 
                serie.name,
                fechaEstreno, 
                url, 
                serie.overview, 
                serie.number_of_seasons ?? 1,
                serie.genre_ids
                );

            seriesEnPantalla.Add(serieAdd);
            return serieAdd;
        }

        private void ImageOrTitle_Click(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element?.Tag is Serie serieData)
            {
                var detalleWindow = new DetalleWindow(serieData,usuarioLoged);
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
            catch
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

            List<TVShowDTO> resultados = await api.SearchSeriesByTitleAsync(titulo);

            if (resultados != null)
            {
                foreach (var serie in resultados)
                {
                    string url = $"https://image.tmdb.org/t/p/w500{serie.poster_path}";

                    Serie peliculaObj = convertirASerie(url, serie);

                    SeriesPanel.Children.Add(CrearElementoVisual(peliculaObj));
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados o hubo un error con la API.");
            }
        }
    }
}
