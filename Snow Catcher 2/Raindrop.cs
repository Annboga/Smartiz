using Processing;
using NetProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing
{
    class Raindrop
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Velocity { get; set; }
        public int Opacity { get; set; }
        public SmartizSketch.PImage Blueraindrop { get; set; }
        public SmartizSketch.PImage Yellowraindrop { get; set; }

        public double generatedNumber = SmartizSketch.Random(0, 10);
       

        public Raindrop(double X, double Y, double Width, double Height, double Velocity, int Opacity)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Velocity = Velocity;
            this.Opacity = Opacity;
            this.Blueraindrop = SmartizSketch.LoadImage("Blue raindrop.png");
            this.Yellowraindrop = SmartizSketch.LoadImage("Yellow raindrop.png");
            
        }
        
        public void Setup()
        {
            PickAColor();
        }

        public void Draw()
        {
            DrawRaindrop();
        }

        public string PickAColor()
        {
            if (generatedNumber < 2.5)
            {  
                return "yellow";
            }
            else 
            {
                return "blue";
            }
        }


        public void DrawRaindrop()
        {
            if(PickAColor() == "blue")
            {
                SmartizSketch.Image(Blueraindrop, X, Y, Width, Height);
            } else if (PickAColor() == "yellow")
            {
                SmartizSketch.Image(Yellowraindrop, X, Y, Width, Height);
            }
        }

      
        public void Move()
        {
            X = Math.Sin(20 * Y) + X;
            Y += Velocity;
        }

        public void Melt()
        {
            Opacity -= 60;
            Velocity -= 0.5;

        }

        public bool IsMelted()
        {
            return Opacity < 255;
        }

        public void Refresh()
        {
            Y = -100;
            X += SmartizSketch.Random(-30, 30);
            Opacity = 255;
        }
    }
}
