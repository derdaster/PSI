using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Data;

namespace Model.Entities
{
    public class ExSpecjalnosc
    {
        public int Id { get; set; }
        public string Specjalnosc { get; set; }
        public ExSpecjalnosc(Program_kształcenia program)
        {
            this.Id = program.ID;
            this.Specjalnosc = program.Specjalność;
        }

        public ExSpecjalnosc(int p)
        {
            this.Id = p;
        }

 
    }
}
