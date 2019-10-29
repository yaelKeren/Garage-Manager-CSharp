using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace GarageLogic
{
    public class Fuel : EnergySource
    {
        private readonly float r_MaximumAmountOfFuelPerLiter;
        private readonly eKindOfFuel r_KindOfFuel;
        private float m_CurrentAmountOfFuelInLiters;

        public Fuel(float i_CurrentAmountOfFuelInLiters, eKindOfFuel iRKindOfFuel, float iRMaximumAmountOfFuelPerLiter)
        {
            m_CurrentAmountOfFuelInLiters = i_CurrentAmountOfFuelInLiters;
            r_MaximumAmountOfFuelPerLiter = iRMaximumAmountOfFuelPerLiter;
            r_KindOfFuel = iRKindOfFuel;
        }

        public float MaximumAmountOfFuelPerLiter
        {
            get { return r_MaximumAmountOfFuelPerLiter; }
        }

        public eKindOfFuel KindOfFuel
        {
            get { return r_KindOfFuel; }
        }

        public float CurrentAmountOfFuelInLiters
        {
            set { m_CurrentAmountOfFuelInLiters = value; }
            get { return m_CurrentAmountOfFuelInLiters; }
        }

        public void Refuel(float i_FuelQuantityToFillInLiter)
        {
            if (i_FuelQuantityToFillInLiter + m_CurrentAmountOfFuelInLiters > r_MaximumAmountOfFuelPerLiter)
            {
                float maxFuelQuantityToFillInLiter = r_MaximumAmountOfFuelPerLiter - m_CurrentAmountOfFuelInLiters;
                float minFuelQuantityToFillInLiter = 0;

                throw new ValueOutOfRangeException(maxFuelQuantityToFillInLiter, minFuelQuantityToFillInLiter);
            }

            m_CurrentAmountOfFuelInLiters += i_FuelQuantityToFillInLiter;
        }

        public override float GetCurrentEnergy()
        {
            float PercentageRemainingEnergy = (m_CurrentAmountOfFuelInLiters / r_MaximumAmountOfFuelPerLiter)
                * 100;

            return PercentageRemainingEnergy;
        }

        public void UpdateCurrentAmountOfFuelInLiters(float i_PercentageRemainingEnergy)
        {
            m_CurrentAmountOfFuelInLiters = (i_PercentageRemainingEnergy / 100) * r_MaximumAmountOfFuelPerLiter;
        }

        public override string ToString()
        {
            StringBuilder motorcycleString = new StringBuilder();
            motorcycleString.AppendFormat("Kind of fuel: {0}, ", KindOfFuelEnumToText.AsText(r_KindOfFuel));
            motorcycleString.AppendFormat("The amount of fuel left in litters: {0}. ", m_CurrentAmountOfFuelInLiters.ToString("F3"));

            return motorcycleString.ToString();
        }
    }
}
