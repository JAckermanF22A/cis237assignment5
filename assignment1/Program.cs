//Author: David Barnes
//CIS 237
//Assignment 1
/*
 * The Menu Choices Displayed By The UI
 * 1. Load Wine List From CSV
 * 2. Print The Entire List Of Items
 * 3. Search For An Item
 * 4. Add New Item To The List
 * 5. Exit Program
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Expands the console for easier viewing
            Console.SetWindowSize(150, 40); 

            //Set a constant for the size of the collection
            const int wineItemCollectionSize = 4000;

            //Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            //Create an instance of the wine database
            BeverageJAckermanEntities beverageEntities = new BeverageJAckermanEntities();

            //Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            //Display the Menu and get the response. Store the response in the choice integer
            //This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        //Print Entire List Of Items
                        userInterface.DisplayAllItems(beverageEntities);
                        break;

                    case 2:
                        //Search For An Item
                        string searchQuery = userInterface.GetSearchQuery();
                        Beverage foundWine = beverageEntities.Beverages.Find(searchQuery);
                        if (foundWine != null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("Here is the wine with that ID:");
                            Console.WriteLine(foundWine.id + " " + foundWine.name + " " + foundWine.pack + " " + foundWine.price);
                        }
                        else
                        {
                            Console.WriteLine("Sorry, no wine with that ID could be found. the ID you entered may be incorrect or does not exist in the database.");
                        }

                        break;

                    case 3:
                        //Add A New Item To The List
                        string ID;
                        string Name;
                        decimal Price;
                        string Pack;
                        userInterface.GetNewItemInformation(out ID, out Name, out Price, out Pack);

                        bool WineAddedBool = false;

                        while (WineAddedBool == false)
                        {
                            Beverage IDCheck = beverageEntities.Beverages.Find(ID);
                            if (IDCheck == null)
                            {
                                Beverage newWine = new Beverage();
                                newWine.id = ID;
                                newWine.name = Name;
                                newWine.pack = Pack;
                                newWine.price = Price;
                                beverageEntities.Beverages.Add(newWine);
                                WineAddedBool = true;
                            }
                            else
                            {
                                Console.WriteLine("Sorry, a wine with this ID already exists. Would you like to enter a new ID?");
                                Console.WriteLine("y/n?");
                                string input = "nothing";
                                while (input != "y")
                                {
                                    input = Console.ReadLine();
                                    if (input == "y")
                                    {
                                        ID = userInterface.GetNewID();
                                    }
                                    else if (input == "n")
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sorry, you entered something other than a y or an n");
                                    }
                                }

                            }
                        }
                        
                        break;

                    case 4:
                        //Delete an item from the list
                        string IDToDelete = userInterface.GetDeleteQuery();
                        Beverage deleteWine = beverageEntities.Beverages.Find(IDToDelete);

                        if(deleteWine != null)
                        {
                            beverageEntities.Beverages.Remove(deleteWine);
                            //Insert success UI method
                        }
                        else
                        {
                            Console.WriteLine("Error, the ID you entered does not corrospond to any Wine currently in the database.");
                        }
                        break;
                    case 5:
                        //Update an item from the database
                        string IDToUpdate = userInterface.GetUpdateQuery();
                        Beverage updateBeverage = beverageEntities.Beverages.Find(IDToUpdate);

                        if(updateBeverage != null)
                        {
                            Console.WriteLine("Would you like to change the ID of the wine?");
                            Console.WriteLine("y/n?");
                            string userInput = Console.ReadLine();
                            if(userInput == "y")
                            {
                                Console.WriteLine("What is the new ID you would like to assign to the wine?");

                                userInput = Console.ReadLine();

                                updateBeverage.id = userInput;
                            }
                            Console.WriteLine("Would you like to change the name of the wine?");
                            Console.WriteLine("y/n?");
                            userInput = Console.ReadLine();
                            if (userInput == "y")
                            {
                                Console.WriteLine("What is the new name you would like to assign to the wine?");

                                userInput = Console.ReadLine();

                                updateBeverage.name = userInput;
                            }
                            Console.WriteLine("Would you like to change the price of the wine?");
                            Console.WriteLine("y/n?");
                            userInput = Console.ReadLine();
                            if (userInput == "y")
                            {
                                Console.WriteLine("What is the new price you would like to assign to the wine?");

                                userInput = Console.ReadLine();

                                updateBeverage.price = decimal.Parse(userInput);
                            }
                            Console.WriteLine("Would you like to change the pack of the wine?");
                            Console.WriteLine("y/n?");
                            userInput = Console.ReadLine();
                            if (userInput == "y")
                            {
                                Console.WriteLine("What is the new pack you would like to assign to the wine?");

                                userInput = Console.ReadLine();

                                updateBeverage.pack = userInput;
                            }
                        }
                        break;
                    case 6:
                        
                        break;
                }

                //Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }

            beverageEntities.SaveChanges();

        }
    }
}
