using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    // $G$ DSN-999 (-7) if electricCar and Fuelcar inherit from car then they shouldn't recive from the UI the EnergySource but send it by themself. In this implementation it is possible to send Fuel as EnergySource to electricCar and vice versa. 
    internal abstract class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eDoorsAmount m_NumberOfDoors;

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }

        public eDoorsAmount NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                m_NumberOfDoors = value;
            }
        }

        protected Car(string i_LicenseNumber,  EnergySource i_EnergySource, float i_MaxWheelPressure, int i_AmountOfWheels)
            : base(i_LicenseNumber, i_MaxWheelPressure, i_EnergySource, i_AmountOfWheels)
        {
        }

        public enum eCarColor
        {
            Red = 1,
            Blue,
            Black,
            White,
        }

        public enum eDoorsAmount
        {
            TwoDoors = 1,
            ThreeDoors,
            FourDoors,
            FiveDoors,
        }

        public sealed override Dictionary<eVehicleInGarageData, string> GetUniqueInfo()
        {
            Dictionary<eVehicleInGarageData, string> extraDetailsDict = base.GetUniqueInfo();

            extraDetailsDict.Add(eVehicleInGarageData.CarColor, "Enter the car color :");
            extraDetailsDict.Add(eVehicleInGarageData.CarDoors, "Enter the car doors amount :");
            return extraDetailsDict;
        }

        public sealed override void SetUniqueInfo(Dictionary<eVehicleInGarageData, object> i_DetailsToAdd)
        {
            CarColor = (eCarColor)i_DetailsToAdd[eVehicleInGarageData.CarColor];
            NumberOfDoors = (eDoorsAmount)i_DetailsToAdd[eVehicleInGarageData.CarDoors];
            base.SetUniqueInfo(i_DetailsToAdd);
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.Append(base.ToString()).AppendLine();
            vehicleInfo.Append(string.Format("Car color : {0}", CarColor)).AppendLine();
            vehicleInfo.Append(string.Format("Number of Doors : {0}", NumberOfDoors));
            return vehicleInfo.ToString();
        }
    }
}
