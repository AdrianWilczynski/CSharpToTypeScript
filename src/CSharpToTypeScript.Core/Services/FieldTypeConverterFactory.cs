using CSharpToTypeScript.Core.Services.FieldTypeConversionHandlers;

namespace CSharpToTypeScript.Core.Services
{
    public static class FieldTypeConverterFactory
    {
        public static FieldTypeConversionHandler Create()
        {
            var converter = new QualifiedUnpacker();

            converter.SetNext(new ByteArrayHandler())
                .SetNext(new StringConverter())
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