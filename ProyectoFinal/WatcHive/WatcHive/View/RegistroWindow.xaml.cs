using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WatcHive.Domain;

namespace WatcHive.View
{
    /// <summary>
    /// Lógica de interacción para RegistroWindow.xaml
    /// </summary>
    public partial class RegistroWindow : Window
    {
        public RegistroWindow()
        {
            InitializeComponent();
        }
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Comprueba que todo este rellenado en el formulario
            if (comprobarVacios())
            {
                string nombre = nombrebox.Text;
                string apellidos = apellidosbox.Text;
                string username = usernamebox.Text;
                string email = emailbox.Text;
                DateTime fechaNacimiento = birthdaybox.SelectedDate.Value;
                int numHijos = Convert.ToInt32(numhijosbox.Text);
                string pass = passbox.Password;
                string repass = repassbox.Password;

                bool coinciden;
                //Comprueba que las contraseñas son identicas en ambos campos
                coinciden = comprobarContrasena(pass, repass);
                
                if (!coinciden) return;

                Usuario usuario = new Usuario(username, EncriptarContraseña( pass), email,nombre,apellidos,numHijos,fechaNacimiento);
                //Comprueba si el username ya existe en la bbdd
                if (usuario.existeUsername()) {
                    MessageBox.Show("Ya existe una cuenta registrada con ese nombre de usuario");
                    return; 
                
                }

                usuario.insert();

                var mainw = new MainWindow(usuario);
                App.Current.MainWindow = mainw;
                mainw.Show();
                this.Close();

            }
            else {
                return;
            }

        }

        public string EncriptarContraseña(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {

                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));


                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void SoloNumeros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !EsNumero(e.Text);
        }

        private bool EsNumero(string texto)
        {
            return texto.All(char.IsDigit);
        }

        private bool comprobarVacios()
        {
            if(nombrebox.Text == null || nombrebox.Text.Equals(""))
            {
                MessageBox.Show("El campo 'Nombre' es obligatorio.\nPor favor, introduzca un valor valido");
                return false;
            }
            if (apellidosbox.Text == null || apellidosbox.Text.Equals(""))
            {
                MessageBox.Show("El campo 'Apellido' es obligatorio.\nPor favor, introduzca un valor valido");
                return false;
            }
            if (birthdaybox.SelectedDate == null || birthdaybox.SelectedDate.Value.Equals(""))
            {
                MessageBox.Show("El campo 'Fecha de nacimiento' es obligatorio.\nPor favor, introduzca un valor valido");
                return false;
            }
            if (numhijosbox.Text == null || numhijosbox.Text.Equals(""))
            {
                MessageBox.Show("El campo 'Número de hijos' es obligatorio.\nPor favor, introduzca un valor valido");
                return false;
            }
            if (emailbox.Text == null || emailbox.Text.Equals(""))
            {
                MessageBox.Show("El campo 'Correo electrónico' es obligatorio.\nPor favor, introduzca un valor valido");
                return false;
            }
            if (usernamebox.Text == null || usernamebox.Text.Equals(""))
            {
                MessageBox.Show("El campo 'Nombre de usuario' es obligatorio.\nPor favor, introduzca un valor valid");
                return false;
            }
            if (string.IsNullOrWhiteSpace(passbox.Password))
            {
                MessageBox.Show("El campo 'Contraseña' es obligatorio.\nPor favor, introduzca un valor válido");
                return false;
            }
            if (passbox.Password.Length < 6 || passbox.Password.Length > 16)
            {
                MessageBox.Show("La contraseña debe tener entre 6 y 16 caracteres");
                return false;
            }

            if (!passbox.Password.Any(char.IsLetter) || !passbox.Password.Any(char.IsDigit))
            {
                MessageBox.Show("La contraseña debe incluir al menos una letra y un número.");
                return false;
            }
            if (repassbox.Password == null || repassbox.Password.Equals(""))
            {
                MessageBox.Show("Por favor, introduzca de nuevo la contraseña");
                return false;
            }

            return true;
        }

        private bool comprobarContrasena(string pass, string repass)
        {
            bool resultado = true;
            if (!pass.Equals(repass))
            {
                MessageBox.Show("Las contraseñas deben coincidir");
                resultado = false;
            }

            return resultado;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void RegisterText_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            App.Current.MainWindow = login;
            login.Show();
            this.Close();
        }
    }
}
