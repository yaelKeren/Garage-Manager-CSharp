using System;
using System.Collections.Generic;
using System.Text;

namespace Enums
{
    public enum eVehicleModel
    {
        RegularMotorcycle = 1,
        ElectricMotorcycle,
        RegularCar,
        ElectricCar,
        Truck,
    }

    //this class convert from enum eVehicleModel to string
    public class VehicleModelToText
    {
        public static string AsText(eVehicleModel i_eSupport)
        {
            string enumDescripton = "null";
            switch (i_eSupport)
            {
                case eVehicleModel.RegularMotorcycle:
                {
                    enumDescripton = "Regular Motorcycle";
                    break;
                }

                case eVehicleModel.ElectricMotorcycle:
                {
                    enumDescripton = "Electric Motorcycle";
                    break;
                }

                case eVehicleModel.RegularCar:
                {
                    enumDescripton = "Regular Car";
                    break;
                }

                case eVehicleModel.ElectricCar:
                {
                    enumDescripton = "Electric Car";
                    break;
                }

                case eVehicleModel.Truck:
                {
                    enumDescripton = "Truck";
                    break;
                }
            }

            return enumDescripton;
        }
    }

}
