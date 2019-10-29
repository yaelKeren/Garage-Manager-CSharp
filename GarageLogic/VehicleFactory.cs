using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace GarageLogic
{
    public class VehicleFactory
    {
        public static Vehicle CreateNewVehicle(eVehicleModel i_Model, string i_LicenseNumber)
        {
            Vehicle newVehicle = null;
            string modelStr = VehicleModelToText.AsText(i_Model);

            switch (i_Model)
            {
                case eVehicleModel.RegularMotorcycle:
                    newVehicle = createRegularMotorcycle(modelStr,i_LicenseNumber);
                    break;
                case eVehicleModel.ElectricMotorcycle:
                    newVehicle = createElectricMotorcycle(modelStr, i_LicenseNumber);
                    break;
                case eVehicleModel.RegularCar:
                    newVehicle = createRegularCar(modelStr, i_LicenseNumber);
                    break;
                case eVehicleModel.ElectricCar:
                    newVehicle = createElectricCar(modelStr, i_LicenseNumber);
                    break;
                case eVehicleModel.Truck:
                    newVehicle = createTruck(modelStr, i_LicenseNumber);
                    break;
            }

            return newVehicle;
        }

        private static Vehicle createTruck(string i_Model, string i_LicenseNumber)
        {
            EnergySource energySource = new Fuel(Consts.k_UndefinedValue, eKindOfFuel.Soler,
                Consts.k_TruckTank);
            Truck truck = new Truck(false, Consts.k_UndefinedValue,
                i_Model, i_LicenseNumber, Consts.k_UndefinedValue,energySource, Consts.k_TruckNumOfWheel);

            return truck;
        }

        private static Vehicle createRegularCar(string i_Model, string i_LicenseNumber)
        {
            EnergySource energySource = new Fuel(Consts.k_UndefinedValue, eKindOfFuel.Octan96, Consts.k_CarTank);
            Car car = new Car(eCarEnums.kindOfColor.Undefined, eCarEnums.NumberOfDoors.Undefined,
                i_Model, i_LicenseNumber, Consts.k_UndefinedValue, energySource, Consts.k_CarNumOfWheel);

            return car;
        }

        private static Vehicle createRegularMotorcycle(string i_Model, string i_LicenseNumber)
        {
            EnergySource energySource = new Fuel(Consts.k_UndefinedValue, eKindOfFuel.Octan95,
                Consts.k_MotorcycleTank);
            Motorcycle motorcycle = new Motorcycle((int)eLicenseType.Undefined, (int)eLicenseType.Undefined,
                i_Model, i_LicenseNumber, Consts.k_UndefinedValue, energySource, Consts.k_MotorcycleNumOfWheel);

            return motorcycle;
        }
        
        private static Vehicle createElectricMotorcycle(string i_Model, string i_LicenseNumber)
        {
            EnergySource energySource = new Electric(Consts.k_UndefinedValue,
                Consts.k_MotorcycleBattery);
            Motorcycle motorcycle = new Motorcycle(eLicenseType.Undefined, Consts.k_UndefinedValue,
                i_Model, i_LicenseNumber, Consts.k_UndefinedValue, energySource, Consts.k_MotorcycleNumOfWheel);

            return motorcycle;
        }

        private static Vehicle createElectricCar(string i_Model, string i_LicenseNumber)
        {
            EnergySource energySource = new Electric(Consts.k_UndefinedValue,
                Consts.k_CarBattery);
            Car car = new Car(eCarEnums.kindOfColor.Undefined, eCarEnums.NumberOfDoors.Undefined,
                i_Model, i_LicenseNumber, Consts.k_UndefinedValue, energySource, Consts.k_CarNumOfWheel);

            return car;
        }
    }
}
