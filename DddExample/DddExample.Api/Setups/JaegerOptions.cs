namespace DddExample.Api.Setups
{
    public class JaegerOptions
    {
        public const string SectionKey = "Jaeger";

        public bool Enabled { get; set; }
        
        public string Host { get; set; }

        public int Port { get; set; }
    }
}