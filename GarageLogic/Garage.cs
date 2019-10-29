using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace GarageLogic
{
    public class Garage
    {
        private const string k_ErrVehicleExsit = "Vehicle exits, and now is in repairing";
        private const string k_ErrVehicleNotExists = "There are no vehicles in the garage at the moment";
        private const string k_GarageEmptyMsg = "No vehicles Found";
        private const string k_ErrVehicleEnergySourceNotMatch = "Requested energy source does not match vehicle energy source";
        private const string k_ErrVehicleFuelNotMatch = "Requested fuel type does not match vehicle fuel type";
        private Dictionary<string, Customer> m_Customers;

        public Garage()
        {
            m_Customers = new Dictionary<string, Customer>();
        }

        public void AddNewCustomer(ref Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            Customer newCustomer = new Customer(i_OwnerName, i_OwnerPhoneNumber, eVehicleState.Fix, i_Vehicle);
            m_Customers.Add(newCustomer.Vehicle.LicenseNumber, newCustomer);
        }

        //this method get customer by license number
        public Customer GetCustomerByLicenseNumber(string i_LicenseNumber)
        {
            Customer customer = null;

            if (m_Customers.ContainsKey(i_LicenseNumber))
            {
                customer = m_Customers[i_LicenseNumber];
            }

            return customer;
        }

        // find and returns all license numbers which their matching car equals to the requested state
        public List<string> CreateLicenseNumbersListByState(eVehicleState i_SortState)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (KeyValuePair<string, Customer> customer in m_Customers)
            {
                if(i_SortState.Equals(eVehicleState.Undefined) ||
                    i_SortState.Equals(customer.Value.State))
                {
                    licenseNumbers.Add(customer.Key);
                }
            }

            return licenseNumbers;
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_ChargingTimeInMinutes)
        {
            Customer requestedCustomer;
            m_Customers.TryGetValue(i_LicenseNumber, out requestedCustomer);
            Electric elctric = requestedCustomer.Vehicle.EnergySource as Electric;

            if (null != elctric)
            { 
                // the vehicle energy source is electric
                elctric.Charge(i_ChargingTimeInMinutes / 60);
                requestedCustomer.Vehicle.PercentageRemainingEnergy = 
                    requestedCustomer.Vehicle.EnergySource.GetCurrentEnergy();
            }
            else
            {
                throw new ArgumentException(k_ErrVehicleEnergySourceNotMatch);
            }
        }

        public bool IsCustomerExist(string i_LicenseNumber)
        {
            return m_Customers.ContainsKey(i_LicenseNumber);
        }

        //private void chargeHandleVehicleExists(Customer i_RequestedCustomer, float i_ChargingTimeInMinutes)
        //{
        //    if (i_RequestedCustomer.Vehicle.EnergySource is Electric)
        //    { // the vehicle energy source is electric
        //        chargeHandleElectricVehicle(i_RequestedCustomer, i_ChargingTimeInMinutes);
        //    }
        //    else
        //    {
        //        throw new ArgumentException(k_ErrVehicleEnergySourceNotMatch);
        //    }
        //}

        //צמצמתי הכל למעלה וזרקנו אקספשניין מהמקור אנרגיה חשמלי או דלק
        //private void chargeHandleElectricVehicle(Customer i_RequestedCustomer, float i_ChargingTimeInMinutes)
        //{
        //    Electric electricVehicle = i_RequestedCustomer.Vehicle.EnergySource as Electric;
        //    float ChargingTimeInHours = i_ChargingTimeInMinutes / 60; // convert minutes to hours

        //    if (electricVehicle.BatteryLeftInTheHours + ChargingTimeInHours <= electricVehicle.MaximumHourlyBattery)
        //    { // the requested charging time is valid
        //        electricVehicle.Charge(ChargingTimeInHours);
        //    }
        //    else
        //    {
        //        float maxChargingTimeInHours = electricVehicle.MaximumHourlyBattery
        //            - electricVehicle.BatteryLeftInTheHours;
        //        float minChargingTimeInHours = 0;

        //        throw new ValueOutOfRangeException(maxChargingTimeInHours, minChargingTimeInHours);
        //    }
        //}

        public void RefuelVehicle(string i_LicenseNumber, eKindOfFuel i_FuelType, float i_FuelQuantityToFillInLiter)
        {
            Customer requestedCustomer;
            bool vehicleExists = m_Customers.TryGetValue(i_LicenseNumber, out requestedCustomer);

            if (vehicleExists)
            {
                refuelHandleVehicleExists(requestedCustomer, i_FuelType, i_FuelQuantityToFillInLiter);
            }
            else
            {
                throw new ArgumentException(k_ErrVehicleNotExists);
            }
        }

        private void refuelHandleVehicleExists(Customer i_RequestedCustomer, eKindOfFuel i_FuelType,
            float i_FuelQuantityToFillInLiter)
        {
            if (i_RequestedCustomer.Vehicle.EnergySource is Fuel)
            { // the vehicle energy source is fuel
                refuelHandleFuelVehicle(i_RequestedCustomer, i_FuelType, i_FuelQuantityToFillInLiter);
            }
            else
            {
                throw new ArgumentException(k_ErrVehicleEnergySourceNotMatch);
            }
        }

        private void refuelHandleFuelVehicle(Customer i_RequestedCustomer, eKindOfFuel i_FuelType,
            float i_FuelQuantityToFillInLiter)
        {
            Fuel fuelVehicle = i_RequestedCustomer.Vehicle.EnergySource as Fuel;

            if (fuelVehicle.KindOfFuel.Equals(i_FuelType))
            {  // the vehicle fuel type match the requested fuel type
                refuelHandleCorrectType(fuelVehicle, i_FuelQuantityToFillInLiter);
                i_RequestedCustomer.Vehicle.PercentageRemainingEnergy = 
                    i_RequestedCustomer.Vehicle.EnergySource.GetCurrentEnergy();
            }
            else
            {
                throw new ArgumentException(k_ErrVehicleFuelNotMatch);
            }
        }

        private void refuelHandleCorrectType(Fuel i_FuelVehicle, float i_FuelQuantityToFillInLiter)
        {
            if (i_FuelVehicle.CurrentAmountOfFuelInLiters + i_FuelQuantityToFillInLiter <=
                    i_FuelVehicle.MaximumAmountOfFuelPerLiter)
            { // the requested fuel quantity to fill is valid
                i_FuelVehicle.Refuel(i_FuelQuantityToFillInLiter);
            }
            else
            {
                float maxFuelQuantity = i_FuelVehicle.MaximumAmountOfFuelPerLiter
                    - i_FuelVehicle.CurrentAmountOfFuelInLiters;
                float minFuelQuantity = 0;

                throw new ValueOutOfRangeException(maxFuelQuantity, minFuelQuantity);
            }
        }

        public bool InflateWheelsToMax(string i_LicenseNumber)
        {
            Customer requestedCustomer;
            bool isInflated = false;
            bool vehicleExists = m_Customers.TryGetValue(i_LicenseNumber, out requestedCustomer);

            if(vehicleExists)
            {
                isInflated = requestedCustomer.Vehicle.InflateWheelsToMax();
            }
            else
            {
                throw new ArgumentException(k_ErrVehicleNotExists);
            }

            return isInflated;
        }

        public void ChangeVehicleState(string i_LicenseNumber, eVehicleState i_NewState)
        {
            Customer requestedCustomer;
            bool vehicleExists = m_Customers.TryGetValue(i_LicenseNumber, out requestedCustomer);

            if (vehicleExists)
            {
                requestedCustomer.State = i_NewState;
            }
            else
            {
                throw new ArgumentException(  k_GarageEmptyMsg);
            }
        }


        // $G$ CSS-015 (-5) Bad variable name (should be in the form of: ref io_CamelCase).
        //change the state of customer vehicle
        public void ChangeVehicleStateByCustomer(ref Customer i_VehicleExistsCustomer, eVehicleState i_State)
        {
            i_VehicleExistsCustomer.State = i_State;
        }

        public bool IsEmpty()
        {
            return m_Customers.Count < 1;
        }
    }
}
