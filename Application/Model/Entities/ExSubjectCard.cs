using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class ExSubjectCard
    {
        public string Kod { get; set; }
        public string NazwaPolska { get; set; }
        public string NazwaAngielska { get; set; }
        public string Wydział { get; set; }
        public string Kierunek { get; set; }
        public string Specjalność { get; set; }
        public string Stopień { get; set; }
        public string FormaStudiów { get; set; }

        public ExSubjectCard(Karta_przedmiotu item)
        {
            this.NazwaPolska = item.NazwaPolska;
            this.NazwaAngielska = item.NazwaAngielska;
        }
    }
}
