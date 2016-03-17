namespace demo
{
    public static class Visitor
    {
        public static string TrimEnd(this string a, string Str)
        {
            for (int i = Str.Length - 1; i >= 0; i--)
                a = a.TrimEnd(Str[i]);
            return a;
        }
    }
}