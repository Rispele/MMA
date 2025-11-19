using System.Text;

namespace Commons;

public static class RandomExtensions
{
    private static readonly char[] Alphabet = Enumerable
        .Range('a', 'z' - 'a' + 1)
        .Concat(Enumerable.Range('A', 'Z' - 'A' + 1))
        .Concat(Enumerable.Range('0', '9' - '0' + 1))
        .Select(t => (char)t)
        .ToArray(); 
    
    public static string NextString(this Random random, int length = 10)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            sb.Append(Alphabet[random.Next(Alphabet.Length)]);
        }
        
        return sb.ToString();   
    }
}