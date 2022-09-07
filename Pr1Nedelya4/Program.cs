using System;

namespace Pr1Nedelya4
{
    class Program
    {
        static void Main(string[] args)
        {
            int hpHero = 100;
            int hpBoss = 200;
            int hpGroop = 100;
            int bonus = 0;
            bool groop = false;
            bool endFight = true;
            Random random = new Random();
            Console.WriteLine("Вы подходите к трактиру \"Жирный ёж\". По словам старосты деревни, именно здесь обосновалась шайка бандитов. У входа вы замечаете человека, который явно от вас что-то хочет");
            Console.WriteLine("-Эй, ты! Я знаю что ты задумал!\n" +
                "-И что же?\n" +
                "-Слушай, не надо притворятся. Староста поручил тебе выпроводить этих бандитов. Я хочу помочь.\n" +
                "Согласится / Отказать(Y/N)");
            char inp = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (inp == 'Y' || inp == 'y')
            {
                Console.WriteLine("Вы принимаете предложение незнакомца. Вдвоем у вас будет больше шансов.");
                groop = true;
            }
            else
                Console.WriteLine("Вы рещаете, что доверять свою жизнь незнакомому человеку не следует. Отказав, вы открываете дверь.");
            Console.WriteLine("Вы заходите внутрь, после чего к вам подходит амбал \n" +
                "-Вали от сюда, придурок. Тут занто.");
            Console.ReadKey();
            while (endFight)
            {
                Console.Clear();
                Console.WriteLine("Ваше здоровье: "+hpHero+"\nЗдоровье босса: "+hpBoss);
                if(groop)
                    Console.WriteLine("Здоровье напарника: " + hpGroop);
                Console.WriteLine("Воспользовавшись небольшой паузой, вы обдумываете свое следующее действие\n" +
                    "Ваши варианты:\n" +
                    "1 - Быстрый удар (Не сильно, но большой шанс уворота)\n"+
                    "2 - Сильный удар (Будет больно, скорее всего вам тоже)\n"+
                    "3 - Помощь незнакомца (Незнакомец нанесет свой удар, вы же подготовите свою следующую атаку)\n"+
                    "4 - Перевязать раны (Вы востанавливаете свое здоровье)\n"+
                    "5 - Побег (Вы покинете сражение, сохранив свое здоровье)\n");
                int dodge = 10;
                inp = Console.ReadKey().KeyChar; // Удар героя
                Console.WriteLine();
                switch (inp){
                    case '1':
                        Console.WriteLine("Вы наносите удар и  отпрыгиваете назад. Вероятность уклонится увеличивается.");
                        hpBoss -= 10+bonus;
                        bonus = 0;
                        dodge = 40;
                        break;
                    case '2':
                        Console.WriteLine("Вы размахиваетесь и наносите сокрушительный удар.");
                        hpBoss -= 30+bonus;
                        bonus = 0;
                        break;
                    case '3':
                        Console.WriteLine("Вы отпрыгиваете назад и подготавливаетесь к атаке.");
                        if (groop)
                        {
                            Console.WriteLine("Ваш напарник наносит удар по бандиту, отвлекая внимание на себя");
                            hpBoss -= 10;
                            bonus = 10;
                            dodge = 100;
                        }
                        else
                            Console.WriteLine("В пылу сражения, вы вероятно забыли, что сражаетесь один. Собственно из-за этого ваши действия не имели смысла.");
                        break;
                    case '4':
                        Console.WriteLine("Вы быстро перематываете свою руку и продолжаете сражение.");
                        if (hpHero<100)
                        {
                            hpHero += 20;
                            if (hpHero > 100)
                                hpHero = 100;
                        }
                        else
                            Console.WriteLine("Зачем вы это сделали непонятно. Ваше здоровье и так было полным.");
                        bonus = 0;
                        break;
                    case '5':
                        endFight = false; 
                        Console.WriteLine("Вы выбегаете в открытую дверь. Возможно вы могли бы победить, но уже поздно назад дороги нет.");
                        break;
                    default:
                        Console.WriteLine("Вы растерялись и просто стояли столбом.");
                        if (groop)
                        {
                            Console.WriteLine("Ваш напарник оказался более сообразительным и успел нанести удар.");
                            hpBoss -= 10;
                        }
                        bonus = 0;
                        break;
                }
                bool hitHero = true; // Удар Босса
                if (groop){
                    if(random.Next(101)>70||dodge==100)
                        hitHero = false;
                }
                if(hitHero == false){
                    Console.WriteLine("Амбал наносит удар вашему напарнику."); 
                    hpGroop -= 20;
                }
                else
                {
                    if (random.Next(101) > dodge)
                    {
                        Console.WriteLine("Громила бьет вас.");
                        hpHero -= 20;
                    }
                    else
                        Console.WriteLine("Громила бьет вас, но вы уворачиваетесь от атаки.");
                }
                if (hpGroop <= 0 && groop)
                {
                    Console.WriteLine("Ваш напарник пал. Убейте амбала, чтобы его жертва не была напрасна");
                    groop = false;
                }
                if (hpHero <= 0)
                {
                    Console.WriteLine("Вы погибли. Ваша история закончилась не успев начатся.");
                    endFight = false;
                }
                if (hpBoss <= 0)
                {
                    Console.WriteLine("Вы выиграли битву, но не войну. Ваша история только начинается...");
                    endFight = false;
                }
                Console.ReadKey();
            }

            if (hpBoss <= 0)
            {
                if (groop)
                    Console.WriteLine("После завершения схватки незнакомец бьет вас по голове. Очнувшись, вы понимаете, что ваша сумка пуста. Скорее всего вы его больше не увидите.");
                Console.WriteLine("Вернувшись в деревню вы забираете свою награду и отправляетесь в путь.");
            }
            else if (hpHero <= 0)
            {
                if (groop)
                    Console.WriteLine("Судьба вашего напарника останется для вас неизвестной. Скорее всего его больше нет в живых.");
                Console.WriteLine("Где похоронили ваше тело, и похоронили ли его вообще останется загадкой. Скорее всего ваш труп сгнил в ближайшей канаве.");
            }
            else
            {
                if (groop)
                    Console.WriteLine("Судьба вашего напарника останется для вас неизвестной. Скорее всего его больше нет в живых.");
                Console.WriteLine("Возвращатся в деревню - плохая идея. Громила из трактира будет искать вас, и деревня - это первое место куда он завится.");
            }
        }
    }
}
