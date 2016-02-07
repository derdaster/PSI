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
    /// Interaction logic for SubjectEditorWizard.xaml
    /// </summary>
    public partial class SubjectEditorWizard : Window
    {
        SubjectEditorModel ViewModel;
        
        public SubjectEditorWizard()
        {
            InitializeComponent();
            SubjectCardWizard.Finish += SubjectCardWizard_Finish;
            ViewModel = new SubjectEditorModel();
            DataContext = ViewModel;
        }

        void SubjectCardWizard_Finish(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
        }
    }
}
