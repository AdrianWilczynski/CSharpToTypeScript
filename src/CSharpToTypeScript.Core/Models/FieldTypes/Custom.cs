using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Custom : FieldType
    {
        public Custom(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string WriteTypeScript() => Name.RemoveInterfacePrefix();
    }
}