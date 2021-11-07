using RegistroLogin.BLL;
using RegistroLogin.Entidades;
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

namespace RegistroLogin.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cUsuarios.xaml
    /// </summary>
    public partial class cUsuarios : Window
    {
        public cUsuarios()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Usuario>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)//LISTO a
                {
                    case 0: //UsuarioId
                        int.TryParse(CriterioTextBox.Text, out int UsuarioId);
                        listado = UsuariosBLL.GetList(a => a.UsuarioId==UsuarioId);
                        break;
                         
                    case 1: //Nombre                       
                        listado = UsuariosBLL.GetList(a => a.Nombres.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;
                }
                
            }
            else
            {
                listado = UsuariosBLL.GetList(c => true);
            }

            if (DesdeDataPicker.SelectedDate != null)
                listado = listado.Where(c => c.FechaIngreso.Date >= DesdeDataPicker.SelectedDate).ToList();

            if (HastaDatePicker.SelectedDate != null)
                listado = listado.Where(c => c.FechaIngreso.Date <= HastaDatePicker.SelectedDate).ToList();

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
