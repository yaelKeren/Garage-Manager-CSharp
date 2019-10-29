using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace GarageLogic
{
    public class Car : Vehicle
    {
        private eCarEnums.kindOfColor m_Color;
        private eCarEnums.NumberOfDoors m_NumberOfDoors;

        public Car(eCarEnums.kindOfColor i_Color, eCarEnums.NumberOfDoors i_NumberOfDoors, string i_ModelName, 
            string i_LicenseNumber, float i_PercentageRemainingEnergy ,EnergySource i_EnergySource, int i_NumOfWheels) 
            : base(i_ModelName, i_LicenseNumber, i_PercentageRemainingEnergy, i_EnergySource, i_NumOfWheels)
        {
            m_Color = i_Color;
            m_NumberOfDoors = i_NumberOfDoors;
        }


        public eCarEnums.kindOfColor Color
        {
            set { m_Color = value; }
            get { return m_Color; }
        }

        public eCarEnums.NumberOfDoors NumberOfDoors
        {
            set { m_NumberOfDoors = value; }
            get { return m_NumberOfDoors; }
        }

        public override string ToString() 
        {
            StringBuilder carString = new StringBuilder();
            carString.AppendFormat(base.ToString());
            carString.AppendFormat("Car color: {0}, ", eCarEnums.CarEnumsToText.AsText(m_Color));
            carString.AppendFormat("Number of Doors: {0}. ", eCarEnums.CarEnumsToText.AsText(m_NumberOfDoors));

            return carString.ToString();
        }
    }
}
