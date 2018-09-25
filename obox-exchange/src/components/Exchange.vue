<template>
<div class="main-content">
  <div class="account-balance">
      <p> Your Token Balance: </p>
      <div><span class="label">Address: </span>{{account}}</div>
      <div><span class="label">Token1: </span> {{accountBalance.token1}}</div>
      <div><span class="label">Token2: </span> {{accountBalance.token2}}</div>      
    </div>
<div class="container">
    <h1>Ontology Exchange</h1>
    <div class="charge-tip">Service Charge: 5%</div>
    <!-- Main page -->
    <a-table :columns="columns" :dataSource="data">
      <span slot="action" slot-scope="text, record">
          <a href="javascript:;" @click="toExchange(record)">Exchange</a>
      </span>
    </a-table>
    <a-alert message="Token1:Token2 = 1 : 2" type="info"/>

    <a-modal
            title="Exchange Token"
            :visible="showModal"
            @ok="handleOk"
            @cancel="handleCancel"
            >
            <div class="modal-item">
              <div class="modal-border">
                <div><span class="label">Token To Exchange: </span> {{tokenToExchange}}</div>
              <div><span class="label">Amount To Exchange: </span> <a-input class="token-input" @change="handleInputChange" v-model="amountToExchange"></a-input></div>
              </div>
                <div class="change-icon"><a-icon type="sync" style="font-size:20px;"/></div>
              <div class="modal-border">
                <div><span class="label">Token To Cost: </span> {{tokenToCost}}</div>
                <div><span class="label">Amount To Cost: </span> {{amountToCost}} (Balance: {{costBalance}})</div>
              </div>
              
            </div>
        </a-modal>
</div>
</div>
</template>

<script>
import {Parameter, ParameterType, utils, Crypto} from 'ontology-ts-sdk'
import {client} from 'ontology-dapi'

const gasLimit = 30000;
const gasPrice = 500;
const Exchange_Contract = '7dffd39e53be06f104f443857f9115ec55212b43';
const Recycle_Address = 'AWyZRDzFp3c53VTLdyD1Z31gB4bUo8ojN4'
const columns = [{
  dataIndex: 'tokenName',
  title: 'Token Name',
  key: 'tokenName'
}, {
  dataIndex:'contractAddr',
  title: 'Contract Address',
  key: 'contractAddr'
}, {
  dataIndex: 'balance',
  title: 'Balance',
  key: 'balance'
}, {
  dataIndex: 'ratio',
  title: 'Ratio',
  key: 'ratio'
}, {
  title: 'Action',
  key: 'action',
  scopedSlots: {customRender: 'action'}
}]
const data = [{
        key:'token1',
        tokenName: 'Token1',
        contractAddr: '749a701ae89c0dbdab9b4b660ba84ee478004219',
        balance: 0,
        ratio: 1
      }, {
        key: 'token2',
        tokenName : 'Token2',
        contractAddr: 'f9417534cbd3b09976f75f8597fe7be3fd88456b',
        balance: 0,
        ratio: 2
      }]
