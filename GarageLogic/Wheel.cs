using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
     public class Wheel
    {
        private float m_CurrentAirPressure;
        private readonly string r_ManufacturerName;
        private readonly float r_MaximumAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaximumAirPressure)
        {
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_ManufacturerName = i_ManufacturerName;
            r_MaximumAirPressure = i_MaximumAirPressure;
        }

        public float MaximumAirPressure
        {
            get { return r_MaximumAirPressure; }
        }

        public float CurrentAirPressure
        {
            set
            {
                if (value <= r_MaximumAirPressure && value >= 0)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(r_MaximumAirPressure, 0);
                }
            }

            get { return m_CurrentAirPressure; }
        }

        public override string ToString()
        {
            StringBuilder wheelStr = new StringBuilder();
            wheelStr.AppendFormat("Manufacturer Name: {0}, ", r_ManufacturerName);
            wheelStr.AppendFormat("Current air pressure: {0}, ", m_CurrentAirPressure);
            wheelStr.AppendFormat("Maximum air pressure: {0}. ", r_MaximumAirPressure);

            return wheelStr.ToString();
        }

        public void InflatAir(float i_QuantityOfAirToAdd)
        {
            if(m_CurrentAirPressure + i_QuantityOfAirToAdd <= r_MaximumAirPressure)
            { // requested quantity of air to add is valid
                m_CurrentAirPressure += i_QuantityOfAirToAdd;
            }
            else
            {
                float maxQuantityOfAirToAdd = r_MaximumAirPressure - m_CurrentAirPressure;
                float minQuantityOfAirToAdd = 0;

                throw new ValueOutOfRangeException(maxQuantityOfAirToAdd, minQuantityOfAirToAdd);
            }
        }
     }
}
