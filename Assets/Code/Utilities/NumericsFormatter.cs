namespace Dimasyechka.Code.Utilities
{
    public static class NumericsFormatter
    {
        private static string[] _numerics =
        {
            "",    // None
            "K",   // Thousand
            "M",   // Million
            "B",   // Billion
            "T",   // Trillion
            "q",   // Quadrillion
            "Q",   // Quintillion
            "s",   // Sextillion
            "S",   // Septillion
            "O",   // Octillion
            "N",   // Nonillion
            "D",   // Decillion
            "U",   // Undecillion 
            "Dd",  // Duodecillion
            "Td",  // Tredicillion
            "Qq",  // Quattuordecillion
            "Qn",  // Quindecillion
            "Sd"   // Sexdecillion
        };

        public static string FormatNumerics(double input, string format = "")
        {
            double division = (double)input;
            int numericsIndex = 0;

            while (division >= 1000)
            {
                if (numericsIndex >= _numerics.Length) break;

                division /= 1000;
                numericsIndex++;
            }

            if (numericsIndex >= _numerics.Length) numericsIndex = _numerics.Length - 1;

            string output = $"{division.ToString(format)}{_numerics[numericsIndex]}";

            return output;
        }
    }
}
