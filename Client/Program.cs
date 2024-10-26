var isRunning = true;

while (isRunning)
{
    Console.WriteLine("Enter a command (generate, use, exit):");
    string input = Console.ReadLine() ?? "0";

    switch (input.ToLower())
    {
        case "generate":
            await Order.GenerateCode();
            break;

        case "use":
            await Order.UseCode();
            break;

        case "exit":
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid command. Please type 'generate', 'use', or 'exit'.");
            break;
    }
}

Console.WriteLine("Client program exited.");