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
using WatcHive.View;

namespace WatcHive
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Usuario usuarioLoged;
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();
            usuarioLoged = usuario;
            tagNombreUser.Text = usuarioLoged.username;
            MainContent.Content = new PeliculasView(usuarioLoged);
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
            MainContent.Content = new PeliculasView(usuarioLoged);
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
        }

        private void btnMenuSeries_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new SeriesView(usuarioLoged);
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
        }

        private void btnMenuInicio_Click(object sender, RoutedEventArgs e)
        {
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

        private void btnMenuListas_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ListasUserView(usuarioLoged);
            UIElementCollection elementos = sidebar.Children;
            setBackgroundBtn(sender);
        }
    }
}
