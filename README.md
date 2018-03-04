MoneroMiner
====================================

MoneroMiner is an assembly/ library to run a monero miner in .Net.
The assembly was written and tested in .Net 4.7.

[![Build status](https://ci.appveyor.com/api/projects/status/jk5qalw6jgqy9ol6?svg=true)](https://ci.appveyor.com/project/SeppPenner/monerominer)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/MoneroMiner.svg)](https://github.com/SeppPenner/MoneroMiner/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/MoneroMiner.svg)](https://github.com/SeppPenner/MoneroMiner/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/MoneroMiner.svg)](https://github.com/SeppPenner/MoneroMiner/stargazers)
[![GitHub license](https://img.shields.io/badge/license-AGPL-blue.svg)](https://raw.githubusercontent.com/SeppPenner/MoneroMiner/master/License.txt)
[![Nuget](https://img.shields.io/badge/MoneroMiner-Nuget-brightgreen.svg)](https://www.nuget.org/packages/HaemmerElectronics.SeppPenner.Language/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/HaemmerElectronics.SeppPenner.Language.svg)](https://www.nuget.org/packages/HaemmerElectronics.SeppPenner.Language/)

## Basic usage:
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

* **Version 1.0.0.0 (2018-03-04)** : 1.0 release.
