using System.Linq;
using CSharpToTypeScript.Core.Services;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace CSharpToTypeScript.UnitTests
{
    public class SyntaxTreeConverterShould
    {
        private CompilationUnitSyntax GetRoot(string code)
            => CSharpSyntaxTree.ParseText(code)
                .GetCompilationUnitRoot();

        [Fact]
        public void ConvertClass()
        {
            const string code = @"class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}";

            var types = new SyntaxTreeConverter().Convert(GetRoot(code));

            Assert.Contains(types, t => t.Name == "Item");

            var fields = types.Single().Fields;

            Assert.Contains(fields, p => p.Name == "Id");
            Assert.Contains(fields, p => p.Name == "Name");
        }

        [Fact]
        public void ConvertInterface()
        {
            const string code = @"interface IItem
{
    int Id { get; set; }
    string Name { get; set; }
}";

            var types = new SyntaxTreeConverter().Convert(GetRoot(code));

            Assert.Contains(types, t => t.Name == "IItem");

            var fields = types.Single().Fields;

            Assert.Contains(fields, p => p.Name == "Id");
            Assert.Contains(fields, p => p.Name == "Name");
        }

        [Fact]
        public void ConvertMultipleMixedTypes()
        {
            const string code = @"interface IItem
{
    int Id { get; set; }
    string Name { get; set; }
}

class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}";

            var types = new SyntaxTreeConverter().Convert(GetRoot(code));

            Assert.Contains(types, t => t.Name == "IItem");
            Assert.Contains(types, t => t.Name == "Item");
        }

        [Fact]
        public void ConvertTypeInsideOfNamespace()
        {
            const string code = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpToTypeScript.UnitTests
{
    class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}";

            var types = new SyntaxTreeConverter().Convert(GetRoot(code));

            Assert.Contains(types, t => t.Name == "Item");
        }

        [Fact]
        public void ConvertTypesInMultipleNamespaces()
        {
            const string code = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpToTypeScript.UnitTests
{
    class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

namespace CSharpToTypeScript.Models
{
    class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}";

            var types = new SyntaxTreeConverter().Convert(GetRoot(code));

            Assert.Contains(types, t => t.Name == "Item");
            Assert.Contains(types, t => t.Name == "Unit");
        }
    }
}