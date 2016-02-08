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
        public StopieńStudiówEnum Stopień { get; set; }
        public FormaStudiówEnum FormaStudiów { get; set; }
        public RodzajPrzedmiotuEnum RodzajPrzedmiotu { get; set; }
        public string GrupaKursów { get; set; }

        public ExSubjectCard()
        {
        }

        public ExSubjectCard(Karta_przedmiotu kp)
        {
            this.NazwaPolska = kp.NazwaPolska;
            this.NazwaAngielska = kp.NazwaAngielska;
        }
    }
}
