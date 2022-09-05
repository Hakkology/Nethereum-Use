using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Nethereum.Web3;

namespace Hakan.ConsoleApp.GetBalance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetAccountBalance().Wait();
            Console.ReadLine();
        }

        static async System.Threading.Tasks.Task GetAccountBalance()
        {
            var web3 = new Web3("https://kovan.infura.io/v3/caffea16b05a4541b560f288be086598");
            var balance = await web3.Eth.GetBalance.SendRequestAsync("0x2F2539fd37F0AB30287Bf8d793eD5EbcF53ef7F2");
            Console.WriteLine($"Balance in Wei: {balance.Value}");
            
            var etherAmount = Web3.Convert.FromWei(balance.Value);
            Console.WriteLine($"Balance in Ether: {etherAmount}");
        }
    }
}

