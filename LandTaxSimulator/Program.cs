﻿namespace LandTaxSimulator
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
