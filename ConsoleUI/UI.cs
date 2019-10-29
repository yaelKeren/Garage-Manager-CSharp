using System;
using System.Collections.Generic;
using System.Text;
using GarageLogic;
using Enums;

namespace ConsoleUI
{
    public class UI
    {
        private const string k_FuelAmountFormat = "Fuel In Litters. ";
        private const string k_ElecAmountFormat = "of remaining power in minutes. ";
        private const string k_ErrInvalidInput = "Invalid Input.";
        private const string k_ErrBlankLine = "You must provide the required information. ";
        private const string k_ErrOnlyLetters = "Invalid Input. Please enter only letters. ";
        private const string k_ErrOnlyNumbers = "Invalid Input. Please enter only numbers. ";
        private const string k_EnergyAddedSuccessfully = "Energy was successfully added. ";
        private const string k_ErrFindSuitableVehicles = "Could not find suitable vehicles. ";
        private const int k_Empty = 0;
        private const int k_PhoneNumberLenght = 10;
        private const int k_MenuFirstOption = 1;
        private const int k_MenuLastOption = 8;

        internal static void DisplayMenu()
        {
            Console.WriteLine(string.Format(@"Please select one of the options bellow:
1. Add new vehicle
2. Present all vehicles by license number
3. Change vehicle state
4. Inflate wheels to maximum
5. Refuel a vehicle
6. Charge an electric vehicle
7. Display vehicle data
8. Exit"));
        }

        internal static int GetUserSelection()
        {
                string userSelectionStr = Console.ReadLine();
                int userSelection;
                bool isValidInput = int.TryParse(userSelectionStr, out userSelection);

                if (isValidInput)
                {
                    if (userSelection < k_MenuFirstOption || userSelection > k_MenuLastOption)
                    {
                        throw new ValueOutOfRangeException(k_MenuLastOption, k_MenuFirstOption);
                    }

                    return userSelection;
                }
                else
                {
                    throw new FormatException(k_ErrOnlyNumbers);
                }
        }

        //this method take license number from the user
        internal static string GetLicenseNumber()
        {
            Console.WriteLine("Please Enter license number: ");
            string licenseNumber = Console.ReadLine();

            while (licenseNumber.Equals(string.Empty))
            {
                PrintMessage(k_ErrBlankLine);
                licenseNumber = Console.ReadLine();
            }

            return licenseNumber;
        }

        //get owner's name from console
        internal static string GetOwnerName()
        {
            Console.WriteLine("Please Enter the owner's name: ");
            string ownerName = getName();

            return ownerName;
        }

        //get owner's phone number from console
        internal static string GetOwnerPhoneNumber()
        {
            Console.WriteLine("Please Enter the owner's phone number({0} digits): ", 
                k_PhoneNumberLenght);

            string ownerPhone = string.Empty;
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    ownerPhone = getPhoneNumber();
                    isValidInput = true;
                }
                catch (ArgumentException ex)
                {
                   PrintMessage(ex.Message);
                }
            }

            return ownerPhone;
        }

        private static string getPhoneNumber()
        {
            string phoneNumber = Console.ReadLine();

            if (phoneNumber.Length != 10)
            {
                StringBuilder phoneErrMsg = new StringBuilder("Please enter ").Append(k_PhoneNumberLenght).
                    Append(" digits: ");

                throw new ArgumentException(phoneErrMsg.ToString());
            }

            foreach (char letter in phoneNumber)
            {
                if (letter < '0' || letter > '9')
                {
                    Console.WriteLine(k_ErrOnlyNumbers);
                    break;
                }
            }
            
            return phoneNumber;
        }

