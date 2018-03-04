using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MoneroMiner
{
    // ReSharper disable once UnusedMember.Global
    public class Miner: IMiner
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly Process _process = new Process();
        private string _poolAddress = "";
        private string _xmrAddress = "";
        private string _threads = "";

        // ReSharper disable once UnusedMember.Global
        /// <inheritdoc />
        public void RunMiner(string poolAddress, string xmrAddress, string threads)
        {
            _poolAddress = poolAddress;
            _xmrAddress = xmrAddress;
            _threads = threads;
            if (_xmrAddress.Length == 95 & _xmrAddress.Substring(0, 1) == "4")
            {
                _worker.WorkerSupportsCancellation = true;
                _worker.DoWork += Mine;
                _worker.RunWorkerAsync();
            }
            else
            {
                Console.WriteLine("Address looks wrong!");
            }
        }

        private void Mine(object sender, DoWorkEventArgs e)
        {
            short cntr = 0;
            var oStartInfo = new ProcessStartInfo("cpuminer-multi-wolf-AES\\minerd.exe",
                "-a cryptonight -o " + _poolAddress + " -u " + _xmrAddress + " -p x -t " + _threads)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                RedirectStandardError = true
            };
            _process.StartInfo = oStartInfo;
            Console.WriteLine("Starting process");
            _process.Start();
            using (var oStreamReader = _process.StandardError)
            {
                while (true)
                {
                    Console.Write("Getting new line...");
                    var sOutput = oStreamReader.ReadLine();
                    Console.WriteLine(sOutput);
                    try
                    {
                        if (sOutput != null)
                        {
                            var splitOutput = sOutput.Split(',', ' ', '/');
                            foreach (var part in splitOutput)
                            {
                                Console.WriteLine(cntr + " " + part);
                                cntr += 1;
                            }

                            if (sOutput.Contains("yay!"))
                            {
                                Console.WriteLine("yay!");
                                Console.WriteLine(splitOutput[3]);
                                Console.WriteLine(int.Parse(splitOutput[4]) - int.Parse(splitOutput[3]));
                                Console.WriteLine(splitOutput[7] + " h/s");
                            }
                        }
                    }
                    catch
                    {
                        Console.Write("null");
                    }

                    cntr = 0;
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        // ReSharper disable once UnusedMember.Local
        /// <inheritdoc />
        public void Exit()
        {
            Console.WriteLine("Pool=" + _poolAddress);
            Console.WriteLine("Address=" + _xmrAddress);
            Console.WriteLine("Threads=" + _threads);
            try
            {
                _process.Kill();
            }
            catch
            {
                Console.WriteLine("Already killed 1");
            }

            foreach (var p in Process.GetProcesses())
            {
                if (string.Compare(p.ProcessName, "minerd.exe", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    p.Kill();
                }
            }
            Environment.Exit(0);
        }
    }
}