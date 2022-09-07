using System;

namespace Pr1Nedelya5
{
    class Program
    {
        static void Main(string[] args)
        {
            Creator Crea = new Creator();
            Crea.Create2(13, 50);
            //Crea.tAct = false;
            if (Crea.tAct)
            {
                Crea.Tyman();
            }
            else
                Crea.Vivod();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Crea.Go(key);
                if (Crea.editMode == true)
                    Console.CursorVisible = true;
                else
                {
                    Console.CursorVisible = false;
                }
            }
        }
    }
}
