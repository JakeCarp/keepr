<template>
  <div class="container-fluid">
    <div class="row">
      <h1 class="col-12">{{vault.name}}</h1>
      <h2 class="col-12">{{vault.description}}</h2>
    </div>
    <div class="card-deck">
      <div v-for="keep in keeps" class="card col-4 text-white bg-dark border-info">
        <div class="card-header row">
          <i v-if="!keep.isPrivate && user.id == keep.userId" class="fas fa-lock-open col-4" @click="togglePrivate(keep)"></i>
          <i v-if="keep.isPrivate" class="fas fa-lock col-4" @click="togglePrivate(keep)"></i>
          <i v-if="user.id == keep.userId" class="fas fa-trash col-4 offset-4" @click="deletekeep(keep)"></i>
        </div>
        <div class="card-img-top">
          <img :src="keep.imgUrl" />
        </div>
        <div class="card-body">
          <h1 class="card-title">{{keep.name}}</h1>
        </div>
        <div class="row card-footer">
          <i class="fas fa-eye col-4"> {{keep.views}}</i>
          <i @click="modalShow = !modalShow" class="fas fa-share col-4"> {{keep.shares}}</i>
          <b-modal v-model="modalShow" :id="keep.name" hide-footer hide-header>
            <div class="row">
              <!-- <a :href="facebookUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/keep.id " target="_blank"></a> -->
              <i @click="addShare(keep)" class="modalContent fab fa-facebook-square col-2"></i>
              <h3 @click="addShare(keep)" class="col-7 modalContent">Share to Facebook</h3>
            </div>
            <div class="row">
              <!-- <a :href="twitterUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/keep.id"></a> -->
              <i @click="addShare(keep)" class="modalContent fab fa-twitter-square col-2"></i>
              <h3 @click="addShare(keep)" class="modalContent col-7">Share To Twitter</h3>
            </div>
            <div class="row">
              <!-- <a :href="linkedinUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/keep.id"></a> -->
              <i @click="addShare(keep)" class="modalContent fab fa-linkedin col-2"> </i>
              <h3 @click="addShare(keep)" class="modalContent col-7">Share To Linkedin</h3>
            </div>
          </b-modal>
          <i class="fas fa-folder-plus col-4"> {{keep.keeps}}</i>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  export default {
    name: 'vault',
    mounted() {
      this.$store.dispatch('GetVaultById', this.$route.params.vaultId)
      this.$store.dispatch("GetKeepsByVaultId", this.$route.params.vaultId)
    },
    computed: {
      vault() {
        return this.$store.state.targetVault
      },
      keeps() {
        return this.$store.state.vaultKeeps
      }
    },
    components: {

    }
  }
</script>