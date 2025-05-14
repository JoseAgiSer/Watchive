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
    /// Lógica de interacción para GestionUsuariosView.xaml
    /// </summary>
    public partial class GestionUsuariosView : UserControl
    {
        private List<Usuario> listaUsuarios;  
        public GestionUsuariosView()
        {
            InitializeComponent();
            inicializarUsuarios();
        }

        private void inicializarUsuarios()
        {
            Usuario usuario = new Usuario();
            usuario.readUsuario();
            tabla.ItemsSource = usuario.getListUsuarios();
            listaUsuarios = (List<Usuario>)tabla.ItemsSource;

        }

        private void SoloNumeros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !EsNumero(e.Text);
        }

        private bool EsNumero(string texto)
        {
            return texto.All(char.IsDigit);
        }

        private void btnAnadir_Click(object sender, RoutedEventArgs e)
        {
            if (!btnModificar.IsEnabled) // Modo MODIFICAR
            {
                if (tabla.SelectedItem is Usuario usuarioSeleccionado)
                {
                    // Actualizar propiedades desde los campos del formulario
                    usuarioSeleccionado.username = usernamebox.Text;
                    usuarioSeleccionado.password = passbox.Password;
                    usuarioSeleccionado.nombre = nombrebox.Text;
                    usuarioSeleccionado.apellidos = apellidosbox.Text;
                    usuarioSeleccionado.email = emailbox.Text;
                    usuarioSeleccionado.numHijos = int.TryParse(numhijosbox.Text, out int hijos) ? hijos : 0;
                    usuarioSeleccionado.fechaNacimiento = birthdaybox.SelectedDate ?? DateTime.Now;

                    usuarioSeleccionado.update(); // Guardar cambios en BD

                    tabla.Items.Refresh();
                    MessageBox.Show("Usuario modificado correctamente.");

                    // Restaurar estado
                    btnModificar.IsEnabled = true;
                    btnEliminar.IsEnabled = true;
                    usernamebox.IsEnabled = true;
                    tabla.IsHitTestVisible = true;
                    btnAnadir.Content = "Dar de alta";

                    LimpiarCamposFormulario();
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un usuario válido para modificar.");
                }
            }
            else // Modo AÑADIR
            {
                string username = usernamebox.Text;
                string password = passbox.Password;
                string numhijos = numhijosbox.Text;
                string nombre = nombrebox.Text;
                string apellidos = apellidosbox.Text;
                string email = emailbox.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                    birthdaybox.SelectedDate !=null || string.IsNullOrEmpty(numhijos) || string.IsNullOrEmpty(nombre)|| string.IsNullOrEmpty(apellidos)||string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Se deben rellenar todos los campos del formulario correctamente");
                    return;
                }

                Usuario nuevoUsuario = new Usuario
                {
                    username = username,
                    password = password,
                    nombre = nombrebox.Text,
                    apellidos = apellidosbox.Text,
                    email = emailbox.Text,
                    numHijos = int.TryParse(numhijosbox.Text, out int hijos) ? hijos : 0,
                    fechaNacimiento = birthdaybox.SelectedDate ?? DateTime.Now
                };

                if (!nuevoUsuario.existeUsername())
                {
                    nuevoUsuario.insert();

                    listaUsuarios = (List<Usuario>)tabla.ItemsSource;
                    listaUsuarios.Add(nuevoUsuario);
                    tabla.Items.Refresh();

                    MessageBox.Show("Usuario añadido correctamente.");
                    LimpiarCamposFormulario();
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya existe.");
                }
            }
        }


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LimpiarCamposFormulario()
        {
            usernamebox.Clear();
            passbox.Clear();
            nombrebox.Clear();
            apellidosbox.Clear();
            emailbox.Clear();
            numhijosbox.Text = "0";
            birthdaybox.SelectedDate = null;
        }


        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            RealizarBusqueda();
        }

        private void RealizarBusqueda()
        {
            string searchText = search.Text.Trim().ToLower();

            if (listaUsuarios == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                tabla.ItemsSource = listaUsuarios;
            }
            else
            {
                var filteredList = listaUsuarios.Where(usuario =>
                    usuario != null && (
                    usuario.username != null && usuario.username.ToLower().Contains(searchText) ||
                    (usuario.nombre != null && usuario.nombre.ToLower().Contains(searchText)) ||
                    (usuario.apellidos != null && usuario.apellidos.ToLower().Contains(searchText)) ||
                    (usuario.fechaNacimiento != null && usuario.fechaNacimiento.ToString("yyyy-MM-dd").Contains(searchText)) ||
                    (usuario.password != null && usuario.password.ToLower().Contains(searchText)) ||
                    (usuario.numHijos.ToString().Contains(searchText)) ||
                    (usuario.email != null && usuario.email.ToLower().Contains(searchText))
                    )
                ).ToList();

                tabla.ItemsSource = filteredList;
            }

            tabla.Items.Refresh();
        }
    }
}
