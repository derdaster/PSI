using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public static class DbManager
    {
        public static bool LoginUser(string login, string password)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Użytkownik.Where(x => x.Login.Equals(login) && x.Hasło.Equals(password)).Any();
            }
        }

        public static List<Wydział> GetWydział()
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Wydział.ToList();
            }
        }

        public static Przedmiot GetPrzedmiot(string kodPrzedmiotu)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Przedmiot.Where(x => x.Kod.Equals(kodPrzedmiotu)).FirstOrDefault();
            }
        }

        public static Autor_karty_przedmiotu GetAutor(string imie, string nazwisko, string mail)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Autor_karty_przedmiotu.Where(x => x.Imię.Equals(imie) && x.Nazwisko.Equals(nazwisko) && x.Email.Equals(mail)).FirstOrDefault();
            }
        }

        public static List<ExSpecjalnosc> GetSpecjalnosci(int idKierunek)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Program_kształcenia.Where(x => x.KierunekID == idKierunek).ToList().Select(x => new ExSpecjalnosc(x)).ToList();
            }
        }

        public static ExSpecjalnosc GetProgramKsztalcenia(int idKierunek, int poziomKsztalcenia, int formaStudiow)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Program_kształcenia.Where(x => x.KierunekID == idKierunek && x.PoziomKształcenia == poziomKsztalcenia && x.FormaStudiów == formaStudiow).ToList().Select(x => new ExSpecjalnosc(x)).FirstOrDefault();
            }
        }

        public static List<Kierunek> GetKierunek(int idWydział)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Kierunek.Where(x => x.WydziałID == idWydział).ToList();
            }
        }


        public static List<ExSubjectCard> GetKartyPrzedmiotu()
        {
            using (var ctx = new DbEasyKRK())
            {
                var list = (from kp in ctx.Karta_przedmiotu
                            join pk in ctx.Program_kształcenia on kp.Program_KształceniaID equals pk.ID
                            join p in ctx.Przedmiot on kp.PrzedmiotID equals p.ID
                            join k in ctx.Kierunek on pk.KierunekID equals k.ID
                            join w in ctx.Wydział on k.WydziałID equals w.ID
                            select new ExSubjectCard()
                                {
                                    NazwaPolska = kp.NazwaPolska,
                                    NazwaAngielska = kp.NazwaAngielska,
                                    FormaStudiów = (FormaStudiówEnum)pk.FormaStudiów,
                                    RodzajPrzedmiotu = (RodzajPrzedmiotuEnum)kp.RodzajPrzedmiotu,
                                    Stopień = (StopieńStudiówEnum)pk.PoziomKształcenia,
                                    Kod = p.Kod,
                                    Kierunek = k.Nazwa,
                                    Wydział = w.Nazwa,
                                    Specjalność = pk.Specjalność,
                                    Id = kp.ID,
                                    GrupaKursów = kp.GrupaKursów ? GrupaKursowEnum.Tak : GrupaKursowEnum.Nie
                                }
                        ).ToList();

                return list;
            }
        }

        public static List<ExSubjectCard> GetKartyPrzedmiotuBy(Func<ExSubjectCard, bool> filter)
        {
            return GetKartyPrzedmiotu().Where(x => filter(x)).ToList();
        }

        public static Karta_przedmiotu AddKartaPrzedmiotu(Karta_przedmiotu karta)
        {
            using (var ctx = new DbEasyKRK())
            {
                Karta_przedmiotu dodanaKarta = ctx.Karta_przedmiotu.Add(karta);
                ctx.SaveChanges();
                return dodanaKarta;
            }
        }

        public static Narzędzia_dydaktyczne AddNarzedziaDydaktyczne(Narzędzia_dydaktyczne narzedzia)
        {
            using (var ctx = new DbEasyKRK())
            {
                Narzędzia_dydaktyczne dodane = ctx.Narzędzia_dydaktyczne.Add(narzedzia);
                ctx.SaveChanges();
                return dodane;
            }
        }

        public static void AddMultipleNarzedziaDydaktyczne(List<Narzędzia_dydaktyczne> narzedzia)
        {
            using (var ctx = new DbEasyKRK())
            {
                foreach (Narzędzia_dydaktyczne element in narzedzia)
                {
                    ctx.Narzędzia_dydaktyczne.Add(element);
                }

                ctx.SaveChanges();
            }
        }

        public static Wymaganie_wstępne AddWymaganieWstepne(Wymaganie_wstępne wymaganie)
        {
            using (var ctx = new DbEasyKRK())
            {
                Wymaganie_wstępne dodane = ctx.Wymaganie_wstępne.Add(wymaganie);
                ctx.SaveChanges();
                return dodane;
            }
        }

        public static void AddMultipleWymaganiaWstępne(List<Wymaganie_wstępne> wymagania)
        {
            using (var ctx = new DbEasyKRK())
            {
                foreach (Wymaganie_wstępne element in wymagania)
                {
                    ctx.Wymaganie_wstępne.Add(element);
                }

                ctx.SaveChanges();
            }
        }

        public static Cel_przedmiotu AddCelPrzedmiotu(Cel_przedmiotu cel)
        {
            using (var ctx = new DbEasyKRK())
            {
                Cel_przedmiotu dodane = ctx.Cel_przedmiotu.Add(cel);
                ctx.SaveChanges();
                return dodane;
            }
        }

        public static void AddMultipleCelPrzedmiotu(List<Cel_przedmiotu> cele)
        {
            using (var ctx = new DbEasyKRK())
            {
                foreach (Cel_przedmiotu element in cele)
                {
                    ctx.Cel_przedmiotu.Add(element);
                }

                ctx.SaveChanges();
            }
        }

        public static Literatura AddLiteratura(Literatura literatura)
        {
            using (var ctx = new DbEasyKRK())
            {
                Literatura dodane = ctx.Literatura.Add(literatura);
                ctx.SaveChanges();
                return dodane;
            }
        }

        public static void AddMultipleLiteratura(List<Literatura> literatura)
        {
            using (var ctx = new DbEasyKRK())
            {
                foreach (Literatura element in literatura)
                {
                    ctx.Literatura.Add(element);
                }

                ctx.SaveChanges();
            }
        }

        public static Treść_programowa AddTreśćProgramowa(Treść_programowa treść)
        {
            using (var ctx = new DbEasyKRK())
            {
                Treść_programowa dodane = ctx.Treść_programowa.Add(treść);
                ctx.SaveChanges();
                return dodane;
            }
        }

        public static void AddMultipleTematZajęć(List<Temat_zajęć> tematy)
        {
            using (var ctx = new DbEasyKRK())
            {
                foreach (Temat_zajęć element in tematy)
                {
                    ctx.Temat_zajęć.Add(element);
                }

                ctx.SaveChanges();
            }
        }

        public static Przedmiotowy_efekt_kształcenia AddPEK(Przedmiotowy_efekt_kształcenia pek)
        {
            using (var ctx = new DbEasyKRK())
            {
                Przedmiotowy_efekt_kształcenia dodane = ctx.Przedmiotowy_efekt_kształcenia.Add(pek);
                ctx.SaveChanges();
                return dodane;
            }
        }

        public static void AddMultiplePEK(List<Przedmiotowy_efekt_kształcenia> peki)
        {
            using (var ctx = new DbEasyKRK())
            {
                foreach (Przedmiotowy_efekt_kształcenia element in peki)
                {
                    ctx.Przedmiotowy_efekt_kształcenia.Add(element);
                }

                ctx.SaveChanges();
            }
        }

        public static void JoinAutorWithKarta(Karta_przedmiotu karta, Autor_karty_przedmiotu autor)
        {
            using (var ctx = new DbEasyKRK())
            {
                karta.Autor_karty_przedmiotu.Add(autor);
                ctx.Entry(karta).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

            }
        }

        public static List<Wymaganie_wstępne> GetWymagania(int kartaID)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Wymaganie_wstępne.Where(x => x.Karta_PrzedmiotuID == kartaID).ToList();
            }
        }

        public static List<Cel_przedmiotu> GetCele(int kartaID)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Cel_przedmiotu.Where(x => x.Karta_PrzedmiotuID == kartaID).ToList();
            }
        }

        public static List<Narzędzia_dydaktyczne> GetNarzędzia(int kartaID)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Narzędzia_dydaktyczne.Where(x => x.Karta_PrzedmiotuID == kartaID).ToList();
            }
        }

        public static List<Literatura> GetLiteratura(int kartaID, int typ)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Literatura.Where(x => x.Karta_PrzedmiotuID == kartaID && x.Typ == typ).ToList();
            }
        }

        public static List<Treść_programowa> GetTreściProgramowe(int kartaID)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Treść_programowa.Where(x => x.Karta_PrzedmiotuID == kartaID).ToList();
            }
        }

        public static List<Temat_zajęć> GetTematyZajęć(int trescID)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Temat_zajęć.Where(x => x.Treść_ProgramowaID == trescID).ToList();
            }
        }

        public static List<Przedmiotowy_efekt_kształcenia> GetPEK(int kartaID, int zakres)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Przedmiotowy_efekt_kształcenia.Where(x => x.Karta_PrzedmiotuID == kartaID && x.Zakres == zakres).ToList();
            }
        }
    }
}
