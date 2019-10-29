using System;
using System.Collections.Generic;
using System.Text;

namespace Enums
{
    public enum eLicenseType
    {
        Undefined,
        A,
        A1,
        AB,
        B1
    }

    //this class convert from enum eLicenseType to string
    public class eLicenseTypeToText
    {
        public static string AsText(eLicenseType i_eLicenseType)
        {
            string enumDescripton = "null";
            switch (i_eLicenseType)
            {
                case eLicenseType.A:
                {
                    enumDescripton = "A";
                    break;
                }

                case eLicenseType.A1:
                {
                        enumDescripton = "A1";
                    break;
                }

                case eLicenseType.AB:
                {
                        enumDescripton = "AB";
                    break;
                }

                case eLicenseType.B1:
                {
                        enumDescripton = "B1";
                    break;
                }
            }

            return enumDescripton;
        }
    }
}
