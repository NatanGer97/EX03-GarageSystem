using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    // $G$ DSN-999 (-7) parameters that are constant in each vehicle type should come from concrete vehicle constructor and not from the fuctory/ UI.
    public abstract class Vehicle
    {
        private readonly float r_MaxAirPressure;
        private string m_ModelName;
        private string m_LicenseNumber;
        private EnergySource m_EnergySource;
        private List<Wheel> m_Wheels;

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }

            set
            {
                m_EnergySource = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        protected Vehicle(string i_LicenseNumber, float i_MaxAirPressure, EnergySource i_EnergySource, int i_NumberOfWheels)
        {
            m_LicenseNumber = i_LicenseNumber;
            r_MaxAirPressure = i_MaxAirPressure;
            m_EnergySource = i_EnergySource;
            createWheels(i_NumberOfWheels);
        }

        private void createWheels(int i_AmountOfWheelsToCreate)
        {
            m_Wheels = new List<Wheel>(i_AmountOfWheelsToCreate);

            for(int i = 0; i < i_AmountOfWheelsToCreate; i++)
            {
                m_Wheels.Add(new Wheel(MaxAirPressure));
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();
            int i = 0;

            vehicleInfo.Append(string.Format("License Number:{0}", LicenseNumber)).AppendLine();
            vehicleInfo.Append(string.Format("Car Model:{0}", ModelName)).AppendLine();
            foreach (Wheel wheel in m_Wheels)
            {
                vehicleInfo.Append(string.Format("Wheel[{0}]:{1}", ++i, wheel)).AppendLine();
            }

            vehicleInfo.Append(string.Format("Remaining Energy:{0}%", m_EnergySource.CurrentEnergyAmountPercentage)).AppendLine();
            vehicleInfo.Append(string.Format("Energy details : {0}", m_EnergySource));
            return vehicleInfo.ToString();
        }

        public virtual Dictionary<eVehicleInGarageData, string> GetUniqueInfo()
        {
            Dictionary<eVehicleInGarageData, string> extraDetailsDictionary = new Dictionary<eVehicleInGarageData, string>();

            extraDetailsDictionary.Add(eVehicleInGarageData.VehicleModel, "Enter your vehicle model :");
            extraDetailsDictionary.Add(eVehicleInGarageData.VehicleWheelsManufacturerName, "Enter the wheels manufacturer name :");
            return extraDetailsDictionary;
        }

        public virtual void SetUniqueInfo(Dictionary<eVehicleInGarageData, object> i_DetailsToAdd)
        {
            ModelName = (string)i_DetailsToAdd[eVehicleInGarageData.VehicleModel];
            foreach(var wheel in Wheels)
            {
                wheel.ManufacturerName = (string)i_DetailsToAdd[eVehicleInGarageData.VehicleWheelsManufacturerName];
            }
        }
    }
}
