using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POOI_EF_SALAZAR.Models
{
    public class Consumidor
    {
        public string Dnicont { get; set; }
        public string Nomcont { get; set; }
        public string Apecont { get; set; }
        public string Emailcont { get; set; }
        public string Idpais { get; set; }

        public Consumidor() { }

        public Consumidor(string dnicont, string nomcont, string apecont, string emailcont, string idpais)
        {
            Dnicont = dnicont;
            Nomcont = nomcont;
            Apecont = apecont;
            Emailcont = emailcont;
            Idpais = idpais;
        }
    }
}