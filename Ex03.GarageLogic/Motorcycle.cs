using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    // $G$ DSN-999 (-7) if electricMotorcycle and FuelMotorcycle inherit from Motorcycle then they shouldn't recive from the UI the EnergySource but send it by themself. In this implementation it is possible to send Fuel as EnergySource to electricMotorcycle and vice versa. 
    internal abstract class Motorcycle : Vehicle
    {
        private int m_EngineCc;
        private eMotorcycleLicenseType m_LicenseType;

        public int EngineCc
        {
            get
            {
                return m_EngineCc;
            }

            set
            {
                m_EngineCc = value;
            }
        }

        public eMotorcycleLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        protected Motorcycle(string i_LicenseNumber, float i_MaxAirPressure, EnergySource i_EnergySource, int i_NumberOfWheels)
            : base(i_LicenseNumber, i_MaxAirPressure, i_EnergySource, i_NumberOfWheels)
        {
        }

        public sealed override Dictionary<eVehicleInGarageData, string> GetUniqueInfo()
        {
            Dictionary<eVehicleInGarageData, string> extraDetailsDict = base.GetUniqueInfo();

            extraDetailsDict.Add(eVehicleInGarageData.MotorcycleEngineCc, "Enter engine cc :");
            extraDetailsDict.Add(eVehicleInGarageData.MotorcycleLicenseType, "Enter type of motorcycle license :");
            return extraDetailsDict;
        }

        public sealed override void SetUniqueInfo(Dictionary<eVehicleInGarageData, object> i_DetailsToAdd)
        {
            EngineCc = (int)i_DetailsToAdd[eVehicleInGarageData.MotorcycleEngineCc];
            LicenseType = (eMotorcycleLicenseType)i_DetailsToAdd[eVehicleInGarageData.MotorcycleLicenseType];
            base.SetUniqueInfo(i_DetailsToAdd);
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.Append(base.ToString()).AppendLine();
            vehicleInfo.Append(string.Format("Motorcycle license type : {0}", LicenseType)).AppendLine();
            vehicleInfo.Append(string.Format("Engine CC : {0}", EngineCc));
            return vehicleInfo.ToString();
        }

        public enum eMotorcycleLicenseType
        {
            A = 1,
            A2,
            AA,
            B,
        }
    }
}
