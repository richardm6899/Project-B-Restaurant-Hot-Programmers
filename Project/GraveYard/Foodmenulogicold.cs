/*


ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            // go down
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            }
            // choose
            else if (key == ConsoleKey.Enter)
            {
                // enter selected thing
                lookingAtFood = FoodMenuLogic.MainSelected(selectedIndex);
            }
        }

            private static bool MainSelected(int selectedIndex)
    {


        return true; // Keep running the menu
    }



        public static void GetOptionMain(string[] options)
    {
        
        int selectedIndex = 0;
        bool lookingAtFood = true;
        while (lookingAtFood)
        {
            Console.Clear();
            Console.ResetColor();
            System.Console.WriteLine("Welcome to the food menu.");
            HelperPresentation.DisplayOptions(options, selectedIndex);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            // go down
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            }
            // choose
            else if (key == ConsoleKey.Enter)
            {
                // enter selected thing
                lookingAtFood = FoodMenuLogic.MainSelected(selectedIndex);
            }
        }
    }




        private static bool MainSelected(int selectedIndex)
    {

        switch (selectedIndex)
        {
            // look at whole menu
            case 0:
                FoodMenuDisplay.DisplayWholeMenu();
                break;
            // filter by
            case 1:
               FoodMenuDisplay.TypesFilter();
                break;
            // filter by
            case 2:
                FoodMenuDisplay.DisplayByAllergy();
                break;
            // return to where the user came from
            case 3:

                Console.ReadLine();
                return false; // Exit the loop
        }
        return true; // Keep running the menu
    }
*/ 