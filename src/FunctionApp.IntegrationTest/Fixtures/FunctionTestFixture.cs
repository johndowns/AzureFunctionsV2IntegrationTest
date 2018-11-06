using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace FunctionApp.IntegrationTest.Fixtures
{
    public class FunctionTestFixture : IDisposable
    {
        private readonly Process _funcHostProcess;

        public int Port { get; } = 7001;

        public readonly HttpClient Client = new HttpClient();

        public FunctionTestFixture()
        {
            var npmPackagesFolder = @"%appdata%\npm";
            var useShellExecute = false;

            var functionCliPath = Path.Combine(Environment.ExpandEnvironmentVariables(npmPackagesFolder), "func.cmd");
            var functionAppFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "FunctionApp", "bin", "Debug", "netcoreapp2.1");

            _funcHostProcess = new Process
            {
                StartInfo =
                {
                    FileName = functionCliPath,
                    Arguments = $"start -p {Port}",
                    WorkingDirectory = functionAppFolder,
                    CreateNoWindow = true,
                    UseShellExecute = useShellExecute
                }
            };
            var success = _funcHostProcess.Start();
            if (!success)
            {
                throw new InvalidOperationException("Could not start function CLI.");
            }

            Client.BaseAddress = new Uri($"http://localhost:{Port}");
        }

        public virtual void Dispose()
        {
            if (!_funcHostProcess.HasExited)
            {
                _funcHostProcess.Kill();
            }

            _funcHostProcess.Dispose();
        }
    }
}
