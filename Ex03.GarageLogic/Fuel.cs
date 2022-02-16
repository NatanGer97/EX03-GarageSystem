using System;

namespace Ex03.GarageLogic
{
    public sealed class Fuel : EnergySource
    {
        private eFuelType m_FuelType;

        public Fuel(float i_CurrentEnergyAmountPercentage, float i_MaxEnergyCapacity, eFuelType i_FuelType)
            : base(i_CurrentEnergyAmountPercentage, i_MaxEnergyCapacity)
        {
            FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler,
        }

        public void SupplyEnergySource(float i_AmountToAdd, eFuelType i_FuelType)
        {
            if(i_FuelType != FuelType)
            {
                throw new ArgumentException("Wrong type of fuel!");
            }

            CurrentEnergyAmount += i_AmountToAdd;
        }

        public override string ToString()
        {
            string fuelSourceFormat = string.Format(
                "Current amount of liters left : {0} , Maximum tank capacity : {1} , Fuel Type : {2}",
                CurrentEnergyAmount,
                MaxEnergyCapacity,
                FuelType);
            return fuelSourceFormat;
        }
    }
}
