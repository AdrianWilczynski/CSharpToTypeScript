using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyProject.DTOs
{
    public class Item : BaseItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsWhatever { get; set; }
        public IEnumerable<string> Collection { get; set; }
        public double[] Array { get; set; }
        public (int, string) Tuple { get; set; }
        public int? Nullable { get; set; }
        public GenericItem<string> Generic { get; set; }
        public Dictionary<string, string> Dictionary { get; set; }
        public DateTime Date { get; set; }
        public string Hello => "Hello World!";
        public byte[] File { get; set; }

        [JsonIgnore]
        public string IgnoreMe { get; set; }

        [JsonProperty("new_name")]
        public string RenameMe { get; set; }
    }

    public class GenericItem<IT>
    {
        public IT Stuff { get; set; }
    }

    public class BaseItem
    {
        public ImportMe Imported { get; set; }
    }

    public static class Helper
    {
        public static void Help() { }
    }
}