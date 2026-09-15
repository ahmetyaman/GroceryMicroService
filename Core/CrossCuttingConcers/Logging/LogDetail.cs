namespace Core.CrossCuttingConcers.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public string User { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }

}