public class Order
{
    public static async Task GenerateCode()
    {
        Console.Write("Enter the number of codes to generate: ");
        ushort count;
        while (!ushort.TryParse(Console.ReadLine(), out count) || count > 2000 || count < 1)
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the count. The number should be between 0 and 2000.");
        }


        Console.Write("Enter the length of the discount codes (7 or 8): ");
        byte length;
        while (!byte.TryParse(Console.ReadLine(), out length) || length < 7 || length > 8)
        {
            Console.WriteLine("Invalid input. Please enter a valid length (7 or 8).");
        }
  
        await TCPClient.GenerateDiscountCodes(count, length);
    }

    public static async Task UseCode()
    {
        Console.Write("Enter the discount code to use: ");
        string code = Console.ReadLine() ?? "0";

        await TCPClient.UseDiscountCode(code);
    }
}