        //get model name from console
        internal static eVehicleModel GetModelName()
        {
            int numberOfOptions = Enum.GetNames(typeof(eVehicleModel)).Length;

            Console.WriteLine("Choose a vehicle:");
            foreach (eVehicleModel model in Enum.GetValues(typeof(eVehicleModel)))
            {
                Console.WriteLine("{0}. {1}", (int)model, VehicleModelToText.AsText(model));
            }

            int userInput;
            bool tryParse = int.TryParse(Console.ReadLine(), out userInput);

            if (!tryParse)
            {
                throw new FormatException(k_ErrInvalidInput);
            }

            if (userInput < k_MenuFirstOption || userInput > numberOfOptions)
            {
                throw new ValueOutOfRangeException(numberOfOptions, k_MenuFirstOption);
            }
            return (eVehicleModel)userInput;
        }

        //get float with the current energy that remain in precent
        internal static float GetRemainingPercentageEnergy(string i_KindOfVehicle,
            int i_MaxValue, int i_MinValue)
        {
            Console.WriteLine(string.Format("Enter the precent of the remain energy in your {0} :", 
                i_KindOfVehicle));

            float userInput;
            bool tryParse = float.TryParse(Console.ReadLine(), out userInput);

                if (!tryParse)
                {
                    throw new FormatException(k_ErrInvalidInput);
                }
                else if(userInput < i_MinValue || userInput > i_MaxValue)
                {
                    throw new ValueOutOfRangeException(i_MaxValue, i_MinValue);
                }

                return userInput;
        }

        //get string of a name input from user
        private static string getName()
        {
            string name = string.Empty;
            bool isValid = false;

            while (!isValid)
            {
                name = Console.ReadLine();

                if (name.Equals(string.Empty) || name.Equals(Environment.NewLine))
                {
                    Console.WriteLine(k_ErrOnlyLetters);
                }

                foreach (char letter in name)
                {
                    if (char.IsDigit(letter))
                    {
                        isValid = false;
                        Console.WriteLine(k_ErrOnlyLetters);
                        break;
                    }

                    isValid = true;
                }
            }

            return name;
        }

        //get float input of preseure for current wheel
        internal static float GetWheelPressure(int i_WheelNumber, float i_MaxPreseure)
        {
            Console.WriteLine(string.Format("Enter a pressure of the {0} wheel: ", i_WheelNumber));

            float userInput;
            bool isValid = float.TryParse(Console.ReadLine(), out userInput);

            if (!isValid)
            {
                throw new FormatException(k_ErrInvalidInput);
            }

            if (i_MaxPreseure < userInput)
            {
                int minPreseure = 0;
                throw new ValueOutOfRangeException(i_MaxPreseure, minPreseure);
            }

            return userInput;
        }

        internal static void PrintMessage(string i_Message)
        {
            Console.WriteLine("{0} {1}", i_Message, Environment.NewLine);
        }

        internal static string GetWheelManufacturer(int i_WheelNumber)
        {
            Console.WriteLine(string.Format("Enter the manufacturer name of the {0} wheel: ", i_WheelNumber));
            return getName();
        }

        internal static eLicenseType GetKindOfLicense()
        {
            int userInput;
            int numberOfOptions = Enum.GetNames(typeof(eLicenseType)).Length - 1;

            Console.WriteLine("Please choose a license type:");
            foreach (eLicenseType licenseType in Enum.GetValues(typeof(eLicenseType)))
            {
                if (!licenseType.Equals(eLicenseType.Undefined))
                {
                    Console.WriteLine("{0}. {1}", (int)licenseType, eLicenseTypeToText.AsText(licenseType));
                }
            }

            getAndCheckEnumInputFromUser(numberOfOptions, out userInput);

            return (eLicenseType)userInput;
        }

        internal static int GetEngineVolume()
        {
            Console.WriteLine("Please enter Cc engine volume: ");

            int userInput;
            bool tryParse = int.TryParse(Console.ReadLine(), out userInput);

            if (!tryParse)
            {
                throw new FormatException(k_ErrOnlyNumbers);
            }

            return userInput;
        }

