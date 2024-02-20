namespace Jyunrcaea
{
    internal class Texter
    {
        Dictionary<string, string> data;
        public bool IsLoad { get; internal set; } = false;

        public Texter(string path)
        {
            if(!File.Exists(path))
            {
                return;
            }
            FileInfo info = new FileInfo(path);
            if (info.Length > 614)
            {
                return;
            }
            data = new();
            string[] lines = File.ReadAllLines(path);
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                var line = lines[i].Trim();
                int space = line.IndexOf(' ');
                if (space < 0) continue;
                string name = line.Substring(0,space);
                string value = line.Substring(space + 1);
                data.Add(name, value);
            }
            IsLoad = true;
        }

        public string? Get(string name)
        {
           if (!this.data.TryGetValue(name, out var value)) return null;
            return value;
        }
    }
}
