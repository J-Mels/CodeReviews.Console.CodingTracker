using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public class InputValidation
    {
        public static bool TryParseDateTime(string userInput,, out DateTime dateTime, string format = "yyyy-MM-dd HH:mm")
        {
            return DateTime.TryParseExact(userInput, format, null, System.Globalization.DateTimeStyles.None, out dateTime);
        }
    }
}
