/*
                           __                           __     
                         /'__`\                  __    /\ \    
 _____      __     _ __ /\ \/\ \    ___     ___ /\_\   \_\ \   
/\ '__`\  /'__`\  /\`'__\ \ \ \ \ /' _ `\  / __`\/\ \  /'_` \  
\ \ \L\ \/\ \L\.\_\ \ \/ \ \ \_\ \/\ \/\ \/\ \L\ \ \ \/\ \L\ \ 
 \ \ ,__/\ \__/.\_\\ \_\  \ \____/\ \_\ \_\ \____/\ \_\ \___,_\
  \ \ \/  \/__/\/_/ \/_/   \/___/  \/_/\/_/\/___/  \/_/\/__,_ /
   \ \_\                                                       
    \/_/                                      addicted to code


Copyright (C) 2018  Stefan 'par0noid' Zehnpfennig

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Text;
using System.IO;

namespace par0noid
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load PNG-File from harddrive
            byte[] PNG = File.ReadAllBytes($@"{Environment.CurrentDirectory}\test.png");

            //Set your data to inject
            byte[] DataToInject = Encoding.UTF8.GetBytes("Hello World!");

            //Inject your data (PNG will be "overwritten")
            PNGInjector.Inject(ref PNG, DataToInject);

            //Save the PNG with injected data to your harddrive
            File.WriteAllBytes($@"{Environment.CurrentDirectory}\test_injected.png", PNG);

            //Check if the PNG is injected
            if(PNGInjector.IsInjected(ref PNG))
            {
                Console.WriteLine($"Readed data: {Encoding.UTF8.GetString(PNGInjector.Read(ref PNG))}");
            }

            //Define new data to inject
            DataToInject = Encoding.UTF8.GetBytes("This is crazy!");

            //Replace the injected data with the new one
            PNGInjector.Change(ref PNG, DataToInject);

            //Print out the new injected data
            if (PNGInjector.IsInjected(ref PNG))
            {
                Console.WriteLine($"Readed changed data: {Encoding.UTF8.GetString(PNGInjector.Read(ref PNG))}");
            }

            //Remove the injection from PNG
            PNGInjector.Remove(ref PNG);

            //Write the cleared PNG to your harddrive
            File.WriteAllBytes($@"{Environment.CurrentDirectory}\test_cleared.png", PNG);

            //Check if removing succeeded
            if (!PNGInjector.IsInjected(ref PNG))
            {
                Console.WriteLine("PNG is now cleared");
            }

            Console.ReadKey();
        }
    }
}
