using Casino.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    internal class Utils
    {
        public static CasinoDBEntities DB = new CasinoDBEntities();
        public static User LoggedUser;
    }
}
