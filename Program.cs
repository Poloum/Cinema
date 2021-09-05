using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Theather
{
    class Program
    {
        static void Main(string[] args)
        {

            //הגדרת משתנים
            int MainMenuChoice = 0;
            int ClientIndex;                        // משתנה להגדרת הלקוח במערך = מספr לקוח
            int x, y;
            string ClientName;
            bool ExistClient;               // משתנה לצוקך בדיקה - האם הלקוח חדש או קיים


            //הגדרת מערכים 
            String[] Client = new String[100];      // Array for Clients Names

            int[,] Tickets = new int[10, 10];       // מערך לרישום כרטיסים שנמכרו
            for (y = 0; y <= 9; y++)
                for (x = 0; x <= 9; x++)
                    Tickets[y, x] = 100;            //100=Empty Chair
            x = 0; y = 0;                           // איפוס המשתנים לשימוש חוזר  

            // START OF MAIN PROGRAM  //

            do
            {
                MainMenuChoice = MainMenu(MainMenuChoice);
                ClientIndex = 0;
                ExistClient = false;                        //איפוס משתנה

                switch (MainMenuChoice)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Enter Your Full Name : ");
                        ClientName = Console.ReadLine();

                        // בדיקה - האם לקוח חדש או קיים
                        while (Client[ClientIndex] != null)
                        {
                            if (Client[ClientIndex] == ClientName)
                            {
                                ExistClient = true;
                                break;
                            }
                            else
                            {
                                ClientIndex++;
                            }
                        }
                        if (ExistClient)
                            ExistingClientMenu(ClientIndex, Client, Tickets);       //Existing Client Menu
                        else
                        {
                            Client[ClientIndex] = ClientName;
                            NewClientMenu(ClientIndex, Client, Tickets);            //New Client Menu
                        }
                        break;
                    case 2:
                        Console.Clear();
                        ManagerReport(ClientIndex, Client, Tickets);
                        break;
                    case 3:
                        Console.WriteLine("Good Bye!");
                        break;
                    default:
                        Console.WriteLine("Unknown Operation");
                        break;
                }
            }
            while (MainMenuChoice != 3);
        }
        // END OF MAIN //

        // FUNCTIONS   //

        static int MainMenu(int Choice)
        {
            string[] MainMenuArr = new string[3] {"1. I'm a Client:",
                                                  "2. I'm a Manager",
                                                  "3. Exit",
                                             };
            Console.Clear();
            for (int i = 0; i < MainMenuArr.Length; i++)
                Console.WriteLine(MainMenuArr[i]);
            Choice = int.Parse(Console.ReadLine());
            return Choice;
        }

        static void NewClientMenu(int ClientIndexF, string[] Clientname, int[,] Ticket)
        {
            string[] NewClientMenuArr = new string[2] {"1. Order Tickets:",
                                                       "2. Exit:",
                                                      };

            Console.Clear();
            for (int i = 0; i < NewClientMenuArr.Length; i++)
                Console.WriteLine(NewClientMenuArr[i]);
            int Choice = int.Parse(Console.ReadLine());
            switch (Choice)
            {
                case 1:                                 // הזמנת כרטיסים ללקוח חדש
                    Console.Clear();
                    TicketOrder(ClientIndexF, Clientname, Ticket);
                    break;
                case 2:
                    Console.Write("");
                    break;
                case 3:
                    Console.WriteLine("Good Bye!");
                    break;
                default:
                    Console.WriteLine("Unknown Operation");
                    break;
            }
        }

        static void ExistingClientMenu(int ClientIndexF, string[] Clientname, int[,] Ticket)
        {
            string[] NewClientMenuArr = new string[3] {"1. Order More Tickets:",
                                                       "2. Change Your Order:",
                                                       "3. Exit:",
                                                      };

            Console.Clear();
            for (int i = 0; i < NewClientMenuArr.Length; i++)
                Console.WriteLine(NewClientMenuArr[i]);
            int Choice = int.Parse(Console.ReadLine());
            switch (Choice)
            {
                case 1:                                 // הזמנת כרטיסים ללקוח קיים
                    Console.Clear();
                    TicketOrder(ClientIndexF, Clientname, Ticket);
                    break;
                case 2:
                    string yn = " ";  //yes or now
                    while (yn != "y" && yn != "n")
                    {
                        Console.Clear();
                        Console.Write("It will erase all your previous orders. Continue? (y/n) : ");
                        yn = Console.ReadLine();
                        if (yn == "y")
                        {
                            // מחיקת כרטיסים שהוזמנו על ידי הלקוח
                            for (int x = 0; x <= 9; x++)
                                for (int y = 0; y <= 9; y++)
                                    if (Ticket[x, y] == ClientIndexF)
                                        Ticket[x, y] = 100;
                            // הזמנה מחדש של הכרטיסים על ידי הלקוח 
                            Console.Clear();
                            TicketOrder(ClientIndexF, Clientname, Ticket);
                        }
                        else
                            Console.WriteLine("Good Bye!");
                    }
                    break;
                case 3:
                    Console.WriteLine("Good Bye!");
                    break;
                default:
                    Console.WriteLine("Unknown Operation");
                    break;
            }
        }

        // PRINTING THE CINEMA OCCUPANCY STATUS//
        static void PrintCinema(int ClientIndexF, string[] Clientname, int[,] arr)
        {
            Console.Write("            ");
            for (int i = 0; i <= 9; i++)
                Console.Write("Chair {0}|     ", i + 1);
            Console.WriteLine();

            for (int y = 0; y <= 9; y++)
            {
                Console.Write("Row {0,2} : ", y + 1);
                for (int x = 0; x <= 9; x++)
                {
                    if (arr[x, y] == 100)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("[[ Empty ]]  ");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (arr[x, y] == ClientIndexF)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("[[ Ticket]]  ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("[[Ordered]]  ");
                            Console.ResetColor();
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("========================================================================");
        }

        // TICKETS ORDER FUNCTION //
        static void TicketOrder(int ClientIndexF, string[] Clientname, int[,] Ticket)
        {
            int NumOfTickets, NumOfChair, NumOfRow;
            PrintCinema(ClientIndexF, Clientname, Ticket);
            Console.Write("How Many Tickets Do You Want To Order? : ");
            NumOfTickets = int.Parse(Console.ReadLine());
            int[] ChairNumbers = new int[NumOfTickets];   // מערך לצורך הדפסת כרטיסים ללקוח
            Console.Write("In Row : ");
            NumOfRow = int.Parse(Console.ReadLine());

            // ENTERING CHAIR NUBERS FROM CUSTOMER:
            for (int i = 1; i <= NumOfTickets; i++)
            {
                Console.Write("Chair Number :");
                NumOfChair = int.Parse(Console.ReadLine());
                Ticket[NumOfChair - 1, NumOfRow - 1] = ClientIndexF;    //משייך מספר לקוח למערך של כרטיסים
                ChairNumbers[i - 1] = NumOfChair;                   //שומר מספרי כסאות לצורך הדפסת כרטיס ללקוח
            }

            // PRINTING THE TICKET
            Console.Clear();
            Console.WriteLine("Your Tickets:");
            Console.WriteLine("********************");
            Console.WriteLine("* Customer : {0}", Clientname[ClientIndexF]);
            Console.WriteLine("* Row : {0}", NumOfRow);
            Console.Write("* Chairs: ");
            for (int i = 1; i <= NumOfTickets; i++)
                Console.Write("{0,3}", ChairNumbers[i - 1]);
            Console.WriteLine();
            Console.WriteLine("********************");
            Console.ReadKey();
            Console.Clear();
        }

        static void ManagerReport(int ClientIndexF, string[] Clientname, int[,] arr)
        {
            int i = 0;                      // מונה של מספר לקוח 
            if (Clientname[i] == null)
            {
                Console.WriteLine("There is no current orders");
                Console.ReadLine();
            }
            else
            {
                while (Clientname[i] != null)
                {
                    Console.WriteLine("Client Name : {0}", Clientname[i]);
                    for (int y = 0; y <= 9; y++)
                        for (int x = 0; x <= 9; x++)
                            if (arr[x, y] == i)
                            {
                                Console.Write("Row : {0} --> Chairs: ", y + 1);
                                while (arr[x, y] == i)
                                {
                                    Console.Write("{0,2},", x + 1);
                                    x++;
                                }
                                Console.WriteLine();
                            }
                    Console.WriteLine("****************************");
                    i++;
                }
                Console.ReadLine();
            }
        }
    }
}