        internal static eCarEnums.kindOfColor GetCarColor()
        {
            int userInput;
            int numberOfOptions = Enum.GetNames(typeof(eCarEnums.kindOfColor)).Length - 1;

            Console.WriteLine("Please choose a color:");
            foreach (eCarEnums.kindOfColor color in Enum.GetValues(typeof(eCarEnums.kindOfColor)))
            {
                if (!color.Equals(eCarEnums.kindOfColor.Undefined))
                {
                    Console.WriteLine("{0}. {1}", (int)color, eCarEnums.CarEnumsToText.AsText(color));
                }
            }

            getAndCheckEnumInputFromUser(numberOfOptions, out userInput);

            return (eCarEnums.kindOfColor)userInput;
        }

        internal static eCarEnums.NumberOfDoors GetNumberOfDoors()
        {
            int userInput;
            int numberOfOptions = Enum.GetNames(typeof(eCarEnums.NumberOfDoors)).Length - 1;

            Console.WriteLine("Please choose a number of doors:");
            foreach (eCarEnums.NumberOfDoors numberOfDoors in Enum.GetValues(typeof(eCarEnums.NumberOfDoors)))
            {
                if (!numberOfDoors.Equals(eCarEnums.NumberOfDoors.Undefined))
                {
                    Console.WriteLine("{0}. {1}", (int)numberOfDoors, eCarEnums.CarEnumsToText.AsText(numberOfDoors));
                }
            }

            getAndCheckEnumInputFromUser(numberOfOptions, out userInput);

            return (eCarEnums.NumberOfDoors)userInput;
        }

        internal static bool IsTruckCarryingHazardousMaterials()
        {
            Console.WriteLine("Is the car carry a hazardous materials? Y/N");
            string userInput = Console.ReadLine();
            bool isTruckCarryingHazardousMaterials = userInput.ToUpper().Equals("Y");

            while (!userInput.ToUpper().Equals("Y") && !userInput.ToUpper().Equals("N"))
            {
                Console.WriteLine("Please enter Y/N only.");
                userInput = Console.ReadLine();
            }

            return isTruckCarryingHazardousMaterials;
        }

        internal static float GetCargoVolume()
        {
            Console.WriteLine("Enter your truck cargo volume: ");

            float userInput;
            bool isValidInput = float.TryParse(Console.ReadLine(), out userInput);

            while (!isValidInput)
            {
                Console.WriteLine(k_ErrOnlyNumbers);
                isValidInput = float.TryParse(Console.ReadLine(), out userInput);
            }

            return userInput;
        }

        //print that the vehicle is already in our garage
        internal static void VehicleExist()
        {
            Console.WriteLine("The vehicle is already in the garage. It's state is 'Fix'. {0} ",
                Environment.NewLine);
        }

        internal static void VehicleAdded()
        {
            Console.WriteLine("Vehicle added to the garage! {0} ", Environment.NewLine);
        }
        
        //get state to sort the vehicles
        internal static eVehicleState GetSortState()
        {
            int userInput;
            bool isFiltered = checkIfUserWantsFilter();

            if (isFiltered)
            {
                int numberOfOptions = Enum.GetNames(typeof(eVehicleState)).Length - 1;

                displayStatesToChoose();
                getAndCheckEnumInputFromUser(numberOfOptions, out userInput);
            }
            else
            {
                userInput = (int)eVehicleState.Undefined;
            }

            return (eVehicleState)userInput;
        }

        //print the vehicle states in our garage
        private static void displayStatesToChoose()
        {
            Console.WriteLine("Please choose a state:");
            foreach (eVehicleState state in Enum.GetValues(typeof(eVehicleState)))
            {
                if (!state.Equals(eVehicleState.Undefined))
                {
                    Console.WriteLine("{0}. {1}", (int)state, VehicleStateToText.AsText(state));
                }
            }
        }

        internal static eVehicleState GetNewState()
        {
            int userInput;
            int numberOfOptions = Enum.GetNames(typeof(eVehicleState)).Length - 1;

            displayStatesToChoose();
            getAndCheckEnumInputFromUser(numberOfOptions, out userInput);

            return (eVehicleState)userInput;
        }

        internal static void ChangeVehicleStateSuccess()
        {
            Console.WriteLine("Vehicle State Changed! {0}", Environment.NewLine);
        }

