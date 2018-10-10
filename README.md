# Punica-demo


## dApp开发框架Punica

Punica框架是方便用户开发dApp的工具集，使用Typescript和python开发，具体功能包含以下几个模块。
* Blockchain(Ontology Node)
* Smart Contract(compile,deploy,invoke,debug,test)
* SDKs and dAPI
* Decentralized Storage (IPFS)
* Web Technologies
* Other

Punica框架目前实现了Typescript和Python版本，[punica-ts-demo](punica-ts-demo)，[punica-python-demo](punica-python-demo)。接下来描述每个模块的使用方法和文档说明。

**Blockchain(Ontology Node)**

* 主网(http://dappnode1.ont.io)，测试网(http://polaris1.ont.io)。端口号：rpc(20336),restful(20334),websocket(20335)
* [安装](https://ontio.github.io/documentation/install_en.html)
* [源码](https://github.com/ontio/ontology)
* [release](https://github.com/ontio/ontology/releases)
* [http API](https://ontio.github.io/documentation/rpc_api_en.html)
* [postman](https://documenter.getpostman.com/view/1459587/RWaRP68Y)
* [命令行文档](https://github.com/ontio/ontology/blob/master/docs/specifications/cli_user_guide_CN.md)

**Smart Contract(compile,deploy,invoke,debug,test)**

* [教材](https://ontio.github.io/documentation/Introduction_of_Ontology_Smart_Contract_en.html)
* [智能合约文档](https://github.com/ontio/ontology-smartcontract)
* [API文档](https://apidoc.ont.io/smartcontract/)
* [SmartX](https://smartx.ont.io/)
* [C#编译器](https://github.com/ontio/ontology-compiler)
* [Python编译器](https://github.com/ontio/neo-boa)

**SDKs and dAPI**

* [Java SDK](https://github.com/ontio/ontology-java-sdk)， [Typescript SDK](https://github.com/ontio/ontology-ts-sdk)， [Python SDK](https://github.com/ontio/ontology-python-sdk) ， [Golang SDK](https://github.com/ontio/ontology-go-sdk)
* [dAPI教程](https://ontio.github.io/documentation/ontology_dapp_dev_tutorial_en.html)
* [dAPI源码](https://github.com/ontio/ontology-dapi)

* [iOS钱包集成](https://ontio.github.io/documentation/ontology_wallet_dev_ts_sdk_en.html)
* [Android钱包集成](https://ontio.github.io/documentation/ontology_wallet_dev_android_en.html)


**Decentralized Storage (IPFS)**

* [IPFS实践文档](https://github.com/ChainBook/IPFS-For-Chinese)
* [IPFS安装及python客户端调用](https://github.com/lucas7788/workingdata/tree/master/ipfs)

**Web Technologies**



**Other**

* [区块链浏览器](https://explorer.ont.io/)
* [官方文档](https://ontio.github.io/documentation/)
* [Ontid和Claim文档](https://ontio.github.io/documentation/ontology_DID_en.html)
* [同步区块程序](https://github.com/zzsZhou/OntSynHandler)
* [签名机文档](https://github.com/ontio/ontology/blob/master/docs/specifications/sigsvr_CN.md)


## dApp


案例[这里](examples)，官网已经上线的dApps[这里](https://dapp.ont.io/)。以下针对dApp的不同类型分别有对应例子。

* [Nep-5]()(没文档)
* [Oep-4]()(没文档)
* [Bancor]()(没文档)
* [一元夺宝]()(没文档)
* [域名拍卖](examples/domain-auction.md)
* [上线的钱包](https://dapp.ont.io/)
* [区块链浏览器](https://explorer.ont.io/)
* [dFS去中心化文件系统]()(没文档)
* [Token兑换服务](examples/token-exchange.md)
* [对人民币交易平台](ddfx.md)(没文档)
* [数据交易](ddfx.md)(没文档)
* [交易定时器](examples/transaction-timer.md)
* [余额变动短信提醒](dfs.md)
* [加密邮箱](examples/crypto-message.md)



