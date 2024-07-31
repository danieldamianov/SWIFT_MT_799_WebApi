using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWIFT_MT799_Logic
{
    public interface ISwiftMT799Parser
    {
        SwiftMT799Message ParseSwiftMT799Message(string message);
    }
}
