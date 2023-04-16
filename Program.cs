using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZInput;
using GameList.BL;
using System.Threading;

namespace GameList
{
   

    class Program
    {
        // variables for counting score,player and enemy health etc
        static int score = 0, count = 15, playerhealth = 30, VEH = 15;
        // variable for moving ghost 
        static string direction = "down";
        static string horizontalDirection = "left";
        static string followStatus = "isAlive";
        static char[,] array = new char[,]
        {
            {'%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','%'},
            {'%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%','%'}
        };

        static void Main(string[] args)
        {
            //player coordinates initialization
            player Player = new player();
            Player.px = 12;
            Player.py = 16;
            //vertical enemy coordinates initialization
            verticalGhost vGhost = new verticalGhost();
            vGhost.vgx = 68;
            vGhost.vgy = 8;
            //variables to slow down the speed of ghost and timerB for bullet
            int timerV = 2, timerB = 2;

            //list to store the bullets
            List<playerBullet> bullet = new List<playerBullet>();
            playerBullet bul = new playerBullet();


            // variables to print character used in printing player and enemy

            char p = (char)148;
            char l = (char)240;
            char g = (char)219;
            char a = (char)162;
            char b = (char)229;

            //2D array to print player
            char[,] player = new char[,]
            {
                { ' ', p, ' ' },
                { '(', l, ')' },
                { '/', ' ', '\\' }
             };
            //2D array to print vertical ghost
            char[,] verticalGhost = new char[,]
                {
                   { ' ','@',' '},
                   { '(',g,')'},
                   { '/',' ','\\'}
                };
            //2D array to print horizontal ghost 
            char[,] horizontalGhost = new char[,]
                {
                   { ' ',a,' '},
                   { '{',g,'}'},
                   { '<',' ','>'}
                };
            //2D array to print follow ghost
            char[,] followGhost = new char[,]
                {
                   { ' ',b,' '},
                   { '[',g,']'},
                   { '=',' ','='}
                };
            //calling function to print logo and opening menu

            Console.Clear();
            logo();
            loading();
            Console.Clear();
            loginMenuHeader();
            Console.WriteLine("\n\n\n");
            menu();
            //variables to store users option for main menu and optional menu
            int option, keyoption, choice = 3;
            bool gamerunning = true;
            while (gamerunning)
            {
                Console.WriteLine("Enter Your Option:");
                option = int.Parse(Console.ReadLine());
                if (option == 1 || option == 2)
                {
                    if (option == 2)
                    {
                    }
                    //calling function to start the game
                    Console.Clear();
                    printMaze();
                    printPlayer(player, Player);
                    printVerticalGhost(verticalGhost, vGhost);
                    //loop for game running
                    while (true)
                    {
                        //functions to print health and score
                        playerHealth();
                        printScore();
                        verticalEnemyHealth();
                        if (Keyboard.IsKeyPressed(Key.RightArrow))
                        {
                            moveright(player, Player);
                        }
                        if (Keyboard.IsKeyPressed(Key.LeftArrow))
                        {
                            moveleft(player, Player);
                        }
                        if (Keyboard.IsKeyPressed(Key.UpArrow))
                        {
                            moveup(player, Player);
                        }
                        if (Keyboard.IsKeyPressed(Key.DownArrow))
                        {
                            movedown(player, Player);
                        }
                        if (Keyboard.IsKeyPressed(Key.Space))
                        {
                            //to fire bullets of player
                           // char nextlocation = getCharatxy(px + 3, py);
                           // if (nextlocation != '%')
                           // {
                                generateBullet(bullet,Player);
                           // }
                        }



                        // timer to slow down the vertical ghost
                        if (timerV == 2)
                        {
                            //count varibles to remove ghost if health is 0
                            if (count > 0)
                            {
                                moveVerticalGhost(verticalGhost, vGhost);

                                timerV = 0;
                            }
                            if (count <= 0)
                            {
                                removeVerticalGhost(vGhost);
                            }
                        }

                        //calling functions to move bullets of enemy and player
                       // moveBulletV(bulletVX, bulletVY, isBulletActiveVertical);
                        moveBullet(bullet,bul);

                        timerV++;
                        timerB++;
                        Thread.Sleep(100);
                    }
                }
                //if options 3 to open key detail and instruction menu
                if (option == 3)
                {
                    options();
                    while (true)
                    {
                        Console.WriteLine("Enter your option:");
                        keyoption = int.Parse(Console.ReadLine());
                        if (keyoption == 1)
                        {
                            keysDetail();
                            Console.WriteLine("Press any Key to continue:");
                            Console.ReadKey();
                            options();
                        }
                        if (keyoption == 2)
                        {
                            instruction();
                            Console.WriteLine("Press any key to continue:");
                            Console.ReadKey();
                            options();
                        }
                        if (keyoption == 3)
                        {
                            Console.Clear();
                            loginMenuHeader();
                            Console.WriteLine("\n\n\n");
                            menu();
                            break;
                        }
                        else if (keyoption == 0 || keyoption > 3)
                        {
                            Console.WriteLine("INVALID OPTIONS");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            options();
                        }
                    }
                }
                //conditions to close the game
                if (option == 4)
                {
                    return;
                }
                //condition to check invalid options
                else if (option == 0 || option > 4)
                {
                    Console.WriteLine("INVALID OPTIONS");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    loginMenuHeader();
                    Console.WriteLine("\n\n\n");
                    menu();
                }



            }


        }
        static void logo()
        {
            Console.WriteLine("                                    .-==-=-.                    ");
            Console.WriteLine("                                   -*==. :=*=                   ");
            Console.WriteLine("                                   .#========#.                 ");
            Console.WriteLine("                                   #+++==++*#                   ");
            Console.WriteLine("                                   :##**=++*+----.              ");
            Console.WriteLine("                                :---:.           .---.          ");
            Console.WriteLine("                             .=-.                    :=         ");
            Console.WriteLine("                            :=           ...::::-*@+. .+        ");
            Console.WriteLine("                           .+  .%#*+======----*%@#+-=: --       ");
            Console.WriteLine("                           =. :+++*@%#*......==+*:...:  +       ");
            Console.WriteLine("                           =. :....%:..........-%.....: =.      ");
            Console.WriteLine("                           -: :....#=:..........@%....- ::       ");
            Console.WriteLine("                           .= :....=@+..........#@....- :=        ");
            Console.WriteLine("                            * .:....@%..........+@-...- :-        ");
            Console.WriteLine("                            =: -....#@:.........=@+..:: +.        ");
            Console.WriteLine("                             +  :...=@+.........:*=::. --         ");
            Console.WriteLine("                              +. .:::+-....:::::.....-+:          ");
            Console.WriteLine("                               -=-...:::::.....:::-==:            ");
            Console.WriteLine("                               .-+-:---======+++++*+-             ");
            Console.WriteLine("                               *  .+==*#*+*###%==+=*:+.           ");
            Console.WriteLine("                               .=- .==+##%%%%%%+===*%+.           ");
            Console.WriteLine("                                 :*--:=*%%#**+++++++**-.          ");
            Console.WriteLine("                                .*+=.  :=++++++++++++. *-         ");
            Console.WriteLine("                                =*===-====*++++++++**-==*         ");
            Console.WriteLine("                                :#*+++==++=-+*******+++*+         ");
            Console.WriteLine("                                 :*#***##:   ##*=: =##*-          ");
            Console.WriteLine("                                   :--+-   :=:+   =:             ");
            Console.WriteLine("                                     -=   .+  *.  --             ");
            Console.WriteLine("                                    :+    *   *.  --             ");
            Console.WriteLine("                                    .*    +.   *.  --             ");
            Console.WriteLine("                                   .*    -:   .+   =:             ");
            Console.WriteLine("                                -+*++==::=    ++.:-+*+=-.         ");
            Console.WriteLine("                              :*+=======++.  #*=========++=-      ");
            Console.WriteLine("                             -#+====-::-==* +*+=========-::-*=    ");
            Console.WriteLine("                             %+=====:..:=+# #*+=========:..:=++   ");
            Console.WriteLine("                             #***+++++***#- -+****************.   ");
            Console.WriteLine("                             ..:::::::..                         ");
        }
        static void loading()
        {
            Console.WriteLine("                                    LOADING");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(".");
                System.Threading.Thread.Sleep(200);
            }
        }
        static void loginMenuHeader()
        {
            Console.WriteLine(" _____  _              _____             ");
            Console.WriteLine("|   __||_| ___  ___   |     | ___  ___   ");
            Console.WriteLine("|   __|| ||  _|| -_|  | | | || .'||   |  ");
            Console.WriteLine("|__|   |_||_|  |___|  |_|_|_||__,||_|_|  ");
        }
        static void menu()
        {
            Console.WriteLine("MENU");
            Console.WriteLine("----------------");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Resume Game");
            Console.WriteLine("3. Option");
            Console.WriteLine("4. Exit");
        }
        static void options()
        {
            Console.Clear();
            loginMenuHeader();
            Console.WriteLine("\n\n");
            Console.WriteLine("OPTION MENU");
            Console.WriteLine("-----------------");
            Console.WriteLine("1. Keys");
            Console.WriteLine("2. Instruction");
            Console.WriteLine("3. Exit");
        }
        static void keysDetail()
        {
            Console.Clear();
            loginMenuHeader();
            Console.WriteLine("\n\n\n");
            Console.WriteLine("KEYS DETAIL");
            Console.WriteLine("-----------------");
            Console.WriteLine("UP\t\tGo Up");
            Console.WriteLine("DOWN\t\tGo Down");
            Console.WriteLine("LEFT\t\tGo Left");
            Console.WriteLine("RIGHT\t\tGo Right");
            Console.WriteLine("D\t\tFire");
            Console.WriteLine("ESCAPE\t\tExit Game");
        }
        static void instruction()
        {
            Console.Clear();
            loginMenuHeader();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("INSTRUCTIONS");
            Console.WriteLine("------------------");
            Console.WriteLine("Player will fire on right side by pressind 'D' key.");
            Console.WriteLine("Score will add when bullet interact with enemy");
            Console.WriteLine("Player health will decrease when enemy bullet collide with player");
            Console.WriteLine("Player will die if it collide with follow ghost");
            Console.WriteLine("Player will won the game if you score 50");
        }
        static void printMaze()
        {

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }

        }
        static void gotoxy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        static void printPlayer(char[,] player,player Player)
        {
            for (int i = 0; i < 3; i++)
            {
                gotoxy(Player.px, Player.py + i);
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0}", player[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void printVerticalGhost(char[,] verticalGhost,verticalGhost vGhost)
        {
            for (int i = 0; i < 3; i++)
            {
                gotoxy(vGhost.vgx,vGhost.vgy + i);
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0}", verticalGhost[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void playerHealth()
        {
            gotoxy(80, 10);
            Console.Write("PLAYER HEALTH:{0}", playerhealth);
        }
        static void printScore()
        {
            gotoxy(80, 8);
            Console.Write("Score:{0}", score);
        }
        static void addScore()
        {
            score++;
        }
        static void verticalEnemyHealth()
        {
            gotoxy(80, 12);
            Console.Write("Vertical Enemy Health:{0}", count);
        }
        static void moveright(char[,] player, player Player)
        {

            if (array[Player.py, Player.px + 3] == ' ' && array[Player.py + 1, Player.px + 3] == ' ' && array[Player.py + 2, Player.px + 3] == ' ')
            {

                gotoxy(Player.px,Player.py);
                removeplayer(Player);
                Player.px = Player.px + 1;
                gotoxy(Player.px, Player.py);
                printPlayer(player, Player);
            }

        }
        static void moveleft(char[,] player, player Player)
        {

            if (array[Player.py, Player.px - 1] == ' ' && array[Player.py + 1, Player.px - 1] == ' ' && array[Player.py + 2, Player.px - 1] == ' ')
            {
                gotoxy(Player.px, Player.py);
                removeplayer(Player);
                Player.px = Player.px - 1;
                gotoxy(Player.px, Player.py);
                printPlayer(player, Player);
            }

        }
        static void moveup(char[,] player,player Player)
        {

            if (array[Player.py - 1, Player.px] == ' ' && array[Player.py - 1, Player.px + 1] == ' ' && array[Player.py - 1, Player.px + 2] == ' ')
            {
                gotoxy(Player.px, Player.py);
                removeplayer(Player);
                Player.py = Player.py - 1;
                gotoxy(Player.px, Player.py);
                printPlayer(player, Player);
            }

        }
        static void movedown(char[,] player, player Player)
        {

            if (array[Player.py + 4, Player.px] == ' ' && array[Player.py + 3, Player.px + 1] == ' ' && array[Player.py + 3, Player.px + 2] == ' ')
            {
                gotoxy(Player.px, Player.py);
                removeplayer(Player);
                Player.py = Player.py + 1;
                gotoxy(Player.px, Player.py);
                printPlayer(player, Player);
            }
        }
        static void removeplayer(player Player)
        {
            gotoxy(Player.px, Player.py);
            Console.Write("   ");
            gotoxy(Player.px, Player.py + 1);
            Console.Write("   ");
            gotoxy(Player.px, Player.py + 2);
            Console.Write("   ");
        }
        static void removeVerticalGhost(verticalGhost vGhost)
        {
            gotoxy(vGhost.vgx, vGhost.vgy);
            Console.Write("   ");
            gotoxy(vGhost.vgx, vGhost.vgy + 1);
            Console.Write("   ");
            gotoxy(vGhost.vgx, vGhost.vgy + 2);
            Console.Write("   ");
        }
        static void moveVerticalGhost(char[,] verticalGhost, verticalGhost vGhost)
        {
            if (direction == "down")
            {

                if (array[vGhost.vgy + 3, vGhost.vgx] == '%' || array[vGhost.vgy + 3, vGhost.vgy + 1] == '%' || array[vGhost.vgy + 3, vGhost.vgx + 2] == '%' || array[vGhost.vgy + 3, vGhost.vgx] == '*' || array[vGhost.vgy + 3, vGhost.vgy + 1] == '*' || array[vGhost.vgy + 3, vGhost.vgx + 2] == '*')
                {
                    direction = "up";
                }
                else
                {
                    removeVerticalGhost(vGhost);
                    vGhost.vgy = vGhost.vgy + 1;
                    printVerticalGhost(verticalGhost, vGhost);
                }
            }
            if (direction == "up")
            {

                if ((array[vGhost.vgy - 1, vGhost.vgx] == '%' || array[vGhost.vgy - 1, vGhost.vgx - 1] == '%' || array[vGhost.vgy - 1, vGhost.vgx + 2] == '%') || (array[vGhost.vgy - 1, vGhost.vgx] == '*' || array[vGhost.vgy - 1, vGhost.vgx - 1] == '*' || array[vGhost.vgy - 1, vGhost.vgx + 2] == '*'))
                {
                    direction = "down";
                }
                else
                {
                    removeVerticalGhost(vGhost);
                    vGhost.vgy = vGhost.vgy - 1;
                    printVerticalGhost(verticalGhost, vGhost);
                }
            }
        }
        static void generateBullet(List<playerBullet> b, player Player)
        {
            playerBullet info = new playerBullet();
            info.bulletX = Player.px + 3;
            info.bulletY = Player.py + 1;
            info.isBulletActive = true;
            b.Add(info);
            //isBulletActive[bulletCount] = true;
            gotoxy(Player.px + 3,Player. py + 1);
            Console.Write(">");
           // bulletCount++;
        }
        static void moveBullet(List<playerBullet> b,playerBullet p)
        {
            for (int i = 0; i < b.Count; i++)
            {
                
                if (b[i].isBulletActive == true)
                {
                    
                    if (array[b[i].bulletY,b[i].bulletX+1] != ' ')
                    {
                       
                        eraseBullet(p);
                        //makeBulletInactive(i, isBulletActive);
                        if (array[b[i].bulletY,b[i]. bulletX + 1] == '*')
                        {
                            Console.Write(" ");
                            addScore();
                        }
                    }
                    else
                    {
                        
                        eraseBullet(p);
                        b[i].bulletX = b[i].bulletX + 1;
                        printBullet(p);
                    }
                }

            }
        }
        static void makeBulletInactive(int i, bool[] isBulletActive)
        {
            isBulletActive[i] = false;
        }
        static void printBullet(playerBullet b)
        {
            gotoxy(b.bulletX, b.bulletY);
            Console.Write(">");
        }
        static void eraseBullet(playerBullet b)
        {
            gotoxy(b.bulletX, b.bulletY);
            Console.Write(" ");
        }



    }
}
