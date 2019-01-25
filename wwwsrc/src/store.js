import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'


Vue.use(Vuex)


let auth = Axios.create({
  baseURL: "http://localhost:5000/account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: "http://localhost:5000/api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    targetKeep: {},
    vaults: [],
    userKeeps: [],
    targetVault: {},
    vaultKeeps: [],
    targetUser: ''
  },
  mutations: {
    //User Mutations
    //#region 
    setUser(state, user) {
      state.user = user
    },
    SetTargetUser(state, username) {
      state.targetUser = username
    },
    //#endregion
    //Keep mutations
    //#region 
    setKeeps(state, keeps) {
      state.keeps = keeps
    },
    setTargetKeep(state, keep) {
      state.targetKeep = keep
    },
    SetUserKeeps(state, userkeeps) {
      state.userKeeps = userkeeps
    },
    //#endregion
    //Vault Mutations
    //#region 
    SetVaults(state, vaults) {
      state.vaults = vaults
    },
    SetTargetVault(state, vault) {
      state.targetVault = vault
    },
    SetVaultKeeps(state, vaultkeeps) {
      state.vaultKeeps = vaultkeeps
    },
    //#endregion
  },
  actions: {
    //Auth Actions
    //#region 
    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          dispatch("GetUserVaults", res.data.id)
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {
      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
          dispatch("GetUserVaults", res.data.id)
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },
    logout({ commit, dispatch }) {
      auth.delete('logout')
        .then(res => {
          commit('setUser', res.data)
        })
    },
    //#endregion
    //Keep Actions
    //#region 
    //Get all Keeps
    GetAllKeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setKeeps', res.data)
        })
    },
    //Get User Keeps
    GetUserKeeps({ commit, dispatch }, userId) {
      api.get('keeps/user/' + userId)
        .then(res => {
          commit('SetUserKeeps', res.data)
          commit('SetTargetUser', res.data[0].creatorName)
        })
    },
    //Get keep By Id
    GetTargetKeep({ commit, dispatch }, keepId) {
      api.get('keeps/' + keepId)
        .then(res => {
          commit('setTargetKeep', res.data)
        })
    },
    //add keep
    AddKeep({ commit, dispatch }, payload) {
      api.post('keeps', payload)
        .then(res => {
          dispatch('GetUserKeeps', res.data.userId)
        })
    },
    //update Keep
    UpdateKeep({ commit, dispatch }, payload) {
      api.put('keeps/' + payload.id, payload)
        .then(res => {
          commit('setTargetKeep', res.data)
        })
    },
    //delete Keep
    DeleteKeep({ commit, dispatch }, keep) {
      api.delete('keeps/' + keep.id, keep.id)
        .then(res => {
          dispatch('GetUserKeeps', keep.userId)
        })
    },
    //#endregion
    //Vualt Actions
    //#region 
    //Get User Vaults
    GetUserVaults({ commit, dispatch }, userId) {
      api.get('vaults/user/' + userId)
        .then(res => {
          commit('SetVaults', res.data)
        })
    },
    GetVaultById({ commit, dispatch }, vaultId) {
      api.get('vaults/' + vaultId)
        .then(res => {
          commit('SetTargetVault', res.data)
        })
    },
    UpdateVault({ commit, dispatch }, vault) {
      api.put('vaults/' + vault.id, vault)
        .then(res => {
          dispatch('GetUserVaults', vault.userId)
        })
    },
    AddVault({ commit, dispatch }, vault) {
      api.post('vaults', vault)
        .then(res => {
          dispatch('GetUserVaults', vault.userId)
        })
    },
    DeleteVault({ commit, dispatch }, vault) {
      api.delete('vaults/' + vault.id, vault.id)
        .then(res => {
          dispatch('GetUserVaults', vault.userId)
        })
    },
    //#endregion
    //VaultKeep Actions
    //#region 
    //Get keeps by vault ID
    GetKeepsByVaultId({ commit, dispatch }, vaultId) {
      api.get('vaultkeeps/' + vaultId)
        .then(res => {
          commit('SetVaultKeeps', res.data)
        })
    },
    AddVaultKeep({ commit, dispatch }, payload) {
      api.post('vaultkeeps', payload)
        .then(res => {
          window.alert('keep added to vault!')
        })
    },
    DeleteVaultKeep({ commit, dispatch }, payload) {
      api.delete('vaultkeeps/' + payload.keepId + '/' + payload.vaultId, payload)
        .then(res => {
          dispatch('GetKeepsByVaultId', payload.vaultId)
        })
    },
    //#endregion
    //routing Actions
    //#region 
    //route to user dash
    RouteToDash({ commit, dispatch }, payload) {
      commit('SetTargetUser', payload.targetname)
      router.push({ name: 'dashboard', params: { userId: payload.userId } })
    },
    RouteToVault({ commit, dispatch }, vaultId) {
      router.push({ name: 'vault', params: { vaultId: vaultId } })
    }
    //#endregion
  }
})