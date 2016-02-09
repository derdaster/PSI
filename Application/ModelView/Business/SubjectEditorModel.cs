using Model.Data;
using Model.Entities;
using ModelView.Base;
using ModelView.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICommand AddRequirementCmd { get; set; }
        public ICommand RemoveRequirementCmd { get; set; }

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
        private CollectionView _SpecjalnośćList;
        private int _SpecjalnośćSelected;
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
                OnKierunekChanged();
            }
        }

        public CollectionView SpecjalnośćList
        {
            get { return _SpecjalnośćList; }
        }

        public int SpecjalnośćSelected
        {
            get { return _SpecjalnośćSelected; }
            set
            {
                if (value == _SpecjalnośćSelected) return;
                _SpecjalnośćSelected = value;
                OnPropertyChanged("SpecjalnośćSelected");

            }
        }

        private void OnKierunekChanged()
        {
            _SpecjalnośćList = new CollectionView(DbManager.GetSpecjalnosci(KierunekSelected));
            OnPropertyChanged("SpecjalnośćList");
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

        private ObservableCollection<Wymaganie> _WymaganiaList;

        public ObservableCollection<Wymaganie> WymaganiaList
        {
            get { return _WymaganiaList; }
            set
            {
                if (value == _WymaganiaList) return;
                _WymaganiaList = value;
                OnPropertyChanged("WymaganiaList");
            }
        }

        private Wymaganie _WymaganiaSelected;

        public Wymaganie WymaganieSelected
        {
            get { return _WymaganiaSelected; }
            set
            {
                if (value == _WymaganiaSelected) return;
                _WymaganiaSelected = value;
                OnPropertyChanged("WymaganiaSelected");
            }
        }


        private string _NoweWymaganie;

        public string NoweWymaganie
        {
            get { return _NoweWymaganie; }
            set
            {
                if (value == _NoweWymaganie) return;
                _NoweWymaganie = value;
                OnPropertyChanged("NoweWymaganie");
            }
        }


        #endregion

        public SubjectEditorModel()
        {
            SaveCmd = new RelayCommand(par => Save());
            AddRequirementCmd = new RelayCommand(par => AddRequirement());
            RemoveRequirementCmd = new RelayCommand(par => RemoveRequirement());

            Init();
        }

        private void Init()
        {
            _WydziałList = new CollectionView(DbManager.GetWydział());
            _WymaganiaList = new ObservableCollection<Wymaganie>();
        }

        public void Save()
        {
            ExSpecjalnosc program = DbManager.GetProgramKsztalcenia((int)KierunekSelected, (int)StopieńStudiów, (int)FormaStudiów);
            int programId;
            if (program == null)
            {
                programId = 0;
            }
            else
            {
                programId = program.Id;
            }
            var przedmiot = DbManager.GetPrzedmiot(_KodPrzedmiotu);
            var autor = DbManager.GetAutor(Imię, Nazwisko, AdresEmail);

            if (programId == 0)
            {
                MessageBox.Show("Nie znaleziono programu kształcenia");
                return;
            }

            if (przedmiot == null)
            {
                MessageBox.Show("Nie znaleziono przedmiotu");
                return;
            }

            var karta = new Karta_przedmiotu()
            {
                NazwaPolska = NazwaPrzedmiotuPl,
                NazwaAngielska = NazwaPrzedmiotuAng,
                GrupaKursów = true,
                RodzajPrzedmiotu = (int)RodzajPrzedmiotu,
                Program_KształceniaID = programId,
                PrzedmiotID = przedmiot.ID
            };

            karta = DbManager.AddKartaPrzedmiotu(karta);
            DbManager.JoinAutorWithKarta(karta, autor);
            List<Wymaganie_wstępne> wymagania=new List<Wymaganie_wstępne>();
            foreach (var element in WymaganiaList)
            {
                Wymaganie_wstępne nowe=new Wymaganie_wstępne();
                nowe.Karta_PrzedmiotuID=karta.ID;
                nowe.Nazwa=element.Nazwa.ToString();
                wymagania.Add(nowe);
            }
            DbManager.AddMultipleWymaganiaWstępne(wymagania);

            MessageBox.Show("Dodano kartę przedmiotu");
        }

        private void AddRequirement()
        {
            if (String.IsNullOrWhiteSpace(NoweWymaganie))
            {
                MessageBox.Show("Musisz wprowadzić tekst nowego wymagania");
                return;
            }

            var wymagania = WymaganiaList;

            if (wymagania == null)
            {
                wymagania = new ObservableCollection<Wymaganie>();
            }

            var nextId = wymagania.Count > 0 ? wymagania.Max(x => x.ID) + 1 : 1;

            wymagania.Add(new Wymaganie()
            {
                ID = nextId,
                Nazwa = NoweWymaganie
            });

            //WymaganiaList = null;
            //WymaganiaList = wymagania;

            NoweWymaganie = String.Empty;
        }

        private void RemoveRequirement()
        {
            if (WymaganiaList != null && WymaganiaList.Count != 0)
            {
                WymaganiaList.Remove(WymaganieSelected);

                int i = 1;

                foreach (var wym in WymaganiaList)
                {
                    wym.ID = i++;
                }

                var wymagania = WymaganiaList;
                WymaganiaList = null;
                WymaganiaList = wymagania;
            }
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
