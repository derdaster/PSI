using Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class ExSubjectCard
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string NazwaPolska { get; set; }
        public string NazwaAngielska { get; set; }
        public string Wydział { get; set; }
        public string Kierunek { get; set; }
        public string Specjalność { get; set; }
        public StopieńStudiówEnum Stopień { get; set; }
        public FormaStudiówEnum FormaStudiów { get; set; }
        public RodzajPrzedmiotuEnum RodzajPrzedmiotu { get; set; }
        public GrupaKursowEnum GrupaKursów { get; set; }
    }


    public class Wymaganie
    {
        public int ID { get; set; }
        public string Lp { get { return ID.ToString(); } }
        public string Nazwa { get; set; }
    }
}
