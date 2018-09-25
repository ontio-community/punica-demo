using Ont.SmartContract.Framework;
using Ont.SmartContract.Framework.Services.Ont;
using Ont.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace ExchangeContract
{
    public class ExchangeContract : SmartContract
    {
        // 合约管理员，设置Token兑换比率，手续费等操作
        //public static readonly byte[] admin = "AT4dGijAQSMcN5mj2q7ePTWcKFEnG1qeu3".ToScriptHash();
        public static readonly byte[] admin = "AefENjgwPQsGNVocUB2X3Rh3tG18uZPnv6".ToScriptHash();

        public static readonly byte[] basePrefix = "base_".AsByteArray();

        public delegate object NEP5Contract(string method, object[] args);

        public static Object Main(string operation, params object[] args)
        {
            if (operation == "Exchange")
            {
                if (args.Length != 5) return false;
                byte[] from = (byte[])args[0];
                byte[] to = (byte[])args[1];
                string fromSymbol = (string)args[2];
                string toSymbol = (string)args[3];
                ulong value = (ulong)args[4];
                return Exchange(from, to, fromSymbol, toSymbol, value);
            }


            // 设置token兑换比率，比如1TokenA:2TokenB,symbol=tokena, base=1,symbol=tokenb,base=2
            if (operation == "SetTokenBase")
            {
                if (args.Length != 2) return false;
                string symbol = (string)args[0];
                BigInteger value = (BigInteger)args[1];
                return SetTokenBase(symbol, value);
            }

            // 获取Token的兑换比率（百分比）
            if (operation == "GetTokenBase")
            {
                if (args.Length != 1) return false;
                string symbol = (string)args[0];
                return GetTokenBase(symbol);
            }
            // 设置手续费百分比率，例如5%则percentage=5
            if (operation == "SetFeeRate")
            {
                if (args.Length != 1) return false;
                ulong percentage = (ulong)args[0];
                return SetFeeRate(percentage);
            }

            // 获取当前手续费比率（百分比）
            if (operation == "GetFeeRate")
            {
                return GetFeeRate();
            }
            // 设置合约Hash
            if (operation == "SetContractHash")
            {
                if (args.Length != 2) return false;
                string contractKey = (string)args[0];
                byte[] hash = (byte[])args[1];
                return SetContractHash(contractKey, hash);
            }
            // 获取指定商户的合约hash
            if (operation == "GetContractHash")
            {
                if (args.Length != 1) return false;
                string contractKey = (string)args[0];
                return GetContractHash(contractKey);
            }
            return false;
        }
        // from应该是兑换目的token发起方的账户，to 是目的token的回收账户
        // fromSymbol是发起方token的符号，比如TokenA,"tokena", toSymbol同理
        // value 是兑换token的数量
        public static bool Exchange(byte[] from, byte[] to, string fromSymbol, string toSymbol, ulong value)
        {
            if (value < 0) return false;
            if (from.Length != 20 || to.Length != 20) return false;

            byte[] fromHash = GetContractHash(fromSymbol);
            byte[] toHash = GetContractHash(toSymbol);
            if (fromHash.Length != 20 || toHash.Length != 20) return false;

            byte[] contract = ExecutionEngine.ExecutingScriptHash;

            StorageContext context = Storage.CurrentContext;

            ulong fromBase = (ulong)Storage.Get(context, basePrefix.Concat(fromSymbol.AsByteArray())).AsBigInteger();
            ulong toBase = (ulong)Storage.Get(context, basePrefix.Concat(toSymbol.AsByteArray())).AsBigInteger();

            if (fromBase <= 0 || toBase <= 0)
            {
                Runtime.Notify("Invalid token convertion base.");
                return false;
            }

            ulong feeRate = (ulong)Storage.Get(context, "feeRate").AsBigInteger();

            if (feeRate < 0)
            {
                Runtime.Notify("Invalid fee rate.");
                return false;
            }

            ulong toValue = (value * toBase / fromBase) * (100 - feeRate) / 100;

            if (!TransferNEP5(from, to, fromHash, value)) throw new Exception();

            if (!TransferNEP5(contract, from, toHash, toValue)) throw new Exception();

            return true;
        }

        public static bool SetTokenBase(string symbol, BigInteger value)
        {
            if (value <= 0) return false;
            if (!Runtime.CheckWitness(admin)) return false;

            byte[] baseKey = basePrefix.Concat(symbol.AsByteArray());
            Storage.Put(Storage.CurrentContext, baseKey, value);
            return true;
        }

        public static BigInteger GetTokenBase(string symbol)
        {
            return Storage.Get(Storage.CurrentContext, basePrefix.Concat(symbol.AsByteArray())).AsBigInteger();
        }

        public static bool SetFeeRate(ulong percentage)
        {
            if (percentage < 0 || percentage >= 100) return false;
            if (!Runtime.CheckWitness(admin)) return false;

            Storage.Put(Storage.CurrentContext, "feeRate", percentage);
            return true;
        }

        public static BigInteger GetFeeRate()
        {
            return Storage.Get(Storage.CurrentContext, "feeRate").AsBigInteger();
        }

        public static bool SetContractHash(string key, byte[] hash)
        {
            if (!Runtime.CheckWitness(admin)) return false;
            if (key == "" || key.Length == 0 || hash.Length != 20) return false;

            Storage.Put(Storage.CurrentContext, key, hash);
            return true;
        }

        public static byte[] GetContractHash(string key)
        {
            return Storage.Get(Storage.CurrentContext, key);
        }

        private static bool TransferNEP5(byte[] from, byte[] to, byte[] assetID, BigInteger amount)
        {
            var args = new object[] { from, to, amount };
            var contract = (NEP5Contract)assetID.ToDelegate();
            if (!(bool)contract("transfer", args)) return false;
            return true;
        }
    }
}