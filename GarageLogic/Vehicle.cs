using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Vehicle
    {
        private float m_PercentageRemainingEnergy;
        private EnergySource m_EnergySource;
        private List<Wheel> m_Wheels;
        private readonly int r_NumOfWheels;
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        private const string k_WheelsAreFull = "Wheels are fully inflated.";

        public float PercentageRemainingEnergy
        {
            get { return m_PercentageRemainingEnergy; }
            set { m_PercentageRemainingEnergy = value; }
        }

        public EnergySource EnergySource
        {
            get { return m_EnergySource; }
            set { m_EnergySource = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        public int NumOfWheels
        {
            get { return r_NumOfWheels; }
        }

        public Vehicle(string i_ModelName, string i_LicenseNumber, float i_PercentageRemainingEnergy,
            EnergySource i_EnergySource, int i_NumOfWheels)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            m_PercentageRemainingEnergy = i_PercentageRemainingEnergy;
            m_EnergySource = i_EnergySource;
            r_NumOfWheels = i_NumOfWheels;
        }

        public bool InflateWheelsToMax()
        {
            bool isAllWheelsFull = true;

            foreach (Wheel wheel in m_Wheels)
            {
                if(!wheel.MaximumAirPressure.Equals(wheel.CurrentAirPressure))
                {
                    isAllWheelsFull = false;
                }

                float quantityOfAirToAdd = wheel.MaximumAirPressure - wheel.CurrentAirPressure;
                wheel.InflatAir(quantityOfAirToAdd);
            }

            if(isAllWheelsFull)
            {
                throw new Exception(k_WheelsAreFull);
            }

            return !isAllWheelsFull;
        }

        public override string ToString()
        {
            StringBuilder vehicleString = new StringBuilder();
            vehicleString.AppendFormat("Model Name: {0}, ", ModelName);
            vehicleString.AppendFormat("License Number: {0}, ", r_LicenseNumber);
            vehicleString.AppendFormat("Remaining Energy: {0}% ", m_PercentageRemainingEnergy);
            vehicleString.AppendLine();
            vehicleString.AppendFormat("Energy Source: {0} {1}", Environment.NewLine,
                m_EnergySource.ToString());
            vehicleString.AppendLine();
            vehicleString.AppendFormat("Wheels: ");
            vehicleString.AppendLine();

            foreach (Wheel wheel in m_Wheels)
            {
                vehicleString.AppendFormat(wheel.ToString());
                vehicleString.AppendLine();
            }

            return vehicleString.ToString();
        }
    }
}
