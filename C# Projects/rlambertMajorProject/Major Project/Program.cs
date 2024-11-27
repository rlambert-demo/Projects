//Purpose: A program to receive, store and display data on auctioning spoons
//Author: Ransom Lambert
//Date:   10/09/2010
//Version: 1.5

using System;
using System.IO;

namespace Major_Project
{
    class Program
    {
        //Declaration of the struct for storing spoon details
        public struct SpoonDetails
        {
            public string spoonName; //Name of the spoon
            public decimal startPrice; //Starting price for the auction
            public decimal sellPrice; //Expected Selling price for the auction
            public string month; //Month of the Auction
            public decimal insertionFee; //Fee for placing the spoon for auction
            public decimal finalSaleFee; //Fee for selling the spoon in auction
            public decimal saleCost; //Total cost of having a spoon auction
            public decimal saleProfit; //Amount gained after paying fees
        }
        static void Main(string[] args)
        {
            const string fileName = "spoon.txt"; //variable for file
            const int ARRAY_LIMIT = 999; //variable that controls the array size
            int menuChoice = 0; //variable for user input of menu options
            bool continueLooping = true; //variable to control looping of menu
            int spoonCounter = 0; //variable for counting the spoons entered
            bool mistake = false; //variable to flag input error

            //Declaration of array of structs for spoon details
            SpoonDetails[] spoons = new SpoonDetails[ARRAY_LIMIT];

            Console.WriteLine("Welcome Fred"); //Welcome message for user

            //Loop for menu, switch statement taken from pop wiki
            do
            {
                //Displays menu options for user
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("Press 1 to Enter Data");
                Console.WriteLine("Press 2 to Save Data to File");
                Console.WriteLine("Press 3 to View Data in File");
                Console.WriteLine("Press 4 to Exit Program");

                try
                {
                    //Input for menu choice by user for the case statement
                    menuChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    mistake = true; //flag variable for input error
                    menuChoice = 0;
                }

                switch (menuChoice)
                {
                    case 1:
                        //Calls method that controls collecting of data
                        CollectData(ref spoonCounter, spoons, ARRAY_LIMIT);
                        break;
                    case 2:
                        //Calls method to write collected data to file
                        SaveData(spoons, fileName, spoonCounter);
                        break;
                    case 3:
                        //Calls method to display data from file
                        ViewData(spoons, fileName, spoonCounter, ARRAY_LIMIT);
                        break;
                    case 4:
                        Console.WriteLine("Ending Program");
                        continueLooping = false; //allows the loop to end
                        break;
                    default:
                        //If user has not entered a number, error message
                        if (mistake)
                        {
                            Console.WriteLine("You must enter a number");
                        }
                        else
                        {
                        //Error message as default
                        Console.WriteLine("You must press 1, 2, 3 or 4" +
                            " and not {0}", menuChoice);
                        }
                        break;
                }
            } while (continueLooping);

            Console.ReadKey();
        }

        //This method controls the collection of data, which is entering data,
        //calcuations and displaying the new data. Loops for as many spoons
        //as the user wants to enter.
        public static void CollectData(ref int spoonCounter, 
            SpoonDetails[] spoons, int ARRAY_LIMIT)
        {
            string userChoice = ""; //variable for controling data input
            bool enterDetails = true; //variable to control looping
            spoonCounter = 0; //resets the counter for a new lot of assigning

            InitialiseArray(spoons); //To initialise the array spoons

            do
            {   
                //Method call to get input of data from user
                EnterData(spoonCounter, spoons);

                //Method call to calculate fees and profits
                CalculatePrices(spoonCounter, spoons);

                //Method call to display data for user
                DisplayData(spoonCounter, spoons);

                spoonCounter++; //increment the counter

                //prompt user about entering more details
                Console.WriteLine("Would you like to enter more details? (y/n)");
                userChoice = Console.ReadLine();

                //Exits loop if no more details to be entered
                if (userChoice.ToLower() == "n")
                {
                    enterDetails = false;
                }
            } while (enterDetails && spoonCounter < ARRAY_LIMIT);
        }

        //Method that initialises the array for spoon details
        public static void InitialiseArray(SpoonDetails[] spoons)
        {
            //Writes data into the array of structs
            for (int counter = 0; counter < spoons.Length; counter++)
            {
                spoons[counter].spoonName = "Spoon Name";
                spoons[counter].startPrice = 999;
                spoons[counter].sellPrice = 999;
                spoons[counter].month = "Month";
                spoons[counter].insertionFee = 999;
                spoons[counter].finalSaleFee = 999;
                spoons[counter].saleCost = 999;
                spoons[counter].saleProfit = 999;
            }
        }

