using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleUI;
using GarageLogic;
using Enums;

namespace GarageManager
{
    public class GarageManager
    {
        private bool m_IsOpen = false;
        private Garage m_Garage;
        private int m_UserSelection;
        private const string k_ErrCustomerNotExist = "Customer does not exists.";
        private const string k_ErrGarageEmpty = "There are no vehicles in the garage.";

        public GarageManager()
        {
            m_Garage = new Garage();
        }

        public void OpenGarage()
        {
            m_IsOpen = true;
            while (m_IsOpen)
            {
                try
                {
                    UI.DisplayMenu();
                    m_UserSelection = UI.GetUserSelection();
                    handleUserSelection();
                }
                catch(Exception ex)
                {
                    UI.PrintMessage(ex.Message);
                }        
            }
        }

        // $G$ CSS-018 (-5) You should have used enumerations here.
        private void handleUserSelection()
        {
            try
            {
                switch (m_UserSelection)
                {
                    case 1:
                        newCustomer(UI.GetLicenseNumber());
                        break;                    
                    case 2:
                        handleDisplayLicenseNumber();
                        break;
                    case 3:
                        handleCahngeVehicleState();
                        break;
                    case 4:
                        handleInflateVehicleWheelsToMax();
                        break;
                    case 5:
                        handleRefuelVehicle();
                        break;
                    case 6:
                        handleChargeVehicle();
                        break;
                    case 7:
                        handleDisplayVehicleData();
                        break;
                    case 8:
                        closeGarage();
                        break;
                }
            }
            catch (Exception ex)
            {
                UI.PrintMessage(ex.Message);
            }
        }

        private bool checkAndHandleVehicleNotExists(string i_LicenseNumber)
        {
            bool isVehicleExists = m_Garage.IsCustomerExist(i_LicenseNumber);

            if(!isVehicleExists)
            {
                UI.PrintMessage(k_ErrCustomerNotExist);
            }

            return isVehicleExists;
        }

        private void handleDisplayVehicleData()
        {
            string licenseNumber = UI.GetLicenseNumber();

            if(checkAndHandleVehicleNotExists(licenseNumber))
            {
                Customer selectedCustomer = m_Garage.GetCustomerByLicenseNumber(licenseNumber);
                UI.DisPlayVehicleData(selectedCustomer.ToString());
            }        
        }

        private void handleChargeVehicle()
        {
            string licenseNumber = UI.GetLicenseNumber();

            if (checkAndHandleVehicleNotExists(licenseNumber))
            {
                m_Garage.ChargeVehicle(licenseNumber, UI.GetChargingTimeInMinutes());
                UI.ChargeVehicleSuccess();
            }
        }

        private void handleRefuelVehicle()
        {
            string licenseNumber = UI.GetLicenseNumber();

            if (checkAndHandleVehicleNotExists(licenseNumber))
            {
                m_Garage.RefuelVehicle(licenseNumber, UI.GetFuelType(), UI.GetFuelQuantityInLiters());
                UI.RefuelVehicleSuccess();
            }
        }

        private void handleInflateVehicleWheelsToMax()
        {
            string licenseNumber = UI.GetLicenseNumber();

            if (checkAndHandleVehicleNotExists(licenseNumber))
            {
                bool isInflated = m_Garage.InflateWheelsToMax(licenseNumber);

                if (isInflated)
                {
                    UI.InflateWheelsToMaxSuccessed();
                }
            }
        }

        private void handleCahngeVehicleState()
        {
            string licenseNumber = UI.GetLicenseNumber();

            if (checkAndHandleVehicleNotExists(licenseNumber))
            {
                m_Garage.ChangeVehicleState(licenseNumber, UI.GetNewState());
                UI.ChangeVehicleStateSuccess();
            }
        }

        private void handleDisplayLicenseNumber()
        {
            if (!m_Garage.IsEmpty())
            {
                eVehicleState sortState = UI.GetSortState();
                UI.DisPlayLicenseNumbers(m_Garage.CreateLicenseNumbersListByState(sortState),
                    sortState);
            }
            else
            {
                UI.PrintMessage(k_ErrGarageEmpty);
            }
        }

        private void newCustomer(string i_LiceneseNumber)
        {
            Customer vehicleExistsCustomer = m_Garage.GetCustomerByLicenseNumber(i_LiceneseNumber);

            if (vehicleExistsCustomer != null)
            { //vehicle exist, we update it's state
                UI.VehicleExist();
                m_Garage.ChangeVehicleStateByCustomer(ref vehicleExistsCustomer, eVehicleState.Fix);
            }
            else
            {
                addNewCustomer(i_LiceneseNumber);
                UI.VehicleAdded();
            }
        }

        private void addNewCustomer(string i_LiceneseNumber)
        {
            string ownerName = UI.GetOwnerName();
            string ownerPhoneNumber = UI.GetOwnerPhoneNumber();
            eVehicleModel modelName = UI.GetModelName();
            Vehicle newVehicle = VehicleFactory.CreateNewVehicle(modelName, i_LiceneseNumber);

            setWheelsPressure(ref newVehicle, modelName);
            setPercentageRemainingEnergy(modelName, ref newVehicle);
            setVehicleInfoByModel(modelName, ref newVehicle);
            m_Garage.AddNewCustomer(ref newVehicle, ownerName, ownerPhoneNumber);
        }

