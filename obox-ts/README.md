# obox-ts

> A Vue.js project

The dapp implements different tokens to be redeemed at a set ratio.


## contract instruction

* Set the Token base to calculate the conversion ratio of different Tokens.

```
public static bool SetTokenBase(string symbol, BigInteger value)
```

parameter description

`symbol`   Token symbol

`value`    Token base

For example, if the Token base of TokenA is set to 1, and the Token base of TokenB is set to 2, the ratio of Token1 and Token2 when redeeming is 1:2.

* query Token base

```
public static BigInteger GetTokenBase(string symbol)
```

parameter description

`symbol`   Token name

* setting fee

```
public static bool SetFeeRate(ulong percentage)
```

parameter description

percentage   fee

For example, percentage=5, indicating that the handling fee is 5%.

* query fee

```
public static BigInteger GetFeeRate()
```

* setting Token contract hash

```
 public static bool SetContractHash(string key, byte[] hash)
```

parameter description

`key` Token contract name

`hash` Token contract hash

Note: When setting the Token contract address, you need to reverse the Token address.

* query contract hash

```
public static byte[] GetContractHash(string key)
```

parameter description

`key` Token name

* exchanging of different Tokens

```
public static bool Exchange(byte[] from, byte[] to, string fromSymbol, string toSymbol, ulong value)
```

parameter description

`from` Redeem the account of the Token originator

`to` Purpose Token's recycling account

`fromSymbol`  The symbol of the originator Token

`toSymbol` Token symbol to be redeemed

`value` Number of redemptions


## Build Setup

``` bash
# install dependencies
npm install

# serve with hot reload at localhost:8080
npm run dev

# build for production with minification
npm run build

# build for production and view the bundle analyzer report
npm run build --report
```


For a detailed explanation on how things work, check out the [guide](http://vuejs-templates.github.io/webpack/) and [docs for vue-loader](http://vuejs.github.io/vue-loader).



## Directory structure

```shell
$ tree
└── obox-exchange
    ├── build
    └── config
    └── contracts
        └── config
        └── contracts
        ontsctf.py
    └── src
    └── static
    └── wallet
```

```build```  Files for building vue project.

```config```  Configuration files.

```contracts``` Diretory for smart contracts related files. ```config``` contains configuration files for deploying and invoking smart contracts. ```contracts``` contains smartc contract source codes, avm and abi files generated after comiple.

```src```  Vue project source codes.

```static``` Static files

```wallet``` Wallet file.

## How to use ontsctf.py ?

ontsctf.py is used to compile, deploy and test smart contracts。

### ontsctf require:

python >= 3.7

```
pip install ontology-python-sdk
```

### Compile
```
npm run sc-compile
```
It will generate the abi and avm files and put them in /contracts/contracts directory.

### Deploy
```
npm run sc-deploy
```
Smart contract deployment needs the configuration file deploy.json and put it in /contracts/config/ directory.

### Test SC methods
```
npm run sc-invoke
```
Testing sc methods needs the configuration file invoke.json and put it in /contracts/config/ directory.
