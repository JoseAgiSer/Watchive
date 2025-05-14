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

            if (pwdActual.Password.Equals(usuarioLoged.password))
            {
                formularioUsuario.IsEnabled = true;
                pwdActual.IsEnabled = false;
                ((Button)sender).IsEnabled = false;
                btnGuardar.IsEnabled = true;
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
                usuarioLoged.password = NewPassword.Password;
                usuarioLoged.update();
                MessageBox.Show("Contraseña actualizada correctamente.");
                this.Close();
            }
            else { 
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
