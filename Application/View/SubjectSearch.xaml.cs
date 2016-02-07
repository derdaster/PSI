using ModelView.Business;
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

namespace View
{
    /// <summary>
    /// Interaction logic for SubjectSearch.xaml
    /// </summary>
    public partial class SubjectSearch : Window
    {
        public SubjectSearch()
        {
            InitializeComponent();
            var viewModel = new SubjectSearchModel();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var win = new SubjectEditorWizard();
            win.ShowDialog();
        }
    }
}
