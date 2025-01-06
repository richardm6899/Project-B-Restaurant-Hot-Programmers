/*
            string? userDeleteDeactivate = Console.ReadLine();
            switch (userDeleteDeactivate)
            {
                case "1":
                    bool userDeactivate = HelperPresentation.YesOrNo("Are you sure you want to deactivate your account?");
                    if (userDeactivate)
                    {
                        System.Console.WriteLine("Please re-enter your password.");
                        string? passToCheck = Console.ReadLine();
                        if (accountsLogic.ReCheckPassWord(acc, passToCheck))
                        {
                            accountsLogic.deactivateAccount(acc.Id);
                            System.Console.WriteLine("Account has been deactivated. You will be returned to the main menu.");
                            Console.ReadKey();
                            acc = null;
                            Menu.Start();
                        }
                        System.Console.WriteLine("Incorrect password.");
                        deactivateDeletingAccount = false;

                    };
                    break;

                case "2":
                    bool userDelete = HelperPresentation.YesOrNo("Are you sure you want to delete your account?");
                    if (userDelete)
                    {
                        System.Console.WriteLine("Please re-enter your password.");
                        string passToCheck = Console.ReadLine();
                        if (accountsLogic.ReCheckPassWord(acc, passToCheck))
                        {
                            accountsLogic.deleteAccount(acc.Id);
                            System.Console.WriteLine("Account has been deleted. You will be returned to the main menu.");
                            Console.ReadKey();
                            acc = null;
                            Menu.Start();
                        }
                        System.Console.WriteLine("Incorrect password.");
                        deactivateDeletingAccount = false;
                    };
                    break;

                case "3":
                    deactivateDeletingAccount = false;
                    break;

                default:
                    System.Console.WriteLine("Invalid input.");
                    break;F
*/ 