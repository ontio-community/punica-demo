# punica-ts-demo

> A Vue.js project

The dapp implements different tokens to be redeemed at a set ratio.



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

## Compile/deploy/invoke smartcontract

ontsctf.py is used to compile, deploy and test smart contracts.

### Install require:

``` bash
python >= 3.7
```

```
pip install ontology-python-sdk
```

### Compile
```
npm run sc-compile
```
It will generate the abi and avm files and put them in ```/contracts/contracts``` directory.

### Deploy
```
npm run sc-deploy
```
Smart contract deployment needs the configuration file ```deploy.json``` and put it in ```/contracts/config/``` directory.

### Test SC methods
```
npm run sc-invoke
```
Testing sc methods needs the configuration file ```invoke.json``` and put it in ```/contracts/config/``` directory.
