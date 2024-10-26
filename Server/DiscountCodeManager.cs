partial class DiscountCodeManager
{
    private const string FilePath = "discountCodes.txt";
    private static readonly object LockObject = new();

    public DiscountCodeManager()
    {
        LoadCodesFromFile();
    }

    private List<DiscountCode> discountCodes = [];

    public bool GenerateDiscountCodes(int count, byte length)
    {
        lock (LockObject)
        {
            for (int i = 0; i < count; i++)
            {
                var code = GenerateRandomCode(length);
                discountCodes.Add(new DiscountCode { Code = code, IsUsed = false });
            }
            SaveCodesToFile();
            return true;
        }
    }

    public bool UseDiscountCode(string code)
    {
        lock (LockObject)
        {
            var discountCode = discountCodes.FirstOrDefault(c => c.Code == code && !c.IsUsed);
            if (discountCode != null)
            {
                discountCode.IsUsed = true;
                SaveCodesToFile();
                return true;
            }
            return false;
        }
    }

    private static string GenerateRandomCode(byte length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private void SaveCodesToFile()
    {
        lock (LockObject)
        {
            var lines = discountCodes.Select(c => $"{c.Code},{c.IsUsed}");
            File.WriteAllLines(FilePath, lines);
        }
    }

    private void LoadCodesFromFile()
    {
        lock (LockObject)
        {
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    discountCodes.Add(new DiscountCode { Code = parts[0], IsUsed = bool.Parse(parts[1]) });
                }
            }
        }
    }
}
