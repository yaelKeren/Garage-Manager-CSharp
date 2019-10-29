using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_KindOfLicense;
        private int m_EngineCapacity;

        public Motorcycle(eLicenseType i_KindOfLicense, int iMEngineCapacity, string i_ModelName, string i_LicenseNumber,
            float i_PercentageRemainingEnergy, EnergySource i_EnergySource, int i_NumOfWheels) :
            base(i_ModelName, i_LicenseNumber, i_PercentageRemainingEnergy,i_EnergySource, i_NumOfWheels)
        {
            m_KindOfLicense = i_KindOfLicense;
            m_EngineCapacity = iMEngineCapacity;
        }

        public eLicenseType KindOfLicense
        {
            set { m_KindOfLicense = value; }
        }

        public int EngineCapacity
        {
            set { m_EngineCapacity = value; }
        }

        public override string ToString()
        {
            StringBuilder motorcycleString = new StringBuilder();
            motorcycleString.AppendFormat(base.ToString());
            motorcycleString.AppendFormat("Kind of license: {0}, ", eLicenseTypeToText.AsText(m_KindOfLicense));
            motorcycleString.AppendFormat("Engine capacity: {0}. ", m_EngineCapacity.ToString());

            return motorcycleString.ToString();
        }
    }
}