        //Prompts user for input and stores it in the array
        public static void EnterData(int spoonCounter, SpoonDetails[] spoons)
        {
            Console.Write("Enter the name of your spoon: ");
            spoons[spoonCounter].spoonName = Console.ReadLine();
            Console.Write("Enter the starting price: $");
            spoons[spoonCounter].startPrice = 
                Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter the estimated selling price: $");
            spoons[spoonCounter].sellPrice = 
                Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter the month of your auction: ");
            spoons[spoonCounter].month = Console.ReadLine();
        }

        //Users data from user and calculates fees based on them
        public static void CalculatePrices(int spoonCounter, 
            SpoonDetails[] spoons)
        {
            const decimal LOW_FEE_RATE = 0.0525M;
            const decimal MIDDLE_FEE_RATE = 0.0275M;
            const decimal HIGH_FEE_RATE = 0.015M;
            const decimal LOW_FINAL_FEE_BRACKET = 75.00M;
            const decimal HIGH_FINAL_FEE_BRACKET = 1000.00M;

            //Because these numbers are only used in one place, Tim suggested
            //not using constants for them.
            //Determines the insertion fee based on starting price for auction
            if (spoons[spoonCounter].startPrice >= 0.01M &&
                spoons[spoonCounter].startPrice <= 0.99M)
            {
                spoons[spoonCounter].insertionFee = 0.3M;
            }
            if (spoons[spoonCounter].startPrice >= 1.00M &&
                spoons[spoonCounter].startPrice <= 19.99M)
            {
                spoons[spoonCounter].insertionFee = 0.5M;
            }
            if (spoons[spoonCounter].startPrice >= 20.00M &&
                spoons[spoonCounter].startPrice <= 49.99M)
            {
                spoons[spoonCounter].insertionFee = 0.75M;
            }
            if (spoons[spoonCounter].startPrice >= 50.00M &&
                spoons[spoonCounter].startPrice <= 99.99M)
            {
                spoons[spoonCounter].insertionFee = 1.5M;
            }
            if (spoons[spoonCounter].startPrice >= 100.00M &&
                spoons[spoonCounter].startPrice <= 399.99M)
            {
                spoons[spoonCounter].insertionFee = 2.5M;
            }
            if (spoons[spoonCounter].startPrice >= 400.00M)
            {
                spoons[spoonCounter].insertionFee = 3.5M;
            }

            //Calculates the final sale fee based on the final sale price
            if (spoons[spoonCounter].sellPrice == 0)
            {
                spoons[spoonCounter].finalSaleFee = 0;
            }
            if (spoons[spoonCounter].sellPrice > 0 &&
                spoons[spoonCounter].sellPrice <= LOW_FINAL_FEE_BRACKET)
            {
                spoons[spoonCounter].finalSaleFee =
                    LOW_FEE_RATE * spoons[spoonCounter].sellPrice;
            }
            if (spoons[spoonCounter].sellPrice > LOW_FINAL_FEE_BRACKET &&
                spoons[spoonCounter].sellPrice <= HIGH_FINAL_FEE_BRACKET)
            {
                spoons[spoonCounter].finalSaleFee =
                    LOW_FEE_RATE * LOW_FINAL_FEE_BRACKET + MIDDLE_FEE_RATE *
                    (spoons[spoonCounter].sellPrice - LOW_FINAL_FEE_BRACKET);
            }
            if (spoons[spoonCounter].sellPrice > HIGH_FINAL_FEE_BRACKET)
            {
                spoons[spoonCounter].finalSaleFee =
                    LOW_FEE_RATE * LOW_FINAL_FEE_BRACKET + MIDDLE_FEE_RATE *
                    (HIGH_FINAL_FEE_BRACKET - LOW_FINAL_FEE_BRACKET) + 
                    HIGH_FEE_RATE *
                    (spoons[spoonCounter].sellPrice - HIGH_FINAL_FEE_BRACKET);
            }

            //Calculates the total fees and profit from the auction
            spoons[spoonCounter].saleCost = spoons[spoonCounter].insertionFee + 
                spoons[spoonCounter].finalSaleFee;
            spoons[spoonCounter].saleProfit = spoons[spoonCounter].sellPrice -
                spoons[spoonCounter].saleCost;
        }

