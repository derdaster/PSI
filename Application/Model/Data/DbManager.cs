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

        public static Przedmiot getPrzedmiot(string kodPrzedmiotu)
        {
            using (var ctx = new DbEasyKRK())
            {
                try
                {
                    return ctx.Przedmiot.Where(x => x.Kod.Equals(kodPrzedmiotu)).ToList().First();
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }

        public static List<ExSpecjalnosc> getSpecjalnosci(int idKierunek)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Program_kształcenia.Where(x => x.KierunekID == idKierunek).ToList().Select(x => new ExSpecjalnosc(x)).ToList();
            }
        }

        public static ExSpecjalnosc getProgramKsztalcenia(int idKierunek, int poziomKsztalcenia, int formaStudiow)
        {
            using (var ctx = new DbEasyKRK())
            {
                try
                {
                    return ctx.Program_kształcenia.Where(x => x.KierunekID == idKierunek && x.PoziomKształcenia == poziomKsztalcenia && x.FormaStudiów == formaStudiow).ToList().Select(x => new ExSpecjalnosc(x)).ToList().First();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static List<Kierunek> GetKierunek(int idWydział)
        {
            using (var ctx = new DbEasyKRK())
            {
                return ctx.Kierunek.Where(x => x.WydziałID == idWydział).ToList();
            }
        }


        public static List<ExSubjectCard> GetKartyPrzedmiotu() // dodać filtr tylko na użytkownika
        {
            using (var ctx = new DbEasyKRK())
            {
                return (from kp in ctx.Karta_przedmiotu
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
                                Specjalność = pk.Specjalność
                            }
                        ).ToList();
            }
        }

        public static List<ExSubjectCard> GetKartyPrzedmiotuBy(Func<ExSubjectCard, bool> filter) // dodać filtr tylko na użytkownika
        {
            return GetKartyPrzedmiotu().Where(x => filter(x)).ToList();
        }

        public static void AddKartaPrzedmiotu(Karta_przedmiotu karta)
        {
            using (var ctx = new DbEasyKRK())
            {
                ctx.Karta_przedmiotu.Add(karta);
                ctx.SaveChanges();
            }
        }
    }
}
