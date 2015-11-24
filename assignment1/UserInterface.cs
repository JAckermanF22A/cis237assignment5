//Author: David Barnes
//CIS 237
//Assignment 1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class UserInterface
    {
        const int maxMenuChoice = 6;
        //---------------------------------------------------
        //Public Methods
        //---------------------------------------------------

        //Display Welcome Greeting
        public void DisplayWelcomeGreeting()
        {
            Console.WriteLine("Welcome to the wine program");
        }

        //Display Menu And Get Response
        public int DisplayMenuAndGetResponse()
        {
            //declare variable to hold the selection
            string selection;

            //Display menu, and prompt
            this.displayMenu();
            this.displayPrompt();

            //Get the selection they enter
            selection = this.getSelection();

            //While the response is not valid
            while (!this.verifySelectionIsValid(selection))
            {
                //display error message
                this.displayErrorMessage();

                //display the prompt again
                this.displayPrompt();

                //get the selection again
                selection = this.getSelection();
            }
            //Return the selection casted to an integer
            return Int32.Parse(selection);
        }

        //Get the search query from the user
        public string GetSearchQuery()
        {
            Console.WriteLine();
            Console.WriteLine("What is the ID of the wine you would like to search for?");
            Console.Write("> ");
            return Console.ReadLine();
        }

        //Get New Item Information From The User.
        public void GetNewItemInformation(out string id, out string name, out decimal price, out string pack)
        {
            Console.WriteLine();
            Console.WriteLine("What is the new items ID?");
            Console.Write("> ");
            id = Console.ReadLine();
            Console.WriteLine("What is the new items name?");
            Console.WriteLine("> ");
            name = Console.ReadLine();
            Console.WriteLine("What is the new items Price?");
            Console.Write("> ");
            price = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("What is the new items Pack?");
            Console.Write("> ");
            pack = Console.ReadLine();
        }

        //Get the info to identify which wine item to delete
        public string GetDeleteQuery()
        {
            Console.WriteLine();
            Console.WriteLine("What is the ID of the wine you would you like to delete?");
            Console.Write("> ");
            return Console.ReadLine();
        }

        //Get the info for which wine item to update
        public string GetUpdateQuery()
        {
            Console.WriteLine();
            Console.WriteLine("What is the ID of the wine you would you like to update?");
            Console.Write("> ");
            return Console.ReadLine();
        }

        //Update success message
        public void DisplayUpdateSuccess()
        {
            Console.WriteLine();
            Console.WriteLine("The wine has been successfully updated.");
            Console.WriteLine();
        }

        //Update failed message.
        public void DisplayUpdateFailure()
        {
            Console.WriteLine();
            Console.WriteLine("No wine with that ID exists to be altered.");
            Console.WriteLine();
        }

        //Delete successful
        public void DisplayDeleteSuccess()
        {
            Console.WriteLine();
            Console.WriteLine("The wine has been successfully deleted.");
            Console.WriteLine();
        }

        //Get a new ID because the one the user entered is already in use
        public string GetNewID()
        {
            Console.WriteLine("What is the new items ID?");
            Console.Write("> ");
            return Console.ReadLine();
        }

        //Display All Items
        public void DisplayAllItems(BeverageJAckermanEntities beverageEntity)
        {
            Console.WriteLine();

            int counter = 0;
            int alreadyDisplayed = 0;

            string userInput = "";

            bool exitBool = false;

            foreach (Beverage beverage in beverageEntity.Beverages)
            {
                if(exitBool != true)
                {
                    if (counter != 200)
                    {
                        Console.WriteLine(beverage.id + " " + beverage.name + " " + beverage.pack + " " + beverage.price);
                        counter++;
                        alreadyDisplayed++;
                    }
                    else
                    {
                        userInput = "";
                        while (userInput != "y" && exitBool != true)
                        {
                            Console.WriteLine(alreadyDisplayed + "items have been displayed.");
                            Console.WriteLine("Would you like to continue displaying wine items from the database?");
                            Console.WriteLine("y/n?");

                            userInput = Console.ReadLine();

                            if (userInput == "y")
                            {
                                counter = 0;
                            }
                            else if (userInput == "n")
                            {
                                exitBool = true;
                            }
                            else
                            {
                                Console.WriteLine("Error: Please enter y or n");
                            }
                        }
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                    }
                }
                else
                {
                    return;
                }
                    
            }
        }

        //Display Item Found Success
        public void DisplayItemFound(string itemInformation)
        {
            Console.WriteLine();
            Console.WriteLine("Item Found!");
            Console.WriteLine(itemInformation);
        }

        //Display Item Found Error
        public void DisplayItemFoundError()
        {
            Console.WriteLine();
            Console.WriteLine("A Match was not found");
        }

        //Display Add Wine Item Success
        public void DisplayAddWineItemSuccess()
        {
            Console.WriteLine();
            Console.WriteLine("The Item was successfully added");
        }

        //Display Item Already Exists Error
        public void DisplayItemAlreadyExistsError()
        {
            Console.WriteLine();
            Console.WriteLine("An Item With That Id Already Exists");
        }


        //---------------------------------------------------
        //Private Methods
        //---------------------------------------------------

        //Display the Menu
        private void displayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. Print The Entire Database Of Items");
            Console.WriteLine("2. Search For An Item");
            Console.WriteLine("3. Add New Item To The Database");
            Console.WriteLine("4. Delete An Item From The Database");
            Console.WriteLine("5. Update An Item From The Database");
            Console.WriteLine("6. Exit Program.");
        }

        //Display the Prompt
        private void displayPrompt()
        {
            Console.WriteLine();
            Console.Write("Enter Your Choice: ");
        }

        //Display the Error Message
        private void displayErrorMessage()
        {
            Console.WriteLine();
            Console.WriteLine("That is not a valid option. Please make a valid choice");
        }

        //Get the selection from the user
        private string getSelection()
        {
            return Console.ReadLine();
        }

        //Verify that a selection from the main menu is valid
        private bool verifySelectionIsValid(string selection)
        {
            //Declare a returnValue and set it to false
            bool returnValue = false;

            try
            {
                //Parse the selection into a choice variable
                int choice = Int32.Parse(selection);

                //If the choice is between 0 and the maxMenuChoice
                if (choice > 0 && choice <= maxMenuChoice)
                {
                    //set the return value to true
                    returnValue = true;
                }
            }
            //If the selection is not a valid number, this exception will be thrown
            catch (Exception e)
            {
                //set return value to false even though it should already be false
                returnValue = false;
            }

            //Return the reutrnValue
            return returnValue;
        }
    }
}
