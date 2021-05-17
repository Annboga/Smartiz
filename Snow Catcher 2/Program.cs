using NetProcessing;
using Processing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing
{
    public class Program : SmartizSketch
    {
        [STAThread]
        static void Main()
        {
            new Program().Start();
        }

        Raindrop[] Raindrops;
        Tongue Tongue;
        int Counter;
        double Timer;

        public override void Setup()
        {
            Size(600, 600);
            Background(21, 34, 56);

            Tongue = new Tongue(Width, Height);
            Raindrops = new Raindrop[50];

            Counter = Raindrops.Length - 10;
            Timer = 1000 * (Counter + 10);

            for (int i = 0; i < Raindrops.Length; i++)
            {
                Raindrops[i] = new Raindrop(Random(Width - 10), Random(Height), Random(10, 30), Random(10, 30), Random(1, 5), 255);
            }
        }

        public override void DrawFrame()
        {
          
            Background(21, 34, 56);
            Tongue.Draw();
            Tongue.Move(Width);

            foreach (var rain in Raindrops)
            {
                rain.Move();
                UpdateSnowflake(rain);
                rain.Draw();
              
            }
            if (!IsGameover())
            {
                Timer -= DeltaTime;
            }

            DrawDashboard();
           
        }

 
    private void DrawDashboard()
        {
            Fill(255);
            TextSize(25);
            Text("Timer: " + Round(Timer / 1000), 20, 40);
            Text("Counter: " + Counter, Width - 150, 40);
            Fill(255);

            TextSize(40);
            if (Counter == 0)
            {
                Text("YOU WON! :)", Width / 2 - 100, Height / 2);
            }
            else if (Timer < 0)
            {
                Text("YOU LOST! :(", Width / 2 - 100, Height / 2);
            } 
            //else if (rain.PickAColor() == "yellow")
            //{
             //   Text("YOU LOST! :(", Width / 2 - 100, Height / 2);
            //}
        }

        private void UpdateSnowflake(Raindrop rain)
        {
            if (IsOnTongue(rain))
            {
                rain.Melt();

                if (rain.IsMelted() && !IsGameover())
                {
                    Counter--;

                }

                if (rain.PickAColor() == "yellow")
                {
                    Timer = -1;

                }
            }
            else if (IsOutsideCanvas(rain))
            {
                rain.Refresh();
            }
        }

        private bool IsOutsideCanvas(Raindrop rain)
        {
            return rain.Y > Height + rain.Height; //itt eredetileg Size volt
        }

        private bool IsOnTongue(Raindrop rain)
        {
       
            return rain.Y > Tongue.Y + Tongue.Height / 2 && rain.X > Tongue.X + Tongue.Margin && rain.X < Tongue.X + Tongue.Width - Tongue.Margin && rain.Y < Height - Tongue.Margin;
        }

        public override void KeyReleased()
        {
            Tongue.Speed = 0;
        }

        public override void KeyPressed()
        {
            if (KeyCode == KC_LEFT)
            {
                Tongue.Speed = -5;
            }
            else if (KeyCode == KC_RIGHT)
            {
                Tongue.Speed = 5;
            }
        }

        private bool IsGameover()
        {
            return Counter == 0 || Timer < 0;
        }

    }
}
