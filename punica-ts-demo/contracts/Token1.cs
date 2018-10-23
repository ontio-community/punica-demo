using Ont.SmartContract.Framework;
using Ont.SmartContract.Framework.Services.Ont;
using Ont.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace Token1
{
    public class Token1 : SmartContract
    {
        //Token setting
        [DisplayName("name")]
        public static string Name() => "Token1 Token";
        [DisplayName("symbol")]
        public static string Symbol() => "T1";
        [DisplayName("decimals")]
        public static byte Decimals() => 1;
        private const ulong factor = 1; //decided by Decimals()

        // Account setting
        public static readonly byte[] admin = "ANH5bHrrt111XwNEnuPZj6u95Dd6u7G4D6".ToScriptHash();
        // public static readonly byte[] admin = {97,111,42,74,56,57,111,242,3,234,1,230,192,112,174,66,27,184,206,45};
        public static readonly byte[] BaseAccount = "ANH5bHrrt111XwNEnuPZj6u95Dd6u7G4D6".ToScriptHash();
        // public static readonly byte[] BaseAccount = {117,117,82,107,192,102,163,172,198,171,177,52,17,156,214,212,169,4,25,105};
        public static readonly byte[] RecAccount = "AWf8NiLzXSDf1JB2Ae6YUKSHke4yLHMVCm".ToScriptHash();
        // public static readonly byte[] RecAccount = {248,142,51,220,214,177,110,235,27,218,59,86,23,47,140,20,114,119,159,152};


        private const ulong totalAmount = 1000000000 * factor;

        public struct State
        {
            public byte[] From;
            public byte[] To;
            public BigInteger Amount;
        }

        public static Object Main(string operation, params object[] args)
        {
                if (operation == "deploy") return Deploy();
                if (operation == "totalSupply") return TotalSupply();
                if (operation == "name") return Name();
                if (operation == "symbol") return Symbol();
                if (operation == "decimals") return Decimals();
                if (operation == "transfer")
                {
                    if (args.Length != 3) return false;
                    byte[] from = (byte[])args[0];
                    byte[] to = (byte[])args[1];
                    BigInteger value = (BigInteger)args[2];
                    return Transfer(from, to, value);
                }
                if (operation == "balanceOf")
                {
                    if (args.Length != 1) return 0;
                    byte[] account = (byte[])args[0];
                    return BalanceOf(account);
                }

                if (operation == "inflation")
                {
                    if (args.Length != 1) return false;
                    BigInteger count = (BigInteger)args[0];
                    return Inflation(count);
                }

                if (operation == "recycle")
                {
                    if (args.Length != 1) return false;
                    BigInteger count = (BigInteger)args[0];
                    return Recycle(count);
                }
                if (operation == "transferMulti")
                {
                    return TransferMulti(args);
                }
            return false;
        }

        [DisplayName("deploy")]
        public static bool Deploy()
        {
            byte[] total_supply = Storage.Get(Storage.CurrentContext, "totalSupply");
            if (total_supply.Length != 0) return false;

            Storage.Put(Storage.CurrentContext, BaseAccount, totalAmount);
            Runtime.Notify(null, BaseAccount, totalAmount);

            Storage.Put(Storage.CurrentContext, "totalSupply", totalAmount);
            return true;
        }

        [DisplayName("totalSupply")]
        public static BigInteger TotalSupply()
        {
            return Storage.Get(Storage.CurrentContext, "totalSupply").AsBigInteger();
        }

        [DisplayName("transfer")]
        public static bool Transfer(byte[] from, byte[] to, BigInteger value)
        {
            if (value <= 0) return false;
            if (!Runtime.CheckWitness(from)) return false;
            if (to.Length != 20) return false;
            if (from == to) return true;

            BigInteger from_value = Storage.Get(Storage.CurrentContext, from).AsBigInteger();
            if (from_value < value) return false;
            if (from_value == value)
                Storage.Delete(Storage.CurrentContext, from);
            else
                Storage.Put(Storage.CurrentContext, from, from_value - value);

            BigInteger to_value = Storage.Get(Storage.CurrentContext, to).AsBigInteger();
            Storage.Put(Storage.CurrentContext, to, to_value + value);
            Runtime.Notify(from, to, value);
            return true;
        }

        [DisplayName("balanceOf")]
        public static BigInteger BalanceOf(byte[] address)
        {
            return Storage.Get(Storage.CurrentContext, address).AsBigInteger();
        }

        [DisplayName("inflation")]
        public static bool Inflation(BigInteger count)
        {
            if (count < 0) return false;
            if (!Runtime.CheckWitness(admin)) return false;

            BigInteger balance = BalanceOf(BaseAccount);
            StorageContext context = Storage.CurrentContext;
            Storage.Put(context, BaseAccount, balance + count);

            BigInteger totalSupply = Storage.Get(Storage.CurrentContext, "totalSupply").AsBigInteger();
            Storage.Put(context, "totalSupply", totalSupply + count);
            Runtime.Notify(null, admin, count);
            return true;
        }

        [DisplayName("recycle")]
        public static bool Recycle(BigInteger count)
        {
            if (count < 0) return false;
            if (!Runtime.CheckWitness(admin)) return false;

            BigInteger balance = BalanceOf(BaseAccount);
            StorageContext context = Storage.CurrentContext;
            BigInteger totalSupply = Storage.Get(Storage.CurrentContext, "totalSupply").AsBigInteger();

            if ( (balance > count) && (totalSupply - count >= totalAmount))
            {
                Storage.Put(context, BaseAccount, balance - count);
                Storage.Put(context, "totalSupply", totalSupply - count);
                Runtime.Notify(admin, null, count);
                return true;
            }
            return false;
        }
        // <summary>transfer multiple amount of token from  multiple sender to multiple receiver</summary>
        // <returns>return transfer result, if any transfer fail, all of transfers should fail. </returns>
        // <param name="args">state struct</param>
        //  public struct State
        // {
        //    public byte[] From; // transfer sender
        //    public byte[] To; // transfer receiver
        //    public BigInteger Amount; //transfer amount
        //}
        [DisplayName("transferMulti")]
        public static bool TransferMulti(object[] args)
        {
            for(int i=0;i<args.Length;i++)
            {
                State state = (State)args[i];
                if(!Transfer(state.From, state.To, state.Amount)) throw new Exception();
            }
            return true;
        }
    }
}
