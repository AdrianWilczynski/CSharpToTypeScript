namespace CSharpToTypeScript.Server.DTOs
{
    public class Input
    {
        public string Code { get; set; }
        public bool UseTabs { get; set; }
        public int? TabSize { get; set; }
        public bool Export { get; set; }
    }
}