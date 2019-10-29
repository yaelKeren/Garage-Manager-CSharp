using System;
using System.Collections.Generic;
using System.Text;

namespace Enums
{
    public enum eKindOfFuel
    {
        Soler = 1,
        Octan95,
        Octan96,
        Octan98
    }

    //this class convert from enum eKindOfFuel to string
    public class KindOfFuelEnumToText
    {
        public static string AsText(eKindOfFuel i_eKindOfFuel)
        {
            string enumDescripton = "null";
            switch (i_eKindOfFuel)
            {
                case eKindOfFuel.Soler:
                {
                    enumDescripton = "Soler";
                    break;
                }

                case eKindOfFuel.Octan95:
                {
                        enumDescripton = "Octan95";
                    break;
                }

                case eKindOfFuel.Octan96:
                {
                        enumDescripton = "Octan96";
                    break;
                }

                case eKindOfFuel.Octan98:
                {
                        enumDescripton = "Octan98";
                    break;
                }
            }

            return enumDescripton;
        }

    }
}
   

