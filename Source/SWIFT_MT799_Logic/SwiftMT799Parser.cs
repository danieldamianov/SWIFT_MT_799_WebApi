using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWIFT_MT799_Logic
{
    public class SwiftMT799Parser : ISwiftMT799Parser
    {
        public SwiftMT799Message ParseSwiftMT799Message(string message)
        {
            Regex regex = new Regex(SwiftMessageConstants.SWIFT_MT799_REGEX);
            Match match = regex.Match(message);

            if (!match.Success)
            {
                throw new ArgumentException("The message does not match the expected format.");
            }

            SwiftMT799Message swiftMessage = ExtractMessageObject(match);

            return swiftMessage;
        }

        private static SwiftMT799Message ExtractMessageObject(Match match)
        {
            SwiftMT799Message swiftMessage = new SwiftMT799Message();

            foreach (PropertyInfo property in typeof(SwiftMT799Message).GetProperties())
            {
                if (match.Groups[property.Name].Success)
                {
                    property.SetValue(swiftMessage, match.Groups[property.Name].Value);
                }
            }

            return swiftMessage;
        }
    }
}
