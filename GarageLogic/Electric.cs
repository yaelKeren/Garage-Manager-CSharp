using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Electric : EnergySource
    {
        private float m_BatteryLeftInTheHours;
        private readonly float r_MaximumHourlyBattery;

        public Electric(float i_BatteryLeftInTheHours, float i_MaximumHourlyBattery)
        {
            m_BatteryLeftInTheHours = i_BatteryLeftInTheHours;
            r_MaximumHourlyBattery = i_MaximumHourlyBattery;
        }

        public override float GetCurrentEnergy()
        {
            float PercentageRemainingEnergy = (m_BatteryLeftInTheHours / r_MaximumHourlyBattery)
                * 100;

            return PercentageRemainingEnergy;
        }

        public void UpdateBatteryLeftInTheHours(float i_PercentageRemainingEnergy)
        {
            m_BatteryLeftInTheHours = (i_PercentageRemainingEnergy / 100) * r_MaximumHourlyBattery;
        }

        public void Charge(float i_ChargingTimeInHours)
        {
            if (i_ChargingTimeInHours + m_BatteryLeftInTheHours > r_MaximumHourlyBattery)
            {
                float maxChargingTimeInHours = r_MaximumHourlyBattery - m_BatteryLeftInTheHours;
                float maxChargingTimeInMinutes = maxChargingTimeInHours * 60;
                float minChargingTimeInMinutes = 0;

                throw new ValueOutOfRangeException(maxChargingTimeInMinutes, minChargingTimeInMinutes);
            }

            m_BatteryLeftInTheHours += i_ChargingTimeInHours;
        }

        public override string ToString()
        {
            StringBuilder motorcycleString = new StringBuilder();
            motorcycleString.AppendFormat("Battery Left in hours: {0}", m_BatteryLeftInTheHours.ToString("F3"));

            return motorcycleString.ToString();
        }
    }
}
