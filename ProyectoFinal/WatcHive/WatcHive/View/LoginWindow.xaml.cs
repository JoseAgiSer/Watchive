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
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            rellenarBaseDatos();
        }

        private void rellenarBaseDatos()
        {
            Genero g = new Genero();
            Usuario u = new Usuario();
            Emocion e = new Emocion();
            g.readGenero();

            if (g.getListGenero().Count == 0) {
                g.insertGenerosInicio();
                u.crearAdmin();
                e.insertEmocionInicio();
            }

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
            Usuario user = new Usuario();

            Usuario buscado = null;
            user.readUsuario();
            foreach (Usuario u in user.getListUsuarios()) {
                if (u.username.Equals(userbox.Text)) {
                    buscado = u;
                }
            }

            if (buscado != null && buscado.password.Equals(passbox.Password))
            {
                var mainw = new MainWindow(buscado);
                App.Current.MainWindow = mainw;
                mainw.Show();
                this.Close();
            }
            else {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void RegisterText_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var registro = new RegistroWindow();
            App.Current.MainWindow = registro;
            registro.Show();
            this.Close();
        }
    }
}
