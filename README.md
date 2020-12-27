MoneroMiner
====================================

MoneroMiner is an assembly/ library to run a monero miner in .Net.
The assembly was written and tested in .Net 5.0.

[![Build status](https://ci.appveyor.com/api/projects/status/jk5qalw6jgqy9ol6?svg=true)](https://ci.appveyor.com/project/SeppPenner/monerominer)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/MoneroMiner.svg)](https://github.com/SeppPenner/MoneroMiner/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/MoneroMiner.svg)](https://github.com/SeppPenner/MoneroMiner/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/MoneroMiner.svg)](https://github.com/SeppPenner/MoneroMiner/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/SeppPenner/MoneroMiner/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/MoneroMiner/badge.svg)](https://snyk.io/test/github/SeppPenner/MoneroMiner)

## Basic usage:
```csharp
IMiner miner = new Miner();
miner.RunMiner("stratum+tcp://xmr.pool.minergate.com:45560", "rsaUGDSAQIF_UWRTGWUAFSAdshz8fw7wgfh", "2");
miner.Exit();
```

See the method options below:

```csharp
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
```

This project is based on [monerospelunker](https://github.com/jwinterm/monerospelunker).

Change history
--------------

See the [Changelog](https://github.com/SeppPenner/MoneroMiner/blob/master/Changelog.md).