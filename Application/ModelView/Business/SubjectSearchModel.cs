using Model.Data;
using Model.Entities;
using ModelView.Base;
using ModelView.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
        private StopieńStudiówEnum _StopieńStudiów;
        private FormaStudiówEnum _FormaStudiów;
        private RodzajPrzedmiotuEnum _RodzajPrzedmiotu;
        private bool _GrupaKursów;

        private ObservableCollection<ExSubjectCard> _DataList;


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


        public bool GrupaKursów
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


        #endregion

        public SubjectSearchModel()
        {
            FilterCmd = new RelayCommand(par => Filter());
            Init();
        }

        private void Init()
        {
            _WydziałList = new CollectionView(DbManager.GetWydział());
            DataList = new ObservableCollection<ExSubjectCard>(DbManager.GetKartyPrzedmiotu());
        }

        private void Filter()
        {
            DataList = new ObservableCollection<ExSubjectCard>(DbManager.GetKartyPrzedmiotuBy(FilterData));
            writeToXml();
        }

        private bool FilterData(ExSubjectCard item)
        {
            if (!String.IsNullOrWhiteSpace(NazwaPrzedmiotuPl) && !item.NazwaPolska.ToLower().Contains(NazwaPrzedmiotuPl.ToLower()))
            {
                return false;
            }

            if (!String.IsNullOrWhiteSpace(NazwaPrzedmiotuAng) && !item.NazwaAngielska.ToLower().Contains(NazwaPrzedmiotuAng.ToLower()))
            {
                return false;
            }
            
            return true;
        }

        protected virtual void RaiseLoginCompleted()
        {
            if (SaveCompleted != null)
            {
                SaveCompleted(this, EventArgs.Empty);
            }
        }

        private void writeToXml()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\master\\Documents\\studia\\Materialy_Projekt_Hnatkowska\\test.xml");
            ExSubjectCard kartaPrzedmiotu=DbManager.GetKartyPrzedmiotu().First();
            XElement kartaXML = new XElement("Karta");
            kartaXML.Add(new XElement("Nazwa_polska",kartaPrzedmiotu.NazwaPolska));
            kartaXML.Add(new XElement("Nazwa_angielska", kartaPrzedmiotu.NazwaAngielska));
            kartaXML.Add(new XElement("Rodzaj_przedmiotu", kartaPrzedmiotu.RodzajPrzedmiotu));
            kartaXML.Add(new XElement("Grupa_kursow", kartaPrzedmiotu.GrupaKursów));
            kartaXML.Add(new XElement("Forma_studiow", kartaPrzedmiotu.FormaStudiów));
            kartaXML.Add(new XElement("Stopien_studiow", kartaPrzedmiotu.Stopień));
            kartaXML.Add(new XElement("Kod_przedmiotu", kartaPrzedmiotu.Kod));
            kartaXML.Add(new XElement("Kierunek", kartaPrzedmiotu.Kierunek));
            kartaXML.Add(new XElement("Specjalnosc", kartaPrzedmiotu.Specjalność));

            List<Wymaganie_wstępne> wymagania = DbManager.getWymagania(1);
            List<Cel_przedmiotu> cele = DbManager.getCele(1);
            List<Narzędzia_dydaktyczne> narzędzie = DbManager.getNarzędzia(1);
            XElement tree = new XElement("Wymagania_wstepne");
            int i = 0;
            foreach (var element in wymagania)
            {
                i++;
                tree.Add(new XElement("Wymaganie_" + i.ToString(), element.Nazwa));

            }
            kartaXML.Add(tree);

            tree = new XElement("Cele_przedmiotu");
            i = 0;
            foreach (Cel_przedmiotu element in cele)
            {
                i++;
                tree.Add(new XElement("Cel_" + i.ToString(), element.Nazwa));

            }
            kartaXML.Add(tree);

            file.WriteLine(kartaXML);
            file.Close();
        
        }
    }
}
