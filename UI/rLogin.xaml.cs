using RegistroLogin.BLL;
using RegistroLogin.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

namespace RegistroLogin.UI
{
    /// <summary>
    /// Interaction logic for rLogin.xaml
    /// </summary>
    public partial class rLogin : Window
    {
        public rLogin()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void IniciarSesionButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsuariosBLL.Login(NombreTextBox.Text, UsuariosBLL.SHA1(ContraseñaTextBox.Password)))
            {
                this.Hide();//ocultamos la ventana del login
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
                MessageBox.Show("Usuario o Contraseña Incorrecto!!", "Login");
        }
        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}

