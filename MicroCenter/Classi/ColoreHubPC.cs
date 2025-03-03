using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCenter.Classi
{
    public class ColoreHubPC
    {
        //public int? ID_posizione {  get; set; }
        //public string Nome { get; set; }
        //public int Colore { get; set; }
        //public int Saturazione {  get; set; }
        //public bool UI_stato {  get; set; }
        //public string Tipo {  get; set; }
        public List<Colori> Colori { get; set; }
        public List<Animazioni> Animazioni { get; set; }

        //public List<ArduFanHub_4_0> ArduFanHub_4_0 { get; set; }

    }

    public class Colori
    {
        public string Nome { get; set; }
        public int Colore { get; set; }
        public int Saturazione { get; set; }
        public bool UI_stato { get; set; }
       // public int? ID_posizione { get; set; }
        public string Tipo { get; set; }

    }



    public class Animazioni
    {
        public int? ID_posizione { get; set; }
        public string Nome { get; set; }
        public int Colore { get; set; }
       // public int Saturazione { get; set; }
        public int Luminosità { get; set; }
        public bool UI_stato { get; set; }
        public string Tipo { get; set; }

    }


    //public class ArduFanHub_4_0
    //{
    //    //public string HUB { get; set; }
    //    public List<HUB> HUB { get; set; }
    //    public List<Ventole> Ventole { get; set; }
    //    //public string Ventole { get; set; }
    //    //public string Dissipatore { get; set; }

    //    //public string Scheda_Video { get; set; }
    //    //public string Strisca_LED { get; set; }
    //}

    //public class HUB
    //{
    //    public string Nome { get; set; }
    //}

    //public class Ventole
    //{
    //    public string Nome { get; set; }
    //}

}
