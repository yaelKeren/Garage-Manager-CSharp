using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_CarryingHazardousMaterials;
        private float m_CargoVolume;

        public Truck(bool i_CarryingHazardousMaterials, float i_CargoVolume, string i_ModelName, string i_LicenseNumber,
            float i_PercentageRemainingEnergy, EnergySource i_EnergySource, int i_NumOfWheels) : 
            base(i_ModelName, i_LicenseNumber, i_PercentageRemainingEnergy,i_EnergySource, i_NumOfWheels)
        {
            m_CarryingHazardousMaterials = i_CarryingHazardousMaterials;
            m_CargoVolume = i_CargoVolume;
        }

        public bool CarryingHazardousMaterials
        {
            set { m_CarryingHazardousMaterials = value;}
            get { return m_CarryingHazardousMaterials; }
        }

        public float CargoVolume
        {
            set { m_CargoVolume = value; }
            get { return m_CargoVolume; }
        }

        public override string ToString()
        {
            StringBuilder truck = new StringBuilder();
            truck.AppendFormat(base.ToString());
            truck.AppendFormat("Truck does carry hazardous materials: {0}, ", m_CarryingHazardousMaterials.ToString());
            truck.AppendFormat("Cargo volume: {0}.", m_CargoVolume.ToString("F3"));

            return truck.ToString();
        }
    }
}
