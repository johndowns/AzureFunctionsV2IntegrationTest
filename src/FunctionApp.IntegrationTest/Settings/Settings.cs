namespace FunctionApp.IntegrationTest.Settings
{
    public class Settings
    {
        public bool UseShellExecute { get; set; } // TODO is still needed?
        public string DotnetExecutablePath { get; set; }
        public string FunctionHostPath { get; set; }
        public string FunctionApplicationPath { get; set; }
    }
}