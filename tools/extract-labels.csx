using System.Text.RegularExpressions;

private var code = File.ReadAllText(Args[0]);

private var labels = string.Join(
    "," + Environment.NewLine,
    Regex.Matches(code, "label: *('[^']+')").Select(m => m.Groups[1].Value));

File.WriteAllText("labels.txt", labels);