using System.Collections.Generic;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Generic : IFieldType
    {
        public Generic(string name, IEnumerable<IFieldType> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public string Name { get; }
        public IEnumerable<IFieldType> Arguments { get; }

        public override string ToString() => $"{Name.RemoveInterfacePrefix()}<{string.Join(", ", Arguments)}>";
    }
}