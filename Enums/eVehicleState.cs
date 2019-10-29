using System;
using System.Collections.Generic;
using System.Text;

namespace Enums
{
    public enum eVehicleState
    {
        Undefined,
        Fix,
        Fixed,
        Paid
    }

    //this class convert from enum eVehicleState to string
    public class VehicleStateToText
    {
        public static string AsText(eVehicleState eState)
        {
            string enumDescripton = "null";
            switch (eState)
            {
                case eVehicleState.Fix:
                {
                    enumDescripton = "Fix";
                    break;
                }

                case eVehicleState.Fixed:
                {
                    enumDescripton = "Fixed";
                    break;
                }

                case eVehicleState.Paid:
                { 
                    enumDescripton = "Paid";
                    break;
                }

            }

            return enumDescripton;
        }
    }
}
