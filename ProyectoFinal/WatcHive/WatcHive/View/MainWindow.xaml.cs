using Google.Protobuf.WellKnownTypes;
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
using WatcHive.Persistence.Manages;
using WatcHive.View;

namespace WatcHive
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Usuario usuarioLoged;
        private string vistaActual = "peliculas";
        Dictionary<string, int> plataformas;
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();
            usuarioLoged = usuario;
            tagNombreUser.Text = usuarioLoged.username;
            MainContent.Content = new PeliculasView(usuarioLoged);
            rellenarCboxFiltrosAsync();

        }

        private async Task rellenarCboxFiltrosAsync()
        {
            Genero g = new Genero();
            g.readGenero();
            foreach (Genero genero in g.getListGenero())
            {
                if (genero.tipo.Equals("pelicula") || genero.tipo.Equals("both"))
                    cmbGeneros.Items.Add(genero.nombreGenero);
            }
            APIManager api = new APIManager();
            plataformas = await api.GetProvidersAsync();
            foreach (var plataforma in plataformas)
            {
                cmbPlataformas.Items.Add(plataforma.Key);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMenuPeliculas_Click(object sender, RoutedEventArgs e)
        {
            mostrarFiltros();
            Genero g = new Genero();
            g.readGenero();
            cmbGeneros.Items.Clear();
            foreach (Genero genero in g.getListGenero())
            {
                if (genero.tipo.Equals("pelicula")||genero.tipo.Equals("both"))
                    cmbGeneros.Items.Add(genero.nombreGenero);
            }
            vistaActual = "peliculas";
            MainContent.Content = new PeliculasView(usuarioLoged);
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
        }

        private void btnMenuSeries_Click(object sender, RoutedEventArgs e)
        {
            mostrarFiltros();
            Genero g = new Genero();
            g.readGenero();
            cmbGeneros.Items.Clear();
            foreach (Genero genero in g.getListGenero())
            {
                if (genero.tipo.Equals("serie") || genero.tipo.Equals("both"))
                    cmbGeneros.Items.Add(genero.nombreGenero);
            }
            vistaActual = "series";
            MainContent.Content = new SeriesView(usuarioLoged);
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
        }

        private void mostrarFiltros()
        {
            txtBusqueda.Visibility = Visibility.Visible;
            cmbGeneros.Visibility = Visibility.Visible;
            cmbPlataformas.Visibility = Visibility.Visible;
            btnLimpiar.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;
            lblGenero.Visibility = Visibility.Visible;
            lblPlataforma.Visibility = Visibility.Visible;
        }

        private void ocultarFiltros()
        {
            txtBusqueda.Visibility = Visibility.Collapsed;
            cmbGeneros.Visibility = Visibility.Collapsed;
            cmbPlataformas.Visibility = Visibility.Collapsed;
            btnLimpiar.Visibility = Visibility.Collapsed;
            btnSearch.Visibility = Visibility.Collapsed;
            lblGenero.Visibility = Visibility.Collapsed;
            lblPlataforma.Visibility = Visibility.Collapsed;
        }


        private void setBackgroundBtn(object sender)
        {
            UIElementCollection elementos = sidebar.Children;
            foreach (UIElement elemento in elementos)
            {
                if (elemento is Button boton)
                {
                    boton.Background = Brushes.Transparent;
                }
            }

            Button botonPresionado = (Button)sender;
            botonPresionado.Background = (Brush)new BrushConverter().ConvertFromString("#C0392B");
        }

        private void btnMenuListas_Click(object sender, RoutedEventArgs e)
        {
            ocultarFiltros();
            MainContent.Content = new ListasUserView(usuarioLoged);
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
        }

        private void btnMenuEmocion_Click(object sender, RoutedEventArgs e)
        {
            ocultarFiltros();
            MainContent.Content = new RecomendacionesView(usuarioLoged);
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
        }

        private void btnMenuConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new ConfiguracionWindow(usuarioLoged);
            configWindow.ShowDialog();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            cmbGeneros.SelectedIndex = -1;
            cmbPlataformas.SelectedIndex = -1;
            txtBusqueda.Text = "Titulo...";
            if (vistaActual.Equals("peliculas"))
            {
                var vistaPeliculas = new PeliculasView(usuarioLoged);
                MainContent.Content = vistaPeliculas;
            }
            else if (vistaActual.Equals("series"))
            {
                var vistaSeries = new SeriesView(usuarioLoged);
                MainContent.Content = vistaSeries;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string titulo = txtBusqueda.Text.Trim();
            if (!txtBusqueda.Text.Equals("Titulo...") && !string.IsNullOrWhiteSpace(titulo))
            {
                //Filtrar por titulo
                if (vistaActual.Equals("peliculas"))
                {
                    var vistaPeliculas = new PeliculasView(usuarioLoged, titulo,"TITULO");
                    MainContent.Content = vistaPeliculas;
                }
                else if (vistaActual.Equals("series"))
                {
                    var vistaSeries = new SeriesView(usuarioLoged, titulo, "TITULO");
                    MainContent.Content = vistaSeries;
                }
            }
            else if (cmbGeneros.SelectedIndex != -1) {
                
                // Filtrar por genero
                string genero = cmbGeneros.SelectedItem.ToString();
                if (vistaActual.Equals("peliculas"))
                {
                    var vistaPeliculas = new PeliculasView(usuarioLoged, genero, "GENERO");
                    MainContent.Content = vistaPeliculas;
                }
                else if (vistaActual.Equals("series"))
                {
                    var vistaSeries = new SeriesView(usuarioLoged, genero, "GENERO");
                    MainContent.Content = vistaSeries;
                }

            }
            else if (cmbPlataformas.SelectedIndex != -1)
            {
                // Filtrar por plataforma
                string plataforma = cmbPlataformas.SelectedItem.ToString();
                if (vistaActual.Equals("peliculas"))
                {
                    var vistaPeliculas = new PeliculasView(usuarioLoged, plataforma, "PLATAFORMA");
                    MainContent.Content = vistaPeliculas;
                }
                else if (vistaActual.Equals("series"))
                {
                    var vistaSeries = new SeriesView(usuarioLoged, plataforma, "PLATAFORMA");
                    MainContent.Content = vistaSeries;
                }
            }
        }
        private void Limpiarfiltros(object sender, RoutedEventArgs e)
        {
            if (sender == txtBusqueda)
    {
        // Limpia ambos ComboBox
        cmbPlataformas.SelectedIndex = -1;
        cmbGeneros.SelectedIndex = -1;

        // Opcional: limpia placeholder
        if (txtBusqueda.Text == "Titulo...")
        {
            txtBusqueda.Clear();
        }
    }
    else if (sender == cmbPlataformas)
    {
        // Limpia el TextBox y el otro ComboBox
        if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
        {
            txtBusqueda.Clear();
        }

        cmbGeneros.SelectedIndex = -1;
    }
    else if (sender == cmbGeneros)
    {
        // Limpia el TextBox y el otro ComboBox
        if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
        {
            txtBusqueda.Clear();
        }

        cmbPlataformas.SelectedIndex = -1;
    }
        }

    }
}
