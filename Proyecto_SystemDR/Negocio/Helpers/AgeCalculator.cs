using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Helpers
{
    public class AgeCalculator
    {
        public static int GetAge(DateTime birthday, DateTime now)
        {
            bool yaCumplio = false;
            if ((now.Month >= birthday.Month) && (now.Day >= birthday.Day))
            {
                yaCumplio = true;
            }

            int edad = now.Year - birthday.Year;

            return yaCumplio ? edad : edad - 1;
        }
    }
}
