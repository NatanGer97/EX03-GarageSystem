using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseNumber, EnergySource i_EnergySource, float i_MaxWheelPressure, int i_AmountOfWheels)
            : base(i_LicenseNumber, i_EnergySource, i_MaxWheelPressure, i_AmountOfWheels)
        {
        }
    }
}
