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
    /// Lógica de interacción para ConfiguracionWindow.xaml
    /// </summary>
    public partial class ConfiguracionWindow : Window
    {
        private Usuario usuarioLoged;
        public ConfiguracionWindow(Domain.Usuario usuarioLoged)
        {
            InitializeComponent();
            this.usuarioLoged = usuarioLoged;
        }

        private void BtnConfirmarContrasena_Click(object sender, RoutedEventArgs e)
        {
            string contrasenaIngresada = pwdActual.Password;

            if (EncriptarContraseña( pwdActual.Password).Equals(usuarioLoged.password))
            {
                formularioUsuario.IsEnabled = true;
                pwdActual.IsEnabled = false;
                ((Button)sender).IsEnabled = false;
                btnGuardar.IsEnabled = true;
                MessageBox.Show("Contraseña correcta. Introduce tu nueva contraseña", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (NewPassword.Password.Equals(newPasswordRep.Password))
            {
                if (string.IsNullOrWhiteSpace(NewPassword.Password))
                {
                    MessageBox.Show("El campo 'Contraseña' es obligatorio.\nPor favor, introduzca un valor válido");
                    return;
                }
                if (NewPassword.Password.Length < 6 || NewPassword.Password.Length > 16)
                {
                    MessageBox.Show("La contraseña debe tener entre 6 y 16 caracteres");
                    return;
                }

                if (!NewPassword.Password.Any(char.IsLetter) || !NewPassword.Password.Any(char.IsDigit))
                {
                    MessageBox.Show("La contraseña debe incluir al menos una letra y un número.");
                    return;
                }
                usuarioLoged.password = EncriptarContraseña( NewPassword.Password);
                usuarioLoged.updatePass();
                MessageBox.Show("Contraseña actualizada correctamente.");
                this.Close();
            }
            else { 
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