        //Method to display data to the spoon auction for the user
        public static void DisplayData(int spoonCounter, SpoonDetails[] spoons)
        {
            //Takes data in the array that was entered and displays them
            //for the user to view
            Console.WriteLine("\n");
            Console.WriteLine("Based on the details:");
            Console.WriteLine("Spoon Name: {0}", 
                spoons[spoonCounter].spoonName);
            Console.WriteLine("Auction starting price: {0:c2}", 
                spoons[spoonCounter].startPrice);
            Console.WriteLine("Estimated selling price: {0:c2}", 
                spoons[spoonCounter].sellPrice);
            Console.WriteLine("Month of auction: {0}", 
                spoons[spoonCounter].month);
            Console.WriteLine("The insertion fee for your spoon is {0:c2}", 
                spoons[spoonCounter].insertionFee);
            Console.WriteLine("The fee on your projected sale price is {0:c2}", 
                spoons[spoonCounter].finalSaleFee);
            Console.WriteLine("The total fee cost for this auction is {0:c2}", 
                spoons[spoonCounter].saleCost);
            Console.WriteLine("The profit from this auction is {0:c2}", 
                spoons[spoonCounter].saleProfit);
        }

        //Method to store entered data to file
        public static void SaveData(SpoonDetails[] spoons, string fileName, 
            int spoonCounter)
        {
            //Checks to see if any data has already been entered into the array
            //and displays an error message if not
            if (spoonCounter == 0)
            {
                Console.WriteLine("You must first enter some data");
            }
            else
            {
                //opens the file
                StreamWriter outFile = new StreamWriter(fileName, true);

                //Loops through the array and writes the data into the file
                for (int counter = 0; counter < spoonCounter; counter++)
                {
                    outFile.WriteLine(spoons[counter].spoonName);
                    outFile.WriteLine(spoons[counter].startPrice);
                    outFile.WriteLine(spoons[counter].sellPrice);
                    outFile.WriteLine(spoons[counter].month);
                    outFile.WriteLine(spoons[counter].insertionFee);
                    outFile.WriteLine(spoons[counter].finalSaleFee);
                    outFile.WriteLine(spoons[counter].saleCost);
                    outFile.WriteLine(spoons[counter].saleProfit);
                }
                outFile.Close(); //closes the file after writing is complete
            }
        }

        //Method to read the file and display it for the user
        public static void ViewData(SpoonDetails[] spoons, string fileName, 
            int spoonCounter, int ARRAY_LIMIT)
        {
            int inputCounter = 0; //counter to input data into the array

            InitialiseArray(spoons); //Re-initialises the array spoons

            //Calls method for loading data into the array
            LoadData(fileName, spoons, ref inputCounter, ARRAY_LIMIT);

            //Loops through the array of structs
            for (spoonCounter = 0; spoonCounter < inputCounter; spoonCounter++)
            {
                DisplayData(spoonCounter, spoons); //method to display the data
            }
        }

        //Loads data from file into the array of structs
        public static void LoadData(string fileName, SpoonDetails[] spoons, 
            ref int inputCounter, int ARRAY_LIMIT)
        {
            string input = ""; //variable for input checking

            //Input loading based off pop wiki
            //Checks for file and displays error message if it doesn't exist
            if (File.Exists(fileName))
            {
                //opens the file
                StreamReader inFile = new StreamReader(fileName); 
                input = inFile.ReadLine(); //priming read
                //Loops while there is more data in the file to be loaded
                //and array is not full
                while (input != null && inputCounter < ARRAY_LIMIT)
                {
                    spoons[inputCounter].spoonName = input;
                    spoons[inputCounter].startPrice =
                        Convert.ToDecimal(inFile.ReadLine());
                    spoons[inputCounter].sellPrice =
                        Convert.ToDecimal(inFile.ReadLine());
                    spoons[inputCounter].month = inFile.ReadLine();
                    spoons[inputCounter].insertionFee =
                        Convert.ToDecimal(inFile.ReadLine());
                    spoons[inputCounter].finalSaleFee =
                        Convert.ToDecimal(inFile.ReadLine());
                    spoons[inputCounter].saleCost =
                        Convert.ToDecimal(inFile.ReadLine());
                    spoons[inputCounter].saleProfit =
                        Convert.ToDecimal(inFile.ReadLine());

                    inputCounter++; //increments the counter
                    input = inFile.ReadLine(); //priming read for next loop
                }

                //Error message if user tries to read file that is empty
                if (inputCounter == 0)
                {
                    Console.WriteLine("The file is empty");
                }
                inFile.Close(); //closes the file
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
}