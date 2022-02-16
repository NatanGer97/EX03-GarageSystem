using System;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class VehicleFactory
    {
        private const int k_CarAmountOfWheels = 4;
        private const float k_CarWheelsMaxPressure = 29;
        private const Fuel.eFuelType k_FuelCarFuelType = Fuel.eFuelType.Octan95;
        private const float k_FuelCarMaxFuelTankCapacity = 48;
        private const float k_ElectricCarMaxBatteryCapacity = 2.6f;

        private const int k_MotorcycleAmountOfWheels = 2;
        private const float k_MotorcycleWheelsMaxPressure = 30;
        private const Fuel.eFuelType k_FuelMotorcycleFuelType = Fuel.eFuelType.Octan98;
        private const float k_FuelMotorcycleTankCapacity = 5.8f;
        private const float k_ElectricMotorcycleMaxBatteryCapacity = 2.3f;

        private const int k_TruckAmountOfWheels = 16;
        private const float k_TruckWheelsMaxPressure = 25;
        private const Fuel.eFuelType k_TruckFuelType = Fuel.eFuelType.Soler;
        private const float k_TruckFuelMaxTankCapacity = 130;

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle createdVehicle = null;

            switch(i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    createdVehicle = new FuelCar(
                        i_LicenseNumber,
                        new Fuel(0, k_FuelCarMaxFuelTankCapacity, k_FuelCarFuelType),
                        k_CarWheelsMaxPressure,
                        k_CarAmountOfWheels);
                    break;
                case eVehicleType.FuelMotorcycle:
                    createdVehicle = new FuelMotorcycle(
                        i_LicenseNumber,
                        k_MotorcycleWheelsMaxPressure,
                        new Fuel(0, k_FuelMotorcycleTankCapacity, k_FuelMotorcycleFuelType),
                        k_MotorcycleAmountOfWheels);
                    break;
                case eVehicleType.Truck:
                    createdVehicle = new Truck(
                        i_LicenseNumber,
                        k_TruckWheelsMaxPressure,
                        new Fuel(0, k_TruckFuelMaxTankCapacity, k_TruckFuelType),
                        k_TruckAmountOfWheels);
                    break;
                case eVehicleType.ElectricCar:
                    createdVehicle = new ElectricCar(
                        i_LicenseNumber,
                        new Electric(0, k_ElectricCarMaxBatteryCapacity),
                        k_CarWheelsMaxPressure,
                        k_CarAmountOfWheels);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    createdVehicle = new ElectricMotorcycle(
                        i_LicenseNumber,
                        k_MotorcycleWheelsMaxPressure,
                        new Electric(0, k_ElectricMotorcycleMaxBatteryCapacity),
                        k_MotorcycleAmountOfWheels);
                    break;
            }

            return createdVehicle;
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck,
        }
    }
}
