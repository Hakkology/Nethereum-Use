using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Hex.HexTypes;

namespace Hakan.ConsoleApp.EtherTransac
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How much would you like to send? ");
            decimal Amounttobesent = Convert.ToDecimal(Console.ReadLine());
            var privateKey = ""; //enter your own private key

            MakeTransaction(privateKey, Amounttobesent).Wait();
            Console.WriteLine("Transaction Complete.\n" +
                "------------------------------------");

            GetAccountBalance().Wait();
            Console.ReadLine();
        }

        static async Task MakeTransaction(string pkey, decimal amount)
        {
            var Accountname = new Account(pkey, Nethereum.Signer.Chain.Kovan);
            var web3 = new Web3(Accountname, "https://kovan.infura.io/v3/caffea16b05a4541b560f288be086598");
            var toAddress = "0x6dDA31ACF16d6b214D61BcBe2d57EfBa3042ddB9"; //public key for a friend for testing purposes
            var transaction = await web3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(toAddress, amount, 3m);
            System.Diagnostics.Process.Start("https://kovan.etherscan.io/tx/" + transaction.TransactionHash);
        }
        static async Task GetAccountBalance()
        {
            var web3 = new Web3("https://kovan.infura.io/v3/caffea16b05a4541b560f288be086598");
            var balance = await web3.Eth.GetBalance.SendRequestAsync("0xd4741Bf17F6857525Cb46f0fb8F78Af20c41263f"); //public key for self
            Console.WriteLine($"Balance in Wei: {balance.Value}");

            var etherAmount = Web3.Convert.FromWei(balance.Value);
            Console.WriteLine($"Balance in Ether: {etherAmount}");
        }
    }
}
