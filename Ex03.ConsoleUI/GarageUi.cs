using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUi
    {
        private Garage m_Garage = new Garage();

        // $G$ NTT-999 (-5) This method should be private (None of the other classes use this method...)
        public static void DisplayMenuOptions()
        {
            StringBuilder menu = new StringBuilder();

            menu.Append("Please Choose one of the options below:").AppendLine();
            menu.Append("(1) Enter new Vehicle").AppendLine();
            menu.Append("(2) Display Vehicle").AppendLine();
            menu.Append("(3) Display license numbers of vehicles in garage").AppendLine();
            menu.Append("(4) Change vehicle status").AppendLine();
            menu.Append("(5) Supply energy").AppendLine();
            menu.Append("(6) Inflate wheels").AppendLine();
            menu.Append("(0) Exit").AppendLine();
            Console.WriteLine(menu);
        }

        public void Menu()
        {
            bool exitProgram = false;

            DisplayMenuOptions();
            while (!exitProgram && IsMenuChoiceIsValid(out int userChosenAction))
            {
                eUserMenuChoice userMenuChoice = (eUserMenuChoice)userChosenAction;
                Console.Clear();
                try
                {
                    if (Enum.IsDefined(typeof(eUserMenuChoice), userMenuChoice))
                    {
                        switch (userMenuChoice)
                        {
                            case eUserMenuChoice.EnterNewVehicle:
                                enterNewVehicle();
                                break;
                            case eUserMenuChoice.DisplayVehicle:
                                displayVehicle();
                                break;
                            case eUserMenuChoice.DisplayVehiclesLicenseNumbers:
                                displayFilteredVehiclesLicenseNumbers();
                                break;
                            case eUserMenuChoice.SupplyEnergy:
                                supplyEnergySource();
                                break;
                            case eUserMenuChoice.ChangeVehicleStatus:
                                changeVehicleStatus();
                                break;
                            case eUserMenuChoice.InflateWheels:
                                inflateWheels();
                                break;
                            case eUserMenuChoice.Exit:
                                exitProgram = true;
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("An error has occurred , press on any key to return to the main menu");
                    Console.ReadLine();
                    Console.Clear();
                }

                Console.Clear();
                DisplayMenuOptions();
            }
        }

        // $G$ NTT-999 (-5) This method should be private (None of the other classes used this method...)
        public bool IsMenuChoiceIsValid(out int io_Input)
        {
            bool isValid = false;

            io_Input = 0;
            while(!isValid)
            {
                bool isDigit = int.TryParse(Console.ReadLine(), out io_Input);

                if(isDigit && Enum.IsDefined(typeof(eUserMenuChoice), io_Input))
                {
                    isValid = true;
                    break;
                }

                Console.WriteLine("invalid Choice, try again");
            }

            return isValid;
        }

        private void inflateWheels()
        {
            getLicenseNumberFromUser(out string userInput);
            if(!m_Garage.IsVehicleInGarage(userInput))
            {
                throw new ArgumentException("There is no vehicle with the given license in the garage");
            }

            m_Garage.InflateWheels(userInput);
            displayEndOfMenuOptionMessage("Successfully inflated all wheels!");
        }

        private void getLicenseNumberFromUser(out string io_ValidInput)
        {
            io_ValidInput = string.Empty;

            Console.WriteLine("Enter license number");
            io_ValidInput = Console.ReadLine();
            m_Garage.IsLicenseNumberValid(io_ValidInput);
        }

        private eSupplyingEnergyMethod chooseEnergySupplyingMethod()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("(1) Refuel vehicle").AppendLine();
            stringBuilder.Append("(2) Recharge vehicle").AppendLine();
            Console.WriteLine(stringBuilder);
            if(int.TryParse(Console.ReadLine(), out int userChoice))
            {
                if(!Enum.IsDefined(typeof(eSupplyingEnergyMethod), userChoice))
                {
                    throw new ArgumentException("Wrong choice");
                }
            }
            else
            {
                throw new FormatException("Wrong input , you have to enter number");
            }

            return (eSupplyingEnergyMethod)userChoice;
        }

        private Fuel.eFuelType chooseTypeOfFuel()
        {
            int fuelTypesSize = Enum.GetNames(typeof(Fuel.eFuelType)).Length;
            string[] fuelTypes = Enum.GetNames(typeof(Fuel.eFuelType));

            Console.WriteLine("Choose the type of fuel :");

            for(int i = 0; i < fuelTypesSize; i++)
            {
                Console.WriteLine(string.Format("({0}) {1}", i + 1, fuelTypes[i]));
            }

            if(!int.TryParse(Console.ReadLine(), out int userChoice))
            {
                throw new FormatException("Wrong input , you have to enter a number");
            }

            m_Garage.IsInputInsideEnumRange(userChoice, Enum.GetNames(typeof(Fuel.eFuelType)).Length);
            return (Fuel.eFuelType)userChoice;
        }

        private void displayEndOfMenuOptionMessage(string i_MenuOptionMessage)
        {
            Console.WriteLine(i_MenuOptionMessage);
            Console.WriteLine("Press any key to go back to the menu");
            Console.ReadLine();
        }

        // $G$ DSN-999 (-5) If this Enum is a nested enum of this class it shuold be private. Otherwise it should be in a seperate file.
        public enum eSupplyingEnergyMethod
        {
            Refuel = 1,
            Recharge,
        }

        private void supplyEnergySource()
        {
            getLicenseNumberFromUser(out string enteredVehicleLicenseNumber);
            if (m_Garage.IsVehicleInGarage(enteredVehicleLicenseNumber))
            {
                Console.WriteLine("Enter the amount to supply :");
                float amountToSupply = float.Parse(Console.ReadLine());
                switch(chooseEnergySupplyingMethod())
                {
                    case eSupplyingEnergyMethod.Refuel:
                        m_Garage.RefuelVehicle(enteredVehicleLicenseNumber, chooseTypeOfFuel(), amountToSupply);
                        break;
                    case eSupplyingEnergyMethod.Recharge:
                        m_Garage.RechargeVehicle(enteredVehicleLicenseNumber, amountToSupply);
                        break;
                }
            }
            else
            {
                throw new FormatException("Vehicle with the given license number does not exist..");
            }

            displayEndOfMenuOptionMessage("Successfully supplied energy source");
        }


        private void changeVehicleStatus()
        {
            getLicenseNumberFromUser(out string enteredVehicleLicenseNumber);
            if(m_Garage.IsVehicleInGarage(enteredVehicleLicenseNumber))
            {
                m_Garage.DisplayGarageAvailableVehicleStatus();
                int.TryParse(Console.ReadLine(), out int userChoice);
                m_Garage.IsInputInsideEnumRange(userChoice, Enum.GetNames(typeof(Garage.eVehicleStatus)).Length);
                m_Garage.ChangeVehicleStatus(enteredVehicleLicenseNumber, userChoice);
            }

            displayEndOfMenuOptionMessage("Successfully change vehicle status");
        }

        private void displayFilteredVehiclesLicenseNumbers()
        {
            Console.WriteLine("Choose the filter parameter");
            Console.WriteLine("(0) Get All");
            m_Garage.DisplayGarageAvailableVehicleStatus();
            if(!int.TryParse(Console.ReadLine(), out int userChoice))
            {
                throw new FormatException("Wrong input");
            }

            List<string> licenseNumbers = m_Garage.GetAllLicenseNumbersFilteredByStatus(userChoice);

            foreach(string licenseNumber in licenseNumbers)
            {
                Console.WriteLine(licenseNumber);
            }

            if(licenseNumbers.Count == 0)
            {
                Console.WriteLine("Currently there are no vehicles with the selected status in the garage");
            }

            displayEndOfMenuOptionMessage(string.Empty);
        }

        private void displayVehicle()
        {
            getLicenseNumberFromUser(out string userInput);
            if (m_Garage.IsVehicleInGarage(userInput))
            {
                Console.WriteLine(m_Garage.VehiclesInGarage[userInput].ToString());
            }
            else
            {
                throw new ArgumentException("Vehicle does not exist");
            }

            displayEndOfMenuOptionMessage(string.Empty);
        }

        private Dictionary<eVehicleInGarageData, object> getDataFromUser(out string io_LicenseNumber)
        {
            io_LicenseNumber = string.Empty;
            Dictionary<eVehicleInGarageData, object> newVehicleDetails = new Dictionary<eVehicleInGarageData, object>();

            getLicenseNumberFromUser(out io_LicenseNumber);
            Console.WriteLine("Enter owner Name");
            string ownerName = Console.ReadLine();
            m_Garage.IsOwnerNameValid(ownerName);
            Console.WriteLine("Enter owner Phone");
            string ownerPhone = Console.ReadLine();
            m_Garage.IsOwnerPhoneValid(ownerPhone);
            newVehicleDetails.Add(eVehicleInGarageData.OwnerName, ownerName);
            newVehicleDetails.Add(eVehicleInGarageData.OwnerPhone, ownerPhone);
            newVehicleDetails.Add(eVehicleInGarageData.VehicleType, getUserVehicleType());
            newVehicleDetails.Add(eVehicleInGarageData.VehicleLicenseNumber, io_LicenseNumber);

            return newVehicleDetails;
        }

        private int getUserVehicleType()
        {
            Console.WriteLine("Choose one of the following types:");
            Console.WriteLine(m_Garage.GetVehiclesTypeList());
            if(!int.TryParse(Console.ReadLine(), out int userChoice))
            {
                throw new FormatException("Wrong input");
            }

            m_Garage.IsInputInsideEnumRange(userChoice, m_Garage.GetVehiclesEnumSize());
            return userChoice;
        }

        private void enterNewVehicle()
        {
            m_Garage.InsertNewVehicle(getDataFromUser(out string newVehicleLicenseNumber));
            try
            {
                m_Garage.SetUniqueDetailsForTheChosenVehicle(getExtraDetailsFromUser(newVehicleLicenseNumber), newVehicleLicenseNumber);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                m_Garage.VehiclesInGarage.Remove(newVehicleLicenseNumber);
                throw new Exception("Wrong input");
            }

            displayEndOfMenuOptionMessage("Vehicle entered successfully!");
        }

        private void isSomethingEntered(object i_Object)
        {
            if(i_Object.ToString().Length == 0)
            {
                throw new FormatException("No input entered");
            }
        }
        
        private void getInputByKey(string i_DetailString, eVehicleInGarageData i_DetailKey, ref Dictionary<eVehicleInGarageData, object> io_newDetailsDict)
        {
            switch(i_DetailKey)
            {
                case eVehicleInGarageData.VehicleModel:
                    Console.WriteLine(i_DetailString);
                    string modelInput = Console.ReadLine();
                    isSomethingEntered(modelInput);
                    io_newDetailsDict.Add(eVehicleInGarageData.VehicleModel, modelInput);
                    break;
                case eVehicleInGarageData.VehicleWheelsManufacturerName:
                    Console.WriteLine(i_DetailString);
                    string manufacturerInput = Console.ReadLine();
                    isSomethingEntered(manufacturerInput);
                    io_newDetailsDict.Add(eVehicleInGarageData.VehicleWheelsManufacturerName, manufacturerInput);
                    break;
                case eVehicleInGarageData.CarColor:
                    Console.WriteLine(i_DetailString);
                    Console.WriteLine(m_Garage.DisplayCarColors());
                    int carColorInput = int.Parse(Console.ReadLine());
                    m_Garage.IsInputInsideEnumRange(carColorInput, m_Garage.GetCarColorsEnumSize());
                    io_newDetailsDict.Add(eVehicleInGarageData.CarColor, carColorInput);
                    break;
                case eVehicleInGarageData.CarDoors:
                    Console.WriteLine(i_DetailString);
                    Console.WriteLine(m_Garage.DisplayCarDoorsAmount());
                    int amountOfDoors = int.Parse(Console.ReadLine());
                    m_Garage.IsInputInsideEnumRange(amountOfDoors, m_Garage.GetDoorsAmountEnumSize());
                    io_newDetailsDict.Add(eVehicleInGarageData.CarDoors, amountOfDoors);
                    break;
                case eVehicleInGarageData.MotorcycleLicenseType:
                    Console.WriteLine(i_DetailString);
                    Console.WriteLine(m_Garage.DisplayMotorcycleLicenseTypes());
                    int motorcycleLicenseTypeInput = int.Parse(Console.ReadLine());
                    io_newDetailsDict.Add(eVehicleInGarageData.MotorcycleLicenseType, motorcycleLicenseTypeInput);
                    break;
                case eVehicleInGarageData.MotorcycleEngineCc:
                    Console.WriteLine(i_DetailString);
                    int motorcycleEngineCcInput = int.Parse(Console.ReadLine());
                    io_newDetailsDict.Add(eVehicleInGarageData.MotorcycleEngineCc, motorcycleEngineCcInput);
                    break;
                case eVehicleInGarageData.TruckCargoCapacity:
                    Console.WriteLine(i_DetailString);
                    float truckCargoCapacityInput = float.Parse(Console.ReadLine());
                    io_newDetailsDict.Add(eVehicleInGarageData.TruckCargoCapacity, truckCargoCapacityInput);
                    break;
                case eVehicleInGarageData.TruckIsRefrigerating:
                    Console.WriteLine(i_DetailString);
                    Console.WriteLine("Enter 0 - Yes or 1 - No");
                    bool isTruckRefrigerating = int.Parse(Console.ReadLine()) == 1;
                    io_newDetailsDict.Add(eVehicleInGarageData.TruckIsRefrigerating, isTruckRefrigerating);
                    break;
            }
        }

        private Dictionary<eVehicleInGarageData, object> getExtraDetailsFromUser(string i_LicenseNumber)
        {
            Dictionary<eVehicleInGarageData, string> missingInfoQuestions = m_Garage.GetUniqueDetailsForTheChosenVehicle(i_LicenseNumber);
            Dictionary<eVehicleInGarageData, object> newDetailsToAdd = new Dictionary<eVehicleInGarageData, object>();

            try
            {
                foreach (var question in missingInfoQuestions)
                {
                    eVehicleInGarageData detailKey = question.Key;
                    string detailString = question.Value;
                    getInputByKey(detailString, detailKey, ref newDetailsToAdd);
                }
            }
            catch(FormatException)
            {
                throw new Exception("Wrong input while adding new vehicle");
            }

            return newDetailsToAdd;
        }
        
        // $G$ DSN-999 (-5) If this Enum is a nested enum of this class it shuold be private. Otherwise it should be in a seperate file.
        public enum eUserMenuChoice
        {
            Exit,
            EnterNewVehicle,
            DisplayVehicle,
            DisplayVehiclesLicenseNumbers,
            ChangeVehicleStatus,
            SupplyEnergy,
            InflateWheels,
        }
    }
}
