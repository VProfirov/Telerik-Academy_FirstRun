﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var someF = "Red";
            var f = (Feathers) Enum.Parse(typeof(Feathers), someF, true);
            Console.WriteLine(f);
        }

        enum Feathers { Red, Green }
    }
}