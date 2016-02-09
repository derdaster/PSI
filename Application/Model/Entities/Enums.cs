using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public enum StopieńStudiówEnum
    {
        [Description("I stopień")]
        StopieńI = 1,
        [Description("II stopień")]
        StopieńII = 2
    }

    public enum FormaStudiówEnum
    {
        Stacjonarna = 1,
        Niestacjonarna = 2
    }

    public enum RodzajPrzedmiotuEnum
    {
        Obowiązkowy = 1,
        Wybieralny = 2,
        Ogólnouczelniany = 3
    }

    public enum GrupaKursowEnum
    {
        Tak = 1,
        Nie = 2
    }
}
