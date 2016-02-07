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
    }
}
