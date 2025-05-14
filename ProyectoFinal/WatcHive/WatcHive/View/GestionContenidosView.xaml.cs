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

namespace WatcHive.View
{
    /// <summary>
    /// Lógica de interacción para GestionContenidosView.xaml
    /// </summary>
    public partial class GestionContenidosView : UserControl
    {
        private List<Contenido> listaContenidos;
        public GestionContenidosView()
        {
            InitializeComponent();
            inicializarContenido();
        }

        private void inicializarContenido()
        {
            Pelicula peli = new Pelicula();
            Serie serie = new Serie();
            peli.readPelicula();
            serie.readSerie();
            List<Contenido> listaContenidos = new List<Contenido>();
            foreach (Pelicula peliaux in peli.getListPeliculas()) { 
                listaContenidos.Add(peliaux);
            }
            foreach (Serie serieaux in serie.getListSeries())
            {
                listaContenidos.Add(serieaux);
            }


            tabla.ItemsSource = listaContenidos;
            listaContenidos = (List<Contenido>)tabla.ItemsSource;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tabla.SelectedItem is Contenido contenidoSeleccionado)
            {
                // Confirmación opcional
                var resultado = MessageBox.Show($"¿Estás seguro que deseas eliminar el contenido '{contenidoSeleccionado.nombre}'?",
                                                "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {

                    if (contenidoSeleccionado is Pelicula) {
                        Pelicula pelicula = (Pelicula)contenidoSeleccionado;
                        pelicula.delete();
                    }
                    else if (contenidoSeleccionado is Serie)
                    {
                        Serie serie = (Serie)contenidoSeleccionado;
                        serie.delete();
                    }

                    List<Contenido> conenido = (List<Contenido>)tabla.ItemsSource;
                    conenido.Remove(contenidoSeleccionado);

                    tabla.Items.Refresh();
                    MessageBox.Show("Usuario eliminado correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para eliminar.");
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            RealizarBusqueda();
        }

        private void RealizarBusqueda()
        {
            string searchText = search.Text.Trim().ToLower();

            if (listaContenidos == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                tabla.ItemsSource = listaContenidos;
            }
            else
            {
                var filteredList = listaContenidos.Where(contenido =>
                    contenido != null && (
                    (contenido.nombre != null && contenido.nombre.ToLower().Contains(searchText)) ||
                    (contenido.descripcion != null && contenido.descripcion.ToLower().Contains(searchText)) ||
                    (contenido.fechaEstreno != null && contenido.fechaEstreno.ToString("yyyy-MM-dd").Contains(searchText)) ||
                    (contenido.imagen != null && contenido.imagen.ToLower().Contains(searchText)) ||
                    (contenido.listaGeneros != null && contenido.listaGeneros.Any(g => g.ToString().Contains(searchText)))
                    )
                ).ToList();

                tabla.ItemsSource = filteredList;
            }

            tabla.Items.Refresh();
        }
    }
}
