using CSharpToTypeScript.Core.Services.TypeConversionHandlers;

namespace CSharpToTypeScript.Core.Services
{
    internal static class TypeConverterFactory
    {
        public static TypeConversionHandler Create()
        {
            var converter = new QualifiedUnpacker();

            converter.SetNext(new ByteArrayHandler())
                .SetNext(new StringConverter())
                .SetNext(new DateConverter())
                .SetNext(new NumberConverter())
                .SetNext(new BooleanConverter())
                .SetNext(new ArrayConverter(converter))
                .SetNext(new TupleConverter(converter))
                .SetNext(new DictionaryConverter(converter))
                .SetNext(new NullableConverter(converter))
                .SetNext(new GenericConverter(converter))
                .SetNext(new CustomConverter());

            return converter;
        }
    }
}