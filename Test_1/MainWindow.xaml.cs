using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Input;
using System.Threading;
using System.Timers;

namespace Test_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int x;  //x & y snake
        int y;
        int oldy; 
        int oldx;
        int oldyo;
        int oldxo;
        int p; //points

        public static int speed = 200; //move timer speed
        public static bool left = false; //move direction
        public static bool right = false;
        public static bool up = false;
        public static bool down = false;
        
        public static int movet = 30; //move width snake
        public static bool ea=true; //stop game

        public static int targety = rand(2, 12)*30; //random target y
        public static int targetx = rand(2,26)*30; //random target x
        private static int rand(int min, int max) //random number generator
        {
            Random random = new Random();
            return random.Next(min, max);
        }
       
        public MainWindow()
        {
            

            InitializeComponent(); //draw Vectors

            DispatcherTimer keyt = new DispatcherTimer();   //keyboard input & old gen
            keyt.Interval = TimeSpan.FromMilliseconds(25);
            keyt.Tick += key;
            keyt.Start();

            DispatcherTimer movet = new DispatcherTimer();     //move snake by keyboard input
            movet.Interval = TimeSpan.FromMilliseconds(speed);
            movet.Tick += move_Tick;
            movet.Start();

            DispatcherTimer col = new DispatcherTimer();    //collision with walls
            col.Interval = TimeSpan.FromMilliseconds(25);
            col.Tick += collision;
            col.Start();

            DispatcherTimer tar = new DispatcherTimer();    //check if position snake == position target
            tar.Interval = TimeSpan.FromMilliseconds(25);
            tar.Tick += Target;
            tar.Start();

            Label3.Content = "targetx: " + targetx; //Debug info
            Label4.Content = "targety: " + targety; //Debug info

        }

        private void Target(object sender, EventArgs e)
        {
            Canvas.SetLeft(T, targetx);
            Canvas.SetTop(T, targety);
            if (Canvas.GetLeft(Q1) == Canvas.GetLeft(T) && Canvas.GetTop(Q1) == Canvas.GetTop(T))
            {
                p++;
                PA.Content = p;
                targetx = rand(2, 26) * 30; //move target to ranx/rany
                targety = rand(2, 12) * 30;

                Canvas.SetLeft(T, targetx);
                Canvas.SetTop(T, targety);
                speed -= 200;
            }

        }

        private void collision(object sender, EventArgs e)
        {
            if (Canvas.GetLeft(Q1) == 0 | Canvas.GetLeft(Q1) == 810 | Canvas.GetTop(Q1) == 0 | Canvas.GetTop(Q1) == 390)
            {
                left = false;
                right = false;
                up = false;
                down = false;
                ea = false;
                MessageBox.Show("Game Over");
                Canvas.SetLeft(Q1, 420); Canvas.SetLeft(Q2, 390); Canvas.SetLeft(Q3, 360);
                Canvas.SetTop(Q1, 180); Canvas.SetTop(Q2, 180); Canvas.SetTop(Q3, 180);
                Canvas.SetTop(A1, 203);         Canvas.SetLeft(A1, 445);
                Canvas.SetTop(A1_Copy, 183); Canvas.SetLeft(A1_Copy, 445);

                left = false;right = false; up = false; down = false;
                ea = true;
                p = 0;
                PA.Content = p;
              
            }
            
        }

        private void key(object sender, EventArgs e)
        {
            oldx = (int)Canvas.GetLeft(Q1);
            oldy = (int)Canvas.GetTop(Q1);
            oldxo = (int)Canvas.GetLeft(Q2);
            oldyo = (int)Canvas.GetTop(Q2);
            
                if (Keyboard.IsKeyDown(Key.D) & left==false)
                {
                    right = true;
                    down = false;
                    up = false;
                    left = false;
                }
                if (Keyboard.IsKeyDown(Key.A) & right==false)
                {
                    left = true;
                    right = false;
                    up = false;
                    down = false;
                }
                if (Keyboard.IsKeyDown(Key.W) & down == false)
                {
                    up = true;
                    right = false;
                    down = false;
                    left = false;
                }
                if (Keyboard.IsKeyDown(Key.S) & up == false)
                {
                    down = true;
                    right = false;
                    up = false;
                    left = false;
                
            }
        }
     
        private void move_Tick(object sender, EventArgs e)
        {
            

            if (ea == true) {
                
                
                    if (right == true)
                    {
                        Canvas.SetLeft(Q1, Canvas.GetLeft(Q1) + movet);
                        Canvas.SetLeft(Q2, oldx);
                        Canvas.SetTop(Q2, oldy);
                        Canvas.SetLeft(Q3, oldxo);
                        Canvas.SetTop(Q3, oldyo);
                    Canvas.SetLeft(A1, Canvas.GetLeft(Q1)+25);
                    Canvas.SetTop(A1, Canvas.GetTop(Q1) + 23);

                    Canvas.SetLeft(A1_Copy, Canvas.GetLeft(Q1) + 25);
                    Canvas.SetTop(A1_Copy, Canvas.GetTop(Q1) + 3);

                }

                if (left == true)
                    {
                        Canvas.SetLeft(Q1, Canvas.GetLeft(Q1) - movet);
                        Canvas.SetLeft(Q2, oldx);
                        Canvas.SetTop(Q2, oldy);
                        Canvas.SetLeft(Q3, oldxo);
                        Canvas.SetTop(Q3, oldyo);
                    Canvas.SetLeft(A1, Canvas.GetLeft(Q1) + 3);
                    Canvas.SetTop(A1, Canvas.GetTop(Q1) + 23);

                    Canvas.SetLeft(A1_Copy, Canvas.GetLeft(Q1) + 3);
                    Canvas.SetTop(A1_Copy, Canvas.GetTop(Q1) + 3);
                }
                    if (up == true)
                    {
                        Canvas.SetTop(Q1, Canvas.GetTop(Q1) - movet);
                        Canvas.SetLeft(Q2, oldx);
                        Canvas.SetTop(Q2, oldy);
                        Canvas.SetLeft(Q3, oldxo);
                        Canvas.SetTop(Q3, oldyo);
                    Canvas.SetLeft(A1, Canvas.GetLeft(Q1) + 3);
                    Canvas.SetTop(A1, Canvas.GetTop(Q1) + 3);

                    Canvas.SetLeft(A1_Copy, Canvas.GetLeft(Q1) + 23);
                    Canvas.SetTop(A1_Copy, Canvas.GetTop(Q1) + 3);
                }
                    if (down == true)
                    {
                        Canvas.SetTop(Q1, Canvas.GetTop(Q1) + movet);
                        Canvas.SetLeft(Q2, oldx);
                        Canvas.SetTop(Q2, oldy);
                        Canvas.SetLeft(Q3, oldxo);
                        Canvas.SetTop(Q3, oldyo);
                    Canvas.SetLeft(A1, Canvas.GetLeft(Q1) + 3);
                    Canvas.SetTop(A1, Canvas.GetTop(Q1) + 23);

                    Canvas.SetLeft(A1_Copy, Canvas.GetLeft(Q1) + 23);
                    Canvas.SetTop(A1_Copy, Canvas.GetTop(Q1) + 23);
                }
                else { }

            }

            x = (int)Canvas.GetLeft(Q1);
            y = (int)Canvas.GetTop(Q1);

            Label1.Content ="x: "+ x; //Debug info
            Label2.Content ="y: "+ y; //Debug info

        }
    }
}

