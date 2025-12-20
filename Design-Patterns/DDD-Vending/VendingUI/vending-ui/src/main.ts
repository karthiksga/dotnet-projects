import Vue from 'vue';
import { BootstrapVue, IconsPlugin, LayoutPlugin } from 'bootstrap-vue';
import App from './App.vue';
import router from './router';
import store from './store';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

Vue.config.productionTip = false;
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(LayoutPlugin);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
