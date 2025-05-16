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
    /// Lógica de interacción para AgregarVistoWindow.xaml
    /// </summary>
    public partial class AgregarVistoWindow : Window
    {
        public DateTime FechaSeleccionada { get; private set; }
        public string EemocionSeleccionada { get; private set; }
        public int PuntuacionSeleccionada { get; private set; } = 3;

        public AgregarVistoWindow()
        {
            InitializeComponent();
            fechaVista.SelectedDate = DateTime.Now;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (fechaVista.SelectedDate == null || comboEmocion.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una fecha y un estado de ánimo.");
                return;
            }

            FechaSeleccionada = fechaVista.SelectedDate.Value;
            EemocionSeleccionada = (comboEmocion.SelectedItem as ComboBoxItem)?.Content.ToString();

            this.DialogResult = true;
            this.Close();
        }

        private void Puntuacion_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && int.TryParse(rb.Content.ToString(), out int valor))
            {
                PuntuacionSeleccionada = valor;
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
