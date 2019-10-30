using CSharpToTypeScript.Core.DI;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSharpToTypeScript.Core.Tests
{
    public class CodeConverterShould
    {
        private readonly ICodeConverter _codeConverter;

        public CodeConverterShould()
        {
            var serviceProvider = new ServiceCollection()
                .AddCSharpToTypeScript()
                .BuildServiceProvider();

            _codeConverter = serviceProvider.GetRequiredService<ICodeConverter>();
        }

        [Fact]
        public void ConvertClass()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    id: number;
    name: string;
}", converted);
        }

        [Fact]
        public void ConvertInterface()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"interface IItem
{
    int Id { get; set; }
    string Name { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    id: number;
    name: string;
}", converted);
        }

        [Fact]
        public void ConvertEnum()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"enum Color
{
    Red, Blue
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export enum Color {
    Red,
    Blue
}", converted);
        }

        [Fact]
        public void ConvertGenericType()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item<T>
{
    public int Id { get; set; }
    public T SomeT { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item<T> {
    id: number;
    someT: T;
}", converted);
        }

        [Fact]
        public void ConvertMultipleTypes()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"interface IItem
{
    int Id { get; set; }
    string Name { get; set; }
}

class MyItem : IITem
{
    public int Id { get; set; }
    public string Name { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    id: number;
    name: string;
}

export interface MyItem {
    id: number;
    name: string;
}", converted);
        }

        [Fact]
        public void SupportInheritance()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"interface IItem : IItemInterfaceBase
{
    string Name { get; set;}
}                
                
class DerivedItem : ItemBase, IItem
{
    public int Id { get; set; }
    private string Name { get; set; }
}

class ImplementingItem : IItem
{
    public int Id { get; set; }
    private string Name { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item extends ItemInterfaceBase {
    name: string;
}

export interface DerivedItem extends ItemBase {
    id: number;
}

export interface ImplementingItem {
    id: number;
}", converted);
        }

        [Fact]
        public void IgnoreNotSerializableMembers()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public int Id { get; set; }
    private string Name { get; set; }
    public static int Count { get; set; }
    public int Number { private get; set; }

    public int Foo()
    {
        return 0;
    }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    id: number;
}", converted);
        }

        [Fact]
        public void IgnoreStaticTypes()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"public static class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(string.Empty, converted);
        }

        [Fact]
        public void ConvertBasicTypes()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public int Integer { get; set; }
    public double Double { get; set; }
    public string String { get; set; }
    public char Character { get; set; }
    public DateTime Date { get; set; }
    public Guid Guid { get; set; }
    public bool Boolean { get; set; } 
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    integer: number;
    double: number;
    string: string;
    character: string;
    date: string;
    guid: string;
    boolean: boolean;
}", converted);
        }

        [Fact]
        public void ConvertTuples()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public (int, int, string) TupleA { get; set; }
    public (int id, string name) TupleB { get; set; }
    public Tuple<int, string> TupleC { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    tupleA: { item1: number; item2: number; item3: string; };
    tupleB: { id: number; name: string; };
    tupleC: { item1: number; item2: string; };
}", converted);
        }

        [Fact]
        public void ConvertDictionaries()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public Dictionary<string, int> Dict { get; set; }
    public Dictionary<bool, string> IllegalDict { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    dict: { [key: string]: number; };
    illegalDict: Dictionary<boolean, string>;
}", converted);
        }

        [Fact]
        public void ConvertCollections()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public int[] Array { get; set; }
    public string[,] Array2D { get; set; }
    public IEnumerable<string> Enumerable { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    array: number[];
    array2D: string[][];
    enumerable: string[];
}", converted);
        }

        [Fact]
        public void ConvertNullableTypes()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public int? Id { get; set; }
    public IEnumerable<int?> Collection { get; set; }
    public Nullable<float> Generic { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    id?: number;
    collection: (number | null)[];
    generic?: number;
}", converted);
        }

        [Fact]
        public void ConvertComplexTypes()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public IEnumerable<Dictionary<int, (string, int?)>?>? Wtf { get; set; }
}", new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    wtf?: ({ [key: number]: { item1: string; item2?: number; }; } | null)[];
}", converted);
        }

        [Fact]
        public void RespectIndentationSettings()
        {
            const string code = @"class Item
{
    public int MyProperty { get; set; }
};";

            var twoSpaceIndented = _codeConverter.ConvertToTypeScript(
                code, new CodeConversionOptions(export: true, useTabs: false, tabSize: 2));

            var tabIndented = _codeConverter.ConvertToTypeScript(
                code, new CodeConversionOptions(export: true, useTabs: true));

            Assert.Equal(@"export interface Item {
  myProperty: number;
}", twoSpaceIndented);

            Assert.Equal(@"export interface Item {
" + "\t" + @"myProperty: number;
}", tabIndented);
        }

        [Fact]
        public void RespectExportSettings()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public int Id => 4;
}", new CodeConversionOptions(export: false, useTabs: false, tabSize: 4));

            Assert.Equal(@"interface Item {
    id: number;
}", converted);
        }
    }
}