export default {
  name: 'Exchange',
  data () {
    return {
      columns,
      data,
      showModal:false,
      tokenToExchange:'',
      amountToExchange:0,
      tokenToCost: '',
      amountToCost: 0,
      costBalance: 0,
      account: '',
      enoughBalance: true,
      accountBalance: {
        token1:0,
        token2:0
      }
    }
  },
  async mounted() {
    // get provider
      try { 
        const provider = await client.api.provider.getProvider();
        console.log('onGetProvider: ' + JSON.stringify(provider));
        this.provider = provider;
      } catch (e) {
        console.log('No dAPI provider istalled.');
        this.$message.warning('No provider installed. Please install the Cyano Wallet before joining the game.')
        return null;
      }
      // get account
      let account;
      try { 
        account = await client.api.asset.getAccount();
        console.log(account);
      } catch (err) {
        console.log(err)
        this.$message.warning('No account found in the provider. Please prepare an account before joining the game.')
        return 'NO_ACCOUNT';
      }
      this.account = account;
      //get balance of token1 and token2
      this.refresh();
      this.intervalId = setInterval(() => {
        this.refresh()
      }, 5000);
      
  },
  beforeDestroy() {
    // clearInterval(this.intervalId);
  },
  methods: {
    async refresh() {
      const address = new Crypto.Address(Exchange_Contract).toBase58();
      const balance1 = await this.getTokenBalance(address, data[0].contractAddr)
      const balance2 = await this.getTokenBalance(address, data[1].contractAddr)
      this.data[0].balance = balance1
      this.data[1].balance = balance2

      const accountToken1 = await this.getTokenBalance(this.account, data[0].contractAddr)
      const accountToken2 = await this.getTokenBalance(this.account, data[1].contractAddr)
      this.accountBalance = {
        token1: accountToken1,
        token2: accountToken2
      }
    },
    async toExchange(record){
      this.showModal = true;
      console.log(record)
      if(record.tokenName === 'Token1') {
        this.tokenToExchange = 'Token1'
        this.tokenToCost = 'Token2'
        this.amountToExchange = 0
        this.amountToCost = 0
        this.costBalance = this.accountBalance.token2
      } else {
        this.tokenToExchange = 'Token2'
        this.tokenToCost = 'Token1'
        this.amountToExchange = 0
        this.amountToCost = 0
        this.costBalance = this.accountBalance.token1
      }
    },
    async handleOk(){
      if(this.amountToExchange === 0) {
        this.$message.error('Please enter the valid amount to exchange.')
        return;
      }
      if(!this.enoughBalance) {
        this.$message.error('You do not have enough balance to exchange token.')
        return;
      }
      const method = 'Exchange'
      const from = new Crypto.Address(this.account).serialize();
      const to = new Crypto.Address(Recycle_Address).serialize();
      const fromSymbol = this.tokenToCost
      const toSymbol = this.tokenToExchange
      const value = parseInt(this.amountToExchange)
      const parameters = [
        new Parameter('from', ParameterType.ByteArray, from),
        new Parameter('to', ParameterType.ByteArray, to),
        new Parameter('fromSymbol', ParameterType.String, fromSymbol),
        new Parameter('toSymbol', ParameterType.String, toSymbol),
        new Parameter('value', ParameterType.Integer, value),        
      ]
      const contract = Exchange_Contract
      const params = {
        contract,
        method,
        parameters,
        gasPrice,
        gasLimit
      }
      const res = await this.scInvoke(params, false);
      console.log(res);
      this.showModal = false;
    },
    handleCancel() {
      this.showModal = false;
    },
    handleInputChange() {
      if(this.tokenToExchange === 'Token1') {
        this.amountToCost = this.amountToExchange*this.data[1].ratio / this.data[0].ratio
      } else {
        this.amountToCost = this.amountToExchange*this.data[0].ratio / this.data[1].ratio
      }
      if(this.costBalance < this.amountToCost) {
          this.enoughBalance = false
      } else {
        this.enoughBalance = true
      }
    },
    async scInvoke(params, preExec) {
      try {
        let result;
        if(preExec) {
           result = await client.api.smartContract.invokeRead(params);       
        } else {
           result = await client.api.smartContract.invoke(params);
        }
        console.log('onScCall finished, result:' + JSON.stringify(result));        
        return result;
      } catch (e) {
        console.log('onScCall error:', e);
        this.$message.error('Some error happens. Please try later.')
        return null;
      }
    },
    async getTokenBalance(address, contractAddr) {
      const method = 'balanceOf'
      const contract = contractAddr
      const parameters = [
        new Parameter('', ParameterType.ByteArray, new Crypto.Address(address).serialize())
      ]
      const params = {
        contract,
        method,
        parameters
      }
      const res = await this.scInvoke(params, true);
      if(res) {
        return parseInt(utils.reverseHex(res), 16);
      } else {
        return 0;
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.container {
  width:80%;
  margin:0 auto;
  position: relative;
}
.token-input {
  width:200px;
}
.modal-item  div{
  margin-bottom:10px;
}
.change-icon {
  text-align: center;
  color:#42b983;
  font-weight: bold;
}
.label {
  font-weight: bold;
  text-align:left;
}
.modal-border {
  padding:5px;
  border: 1px solid #42b983;
}
.account-balance {
  position: absolute;
  right: 10px;
  top: 10px;
  border: 1px solid #42b983;
  padding:10px;
}
.charge-tip {
  font-weight: bold;
}

.account-balance p {
  margin-bottom:5px;
}
.account-balance div {
  text-align:left;
}
</style>
