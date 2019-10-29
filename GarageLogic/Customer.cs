using System;
using System.Text;
using Enums;

namespace GarageLogic
{
    public class Customer
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private readonly Vehicle r_Vehicle;
        private eVehicleState m_State;

        public Customer(string i_OwnerName, string i_OwnerPhoneNumber, Enums.eVehicleState i_State, Vehicle i_Vehical)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_State = i_State;
            r_Vehicle = i_Vehical;
        }

        public Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        public eVehicleState State
        {
            get { return m_State; }
            set { m_State = value; }
        }

        public override string ToString()
        {
            StringBuilder vehicleString = new StringBuilder();
            vehicleString.AppendFormat("Owner Name: {0}", r_OwnerName);
            vehicleString.AppendLine();
            vehicleString.AppendFormat("Owner phone number: {0}", r_OwnerPhoneNumber);
            vehicleString.AppendLine();
            vehicleString.AppendFormat("Vehicle details: {0}", r_Vehicle.ToString());
            vehicleString.AppendLine();
            vehicleString.AppendFormat("The {0} state is: {1}", r_Vehicle.ModelName,VehicleStateToText.AsText(m_State));

            return vehicleString.ToString();
        }
    }
}
