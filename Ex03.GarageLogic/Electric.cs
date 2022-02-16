using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal sealed class Electric : EnergySource
    {
        public Electric(float i_CurrentEnergyAmountPercentage, float i_MaxEnergyCapacity)
            : base(i_CurrentEnergyAmountPercentage, i_MaxEnergyCapacity)
        {
        }

        public void Recharge(float i_AmountToCharge)
        {
            CurrentEnergyAmount += i_AmountToCharge;
        }

        public override string ToString()
        {
            string electricSourceFormat = string.Format(
                "Current battery time left : {0} , Maximum battery time is : {1}",
                CurrentEnergyAmount,
                MaxEnergyCapacity);
            return electricSourceFormat;
        }
    }
}
