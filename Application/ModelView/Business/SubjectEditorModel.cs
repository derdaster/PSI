using Model.Data;
using ModelView.Base;
using ModelView.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ModelView.Business
{
    public class SubjectEditorModel : BaseViewModel
    {
        public event EventHandler SaveCompleted;
        public ICommand SaveCmd { get; set; }

        #region Script

        /*
         
        SELECT '
		public string ' + Nazwa + '
        {
            get { return _'+ Nazwa+'; }
            set
            {
                if (value == _'+Nazwa+') return;
                _'+Nazwa+' = value;
                OnPropertyChanged("'+NAZWA+'");
            }
        }
		'
        FROM (VALUES
        ('NazwaPrzedmiotuPl'),
        ('NazwaPrzedmiotuAng'),
        ('Wydział'),
        ('KierunekStudiów'),
        ('Specjalność'),
        ('KodPrzedmiotu'),
        ('Imię'),
        ('Nazwisko'),
        ('AdresEmail')
        ) T(Nazwa)
          
         */

        #endregion

        #region Fields

        private string _NazwaPrzedmiotuPl;
        private string _NazwaPrzedmiotuAng;
        private string _Specjalność;
        private string _KodPrzedmiotu;
        private string _Imię;
        private string _Nazwisko;
        private string _AdresEmail;
        private CollectionView _WydziałList;
        private int _WydziałSelected;
        private CollectionView _KierunekList;
        private int _KierunekSelected;


        public string NazwaPrzedmiotuPl
        {
            get { return _NazwaPrzedmiotuPl; }
            set
            {
                if (value == _NazwaPrzedmiotuPl) return;
                _NazwaPrzedmiotuPl = value;
                OnPropertyChanged("NazwaPrzedmiotuPl");
            }
        }


        public string NazwaPrzedmiotuAng
        {
            get { return _NazwaPrzedmiotuAng; }
            set
            {
                if (value == _NazwaPrzedmiotuAng) return;
                _NazwaPrzedmiotuAng = value;
                OnPropertyChanged("NazwaPrzedmiotuAng");
            }
        }


        public CollectionView WydziałList
        {
            get { return _WydziałList; }
        }

        public int WydziałSelected
        {
            get { return _WydziałSelected; }
            set
            {
                if (value == _WydziałSelected) return;
                _WydziałSelected = value;
                OnPropertyChanged("WydziałSelected");
                OnWydziałChanged();
            }
        }

        private void OnWydziałChanged()
        {
            _KierunekList = new CollectionView(DbManager.GetKierunek(WydziałSelected));
            OnPropertyChanged("KierunekList");
        }

        public CollectionView KierunekList
        {
            get { return _KierunekList; }
        }

        public int KierunekSelected
        {
            get { return _KierunekSelected; }
            set
            {
                if (value == _KierunekSelected) return;
                _KierunekSelected = value;
                OnPropertyChanged("KierunekSelected");
            }
        }
        

        public string Specjalność
        {
            get { return _Specjalność; }
            set
            {
                if (value == _Specjalność) return;
                _Specjalność = value;
                OnPropertyChanged("Specjalność");
            }
        }


        public string KodPrzedmiotu
        {
            get { return _KodPrzedmiotu; }
            set
            {
                if (value == _KodPrzedmiotu) return;
                _KodPrzedmiotu = value;
                OnPropertyChanged("KodPrzedmiotu");
            }
        }


        public string Imię
        {
            get { return _Imię; }
            set
            {
                if (value == _Imię) return;
                _Imię = value;
                OnPropertyChanged("Imię");
            }
        }


        public string Nazwisko
        {
            get { return _Nazwisko; }
            set
            {
                if (value == _Nazwisko) return;
                _Nazwisko = value;
                OnPropertyChanged("Nazwisko");
            }
        }


        public string AdresEmail
        {
            get { return _AdresEmail; }
            set
            {
                if (value == _AdresEmail) return;
                _AdresEmail = value;
                OnPropertyChanged("AdresEmail");
            }
        }



        #endregion

        public SubjectEditorModel()
        {
            SaveCmd = new RelayCommand(par => Save());
            Init();
        }

        private void Init()
        {
            _WydziałList = new CollectionView(DbManager.GetWydział());
        }

        public void Save()
        {
            MessageBox.Show("Dodano kartę przedmiotu");
        }

        protected virtual void RaiseLoginCompleted()
        {
            if (SaveCompleted != null)
            {
                SaveCompleted(this, EventArgs.Empty);
            }
        }
    }
}
