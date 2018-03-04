namespace MoneroMiner
{
    public interface IMiner
    {
        /// <summary>
        /// The method to run the miner.
        /// </summary>
        /// <param name="poolAddress">The pool address, something like stratum+tcp://xmr.pool.minergate.com:45560.</param>
        /// <param name="xmrAddress">The wallet address.</param>
        /// <param name="threads">Number of threads.</param>
        // ReSharper disable once UnusedMember.Global
        void RunMiner(string poolAddress, string xmrAddress, string threads);

        /// <summary>
        /// Exits/ kills all miner processes.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        void Exit();
    }
}