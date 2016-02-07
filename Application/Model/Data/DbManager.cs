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
                return ctx.Karta_przedmiotu.ToList().Select(x => new ExSubjectCard(x)).ToList();
            }
        }

        public static void AddKartaPrzedmiotu(Karta_przedmiotu karta)
        {
            Karta_przedmiotu y;
            using (var ctx = new DbEasyKRK())
            {
                y=ctx.Karta_przedmiotu.Add(karta);
                ctx.SaveChanges();
            }
        }
    }
}
