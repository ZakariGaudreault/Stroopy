using System;
using System.Timers;
using System.Diagnostics;
class NamedExample
{
    static string ability; // Variable for a power-up
    static int life = 3; // number of life in game
    static bool stage1win = false; // if you win the easy mode this turns true
    static bool stage2win = false; // if you win the Medium mode this turns true
    static bool stage3win = false; // if you win the Hard mode this turns true
    static bool secret = false; // if all bool stages are this will be also true
    static int difficultyColornumbers; // number of different colors in the game modes
    static int row; // new rows
    static int col; // new cols
    static int oldrow = 0; // old rows
    static int oldcol = 0; // new cols
    static System.Timers.Timer gameTimer; // timer for the game
    static ConsoleKeyInfo key; // enable the use of keys
    static void Main(string[] args) // menu that will be shown only once


    {
        instruction();
        //my loop system
        bool loop = true;
        do
        {
            // if you won all three stages you unlock the secret code.
            if (stage1win && stage2win && stage3win == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(14, 24);
                Console.WriteLine("              By winning in every diffuclties, you unlocked the secret message. Type number 99");
                Console.ForegroundColor = ConsoleColor.White;
                secret = true;

            }
            // makes sure you chose a correct value
            int choicerule;
            bool listOfChoice;
            do
            {
                // game choice
                Console.SetCursorPosition(47, 0);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stage levels");
                Console.WriteLine("   ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                                                   1.  Beginner ");
                Console.WriteLine("   ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                                   2.  Medium ");
                Console.WriteLine("   ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                                                   3.  Hard ");
                Console.WriteLine("   ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(47, 9);
                Console.WriteLine("Others options");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("                                                   4.  Ability explanation ");
                Console.WriteLine("   ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("                                                   5.  Instructions");
                Console.WriteLine("   ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("                                                   6.  Credit");
                Console.WriteLine("   ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("                                                   7.  Close the game");
                Console.WriteLine("   ");
                if (secret == true)// if secret mode true, add this message
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("                                                  99. Secret message ");
                    Console.WriteLine("   ");
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("                                               Enter your Choice ");
                string choiceInput = Console.ReadLine();
                listOfChoice = int.TryParse(choiceInput, out choicerule);
                // This if/else make sure that the user input is between 1 to 8 or 99 and it is a number.

                {
                    if (choicerule > 0 && choicerule < 8 || choicerule == 99)
                    {
                        // if the choice is between 1 to 8, break will break out the while true loop or 99.
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Clear();
                        Console.SetCursorPosition(47, 22);
                        Console.WriteLine("Invalid answer"); // if the answer is not between 1 and 8 or the secret message return this.
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            while (true);
            {
                // Depending on the choice, this will direct to the desired function
                switch (choicerule)
                {
                    case 1:
                        stage1();
                        break;
                    case 2:
                        stage2();
                        break;
                    case 3:
                        stage3();
                        break;
                    case 99:
                        //
                        if (secret == true)
                        {
                            Console.Clear();
                            SecretMessage();

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Clear();
                            Console.SetCursorPosition(47, 22);
                            Console.WriteLine("Invalid answer"); // if the answer is not between 1 and 4 or the secret game return this.
                            Console.SetCursorPosition(47, 0);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        break;
                    case 4:
                        abilityexplanation();
                        break;
                    case 5:
                        instruction();
                        break;
                    case 6:
                        Credit();
                        break;
                    case 7:
                        Console.Clear();
                        Logo();
                        Console.SetCursorPosition(47, 10);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thank you for playing my game");
                        loop = false; // end the loop
                        break;
                }
            }
        }
        while (loop);


    }
    // My controls are here
    static void ProcessKeys()
    {
        // initialize
        row = Console.WindowHeight / 2;
        col = Console.WindowWidth / 2;
        // My game timer of five seconds
        Stopwatch timer = new Stopwatch();
        timer.Start();
        while (timer.Elapsed.TotalSeconds < 5)

        {
            // My controls
            key = Console.ReadKey(true);
            switch (key.Key)

            {

                case ConsoleKey.UpArrow:
                    row = row - 1;
                    break;
                case ConsoleKey.DownArrow:
                    row = row + 1;
                    break;
                case ConsoleKey.RightArrow:

                    // if the palyer chose the Faster ability then they will move from 2 collums
                    // instead of 1
                    if (ability == "Faster")
                    {
                        col = col + 2;
                    }
                    else
                    {
                        col = col + 1;
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    if (ability == "Faster")
                    {
                        col = col - 2;
                    }
                    else
                    {
                        col = col - 1;
                    }

                    break;
                case ConsoleKey.Enter:
                    break;
            }
            if (key.Key == ConsoleKey.Enter)
                break;
        }
        // timer stop after 5 seconds       
        timer.Stop();


    }

    static void SetupGameTimer()
    {
        // my setup for the game
        gameTimer = new System.Timers.Timer(50);
        gameTimer.Elapsed += Draw;
        gameTimer.AutoReset = true;
        gameTimer.Enabled = true;
    }
    static void Draw(Object source, ElapsedEventArgs e)
    {
        if (oldrow != row || oldcol != col)
        {
            // This is what makes the player unable to go past the play zone
            Console.SetCursorPosition(col, row);
            if (col < 19)
            {
                // if the player is trying to go under 19(wall on the left), push the cursor 4 column on the right
                col = col + 4;
            }
            if (col > 103)
            {
                // if the player is trying to go over 103(wall on the right), push the cursor 4 column behind
                col = col - 4;
            }
            if (row > 25)
            {
                // if the player tries to go over 23 then (bottom wall) then push the cursor 4 row on top
                row = row - 4;
            }
            if (row < 2)
            {
                row = row + 4;
            }
            // If you chose the powerup of teleporter, here is how it works
            // if the the player is in a precise spot(the three arrows) then teleport him
            if (row == 15 && col >= 55 && col <= 58 && ability == "teleporter")
            {
                col = col - 37;
            }
            if (row == 15 && col >= 64 && col <= 67 && ability == "teleporter")
            {
                col = col + 37;
            }
            if (row == 15 && col <= 99 && col >= 96 && ability == "teleporter")
            {
                col = col - 38;
            }
            if (row == 15 && col >= 23 && col <= 26 && ability == "teleporter")
            {
                col = col + 37;
            }
            oldrow = row;
            oldcol = col;
        }
    }
    static void Zone3()
    {

        {
            // My map for the hard mode in Red
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("                  ―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――― ");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │―――――――――――――――――――――――――――――――――――――――――    ――――――――――――――――――――――――――――――――――――――――― |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                 │                     |                   │  |                   |                      |");
            Console.WriteLine("                  ―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――― ");
            // All these lines chose the zones for the game
            Console.SetCursorPosition(26, 20);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Yellow");
            Console.SetCursorPosition(26, 7);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Red");
            Console.SetCursorPosition(70, 7);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Blue");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(70, 20);
            Console.WriteLine("Green");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(90, 20);
            Console.WriteLine("Cyan");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(89, 7);
            Console.WriteLine("Magenta");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(48, 7);
            Console.WriteLine("Gray");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(48, 20);
            Console.WriteLine("White");
            Console.ForegroundColor = ConsoleColor.Red;
            if (ability == "teleporter")
            {
                teleporter(); // this will add teleporters if the player chose the player ability
            }

        }

    }
    static void Zone2()
    {
        // My map for the Medium mode in Yellow
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("                                                                                                         ");
        Console.WriteLine("                  ――――――――――――――――――――――――――――――――――――――――― ―――――――――――――――――――――――――――――――――――――――――――― ");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │―――――――――――――――――――――――――――――――――――――――――    ――――――――――――――――――――――――――――――――――――――――― |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                  ――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――― ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(35, 20);
        Console.WriteLine("Yellow");
        Console.SetCursorPosition(36, 7);
        Console.WriteLine("Red");
        Console.SetCursorPosition(80, 7);
        Console.WriteLine("Blue");
        Console.SetCursorPosition(80, 20);
        Console.WriteLine("Green");
        if (ability == "teleporter")
        {
            teleporter(); // this will add teleporters if the player chose the player ability
        }
    }
    static void Zone1()
    {
        // My map for the Easy mode in Green
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("                                                                                                         ");
        Console.WriteLine("                  ――――――――――――――――――――――――――――――――――――――――― ―――――――――――――――――――――――――――――――――――――――――――― ");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         |  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                 │                                         │  |                                          |");
        Console.WriteLine("                  ――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――― "); Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(36, 14);
        Console.WriteLine("Red");

        Console.SetCursorPosition(80, 14);
        Console.WriteLine("Blue");
        if (ability == "teleporter")
        {
            teleporter(); // this will add teleporters if the player chose the player ability
        }
    }
    static void stage3()
    {
        // This lets you chose an ability
        abilityChoice();
        // Set up the game over to false and clear the menu
        bool gameover = false;
        Console.Clear();
        // the difficulty is eight because there is 8 colors (see the rdm color function for more information)
        difficultyColornumbers = 8;
        // This will take the map for the zone 3, which is the hardest
        Zone3();
        if (ability == "extra life")
        {
            life = 4;
        }
        else
        {
            life = 3;
        }
        // As long as the score is under 7 or life over 0, the game will continue
        for (int score = 0; score < 7; score++)
        {
            // healthstate is a function that changes the color of the life and score on the left side of the screen
            // You go from green to yellow to red
            healthstate();
            // This shows the number of life for the user
            Console.WriteLine("number of life " + life);
            Console.SetCursorPosition(0, 8);
            // This shows the score to the user
            Console.WriteLine("score is  " + score + " ");
            bool validation = false;
            // rdm color will decide a random color to display on the screen
            int randomcolor = rdmcolor();
            // the timer of the game and the function to let the player move
            SetupGameTimer();
            ProcessKeys();
            // Validates if the user is in the right zone
            validation = Zone3Validation(randomcolor);
            // if the user is right, this will give him a point
            if (validation == true)
            {

            }
            else
            {
                // if the user is wrong and has under 2 life
                // it means that the player made his last mistake and finishes the for loop with 
                // gmaeover on true ( see below what it does)
                if (life < 2)
                {

                    gameover = true;
                    score = 9999;

                }
                // Otherwise if it is not the last life of the player, remove a life
                // and remove the point that the player was supposed to get
                else
                    life = life - 1;
                score--;

            }
        }// finish for loop

        // if gameover is false then send a win message and unlock the stage 3 win condition
        if (gameover == false)
        {
            stage3win = true;
            winMessage();
        }
        else
        {
            // Otherwise the player gets a game over message and not win condition
            gameOverMessage();
        }
    }
    static void stage2()
    {
        // This lets you chose an ability
        abilityChoice();
        // same thing explained in stage 3 ( upward) except for...
        bool gameover = false;
        Console.Clear();
        // the difficulty level is set to 4 because there is 4 colors in total
        difficultyColornumbers = 4;
        Zone2();
        if (ability == "extra life")
        {
            life = 4;
        }
        else
        {
            life = 3;
        }
        for (int score = 0; score < 7; score++)
        {
            // change the color depending on you life
            healthstate();
            Console.WriteLine("number of life " + life);
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("score is  " + score + " ");
            bool validation = false;
            int randomcolor = rdmcolor();
            SetupGameTimer();
            ProcessKeys();
            validation = Zone2Validation(randomcolor);
            if (validation == true)
            {

            }
            else
            {
                if (life < 2)
                {
                    gameover = true;
                    score = 999;
                }
                else
                    life = life - 1;
                score--;
            }
        }
        if (gameover == false)
        {
            // if score is 7 then unlock stage2win for the secret message and send a win message
            stage2win = true;
            winMessage();
        }
        else
        {
            //if life = 0 then send the gameover message
            gameOverMessage();
        }
    }
    static void stage1()
    {
        // This lets you chose an ability
        abilityChoice();
        // same thing explained in stage 3 ( upward) except for...
        bool gameover = false;
        Console.Clear();
        // The difficulty level is set to 2 because there is 2 colors only.
        difficultyColornumbers = 2;
        Zone1();
        if (ability == "extra life")
        {
            life = 4;
        }
        else
        {
            life = 3;
        }

        for (int score = 0; score < 7; score++)
        {
            Console.SetCursorPosition(0, 6);
            healthstate();
            Console.WriteLine("number of life " + life);
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("score is  " + score + " ");
            bool validation = false;

            int randomcolor = rdmcolor();
            SetupGameTimer();
            ProcessKeys();
            validation = Zone1Validation(randomcolor);

            if (validation == true)
            {

            }
            else
            {
                if (life < 2)
                {
                    gameover = true;
                    score = 999;
                }
                else
                    life = life - 1;
                score--;

            }
        }
        if (gameover == false)
        {
            stage1win = true;
            winMessage();
        }
        else
        {
            gameOverMessage();
        }
    }
    static int rdmcolor()
    {
        bool sameColor = true;
        do
        {
            // the whole list of colors in the game in an array
            string[] colorword = new string[8] { "Red     ", "Blue    ", "Yellow  ", "Green    ", "Magenta ", "Cyan      ", "Gray     ", "White      " };
            Random rnd = new Random();
            // this line choses a random color from the list
            int colornumber = rnd.Next(0, difficultyColornumbers);
            // this line choses a random color of text
            int fontnumber = rnd.Next(0, difficultyColornumbers);
            do
            {
                // if the font number and color
                if (colornumber == fontnumber)
                {
                    colornumber = rnd.Next(0, difficultyColornumbers);
                    fontnumber = rnd.Next(0, difficultyColornumbers);
                }
                else
                {
                    sameColor = false;
                }
            }
            // depending on the number of the color of fontnumber
            // This will be the color for the word on the top of the screen
            while (sameColor == true);

            if (fontnumber == 0)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(59, 0);
                // this line shows the word and color on top of the screen
                Console.WriteLine(colorword[colornumber]);
                // return the font number as it will be useful to validate the zones of the color chosen
                return fontnumber;
            }
            if (fontnumber == 1)
            {

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(59, 0);
                Console.WriteLine(colorword[colornumber]);
                return fontnumber;
            }
            if (fontnumber == 2)
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(59, 0);
                Console.WriteLine(colorword[colornumber]);
                return fontnumber;
            }
            if (fontnumber == 3)
            {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(59, 0);
                Console.WriteLine(colorword[colornumber]);
                return fontnumber;
            }
            if (fontnumber == 4)
            {

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(59, 0);
                Console.WriteLine(colorword[colornumber]);
                return fontnumber;
            }
            if (fontnumber == 5)
            {

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(59, 0);
                Console.WriteLine(colorword[colornumber]);
                return fontnumber;
            }

            if (fontnumber == 6)
            {

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(59, 0);
                Console.WriteLine(colorword[colornumber]);
                return fontnumber;
            }

            if (fontnumber == 7)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(59, 0);
                Console.WriteLine(colorword[colornumber]);
                return fontnumber;
            }

        } while (true);

    }
    static bool Zone1Validation(int randomcolor)

    {

        // This will return a true of false bool depending if the player is in the zone
        // of the color of the word chosen in rdm color
        if (randomcolor == 0)
        {
            // if the player is betwwen col > 15 && col < 59 && row < 27 && row > 0 then
            // return true and dont remove a point
            if (col > 15 && col < 59 && row < 27 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 1)
        {
            if (col > 62 && col < 110 && row < 27 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        return false;

    }
    static bool Zone2Validation(int randomcolor)
    {
        // This will return a true of false bool depending if the player is in the zone
        // of the color of the word chosen in rdm color
        if (randomcolor == 0)
        {
            if (col > 18 && col < 59 && row < 13 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 1)
        {
            if (col > 62 && col < 99 && row < 13 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 2)
        {
            if (col > 18 && col < 59 && row < 27 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 3)
        {
            if (col > 62 && col < 99 && row < 27 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    static bool Zone3Validation(int randomcolor)
    {
        // This will return a true of false bool depending if the player is in the zone
        // of the color of the word chosen in rdm color
        if (randomcolor == 0)
        {
            // if the player is between col 18 and 39 and also between then return true
            // which will not remove a life
            if (col > 18 && col < 39 && row < 13 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 1)
        {
            if (col > 62 && col < 82 && row < 13 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 2)
        {
            if (col > 12 && col < 39 && row < 27 && row > 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 3)
        {
            if (col > 62 && col < 82 && row < 27 && row > 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 4)
        {
            if (col > 82 && col < 105 && row < 13 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 5)
        {
            if (col > 82 && col < 105 && row < 27 && row > 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 6)
        {
            if (col > 39 && col < 59 && row < 13 && row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (randomcolor == 7)
        {
            if (col > 39 && col < 59 && row < 27 && row > 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        return false;
    }
    static void healthstate()
    {
        // My health state explained earlier
        Console.SetCursorPosition(0, 6);
        // 3 lives is green
        if (life == 4)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        if (life == 3)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        // 2 is yellow
        if (life == 2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        // One is red
        if (life == 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
    static void gameOverMessage()
    {
        // the game over message when the player loses at 0 life
        Console.Clear();
        Console.SetCursorPosition(54, 14);
        Console.WriteLine("Gameover");
        Console.SetCursorPosition(40, 18);
        Console.WriteLine("Press anything to comeback to the menu");
        Console.ReadKey();
        Console.Clear();
    }
    static void winMessage()
    {
        // the win message when the player gets to 7 score
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Clear();
        Console.SetCursorPosition(44, 14);
        Console.WriteLine("Congrats!!!! You won this round.");
        Console.SetCursorPosition(40, 18);
        Console.WriteLine("Press anything to comeback to the menu");
        Console.ReadKey();
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
    }
    static void SecretMessage()
    {
        // this is my secret message for when you beat all the levels. Please dont cheat!!!!
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(42, 14);
        Console.WriteLine("https://youtu.be/BcyYQXTHIV8?t=29");
        Console.SetCursorPosition(40, 18);
        Console.WriteLine("Press anything to comeback to the menu");
        Console.ReadKey();
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
    }
    static void Credit()
    {
        // This is not very important, I just wanted some credits
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Production");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Design");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Programming");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Art/Graphics");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Quality Assurance");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Marketing");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Creative Services");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Customer/Technical Support");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Administration");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Support");
        Console.WriteLine("Zakari Gaudreault St-Jean");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Special thank you to");
        Console.WriteLine("Sandra Bultena for helping me");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Press anything to continue");
        Console.ReadKey();
        Console.Clear();

    }
    static void instruction()
    {
        // this my instructions
        Console.Clear();
        // call the Logo because it is pretty : )
        Logo();
        // all the next text is to center the text in the middle of the screen
        string line1 = "Welcome to my stroopy game";
        Console.SetCursorPosition((Console.WindowWidth - line1.Length) / 2, Console.CursorTop);
        Console.WriteLine(line1);
        string line2 = "The game is simple, yet tricky";
        Console.SetCursorPosition((Console.WindowWidth - line2.Length) / 2, Console.CursorTop);
        Console.WriteLine(line2);
        Console.ForegroundColor = ConsoleColor.Magenta;
        string line3 = "In this game you are required to move to a zone depending of a color of a word and not the word";
        Console.SetCursorPosition((Console.WindowWidth - line3.Length) / 2, Console.CursorTop);
        Console.WriteLine(line3);
        string line4 = "You can find the color for the zone on the middle top of your screen. For example, for the word";
        Console.SetCursorPosition((Console.WindowWidth - line4.Length) / 2, Console.CursorTop);
        Console.WriteLine(line4);
        Console.ForegroundColor = ConsoleColor.Blue;
        string line5 = "Red";
        Console.SetCursorPosition((Console.WindowWidth - line5.Length) / 2, Console.CursorTop);
        Console.WriteLine(line5);
        Console.ForegroundColor = ConsoleColor.White;
        string line6 = "you should go to the zone with the word ''blue'' in it because the color of the word is in blue";
        Console.SetCursorPosition((Console.WindowWidth - line6.Length) / 2, Console.CursorTop);
        Console.WriteLine(line6);
        string line7 = "You can move with the arrows and can press enter once you think you are in the right zone";
        Console.SetCursorPosition((Console.WindowWidth - line7.Length) / 2, Console.CursorTop);
        Console.WriteLine(line7);
        Console.ForegroundColor = ConsoleColor.Green;
        string line8 = "However if you did not choose anything after 5 seconds";
        Console.SetCursorPosition((Console.WindowWidth - line8.Length) / 2, Console.CursorTop);
        Console.WriteLine(line8);
        string line8p2 = "pressing any buttons will automatically look if you are in the right zone";
        Console.SetCursorPosition((Console.WindowWidth - line8p2.Length) / 2, Console.CursorTop);
        Console.WriteLine(line8p2);
        string line9 = "If you did not get the answer right, a life will be removed. You only have 3 lives so be careful";
        Console.SetCursorPosition((Console.WindowWidth - line9.Length) / 2, Console.CursorTop);
        Console.WriteLine(line9);
        Console.ForegroundColor = ConsoleColor.Yellow;
        string line10 = "if you make it to a score of seven points, you win";
        Console.SetCursorPosition((Console.WindowWidth - line10.Length) / 2, Console.CursorTop);
        Console.WriteLine(line10);
        string line10p2 = "When you choose a level you will able to choose a special ability";
        Console.SetCursorPosition((Console.WindowWidth - line10p2.Length) / 2, Console.CursorTop);
        Console.WriteLine(line10p2);
        Console.ForegroundColor = ConsoleColor.Cyan;
        string line10p3 = "You can have more info on these abilities in the ability explanation section";
        Console.SetCursorPosition((Console.WindowWidth - line10p3.Length) / 2, Console.CursorTop);
        Console.WriteLine(line10p3);
        string line11 = "I strongly suggest that you start with the first level to understand the concept";
        Console.SetCursorPosition((Console.WindowWidth - line11.Length) / 2, Console.CursorTop);
        Console.WriteLine(line11);
        Console.ForegroundColor = ConsoleColor.Red;
        string line12 = "Good luck !";
        Console.SetCursorPosition((Console.WindowWidth - line12.Length) / 2, Console.CursorTop);
        Console.WriteLine(line12);
        string line13 = "Press anything if you understand";
        Console.SetCursorPosition((Console.WindowWidth - line13.Length) / 2, Console.CursorTop);
        Console.WriteLine(line13);
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.White;
        string line14 = "PS, if you win all three levels, you will win a secret code in the menu : )";
        Console.SetCursorPosition((Console.WindowWidth - line14.Length) / 2, Console.CursorTop);
        Console.WriteLine(line14);
        Console.ReadKey();
        Console.Clear();
    }
    static void abilityexplanation()
    {
        // Very simple. this just the part where you can learn more about the abilities
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Healthy: This ability gives you an extra life so instead of having 3 lives, you have 4.");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Teleportions: This ability creates four teleporters in the map to make it easier to move from");
        Console.WriteLine("side to side of the map by moving to the >>> symbols on the map. ");
        Console.WriteLine("You may want to only use it for the last level. ");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Sugar Rush: This ability allows you to move from left to right 2 times faster");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Press anything to continue");
        Console.ReadKey();
        Console.Clear();
    }
    static void abilityChoice()
    {
        Console.Clear();
        int choicerule;
        bool listOfChoice;
        do
        {
            // menu to choose an ability
            Console.ForegroundColor = ConsoleColor.Green;
            string line1 = "You can chose an ability!";
            Console.SetCursorPosition((Console.WindowWidth - line1.Length) / 2, Console.CursorTop);
            Console.WriteLine(line1);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string line2 = "1. Healthy";
            Console.SetCursorPosition((Console.WindowWidth - line2.Length) / 2, Console.CursorTop);
            Console.WriteLine(line2);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string line3 = "2. Teleportation";
            Console.SetCursorPosition((Console.WindowWidth - line3.Length) / 2, Console.CursorTop);
            Console.WriteLine(line3);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("");
            string line4 = "3. Sugar rush";
            Console.SetCursorPosition((Console.WindowWidth - line4.Length) / 2, Console.CursorTop);
            Console.WriteLine(line4);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            string line5 = "4. Nothing because I feel cool";
            Console.SetCursorPosition((Console.WindowWidth - line5.Length) / 2, Console.CursorTop);
            Console.WriteLine(line5);
            string choiceInput = Console.ReadLine();
            listOfChoice = int.TryParse(choiceInput, out choicerule);
            // the choice has tho be between 1 and 4
            {
                if (choicerule > 0 && choicerule < 5)
                {
                    // once the coice is chosen, this will change the global variable ability and change some
                    // part of the code depending on what you chose
                    if (choicerule == 1)
                    {
                        ability = "extra life";
                        break;
                    }
                    if (choicerule == 2)
                    {
                        ability = "teleporter";
                        break;
                    }
                    if (choicerule == 3)
                    {
                        ability = "Faster";
                        break;
                    }
                    if (choicerule == 4)
                    {
                        ability = "Nothing";
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.SetCursorPosition(52, 18);
                    Console.WriteLine("Invalid answer");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(0, 0);
                }
            }
        } while (true);
    }
    static void teleporter()
    {
        // the position of the teleporters
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(55, 15);
        Console.WriteLine("<<<");
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(65, 15);
        Console.WriteLine(">>>");
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(24, 15);
        Console.WriteLine(">>>");
        Console.SetCursorPosition(96, 15);
        Console.WriteLine("<<<");
    }
    static void Logo()
    {
        // My pretty Logo
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                                ▀ ▀ ▄▄  █████████████████████████████████████████ ▀  ▄▀   ▄  ▀   ▄  ▀  ▀ ▀   ▄    ▄ ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("                                        █─▄▄▄▄█─▄─▄─█▄─▄▄▀█─▄▄─█─▄▄─█▄─▄▄─█▄─█─▄█  ▀  ▄▀ ▀   ▄ ▀ ▀   ▄ ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("       ▀  ▄▀   ▄  ▀  ▄  ▀ ▀ ▀ ▄  ▄▀  ▄  █▄▄▄▄─███─████─▄─▄█─██─█─██─██─▄▄▄██▄─▄██");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("                                        ▀▄▄▄▄▄▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▀▄▄▄▄▀▄▄▄▀▀▀▀▄▄▄▀▀");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("                             ▄▄   ▄▀▀▄        ███  █     ████     █████  ██");
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("                                     ▄▄▀    ▀  ▄        ████     ███   █     █████  ██");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("                            ▄▄▀    ▀  ▄        ██   ██     ███   █     █████  ██");
        Console.ForegroundColor = ConsoleColor.Cyan;
    }
}