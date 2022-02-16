using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleCard> m_VehiclesInGarage;

        public Garage()
        {
            m_VehiclesInGarage = new Dictionary<string, VehicleCard>();
        }

        public Dictionary<string, VehicleCard> VehiclesInGarage
        {
            get
            {
                return m_VehiclesInGarage;
            }
        }

        public void InsertNewVehicle(Dictionary<eVehicleInGarageData, object> i_NewVehicleDetails)
        {
            string licenseNumberOfTheInsertedVehicle = (string)i_NewVehicleDetails[eVehicleInGarageData.VehicleLicenseNumber];

            // Vehicle already exists
            if (IsVehicleInGarage(licenseNumberOfTheInsertedVehicle))
            {
                m_VehiclesInGarage[licenseNumberOfTheInsertedVehicle].m_VehicleStatus = eVehicleStatus.InProcess;
                throw new Exception(string.Format("The Vehicle {0} is already in the garage", licenseNumberOfTheInsertedVehicle));
            }
            else
            {
                Vehicle createdVehicle = VehicleFactory.CreateVehicle(
                    (VehicleFactory.eVehicleType)i_NewVehicleDetails[eVehicleInGarageData.VehicleType],
                    licenseNumberOfTheInsertedVehicle);
                VehicleCard newVehicle = new VehicleCard(
                    (string)i_NewVehicleDetails[eVehicleInGarageData.OwnerPhone],
                    (string)i_NewVehicleDetails[eVehicleInGarageData.OwnerName],
                    createdVehicle);
                m_VehiclesInGarage.Add(licenseNumberOfTheInsertedVehicle, newVehicle);
            }
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, int i_NewVehicleStatus)
        {
            IsInputInsideEnumRange(i_NewVehicleStatus, Enum.GetNames(typeof(eVehicleStatus)).Length);
            m_VehiclesInGarage[i_LicenseNumber].m_VehicleStatus = (eVehicleStatus)i_NewVehicleStatus;
        }

        public List<string> GetAllLicenseNumbersFilteredByStatus(int i_FilterChoice)
        {
            List<string> licenseNumbers = new List<string>();
            if(i_FilterChoice != 0)
            {
                IsInputInsideEnumRange(i_FilterChoice, Enum.GetNames(typeof(eVehicleStatus)).Length);
            }

            foreach (KeyValuePair<string, VehicleCard> vehicle in VehiclesInGarage)
            {
                // if i_FilterChoice is equal to 0 the user wants to get all vehicles regardless their status
                if(i_FilterChoice != 0)
                {
                    if(vehicle.Value.m_VehicleStatus == (eVehicleStatus)i_FilterChoice)
                    {
                        licenseNumbers.Add(string.Format("license: {0}{1}", vehicle.Key, Environment.NewLine));
                    }
                }
                else
                {
                    licenseNumbers.Add(string.Format("license: {0}{1}", vehicle.Key, Environment.NewLine));
                }
            }

            return licenseNumbers;
        }

        public void IsLicenseNumberValid(string i_LicenseNumber)
        {
            if(i_LicenseNumber.Length < 1 || i_LicenseNumber.Length > 8)
            {
                throw new ValueOutOfRangeException(1, 8);
            }

            if(!i_LicenseNumber.All(char.IsDigit))
            {
                throw new FormatException("The license number has to contain only digits");
            }
        }

        public void IsOwnerNameValid(string i_OwnerName)
        {
            if(string.IsNullOrEmpty(i_OwnerName) || !i_OwnerName.All(char.IsLetter))
            {
                throw new FormatException("Owner name not valid");
            }
        }

        public void IsOwnerPhoneValid(string i_OwnerPhone)
        {
            if(!i_OwnerPhone.All(char.IsDigit))
            {
                throw new FormatException("Owner phone number is not valid");
            }
        }

        public void IsInputInsideEnumRange(int i_UserChoice, int i_EnumSize)
        {
            if(i_UserChoice < 1 || i_UserChoice > i_EnumSize)
            {
                throw new ArgumentException("Wrong Selection");
            }
        }

        public void InflateWheels(string i_LicenseNumber)
        {
            foreach(Wheel wheel in m_VehiclesInGarage[i_LicenseNumber].m_Vehicle.Wheels)
            {
                // Inflate to max pressure
                wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, Fuel.eFuelType i_FuelType, float i_AmountToFuel)
        {
            if(IsVehicleInGarage(i_LicenseNumber))
            {
                if(VehiclesInGarage[i_LicenseNumber].m_Vehicle.EnergySource is Fuel)
                {
                    ((Fuel)VehiclesInGarage[i_LicenseNumber].m_Vehicle.EnergySource).SupplyEnergySource(i_AmountToFuel, i_FuelType);
                }
                else
                {
                    throw new ArgumentException("The given vehicle is electric!");
                }
            }
            else
            {
                throw new ArgumentException("There is no vehicle with the given license number in the garage");
            }
        }

        public void RechargeVehicle(string i_LicenseNumber, float i_AmountToFuel)
        {
            if (IsVehicleInGarage(i_LicenseNumber))
            {
                if (VehiclesInGarage[i_LicenseNumber].m_Vehicle.EnergySource is Electric)
                {
                    ((Electric)VehiclesInGarage[i_LicenseNumber].m_Vehicle.EnergySource).Recharge(i_AmountToFuel);
                }
                else
                {
                    throw new ArgumentException("The given vehicle is not electric!");
                }
            }
            else
            {
                throw new ArgumentException("There is no vehicle with the given license number in the garage");
            }
        }

        public Dictionary<eVehicleInGarageData, string> GetUniqueDetailsForTheChosenVehicle(string i_LicenseNumber)
        {
            return m_VehiclesInGarage[i_LicenseNumber].m_Vehicle.GetUniqueInfo();
        }

        public void SetUniqueDetailsForTheChosenVehicle(Dictionary<eVehicleInGarageData, object> i_DetailsToAdd, string i_LicenseNumber)
        {
            m_VehiclesInGarage[i_LicenseNumber].m_Vehicle.SetUniqueInfo(i_DetailsToAdd);
        }

        public void DisplayGarageAvailableVehicleStatus()
        {
            string[] statusStrings = Enum.GetNames(typeof(eVehicleStatus));

            for (int i = 0; i < statusStrings.Length; i++)
            {
                Console.WriteLine(string.Format("({0}) {1}", i + 1, statusStrings[i]));
            }
        }

        public bool IsVehicleInGarage(string i_LicenseNumberOfTheVehicleToFind)
        {
            VehicleCard outParameterForTry;
            return m_VehiclesInGarage.TryGetValue(i_LicenseNumberOfTheVehicleToFind, out outParameterForTry);
        }

        public string DisplayCarColors()
        {
            string[] colors = Enum.GetNames(typeof(Car.eCarColor));
            StringBuilder colorsBuilder = new StringBuilder();

            for (int i = 0; i < colors.Length; i++)
            {
                colorsBuilder.Append(string.Format("{0} {1}", i + 1, colors[i])).AppendLine();
            }

            return colorsBuilder.ToString();
        }

        public int GetCarColorsEnumSize()
        {
            return Enum.GetNames(typeof(Car.eCarColor)).Length;
        }

        public int GetDoorsAmountEnumSize()
        {
            return Enum.GetNames(typeof(Car.eDoorsAmount)).Length;
        }

        public int GetVehiclesEnumSize()
        {
            return Enum.GetNames(typeof(VehicleFactory.eVehicleType)).Length;
        }

        public string DisplayCarDoorsAmount()
        {
            string[] doorsNumber = Enum.GetNames(typeof(Car.eDoorsAmount));
            StringBuilder doorStringBuilder = new StringBuilder();

            for (int i = 0; i < doorsNumber.Length; i++)
            {
                doorStringBuilder.Append(string.Format("{0} {1}", i + 1, doorsNumber[i])).AppendLine();
            }

            return doorStringBuilder.ToString();
        }

        public string DisplayMotorcycleLicenseTypes()
        {
            string[] licenseTypes = Enum.GetNames(typeof(Motorcycle.eMotorcycleLicenseType));
            StringBuilder licenseTypeStringBuilder = new StringBuilder();

            for (int i = 0; i < licenseTypes.Length; i++)
            {
                licenseTypeStringBuilder.Append(string.Format("{0} {1}", i + 1, licenseTypes[i])).AppendLine();
            }

            return licenseTypeStringBuilder.ToString();
        }

        public string GetVehiclesTypeList()
        {
            string[] types = Enum.GetNames(typeof(VehicleFactory.eVehicleType));
            StringBuilder typBuilder = new StringBuilder();
            for (int i = 0; i < types.Length; i++)
            {
                typBuilder.Append(string.Format("{0} {1}", i + 1, types[i])).AppendLine();
            }

            return typBuilder.ToString();
        }

        public enum eVehicleStatus
        {
            InProcess = 1,
            Fixed,
            Done,
        }

        public class VehicleCard
        {
            internal string m_OwnerPhone;
            internal string m_OwnerName;
            internal Vehicle m_Vehicle;
            internal eVehicleStatus m_VehicleStatus;

            public VehicleCard(string i_OwnerPhone, string i_OwnerName, Vehicle i_Vehicle)
            {
                OwnerPhone = i_OwnerPhone;
                OwnerName = i_OwnerName;
                Vehicle = i_Vehicle;
                VehicleStatus = eVehicleStatus.InProcess;
            }

            public string OwnerPhone
            {
                get
                {
                    return m_OwnerPhone;
                }

                set
                {
                    m_OwnerPhone = value;
                }
            }

            public string OwnerName
            {
                get
                {
                    return m_OwnerName;
                }

                set
                {
                    m_OwnerName = value;
                }
            }

            public Vehicle Vehicle
            {
                get
                {
                    return m_Vehicle;
                }

                set
                {
                    m_Vehicle = value;
                }
            }

            public eVehicleStatus VehicleStatus
            {
                get
                {
                    return m_VehicleStatus;
                }

                set
                {
                    m_VehicleStatus = value;
                }
            }

            public override string ToString()
            {
                StringBuilder vehicleInfo = new StringBuilder();

                vehicleInfo.Append(string.Format("Owner Name : {0}", m_OwnerName)).AppendLine();
                vehicleInfo.Append(string.Format("Vehicle Status : {0}", m_VehicleStatus)).AppendLine();
                vehicleInfo.Append(m_Vehicle).AppendLine();
                return vehicleInfo.ToString();
            }
        }
    }
}
