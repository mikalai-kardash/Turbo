using System.Text;

namespace Turbo.Events.Message
{
    public static class StringExtensions
    {
        public static string Show(this string target, int col)
        {
            var result = new StringBuilder();

            if (col < 0)
            {
                col = 0;
            }
            if (col > target.Length)
            {
                col = target.Length;
            }
            if (col > 0)
            {
                result.Append(target.Substring(0, col));
            }

            result.Append("^");

            if (target.Length > col)
            {
                result.Append(target.Substring(col, target.Length - col));
            }

            return result.ToString();
        }
    }
}