        private void setPercentageRemainingEnergy(eVehicleModel i_ModelName, ref Vehicle o_NewVehicle)
        {
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    o_NewVehicle.PercentageRemainingEnergy = UI.GetRemainingPercentageEnergy(
                        VehicleModelToText.AsText(i_ModelName), 100, 0);
                    isValidInput = true;
                }
                catch (FormatException formatException)
                {
                    UI.PrintMessage(formatException.Message);
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    UI.PrintMessage(valueOutOfRangeException.Message);
                }
            }
        }

        private void setWheelsPressure(ref Vehicle io_NewVehicle, eVehicleModel i_ModelName)
        {
            float maxAirPressure = getMaxAirPressure(i_ModelName);
            io_NewVehicle.Wheels = new List<Wheel>();

            for (int i = 0; i < io_NewVehicle.NumOfWheels; ++i)
            {
                try
                {
                    float currentPressure = UI.GetWheelPressure(i + 1, maxAirPressure);
                    string manufacturer = UI.GetWheelManufacturer(i + 1);

                    io_NewVehicle.Wheels.Add(new Wheel(manufacturer, currentPressure, maxAirPressure));
                }
                catch (FormatException formatException)
                {
                    UI.PrintMessage(formatException.Message);
                    --i;
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    UI.PrintMessage(valueOutOfRangeException.Message);
                    --i;
                }
            }
        }

        private float getMaxAirPressure(eVehicleModel i_eVehicleModel)
        {
            float maxAirPressure = 0;

            switch (i_eVehicleModel)
            {
                case eVehicleModel.RegularMotorcycle:
                case eVehicleModel.ElectricMotorcycle:
                    maxAirPressure = Consts.k_MotorcycleWheelPressure;
                    break;

                case eVehicleModel.ElectricCar:
                case eVehicleModel.RegularCar:
                    maxAirPressure = Consts.k_CarWheelPressure; 
                    break;
                case eVehicleModel.Truck:
                    maxAirPressure = Consts.k_TruckWheelPressure;
                    break;
            }

            return maxAirPressure;
        }

        // $G$ CSS-015 (-5) Bad variable name (should be in the form of: ref io_CamelCase).
        // $G$ DSN-002 (-10) The UI should not know Car\Truck\Motorcycle
        private void setVehicleInfoByModel(eVehicleModel i_ModelName, ref Vehicle o_NewVehicle)
        {
            switch (i_ModelName)
            {
                case eVehicleModel.ElectricMotorcycle:
                    setMotorcycle(o_NewVehicle as Motorcycle);
                    (o_NewVehicle.EnergySource as Electric).UpdateBatteryLeftInTheHours(o_NewVehicle.PercentageRemainingEnergy);
                    break;
                case eVehicleModel.RegularMotorcycle:
                    setMotorcycle(o_NewVehicle as Motorcycle);
                    (o_NewVehicle.EnergySource as Fuel).UpdateCurrentAmountOfFuelInLiters(o_NewVehicle.PercentageRemainingEnergy);
                    break;
                case eVehicleModel.RegularCar:
                    setCar(o_NewVehicle as Car);
                    (o_NewVehicle.EnergySource as Fuel).UpdateCurrentAmountOfFuelInLiters(o_NewVehicle.PercentageRemainingEnergy);
                    break;
                case eVehicleModel.ElectricCar:
                    setCar(o_NewVehicle as Car);
                    (o_NewVehicle.EnergySource as Electric).UpdateBatteryLeftInTheHours(o_NewVehicle.PercentageRemainingEnergy);
                    break;
                case eVehicleModel.Truck:
                    setTruck(o_NewVehicle as Truck);
                    (o_NewVehicle.EnergySource as Fuel).UpdateCurrentAmountOfFuelInLiters(o_NewVehicle.PercentageRemainingEnergy);
                    break;
            }
        }

        // $G$ CSS-029 (-5) Bad code duplication.
        // $G$ CSS-013 (-5) Bad variable name (should be in the form of: i_CamelCase).
        private void setCar(Car o_Car)
        {
            setColor(o_Car);
            setNumberOfDoors(o_Car);
        }
        // $G$ CSS-029 (-5) Bad code duplication.
        private void setMotorcycle(Motorcycle o_Motorcycle)
        {
            setKindOfLicense(o_Motorcycle);
            setEngineCapasity(o_Motorcycle);
        }

        private void setKindOfLicense(Motorcycle o_Motorcycle)
        {
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    o_Motorcycle.KindOfLicense = UI.GetKindOfLicense();
                    isValidInput = true;
                }
                catch (FormatException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
            }
        }

        private void setEngineCapasity(Motorcycle o_Motorcycle)
        {
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    o_Motorcycle.EngineCapacity = UI.GetEngineVolume();
                    isValidInput = true;
                }
                catch (FormatException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
            }
        }

        private void setNumberOfDoors(Car o_Car)
        {
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    o_Car.NumberOfDoors = UI.GetNumberOfDoors();
                    isValidInput = true;
                }
                catch (FormatException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
            }
        }

        private void setColor(Car o_Car)
        {
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    o_Car.Color = UI.GetCarColor();
                    isValidInput = true;
                }
                catch (FormatException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
            }
        }

        // $G$ CSS-029 (-5) Bad code duplication.
        private void setTruck(Truck o_Truck)
        {
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    o_Truck.CarryingHazardousMaterials = UI.IsTruckCarryingHazardousMaterials();
                    o_Truck.CargoVolume = UI.GetCargoVolume();
                    isValidInput = true;
                }
                catch (FormatException ex)
                {
                    UI.PrintMessage(ex.Message);
                }
            }
        }

        private void closeGarage()
        {
            m_IsOpen = false;
            UI.close();
        }
    }
}