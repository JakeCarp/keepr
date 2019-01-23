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
    targetKeep: {}
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    //Keep mutations
    //#region 
    setKeeps(state, keeps) {
      state.keeps = keeps
    },
    setTargetKeep(state, keep) {
      state.targetKeep = keep
    }
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
          router.push({ name: 'home' })
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
          commit('setKeeps', res.data)
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
          dispatch('GetAllKeeps')
        })
    },
    //update Keep
    UpdateKeep({ commit, dispatch }, payload) {
      api.put('keeps/' + payload.keepId)
        .then(res => {
          commit('setTargetKeep', res.data)
        })
    },
    //delete Keep
    DeleteKeep({ commit, dispatch }, keepId) {
      api.delete('keeps/' + keepId)
      dispatch('GetAllKeeps')
    },
    //#endregion
    //route to user dash
    RouteToDash({ commit, dispatch }, userid) {
      router.push({ name: 'dashboard', params: { userId: userid } })
    }
  }
})