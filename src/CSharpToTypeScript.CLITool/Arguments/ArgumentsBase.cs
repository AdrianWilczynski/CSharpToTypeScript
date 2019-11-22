using CSharpToTypeScript.CLITool.Commands;

namespace CSharpToTypeScript.CLITool.Arguments
{
    public abstract class ArgumentsBase<T> where T : ArgumentsBase<T>
    {
        public void Override(CommandBase command)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetValue(this) is object value)
                {
                    typeof(CommandBase).GetProperty(property.Name).SetValue(command, value);
                }
            }
        }
    }
}