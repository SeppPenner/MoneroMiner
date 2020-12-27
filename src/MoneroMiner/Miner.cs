// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Miner.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The monero miner class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneroMiner
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <inheritdoc cref="IMiner"/>
    /// <summary>
    /// The monero miner class.
    /// </summary>
    /// <seealso cref="IMiner"/>
    // ReSharper disable once UnusedMember.Global
    public class Miner : IMiner
    {
        /// <summary>
        /// The background worker.
        /// </summary>
        private readonly BackgroundWorker worker = new BackgroundWorker();

        /// <summary>
        /// The process.
        /// </summary>
        private readonly Process process = new Process();

        /// <summary>
        /// The pool address.
        /// </summary>
        private string localPoolAddress = string.Empty;

        /// <summary>
        /// The XMR address.
        /// </summary>
        private string localXmrAddress = string.Empty;

        /// <summary>
        /// The threads.
        /// </summary>
        private string localThreads = string.Empty;

        /// <inheritdoc cref="IMiner"/>
        /// <summary>
        /// The method to run the miner.
        /// </summary>
        /// <param name="poolAddress">The pool address, something like stratum+tcp://xmr.pool.minergate.com:45560.</param>
        /// <param name="xmrAddress">The wallet address.</param>
        /// <param name="threads">Number of threads.</param>
        /// <seealso cref="IMiner"/>
        // ReSharper disable once UnusedMember.Global
        public void RunMiner(string poolAddress, string xmrAddress, string threads)
        {
            this.localPoolAddress = poolAddress;
            this.localXmrAddress = xmrAddress;
            this.localThreads = threads;

            if (this.localXmrAddress.Length == 95 & this.localXmrAddress.Substring(0, 1) == "4")
            {
                this.worker.WorkerSupportsCancellation = true;
                this.worker.DoWork += this.Mine;
                this.worker.RunWorkerAsync();
            }
            else
            {
                Console.WriteLine("Address looks wrong!");
            }
        }

        /// <inheritdoc cref="IMiner"/>
        /// <summary>
        /// Exits/ kills all miner processes.
        /// </summary>
        /// <seealso cref="IMiner"/>
        // ReSharper disable once UnusedMember.Global
        public void Exit()
        {
            Console.WriteLine("Pool=" + this.localPoolAddress);
            Console.WriteLine("Address=" + this.localXmrAddress);
            Console.WriteLine("Threads=" + this.localThreads);

            try
            {
                this.process.Kill();
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

        /// <summary>
        /// Runs the mining process.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Mine(object sender, DoWorkEventArgs e)
        {
            short counter = 0;

            var startInformation = new ProcessStartInfo("cpuminer-multi-wolf-AES\\minerd.exe", "-a cryptonight -o " + this.localPoolAddress + " -u " + this.localXmrAddress + " -p x -t " + this.localThreads)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                RedirectStandardError = true
            };

            this.process.StartInfo = startInformation;
            Console.WriteLine("Starting process");
            this.process.Start();

            using var streamReader = this.process.StandardError;
            while (true)
            {
                Console.Write("Getting new line...");
                var output = streamReader.ReadLine();
                Console.WriteLine(output);

                try
                {
                    if (output != null)
                    {
                        var splitOutput = output.Split(',', ' ', '/');
                        foreach (var part in splitOutput)
                        {
                            Console.WriteLine(counter + " " + part);
                            counter += 1;
                        }

                        if (output.Contains("yay!"))
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

                counter = 0;
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }
}