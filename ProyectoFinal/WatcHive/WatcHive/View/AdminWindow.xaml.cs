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

namespace WatcHive.View
{
    /// <summary>
    /// Lógica de interacción para AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            MainContentAdmin.Content = new GestionUsuariosView();
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            MainContentAdmin.Content = new GestionUsuariosView();
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
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

        private void btnContenidos_Click(object sender, RoutedEventArgs e)
        {
            MainContentAdmin.Content = new GestionContenidosView();
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
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
    }
}
