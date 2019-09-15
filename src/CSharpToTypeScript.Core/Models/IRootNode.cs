namespace CSharpToTypeScript.Core.Models
{
    public interface IRootNode
    {
        string WriteTypeScript(bool useTabs, int? tabSize, bool export);
    }
}