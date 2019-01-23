<template>
  <div class="home">
    <h1>HELLO WORLD</h1>
    <div class="card-deck">
      <div v-for="keep in keeps" class="card col-4">
        <div class="card-img-top">
          <img :src="keep.imgUrl" />
        </div>
        <div class="card-body">
          <h1 class="card-title">{{keep.name}}</h1>
          <h4 class="card-subtitle">- {{keep.creatorName}}</h4>
        </div>
        <div class="row card-footer">
          <i class="fas fa-eye col-4"> {{keep.views}}</i>
          <i class="fas fa-share col-4"> {{keep.shares}}</i>
          <i class="fas fa-folder-plus col-4"> {{keep.keeps}}</i>
        </div>
      </div>
    </div>
    <b-btn class="btn btn-dark" v-b-modal.newKeep>New Keep</b-btn>
    <b-modal hide-footer id="newKeep" title="Create A New Keep">
      <login v-if="!user.id" />
      <newkeep v-if="user.id" />
    </b-modal>
    <button class="btn btn-dark" @click="RouteToDash">My Dashboard</button>
  </div>
</template>

<script>
  import login from "@/Components/Login"
  import newkeep from "@/Components/NewKeep"

  export default {
    name: "home",
    mounted() {
      this.$store.dispatch('GetAllKeeps')
    },
    computed: {
      keeps() {
        return this.$store.state.keeps
      },
      user() {
        return this.$store.state.user
      }
    },
    data() {
      return {

      }
    },
    components: {
      login,
      newkeep,
    },
    methods: {
      RouteToDash() {
        this.$store.dispatch('RouteToDash', this.user.id)
      }
    },
  };
</script>
<style>
  i {
    font-size: 2rem;
  }

  img {
    max-height: 300px;
    max-width: 300px;
  }
</style>