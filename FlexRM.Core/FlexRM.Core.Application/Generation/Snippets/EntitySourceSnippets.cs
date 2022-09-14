using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Application.Generation.Snippets
{
    public class EntitySourceSnippets
    {
        public static string OpenBracket = "{";
        public static string CloseBracket = "}";
        public static string GetNamespace(string name)
            => $"namespace {name}";
        public static string GetUsing(string name)
            => $"using {name};";
        public static string GetProperty(string type, string name)
            => $"public {type} {name} {{ get; set; }}";
        public static string GetClass(string name)
            => $"public class {name}";
    }
}
