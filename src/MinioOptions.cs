namespace MinioStorage
{
    public class MinioOptions
    {
        public string Endpoint { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public bool WithSSL { get; set; } = true;
        public string SessionToken { get; set; } = string.Empty;
    }
}