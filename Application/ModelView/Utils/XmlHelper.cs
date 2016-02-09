using Model.Data;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelView.Utils
{
    public static class XmlHelper
    {
        public static string WriteToXml(ExSubjectCard kartaPrzedmiotu)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + kartaPrzedmiotu.Kod + ".xml";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            XElement kartaXML = new XElement("Karta");
            kartaXML.Add(new XElement("Nazwa_polska", kartaPrzedmiotu.NazwaPolska));
            kartaXML.Add(new XElement("Nazwa_angielska", kartaPrzedmiotu.NazwaAngielska));
            kartaXML.Add(new XElement("Rodzaj_przedmiotu", kartaPrzedmiotu.RodzajPrzedmiotu));
            kartaXML.Add(new XElement("Grupa_kursow", kartaPrzedmiotu.GrupaKursów));
            kartaXML.Add(new XElement("Forma_studiow", kartaPrzedmiotu.FormaStudiów));
            kartaXML.Add(new XElement("Stopien_studiow", kartaPrzedmiotu.Stopień));
            kartaXML.Add(new XElement("Kod_przedmiotu", kartaPrzedmiotu.Kod));
            kartaXML.Add(new XElement("Kierunek", kartaPrzedmiotu.Kierunek));
            kartaXML.Add(new XElement("Specjalnosc", kartaPrzedmiotu.Specjalność));

            List<Wymaganie_wstępne> wymagania = DbManager.getWymagania(kartaPrzedmiotu.Id);
            List<Cel_przedmiotu> cele = DbManager.getCele(1);
            List<Narzędzia_dydaktyczne> narzędzia = DbManager.getNarzędzia(1);

            XElement tree = new XElement("Wymagania_wstepne");
            int i = 0;
            foreach (var element in wymagania)
            {
                i++;
                tree.Add(new XElement("Wymaganie_" + i.ToString(), element.Nazwa));
            }
            kartaXML.Add(tree);

            tree = new XElement("Cele_przedmiotu");
            i = 0;
            foreach (var element in cele)
            {
                i++;
                tree.Add(new XElement("Cel_" + i.ToString(), element.Nazwa));
            }
            kartaXML.Add(tree);

            tree = new XElement("Narzędzia_dydaktyczne");
            i = 0;
            foreach (var element in narzędzia)
            {
                i++;
                tree.Add(new XElement("N_" + i.ToString(), element.Nazwa));
            }
            kartaXML.Add(tree);

            List<Literatura> literatura = DbManager.getLiteratura(kartaPrzedmiotu.Id, 1);
            tree = new XElement("Literatura_podstawowa");
            i = 0;
            foreach (var element in literatura)
            {
                i++;
                tree.Add(new XElement("L_" + i.ToString(), element.Nazwa));
            }
            kartaXML.Add(tree);

            literatura = DbManager.getLiteratura(kartaPrzedmiotu.Id, 2);
            tree = new XElement("Literatura_uzupelniajaca");
            i = 0;
            foreach (var element in literatura)
            {
                i++;
                tree.Add(new XElement("L_" + i.ToString(), element.Nazwa));
            }
            kartaXML.Add(tree);

            List<Przedmiotowy_efekt_kształcenia> peki = DbManager.getPEK(kartaPrzedmiotu.Id, 1);
            tree = new XElement("PEK_z_zakresu_wiedzy");
            i = 0;
            foreach (var element in peki)
            {
                i++;
                tree.Add(new XElement("PEK_" + i.ToString(), element.Nazwa));
            }
            kartaXML.Add(tree);

            peki = DbManager.getPEK(kartaPrzedmiotu.Id, 2);
            tree = new XElement("PEK_z_zakresu_umiejętności");
            i = 0;
            foreach (var element in peki)
            {
                i++;
                tree.Add(new XElement("PEK_" + i.ToString(), element.Nazwa));
            }
            kartaXML.Add(tree);

            peki = DbManager.getPEK(kartaPrzedmiotu.Id, 3);
            tree = new XElement("PEK_z_zakresu_kompetencji");
            i = 0;
            foreach (var element in peki)
            {
                i++;
                tree.Add(new XElement("PEK_" + i.ToString(), element.Nazwa));
            }
            kartaXML.Add(tree);

            List<Treść_programowa> treści = DbManager.getTreściProgramowe(kartaPrzedmiotu.Id);
            tree = new XElement("Treść_programowa");
            i = 0;
            foreach (var element in treści)
            {
                i++;

                XElement tree2 = new XElement("F" + element.FormaZajeć.ToString());
                List<Temat_zajęć> tematy = DbManager.getTematyZajęć((int)element.FormaZajeć);
                int j = 0;
                foreach (var element2 in tematy)
                {
                    tree2.Add(new XElement("Nr" + element2.NumerZajęć.ToString(), element2.Temat));
                }
                tree.Add(tree2);
            }
            kartaXML.Add(tree);

            file.WriteLine(kartaXML);
            file.Close();

            return path;
        }
    }
}
