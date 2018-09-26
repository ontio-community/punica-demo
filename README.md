# dapp-examples

dApp优势：
* 减轻节点故障。现代web应用程序依赖的基础设施，如服务器、代码库、数据库等，即使采用高可用性方案和可靠的基础设施服务商，也难以避免存在单点故障和停机。dapp通过多个对等节点网络上存储数据或基础架构的关键组建来缓解这些问题。
* 减少对中心机构的依赖。软件中的业务逻辑本质上是区块链上的一组智能合约。传统的软件业务逻辑和数据无法保证在服务器上不被篡改。智能合约无法被随意修改，数据在每个节点上都有记录，可以减少对中心化的依赖。
* 提高安全性。dApp可以对上链数据做保护或加密，用户通过密钥获取数据，无需通过数据网关。
* 密码学。账户安全性。
* 去中心化数据存储。多节点存储。

>dapp-examples只是dapp的参考例子，合约还需要用户自己测试，并不适合直接商用。



## dApp开发框架

dApp开发框架是方便用户开发dApp的工具集，包含以下几个模块。
* Blockchain(Ontology Node)
* Smart Contract(compile,deploy,invoke,debug,test)
* SDKs and dAPI
* Decentralized Storage (IPFS)
* Web Technologies
* Other

接下来描述每个模块的使用方法和文档说明。

**Blockchain(Ontology Node)**

* 主网(http://dappnode1.ont.io)，测试网(http://polaris1.ont.io)。端口号：rpc(20336),restful(20334),websocket(20335)
* [安装](https://ontio.github.io/documentation/install_en.html)
* [源码](https://github.com/ontio/ontology)
* [release](https://github.com/ontio/ontology/releases)
* [http API](https://ontio.github.io/documentation/rpc_api_en.html)
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

**Web Technologies**



**Other**

* [区块链浏览器](https://explorer.ont.io/)
* [官方文档](https://ontio.github.io/documentation/)
* [同步区块程序](https://github.com/zzsZhou/OntSynHandler)
* [签名机文档](https://github.com/ontio/ontology/blob/master/docs/specifications/sigsvr_CN.md)


## dApp分类


dApp类型：

* Token
* 游戏
* 娱乐
* 医疗
* 广告/营销
* 交易/电商
* 新闻资讯
* 数字钱包/支付
* 房地产
* 浏览器
* 旅游
* 工具
* 金融
* 投资
* 求职/就业
* 社交
* 存储
* 交易所
* 其他

项目实例[这里](./obox-exchange)，官网已经上线的dApps[这里](https://dapp.ont.io/)。以下针对dApp的不同类型分别有对应例子。

### Token
* [Nep-5]()(没文档)
* [Oep-4]()(没文档)
* [Bancor]()(没文档)

### 游戏
* [一元夺宝]()(没文档)
### 娱乐

### 医疗

### 广告/营销

### 交易/电商
* [域名拍卖](domain-auction.md)

### 新闻资讯



### 数字钱包/支付

* [上线的钱包](https://dapp.ont.io/)


### 房地产

### 浏览器

* [区块链浏览器](https://explorer.ont.io/)

### 旅游

### 金融

### 投资

### 社交



### 存储

* [dFS去中心化文件系统]()(没文档)

### 交易所

* [Token兑换服务](token-exchange.md)
* [对人民币交易平台](ddfx.md)(没文档)
* [数据交易](ddfx.md)(没文档)

### 工具

* [交易定时器](transaction-timer.md)
* [余额变动短信提醒](dfs.md)
* [加密邮箱](crypto-message.md)


### 其他

* [信用查询系统]()(没文档)

