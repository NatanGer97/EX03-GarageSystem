using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        public ElectricMotorcycle(string i_LicenseNumber, float i_MaxAirPressure, EnergySource i_EnergySource, int i_NumberOfWheels)
            : base(i_LicenseNumber, i_MaxAirPressure, i_EnergySource, i_NumberOfWheels)
        {
        }
    }
}
