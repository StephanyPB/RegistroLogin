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

namespace RegistroLogin.UI.Registros
{
    /// <summary>
    /// Interaction logic for rUsuarios.xaml
    /// </summary>
    public partial class rUsuarios : Window
    {
        private Usuario Usuario = new Usuario();
        public rUsuarios()
        {
            InitializeComponent();
            Usuario = new Usuario();
            this.DataContext = this.Usuario;
        }
        private bool Validar()
        {
            bool esValido = true;

            if (NombresTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transacción Fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (ConfirmarClaveTextBox.Password != null && !ClaveTextBox.Password.Equals(ConfirmarClaveTextBox.Password))
            {
                esValido = false;
                MessageBox.Show("Las contraseñas deben ser iguales!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;
        }
        private void Limpiar()
        {
            ClaveTextBox = null;
            ConfirmarClaveTextBox = null;
            DataContext = new Usuario();
        }

        private void BuscarId_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(UsuarioIDTextBox.Text, out int RolId);
            var Usuario = UsuariosBLL.Buscar(RolId);

            if (Usuario != null)
                this.Usuario = Usuario;
            else
                this.Usuario = new Usuario();

            this.DataContext = this.Usuario;
        }

        private void NButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            this.Usuario.Clave = UsuariosBLL.SHA1(ClaveTextBox.Password); 

            var paso = UsuariosBLL.Guardar(this.Usuario);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transaccion Exitosa!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transaccion Fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Usuario existe = UsuariosBLL.Buscar(this.Usuario.UsuarioId);

            if (UsuariosBLL.Eliminar(this.Usuario.UsuarioId))
            {
                Limpiar();
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("No fue posible eliminarlo", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
