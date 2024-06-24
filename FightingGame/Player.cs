using System.Numerics;

namespace FightingGame
{
    public class Player
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int MaxStrength { get; }
        public int Defense { get; set; }
        public int MaxDefense { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; }
        public int HealthPotions { get; set; }
        public bool IsAlive { get; set; }
        public bool IsComputer { get; set; }
        public int StartingRoll { get; set; }

        public Player(string name, int startingRoll)
        {
            if (name == "")
                Name = "Unknown Player";
            else if (name.ToLower().Equals("pc", StringComparison.CurrentCultureIgnoreCase))
                Name = "PC Player";
            else
                Name = name;
            Strength = 10;
            MaxStrength = 35;
            Defense = 5;
            MaxDefense = 15;
            Level = 1;
            Health = 100;
            MaxHealth = 100;
            HealthPotions = 0;
            IsAlive = true;
            if (Name == "PC Player")
                IsComputer = true;
            else
                IsComputer = false;

            if (IsComputer)
                Console.WriteLine($"You are playing against {Name}, good luck!");
            else
                Console.WriteLine($"You have created a player named {Name}, good luck!");

            Console.WriteLine("##########");
            Console.WriteLine($"Player: {Name}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Health Potions: {HealthPotions}");
            Console.WriteLine("##########");
            Console.WriteLine("");
            Console.WriteLine("");

            StartingRoll = startingRoll;

            Console.WriteLine($"{Name} rolled {StartingRoll} for starting order");
        }

        public int Attack(Player target)
        {
            double maxDamage = Strength * 1.2;
            Random rand = new();
            int luck = rand.Next(1, 21);
            double luckModerator = 1 + (luck / 100);
            double damageDouble = Strength * luckModerator;
            int damage = (int)(damageDouble);

            if (luck == 1)
            {
                Console.WriteLine($"{Name} missed!");
                return 0;
            }
            else
            {
                Console.WriteLine($"{Name} dealt {damage} damage to {target.Name}!");
                if (luck >= 17)
                {
                    LevelUp();
                }
                return damage;
            }
        }

        public void TakeDamage(Player player, int damage)
        {
            int totalDamage = damage - (player.Defense / 2);
            if (totalDamage < 0)
                totalDamage = 0;
            player.Health -= totalDamage;
            Console.WriteLine($"{player.Name} took {totalDamage} damage");
            if (player.Health <= 0) {
                player.Kill();
                Console.WriteLine(" and died!");
            }
            else
            {
                Console.WriteLine($" and has {player.Health} health left");
            }
        }

        public void Defend()
        {
            Random rand = new Random();
            int rollLuck = (rand.Next(1, 4));
            Defense = Defense + rollLuck;
            if (Defense >= MaxDefense)
            {
                Defense = MaxDefense;
                Console.WriteLine($"{Name} is at max Defense");
            }
            else
            {
                Console.WriteLine($"{Name} assumed a defensive stance and your Defense has increased by {rollLuck} to {Defense}");
            }
        }

        public void DrinkHealthPotion()
        {
            if (HealthPotions > 0) {
                Health = Health + 25;
                HealthPotions--;
                Console.WriteLine($"{Name} drank a health potion and you health increased to {Health}");
            }
            else
            {
                Console.WriteLine("You don't have any health potions");
                if (RollForLuck() > 15)
                {
                    Console.WriteLine($"By chance you find a bit of water, you drink it and feel vaguely rejuvenated");
                    Console.WriteLine("your health increses by 5");
                    Health = Health + 1;
                }
                else
                {
                    Console.WriteLine("Rummaging around for something you don't have, left you open for attack!");
                    Console.WriteLine("Your defense has decreased by 5");
                    Defense = Defense - 1;

                }
            }
        }

        public void Kill()
        {
            IsAlive = false;
        }

        public void LevelUp()
        {
            Console.WriteLine($"{Name} levelled up!");
            if (Strength <= MaxStrength - 4)
            {
                Strength += 4;
                Console.WriteLine($"{Name}'s Strength has increased to: {Strength}");
            }
            else
            {
                Strength = 25;
                Console.WriteLine($"{Name} is at max Strength: {Strength}");
            }

            if (Defense <= MaxDefense - 1)
            {
                Defense += 1;
                Console.WriteLine($"{Name}'s Defense has increased to: {Defense}");
            }
            else
            {
                Defense = 15;
                Console.WriteLine($"{Name} is at max Defense: {Defense}");
            }

            if (Health <= MaxHealth - 15)
            {
                Health += 15;
                Console.WriteLine($"{Name}'s Health has increased to: {Health}");
            }
            else
            {
                Health = 100;
                Console.WriteLine($"{Name} is at max Health: {Health}");
            }

            Level++;

            if (RollForLuck() > 15)
            {
                HealthPotions++;
                Console.WriteLine($"{Name} also got a health potion!");
            }
        }

        public int RollForLuck()
        {
            Random random = new Random();
            return random.Next(1,21);
        }
    }
}
