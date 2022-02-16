using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private float m_CargoVolume;
        private bool m_IsRefrigerated;

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }

            set
            {
                m_CargoVolume = value;
            }
        }

        public bool IsRefrigerated
        {
            get
            {
                return m_IsRefrigerated;
            }

            set
            {
                m_IsRefrigerated = value;
            }
        }

        public Truck(string i_LicenseNumber, float i_MaxAirPressure, EnergySource i_EnergySource, int i_NumberOfWheels)
            : base(i_LicenseNumber, i_MaxAirPressure, i_EnergySource, i_NumberOfWheels)
        {
        }

        public sealed override Dictionary<eVehicleInGarageData, string> GetUniqueInfo()
        {
            Dictionary<eVehicleInGarageData, string> extraDetailsDict = base.GetUniqueInfo();

            extraDetailsDict.Add(eVehicleInGarageData.TruckCargoCapacity, "Enter the truck cargo capacity :");
            extraDetailsDict.Add(eVehicleInGarageData.TruckIsRefrigerating, "Does the truck Refrigerating :");
            return extraDetailsDict;
        }

        public sealed override void SetUniqueInfo(Dictionary<eVehicleInGarageData, object> i_DetailsToAdd)
        {
            CargoVolume = (float)i_DetailsToAdd[eVehicleInGarageData.TruckCargoCapacity];
            IsRefrigerated = (bool)i_DetailsToAdd[eVehicleInGarageData.TruckIsRefrigerating];
            base.SetUniqueInfo(i_DetailsToAdd);
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.Append(base.ToString()).AppendLine();
            vehicleInfo.Append(string.Format("Cargo volume : {0}", CargoVolume)).AppendLine();
            vehicleInfo.Append(string.Format("Refrigerating truck : {0}", IsRefrigerated));
            return vehicleInfo.ToString();
        }
    }
}
