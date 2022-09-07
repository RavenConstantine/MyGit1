using System;

namespace LabClass
{
    public class Creator
    {
        char[,] lab = new char[1000, 1000];
        char pred = ' ';
        int mainX;
        int mainY;
        int[] posP = new int[2];
        int radTym = 5;
        int coin = 0;
        public bool editMode = false;
        public bool tAct = true;

        //☺░▒▓█■☻†║
        public void Create(int x, int y)
        {
            mainX = x;
            mainY = y;
            for (int i = 0; i < x * 2 + 1; i++)
            {
                for (int j = 0; j < y * 2 + 1; j++)
                {
                    if ((i % 2 == 0) || (j % 2 == 0))
                        lab[i, j] = '█';
                    else
                        lab[i, j] = ' ';
                }
            }
            var rand = new Random();
            for (int i = 1; i < y * 2; i++)
            {
                lab[1, i] = ' ';
            }
            for (int i = 3; i < x * 2; i += 2)
            {
                int value = 0;
                int last = 1;
                while (last < y + 1)
                {
                    do
                    {
                        value = rand.Next(0, 6);
                    }
                    while (value + last > y);
                    for (int j = last * 2 - 1; j < (value + last) * 2; j++)
                    {
                        lab[i, j] = ' ';
                    }
                    int proh = rand.Next(0, value + 1);
                    lab[i - 1, (proh + last) * 2 - 1] = ' ';
                    last += value + 1;
                }
            }
            CreateFin(x, y);
        }
        public void Create2(int x, int y)
        {
            mainX = x;
            mainY = y;
            for (int i = 0; i < x * 2 + 1; i++)
            {
                for (int j = 0; j < y * 2 + 1; j++)
                {
                    if ((i % 2 == 0) || (j % 2 == 0))
                        lab[i, j] = '█';
                    else
                        lab[i, j] = ' ';
                }
            }
            var rand = new Random();
            bool[,] open = new bool[x * 2 + 1, y * 2 + 1];
            bool fullO = false;
            int value1 = rand.Next(0, x + 1);
            value1 = value1 * 2 - 1;
            int value2 = rand.Next(0, y + 1);
            value2 = value2 * 2 - 1;
            while (fullO == false)
            {
                int value3 = rand.Next(0, 5);
                switch (value3)
                {
                    case 1:
                        if (value1 + 2 < x * 2)
                        {
                            if (open[value1 + 2, value2] == false)
                            {
                                lab[value1 + 1, value2] = ' ';
                                open[value1 + 2, value2] = true;
                            }
                            value1 += 2;
                        }
                        break;
                    case 2:
                        if (value2 + 2 < y * 2)
                        {
                            if (open[value1, value2 + 2] == false)
                            {
                                lab[value1, value2 + 1] = ' ';
                                open[value1, value2 + 2] = true;
                            }
                            value2 += 2;
                        }
                        break;
                    case 3:
                        if (value1 - 2 > 0) 
                        { 
                            if (open[value1 - 2, value2] == false)
                            {
                                lab[value1 - 1, value2] = ' ';
                                open[value1 - 2, value2] = true;
                            }
                            value1 -= 2; 
                        }
                        break;
                    case 4:
                        if (value2 - 2 > 0)
                        {
                            if (open[value1, value2 - 2] == false)
                            {
                                lab[value1, value2 - 1] = ' ';
                                open[value1, value2 - 2] = true;
                            }
                            value2 -= 2;
                        }
                        break;
                }
                int counter = 0;
                for (int i = 1; i < x * 2 + 1; i += 2)
                {
                    for (int j = 1; j < y * 2 + 1; j += 2)
                    {
                        if (open[i, j])
                            counter++;
                        if (counter == x * y)
                            fullO = true;
                    }
                }
            }
            CreateFin(x, y);
        }
        void CreateFin(int x, int y)
        {
            var rand = new Random();
            int value3 = rand.Next(0, 4);
            for (int t = 0; t < value3; t++)
            {
                int value1 = rand.Next(0, x * 2);
                int value2 = rand.Next(0, y * 2);
                for (int i = value1; i < value1 + 10; i++)
                    for (int j = value2; j < value2 + 10; j++)
                    {
                        if (lab[i, j] == ' ')
                            lab[i, j] = '░';
                    }
            }
            value3 = rand.Next(0, 4);
            for (int t = 0; t < value3; t++)
            {
                int value1 = rand.Next(0, x * 2);
                int value2 = rand.Next(0, y * 2);
                for (int i = value1; i < value1 + 10; i++)
                    for (int j = value2; j < value2 + 10; j++)
                    {
                        if (lab[i, j] == ' ')
                            lab[i, j] = '▒';
                    }
            }
            int c = 0;
            while (c < 10)
            {
                int value1 = rand.Next(0, x * 2);
                int value2 = rand.Next(0, y * 2);
                if ((lab[value1, value2] != '█') && (lab[value1, value2] != '║'))
                {
                    lab[value1, value2] = 'c';
                    c++;
                }
            }
            Start();
        }
        public void Vivod()
        {
            Console.Clear();
            for (int i = 0; i < mainX * 2 + 1; i++)
            {
                for (int j = 0; j < mainY * 2 + 1; j++)
                {
                    Console.Write(lab[i, j]);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, mainX * 2 + 2);
            Console.WriteLine("Счет: " + coin);
            Legenda();
        }
        void Start()
        {
            posP[0] = 1;
            posP[1] = 1;
            lab[posP[0], posP[1]] = '☺';
            lab[mainX * 2 - 1, mainY * 2 - 1] = '║';
        }
        public void Go(ConsoleKeyInfo key)
        {
            string strKey = key.Key.ToString();
            if (editMode == false)
            {
                lab[posP[0], posP[1]] = pred;
                Console.SetCursorPosition(posP[1], posP[0]);
                Console.Write(lab[posP[0], posP[1]]);
                Console.Write(lab[posP[0], posP[1] + 1]);
                switch (strKey)
                {
                    case "LeftArrow":
                        if (lab[posP[0], posP[1] - 1] != '█')
                            posP[1]--;
                        break;
                    case "RightArrow":
                        if (lab[posP[0], posP[1] + 1] != '█')
                            posP[1]++;
                        break;
                    case "DownArrow":
                        if (lab[posP[0] + 1, posP[1]] != '█')
                            posP[0]++;
                        break;
                    case "UpArrow":
                        if (lab[posP[0] - 1, posP[1]] != '█')
                            posP[0]--;
                        break;
                    case "E":
                        editMode = true;
                        Vivod();
                        break;
                }
                switch (lab[posP[0], posP[1]])
                {
                    case '║':
                        Console.SetCursorPosition(0, mainX * 2 + 1);
                        Console.WriteLine("Красавчик!");
                        break;
                    case '░':
                        radTym = 3;
                        if (tAct && pred != '░')
                        {
                            Console.Clear();
                            Tyman();
                        }
                        pred = '░';
                        break;
                    case '▒':
                        radTym = 1;
                        if (tAct && pred != '▒')
                        {
                            Console.Clear();
                            Tyman();
                        }
                        pred = '▒';
                        break;
                    case 'c':
                        coin++;
                        Console.SetCursorPosition(0, mainX * 2 + 2);
                        Console.WriteLine("Счет: " + coin);
                        break;
                    case ' ':
                        radTym = 5;
                        if (tAct && pred != ' ')
                        {
                            Console.Clear();
                            Tyman();
                        }
                        pred = ' ';
                        break;
                }
                lab[posP[0], posP[1]] = '☺';
                Console.SetCursorPosition(posP[1], posP[0]);
                Console.Write(lab[posP[0], posP[1]]);
                if (tAct&& editMode==false)
                    Tyman();
            }
            else
                switch (strKey)
                {
                    case "LeftArrow":
                        if (Console.CursorLeft - 1 > 0)
                            Console.CursorLeft--;
                        break;
                    case "RightArrow":
                        Console.CursorLeft++;
                        break;
                    case "DownArrow":
                        Console.CursorTop++;
                        break;
                    case "UpArrow":
                        if (Console.CursorTop - 1 > 0)
                            Console.CursorTop--;
                        break;
                    case "D1":
                        lab[Console.CursorTop, Console.CursorLeft] = '█';
                        Console.Write(lab[Console.CursorTop, Console.CursorLeft]);
                        break;
                    case "D2":
                        lab[Console.CursorTop, Console.CursorLeft] = ' ';
                        Console.Write(lab[Console.CursorTop, Console.CursorLeft]);
                        break;
                    case "D3":
                        lab[Console.CursorTop, Console.CursorLeft] = '░';
                        Console.Write(lab[Console.CursorTop, Console.CursorLeft]);
                        break;
                    case "D4":
                        lab[Console.CursorTop, Console.CursorLeft] = '▒';
                        Console.Write(lab[Console.CursorTop, Console.CursorLeft]);
                        break;
                    case "D5":
                        lab[Console.CursorTop, Console.CursorLeft] = 'c';
                        Console.Write(lab[Console.CursorTop, Console.CursorLeft]);
                        break;
                    case "D6":
                        lab[Console.CursorTop, Console.CursorLeft] = '║';
                        Console.Write(lab[Console.CursorTop, Console.CursorLeft]);
                        break;
                    case "E":
                        editMode = false;
                        Console.Clear();
                        if (tAct)
                        {
                            Tyman();
                        }
                        else
                        {
                            Vivod();
                        }
                        break;
                }
        }
        public void Tyman()
        {
            for (int i = posP[1] - radTym - 1; i <= posP[1] + radTym + 1; i++)
            {
                for (int j = posP[0] - radTym - 5; j <= posP[0] + radTym + 5; j++)
                {
                    if ((i >= 0 && j >= 0) && ((i <= mainY * 2 + 1) && (j <= mainX * 2 + 1)))
                    {
                        Console.SetCursorPosition(i, j);
                        if ((i <= posP[1] - radTym - 1) || (i >= posP[1] + radTym + 1) ||
                            (j <= posP[0] - radTym - 1) || (j >= posP[0] + radTym + 1))
                            Console.Write(' ');
                        else
                            Console.Write(lab[j, i]);
                    }
                }
            }
            Console.SetCursorPosition(0, mainX * 2 + 2);
            Console.WriteLine("Счет: " + coin);
            Legenda();
        }
        public void Legenda()
        {
            Console.SetCursorPosition(mainY * 2 + 2, 0);
            Console.WriteLine("Легенда:");
            Console.SetCursorPosition(mainY * 2 + 2, 1);
            Console.WriteLine("☺ - Игрок");
            Console.SetCursorPosition(mainY * 2 + 2, 2);
            Console.WriteLine("█-Стена-1");
            Console.SetCursorPosition(mainY * 2 + 2, 3);
            Console.WriteLine("' '-Пустота-2");
            Console.SetCursorPosition(mainY * 2 + 2, 4);
            Console.WriteLine("░-Трава-3");
            Console.SetCursorPosition(mainY * 2 + 2, 5);
            Console.WriteLine("▒-Густая трава-4");
            Console.SetCursorPosition(mainY * 2 + 2, 6);
            Console.WriteLine("c-Монета-5");
            Console.SetCursorPosition(mainY * 2 + 2, 7);
            Console.WriteLine("║-Финиш-6");
            Console.SetCursorPosition(mainY * 2 + 2, 8);
            Console.WriteLine("Редактирование - e");
            //Console.SetCursorPosition(mainY * 2 + 2, 9);
            //Console.WriteLine("Выбор-клавша напротив");
        }
    }
}
