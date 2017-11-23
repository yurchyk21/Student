using DAL;
using Microsoft.Win32;
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
using System.IO;
using System.Drawing;

namespace WpfStudentApp
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        StudentService studService = new StudentService();

        public AddStudent()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                Img.Source = new BitmapImage(new Uri(dlg.FileName));
                AddBut.IsEnabled = true;

            }
        }

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (NameBox.Text.Length > 0)
                ImgButton.IsEnabled = true;
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            Student newStud = new Student
            {
                Name = NameBox.Text,
                Image = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Img.Source.ToString())
            };
            MessageBox.Show(Img.Source.ToString().Trim(@"file:///".ToCharArray()));
            Bitmap temp = new Bitmap(Img.Source.ToString().Trim(@"file:///".ToCharArray()));

            temp.SetResolution(900, 600);
            temp.Save("600-" + newStud.Image);
            temp.SetResolution(150, 150);
            temp.Save("150-" + newStud.Image);
            temp.SetResolution(32, 32);
            temp.Save("32-" + newStud.Image);
            studService.Add(newStud);
            studService.Save();

        }
    }
}
