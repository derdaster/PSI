using Model.Data;
using Model.Entities;
using ModelView.Base;
using ModelView.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;

namespace ModelView.Business
{
    public class SubjectSearchModel : BaseViewModel
    {
        public event EventHandler SaveCompleted;
        public ICommand FilterCmd { get; set; }
        public ICommand ClearCmd { get; set; }
        public ICommand GenerateXmlCmd { get; set; }

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
        private Wydział _WydziałSelected;
        private CollectionView _KierunekList;
        private Kierunek _KierunekSelected;
        private StopieńStudiówEnum _StopieńStudiów;
        private FormaStudiówEnum _FormaStudiów;
        private RodzajPrzedmiotuEnum _RodzajPrzedmiotu;
        private GrupaKursowEnum _GrupaKursów;

        private ObservableCollection<ExSubjectCard> _DataList;
        private ExSubjectCard _DataSelected;


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

        public Wydział WydziałSelected
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
            if (WydziałSelected != null)
            {
                _KierunekList = new CollectionView(DbManager.GetKierunek(WydziałSelected.ID));
                OnPropertyChanged("KierunekList");
            }
        }

        public CollectionView KierunekList
        {
            get { return _KierunekList; }
        }

        public Kierunek KierunekSelected
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

        public StopieńStudiówEnum StopieńStudiów
        {
            get { return _StopieńStudiów; }
            set
            {
                if (value == _StopieńStudiów) return;
                _StopieńStudiów = value;
                OnPropertyChanged("StopieńStudiów");
            }
        }


        public FormaStudiówEnum FormaStudiów
        {
            get { return _FormaStudiów; }
            set
            {
                if (value == _FormaStudiów) return;
                _FormaStudiów = value;
                OnPropertyChanged("FormaStudiów");
            }
        }


        public RodzajPrzedmiotuEnum RodzajPrzedmiotu
        {
            get { return _RodzajPrzedmiotu; }
            set
            {
                if (value == _RodzajPrzedmiotu) return;
                _RodzajPrzedmiotu = value;
                OnPropertyChanged("RodzajPrzedmiotu");
            }
        }


        public GrupaKursowEnum GrupaKursów
        {
            get { return _GrupaKursów; }
            set
            {
                if (value == _GrupaKursów) return;
                _GrupaKursów = value;
                OnPropertyChanged("GrupaKursów");
            }
        }


        public ObservableCollection<ExSubjectCard> DataList
        {
            get { return _DataList; }
            set
            {
                if (value == _DataList) return;
                _DataList = value;
                OnPropertyChanged("DataList");
            }
        }

        public ExSubjectCard DataSelected
        {
            get { return _DataSelected; }
            set
            {
                if (value == _DataSelected) return;
                _DataSelected = value;
                OnPropertyChanged("DataSelected");
            }
        }


        #endregion

        public SubjectSearchModel()
        {
            FilterCmd = new RelayCommand(par => Filter());
            ClearCmd = new RelayCommand(par => Clear());
            GenerateXmlCmd = new RelayCommand(par => GenerateXmlFile());
            Init();
        }

        private void Clear()
        {
            NazwaPrzedmiotuPl = String.Empty;
            NazwaPrzedmiotuAng = String.Empty;
            Specjalność = String.Empty;
            KodPrzedmiotu = String.Empty;
            
            WydziałSelected = null;
            _WydziałList = new CollectionView(new List<Wydział>());
            OnPropertyChanged("WydziałList");
            _WydziałList = new CollectionView(DbManager.GetWydział());
            _KierunekList = new CollectionView(new List<Kierunek>());
            OnPropertyChanged("KierunekList");
            KierunekSelected = null;
            StopieńStudiów = 0;
            FormaStudiów = 0;
            RodzajPrzedmiotu = 0;
            GrupaKursów = 0;
        }

        private void GenerateXmlFile()
        {
            if (DataSelected == null)
            {
                MessageBox.Show("Musisz zaznaczyć kartę przedmiotu, którą chcesz zapisać");
                return;
            }

            var path = XmlHelper.WriteToXml(DataSelected);

            MessageBox.Show("Karta przedmiotu została zapisana w lokalizacji: " + path);
        }

        private void Init()
        {
            _WydziałList = new CollectionView(DbManager.GetWydział());
            DataList = new ObservableCollection<ExSubjectCard>(DbManager.GetKartyPrzedmiotu());
        }

        private void Filter()
        {
            DataList = new ObservableCollection<ExSubjectCard>(DbManager.GetKartyPrzedmiotuBy(FilterData));
        }

        private bool FilterData(ExSubjectCard item)
        {
            if (!String.IsNullOrWhiteSpace(NazwaPrzedmiotuPl) && !item.NazwaPolska.ToLower().Contains(NazwaPrzedmiotuPl.ToLower()))
            { return false; }

            if (!String.IsNullOrWhiteSpace(NazwaPrzedmiotuAng) && !item.NazwaAngielska.ToLower().Contains(NazwaPrzedmiotuAng.ToLower()))
            { return false; }

            if (WydziałSelected != null && !item.Wydział.Equals(WydziałSelected.Nazwa))
            { return false; }

            if (KierunekSelected != null && !item.Kierunek.Equals(KierunekSelected.Nazwa))
            { return false; }

            if (!String.IsNullOrWhiteSpace(Specjalność) && !item.Specjalność.ToLower().Contains(Specjalność.ToLower()))
            { return false; }

            if (StopieńStudiów != 0 && item.Stopień != StopieńStudiów)
            { return false; }

            if (FormaStudiów != 0 && item.FormaStudiów != FormaStudiów)
            { return false; }

            if (RodzajPrzedmiotu != 0 && item.RodzajPrzedmiotu != RodzajPrzedmiotu)
            { return false; }

            if (!String.IsNullOrWhiteSpace(KodPrzedmiotu) && !item.Kod.ToLower().Contains(KodPrzedmiotu.ToLower()))
            { return false; }

            if (GrupaKursów != 0 && item.GrupaKursów != GrupaKursów)
            { return false; }

            return true;
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
