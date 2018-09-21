#  加密邮箱

加密邮箱,提供去中心化的加密邮箱服务。

## 贡献者

* Peiwen Zhou


## 功能介绍

* 用户可以注册邮箱，发送加密消息
* 接收者用私钥解密消息，就可以看到原文



## 页面示例

主页面：开发中



## 合约

加密邮箱合约实现的功能是：加密的消息储存和获取。

### 用法

1. register： 注册， 用户使用sm2算法生成ont账户，并上传公钥
2. getPubkey： 取得一个注册用户的公钥地址，用于加密消息
3. sendMessage: 发送消息， 向一个已注册用户发送加密/未加密消息
4. getMessageCount: 取得用户的消息总数
5. getMessage： 取得用户邮箱的某一个消息
6. getRangeMessage: 取得用户邮箱的 from number to number的多条消息

>加密和解密过程需在客户端进行

### 合约源代码
```python

'''
cryptoMessage
'''

from boa.interop.System.Runtime import CheckWitness
from boa.interop.System.Runtime import GetTime, Notify
from boa.interop.System.Runtime import Serialize
from boa.interop.System.Storage import Put, Get, GetContext
from boa.builtins import concat
from boa.builtins import range

ctx = GetContext()
messageboxPrefix = 'MESSAGEBOX'
messageCountPrefix = 'MESSAGECOUNT'
pubkeyPrefix = 'PUBKEY'

def Main(operation, args):
    if operation == 'sendMessage':
        if len(args) != 4:
            return False
        from_addr = args[0]
        to_addr = args[1]
        encrypt = args[2]
        message = args[3]
        return sendMessage(from_addr, to_addr, encrypt, message)

    if operation == 'getMessage':
        if len(args) != 2:
            return False
        address = args[0]
        number = args[1]
        return getMessage(address, number)

    if operation == 'getMessageCount':
        if len(args) != 1:
            return False
        address = args[0]
        return getMessageCount(address)

    if operation == 'register':
        if len(args) != 2:
            return False
        address = args[0]
        pubkey = args[1]
        return register(address, pubkey)

    if operation == 'getPubkey':
        if len(args) != 1:
            return False
        address = args[0]
        return getPubkey(address)

    if operation == 'getRangeMessage':
        if len(args) != 3:
            return False
        address = args[0]
        start = args[1]
        end = args[2]
        return getRangeMessage(address,start,end)


    return False


def sendMessage(from_addr, to_addr, encrypt, message):
    '''
    send message to some address
    :param from_addr:
    :param to_addr:
    :param encrypt:
    :param message:
    :return:
    '''

    if CheckWitness(from_addr) == False:
        return False

    timestamp = GetTime()

    msg = {'FROM': from_addr, 'ENCRYPT': encrypt, 'MESSAGE': message, 'TIMESTAMP': timestamp}

    countkey = concatkey(messageCountPrefix, to_addr)
    count = Get(ctx, countkey)

    Put(ctx, concatkey(concatkey(messageboxPrefix, to_addr), count + 1), Serialize(msg))
    Put(ctx, countkey, count + 1)

    Notify(['sendMessage', from_addr, to_addr, count + 1 ])
    return True


def getMessage(address, number):
    key = concatkey(concatkey(messageboxPrefix, address), number)
    content = Get(ctx, key)
    return content


def getMessageCount(address):
    key = concatkey(messageCountPrefix, address)
    return Get(ctx, key)


def register(address, pubkey):

    if CheckWitness(address) == False:
        return False

    key = concatkey(pubkeyPrefix, address)
    if not Get(ctx, key):
        Put(ctx, key, pubkey)
        Notify(['register', address, pubkey])
    return True


def getRangeMessage(address, start, end):

    count = getMessageCount(address)

    if start < 1:
        start = 1
    if start > count:
        return None
    if end > count:
        end = count

    res = []
    for idex in range(start, end + 1):
        content = getMessage(address, idex)
        res.append(content)
    return Serialize(res)


def getPubkey(address):
    key = concatkey(pubkeyPrefix, address)
    return Get(ctx, key)


def concatkey(str1, str2):
    return concat(concat(str1, '_'), str2)

    
```


