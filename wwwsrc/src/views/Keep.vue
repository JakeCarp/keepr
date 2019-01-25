<template>
  <div class="container-fluid">
    <div class="row">
      <h1 class="col">See Something You Like?</h1>
    </div>
    <div class="row">
      <div class="card col text-white bg-dark border-info">
        <h1 class="card-header">{{targetkeep.name}}</h1>
        <div class="card-img-top">
          <img :src="targetkeep.imgUrl" />
        </div>
        <div class="card-body">
          <h3 class="card-text">{{targetkeep.text}}</h3>
          <p class="card-text">{{targetkeep.creatorName}}</p>
        </div>
        <div class="row card-footer">
          <i class="fas fa-eye col-4"> {{targetkeep.views}}</i>
          <i @click="modalShow = !modalShow" class="fas fa-share col-4"> {{targetkeep.shares}}</i>
          <b-modal v-model="modalShow" :id="targetkeep.name" hide-footer hide-header>
            <div class="row">
              <!-- <a :href="facebookUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/targetkeep.id " target="_blank"></a> -->
              <i @click="addShare(targetkeep)" class="modalContent fab fa-facebook-square col-2"></i>
              <h3 @click="addShare(targetkeep)" class="col-7 modalContent">Share to Facebook</h3>
            </div>
            <div class="row">
              <!-- <a :href="twitterUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/targetkeep.id"></a> -->
              <i @click="addShare(targetkeep)" class="modalContent fab fa-twitter-square col-2"></i>
              <h3 @click="addShare(targetkeep)" class="modalContent col-7">Share To Twitter</h3>
            </div>
            <div class="row">
              <!-- <a :href="linkedinUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/targetkeep.id"></a> -->
              <i @click="addShare(targetkeep)" class="modalContent fab fa-linkedin col-2"> </i>
              <h3 @click="addShare(targetkeep)" class="modalContent col-7">Share To Linkedin</h3>
            </div>
          </b-modal>
          <i @click="showVKModal(targetkeep.id)" class="fas fa-folder-plus col-4"> {{targetkeep.keeps}}</i>
          <b-modal :ref="'vkModal' + targetkeep.id" hide-footer hide-header>
            <div class="row">
              <h3 class="modalContent">Add Keep to Which Vault(s)</h3>
            </div>
            <form @submit.prevent="createVaultKeeps(targetkeep)">
              <div v-for="vault in vaults" class="form-group row">
                <input type="checkbox" :name="vault.id" v-model="payload[vault.id]">
                <label class="modalContent col-3" :for="vault.id">{{vault.name}}</label>
              </div>
              <button type="submit" @click="hideVKModal(targetkeep.id)" class="btn btn-dark">Add to Vault(s)</button>
            </form>
          </b-modal>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  export default {
    name: "keep",
    mounted() {
      this.$store.dispatch('GetTargetKeep', this.$route.params.keepId)
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      targetkeep() {
        return this.$store.state.targetKeep
      },
      vaults() {
        return this.$store.state.vaults
      }
    },
    methods: {
      addShare(keep) {
        keep.shares++
        this.$store.dispatch('UpdateKeep', keep)
      },
      showVKModal(id) {
        let mod = 'vkModal' + id
        this.$refs[mod].show()
      },
      hideVKModal(id) {
        let mod = 'vkModal' + id
        this.$refs[mod].hide()
      },
      createVaultKeeps(keep) {
        console.log(this.payload)
        let counter = 0
        //add vault keep
        for (let vid in this.payload) {
          if (this.payload[vid]) {
            counter++
            this.$store.dispatch('AddVaultKeep', { keepId: keep.id, vaultId: vid })
          }
        }
        //increment keeps prop on keep
        keep.keeps += counter
        this.$store.dispatch('UpdateKeep', keep)

      }
    },
    data() {
      return {
        modalShow: false,
        facebookUrl: "https://www.facebook.com/sharer/sharer.php?u=",
        twitterUrl: "https://twitter.com/share?url=",
        linkedinUrl: "https://www.linkedin.com/shareArticle?mini=true&url=",
        payload: {}
      }
    },
  }
</script>
<style scoped>
  .card {
    margin: 1rem;
    ;
  }
</style>