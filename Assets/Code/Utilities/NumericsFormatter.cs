namespace Dimasyechka.Code.Utilities
{
    public static class NumericsFormatter
    {
        private static string[] numerics = { "", "K", "M", "B", "T", "Q" };

        public static string FormatNumerics(double input, string format = "")
        {
            double division = (double)input;
            int numericsIndex = 0;

            while (division >= 1000)
            {
                if (numericsIndex >= numerics.Length) break;

                division /= 1000;
                numericsIndex++;
            }

            if (numericsIndex >= numerics.Length) numericsIndex = numerics.Length - 1;

            string output = $"{division.ToString(format)}{numerics[numericsIndex]}";

            return output;
        }

        //public static string FormatNumerics(ulong input)
        //{
        //    ulong division = input;
        //    int numericsIndex = 0;

        //    while (division >= 1000)
        //    {
        //        division /= 1000;
        //        numericsIndex++;
        //    }

        //    if (numericsIndex >= numerics.Length) numericsIndex = numerics.Length - 1;

        //    string output = $"{division.ToString("f2")}{numerics[numericsIndex]}";

        //    return output;
        //}

        //public static string FormatNumerics(long input)
        //{
        //    long division = input;
        //    int numericsIndex = 0;

        //    while (division >= 1000)
        //    {
        //        division /= 1000;
        //        numericsIndex++;
        //    }

        //    if (numericsIndex >= numerics.Length) numericsIndex = numerics.Length - 1;

        //    string output = $"{division.ToString("f2")}{numerics[numericsIndex]}";

        //    return output;
        //}
    }
}
