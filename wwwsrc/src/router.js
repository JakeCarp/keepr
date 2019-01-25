import Vue from 'vue'
import Router from 'vue-router'
// @ts-ignore
import Home from './views/Home.vue'
// @ts-ignore
import dashboard from './views/Dashboard.vue'
// @ts-ignore
import keep from './views/Keep.vue'
// @ts-ignore
import vault from './views/Vault.vue'
// @ts-ignore
import about from './views/About.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/dashboard/:userId',
      name: 'dashboard',
      component: dashboard
    },
    {
      path: '/keep/:keepId',
      name: 'keep',
      component: keep
    },
    {
      path: '/vault/:vaultId',
      name: 'vault',
      component: vault
    },
    {
      path: '/about',
      name: 'about',
      component: about
    }
  ]
})
