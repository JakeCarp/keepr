<template>
  <div>
    <div class="row">
      <div class="col-3">
        <button class="btn btn-dark">New Keep</button>
      </div>
      <h1 class="col-6" v-if="this.$route.params.userId == user.id">Your Keeps</h1>
      <div class="col-3">
        <button class="btn btn-dark">New Vault</button>
      </div>
    </div>
    <div class="card-deck">
      <div v-for="keep in keeps" class="card col-4 text-white bg-dark border-info">
        <div class="card-header row">
          <i v-if="!keep.isPrivate" class="fas fa-lock-open col-4"></i>
          <i v-if="keep.isPrivate" class="fas fa-lock col-4"></i>
        </div>
        <div class="card-img-top">
          <img :src="keep.imgUrl" />
        </div>
        <div class="card-body">
          <h1 class="card-title">{{keep.name}}</h1>
        </div>
        <div class="row card-footer">
          <i class="fas fa-eye col-4"> {{keep.views}}</i>
          <i class="fas fa-share col-4"> {{keep.shares}}</i>
          <i class="fas fa-folder-plus col-4"> {{keep.keeps}}</i>
        </div>
      </div>
    </div>
    <!-- <div class="card-deck">
      <div v-for="vaults in Vaults" class="card col-4">
        <div class="card-img-top">

        </div>
        <div class="card-body">
          <h1 class="card-title">{{vualt.name}}</h1>
        </div>
      </div>
    </div> -->
  </div>
</template>
<script>
  export default {
    name: "dashboard",
    mounted() {
      this.$store.dispatch('GetUserKeeps', this.$route.params.userId)
    },
    computed: {
      keeps() {
        return this.$store.state.keeps
      },
      user() {
        return this.$store.state.user
      }
    },
  }
</script>