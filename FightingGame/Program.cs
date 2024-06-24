// See https://aka.ms/new-console-template for more information
using FightingGame;

int round = 1;
int playerTurn = 0;
int playerActions = 0;

List<Player> playerList = [];

Console.WriteLine("Do you want to start the game? Yes/No");
bool gameIsRunning = Console.ReadLine()!.ToLower().Equals("yes");

while (gameIsRunning)
{
    Console.WriteLine("This is a turn based two player fighting game, where 2 fighters will compete for glory!");

    if (playerList.Count < 2)
    {
        string? playerName = "";
        int playerNumber = playerList.Count + 1;
        Console.WriteLine($"Create player number {playerNumber}, provide a player name");
        playerName = Console.ReadLine()!;

        // Doesn't work right now
        Random rnd = new Random();
        int roll = rnd.Next(1, 7);

        Player player = new Player(playerName, roll);
        playerList.Add(player);

        Console.WriteLine("Press Return to resume");
        Console.ReadKey();
        Console.Clear();
    }
    if (playerList.Count == 2)
    {
        while (playerList[playerTurn].IsAlive)
        {
            // Get the player who's turn it is now.
            Player player = playerList[playerTurn];

            Thread.Sleep(2000);

            Console.WriteLine($"Round {round}");
            Console.WriteLine($"{player.Name} 's turn!");
            Console.WriteLine("");
            Console.WriteLine("Stats");
            Console.WriteLine($"Strength: {player.Strength}");
            Console.WriteLine($"Defense: {player.Defense}");
            Console.WriteLine($"Level: {player.Level}");
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"Health Potions: {player.HealthPotions}");
            Console.WriteLine("");
            Console.WriteLine($"{player.Name}, what will you do?");
            Console.WriteLine("");
            Console.WriteLine("A. Attack");
            if (!player.Defense.Equals(player.MaxDefense))
                Console.WriteLine("D. Defend");
            Console.Write("H. Heal");
            Console.WriteLine("");

            string action = "";

            if (player.IsComputer)
            {
                Thread.Sleep(2000);
                Random rnd = new Random();
                int pcRoll = rnd.Next(1, 21);
                if(player.Health < 25 && player.HealthPotions > 0)
                    action = "heal";
                else
                {
                    if (pcRoll > 5)
                        action = "attack";
                    if (pcRoll < 6 && player.Defense != 15)
                        action = "defend";
                    else
                        action = "attack";
                }
            }
            else
            {
                // Get the keystroke from the player.
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.A:
                        action = "attack";
                        break;
                    case ConsoleKey.D:
                        action = "defend";
                        break;
                    case ConsoleKey.H:
                        action = "heal";
                        break;
                    default:
                        action = "defend";
                        break;
                }
            }

            Console.WriteLine($"{player.Name} chose {action}");

            switch (action)
            {
                case "attack":
                    int target = 0;
                    if (playerTurn == 0)
                        target = 1;
                    if (playerTurn == 1)
                        target = 0;

                    int damage = player.Attack(playerList[target]);
                    if (damage > 0)
                    {
                        player.TakeDamage(playerList[target], damage);
                    }
                    break;
                case "defend":
                    player.Defend();
                    break;
                case "heal":
                    player.DrinkHealthPotion();
                    break;
            }

            playerActions++;

            if (!player.IsAlive)
                break;

            // Change playerTurn to the next player
            if (playerTurn == 0)
                playerTurn = 1;
            else
                playerTurn = 0;

            Console.WriteLine("");
            Console.WriteLine("###############");
            Console.WriteLine("");

            if (playerActions == 2)
            {
                Console.WriteLine($"Round {round} over");
                round++;
                playerActions = 0;
                Console.WriteLine("Press Return to resume");
                Console.ReadKey();
                Console.Clear();
            }
        }

        string? winner = playerList.FirstOrDefault(p => p.IsAlive)?.Name;
        Console.WriteLine(winner + " won the game!");
        playerList.Clear();
        playerTurn = 0;
        playerActions = 0;
        round = 0;
        Console.WriteLine("Try again? yes/no");
        string? rematch = Console.ReadLine();
        if (rematch=="yes")
            gameIsRunning = true;
        else
            gameIsRunning = false;
        Console.Clear();
    }
}
Console.WriteLine("Game has ended");
Console.ReadKey();

public partial class Program
{

}