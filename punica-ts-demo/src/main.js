// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import { client } from 'ontology-dapi';

client.registerClient({});
Vue.config.productionTip = false

import {Button, Table, Alert, Modal, Input, Icon, message} from 'ant-design-vue'

Vue.component(Button.name, Button);
Vue.component(Table.name, Table);
Vue.component(Alert.name, Alert);
Vue.component(Modal.name, Modal);
Vue.component(Input.name, Input);
Vue.component(Icon.name, Icon);
Vue.prototype.$message = message
Vue.prototype.$confirm = Modal.confirm
Vue.prototype.$success = Modal.success

/* eslint-disable no-new */
new Vue({
  el: '#app',
  components: { App },
  template: '<App/>'
})