        internal static void RefuelVehicleSuccess()
        {
            Console.WriteLine("Refuel vehicle successed! {0}", Environment.NewLine);
        }

        internal static void ChargeVehicleSuccess()
        {
            Console.WriteLine("Charge vehicle successed! {0}", Environment.NewLine);
        }

        internal static float GetChargingTimeInMinutes()
        {
            Console.WriteLine("Please enter charging time in minutes: ");

            float chargingTimeInMinutes;
            bool tryParse = float.TryParse(Console.ReadLine(), out chargingTimeInMinutes);

            if (!tryParse)
            {
                throw new FormatException(k_ErrOnlyNumbers);
            }

            return chargingTimeInMinutes;
        }

        private static bool checkIfUserWantsFilter()
        {
            Console.WriteLine("Would you like to filter by state? Y/N");
            string userChoice = Console.ReadLine();

            while (!userChoice.ToUpper().Equals("Y") && !userChoice.ToUpper().Equals("N"))
            {
                Console.WriteLine("Please enter Y/N only.");
                userChoice = Console.ReadLine();
            }

            return userChoice.ToUpper().Equals("Y");
        }


        // $G$ CSS-999 (-5) If you use string as a condition --> then you should have use constant here.
        internal static void DisPlayLicenseNumbers(List<string> i_SortedLicenseNumbersByState,
                   eVehicleState i_SelectedState)
        {
            if (i_SortedLicenseNumbersByState.Count != k_Empty)
            {
                StringBuilder sorted = new StringBuilder(", sorted by the state '");
                sorted.Append(i_SelectedState).Append("'");

                Console.WriteLine("The license numbers of the vehicles in the garage{0} are:",
                   i_SelectedState != eVehicleState.Undefined ? sorted.ToString() : "");
                i_SortedLicenseNumbersByState.ForEach(Console.WriteLine);
                Console.Write(Environment.NewLine);
            }
            else
            {
                throw new ArgumentException(k_ErrFindSuitableVehicles);
            }
        }

        internal static void InflateWheelsToMaxSuccessed()
        {
            Console.WriteLine("Inflate wheels to max successed! {0}", Environment.NewLine);
        }

        internal static eKindOfFuel GetFuelType()
        {
            int userInput;
            int numberOfOptions = Enum.GetNames(typeof(eKindOfFuel)).Length;

            displayFuelTypesToChoose();
            getAndCheckEnumInputFromUser(numberOfOptions, out userInput);

            return (eKindOfFuel)userInput;
        }

        private static void displayFuelTypesToChoose()
        {
            Console.WriteLine("Please choose a fuel type: ");
            foreach (eKindOfFuel fuelType in Enum.GetValues(typeof(eKindOfFuel)))
            {
                Console.WriteLine("{0}. {1}", (int)fuelType, KindOfFuelEnumToText.AsText(fuelType));
            }
        }

        private static void getAndCheckEnumInputFromUser(int i_NumberOfOptions, out int io_UserInput)
        {
            bool tryParse = int.TryParse(Console.ReadLine(), out io_UserInput);

            if (!tryParse)
            {
                throw new FormatException(k_ErrOnlyNumbers);
            }

            if (io_UserInput < k_MenuFirstOption || io_UserInput > i_NumberOfOptions)
            {
                throw new ValueOutOfRangeException(i_NumberOfOptions, k_MenuFirstOption);
            }
        }

        internal static float GetFuelQuantityInLiters()
        {
            Console.WriteLine("Please enter amount of fuel to fill: ");

            float FuelQuantityInLiters;
            bool tryParse = float.TryParse(Console.ReadLine(), out FuelQuantityInLiters);

            if (!tryParse)
            {
                throw new FormatException(k_ErrOnlyNumbers);
            }

            return FuelQuantityInLiters;
        }

        internal static void DisPlayVehicleData(string i_VehicleInfo)
        {
            Console.WriteLine("{0} {1}", i_VehicleInfo, Environment.NewLine);
        }

        public static void close()
        {
            Console.WriteLine("Thank you, bye bye!");
        }
    }
}

