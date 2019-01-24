<template>
  <div class="home">
    <h1>HELLO WORLD</h1>
    <div class="card-deck">
      <div v-for="keep in keeps" class="card col-4 text-white bg-dark border-info">
        <h1 class="card-header">{{keep.name}}</h1>
        <div class="card-img-top">
          <img :src="keep.imgUrl" />
        </div>
        <div class="card-body">
          <h4 class="card-subtitle">{{keep.creatorName}}</h4>
        </div>
        <div class="row card-footer">
          <i class="fas fa-eye col-4"> {{keep.views}}</i>
          <i @click="modalShow = !modalShow" class="fas fa-share col-4"> {{keep.shares}}</i>
          <b-modal v-model="modalShow" :id="keep.name" hide-footer hide-header>
            <!-- <a :href="facebookUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/keep.id " target="_blank"></a> -->
            <i class="modalContent fab fa-facebook-square"> Share To FaceBook</i>
            <!-- <a :href="twitterUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/keep.id"></a> -->
            <i class="modalContent fab fa-twitter-square">Share To Twitter</i>
            <!-- <a :href="linkedinUrl + !INSERT HEROKU URL AFTER DEPLOYMENT!/keep.id"></a> -->
            <i class="modalContent fab fa-linkedin"> Share To Linkedin</i>
          </b-modal>
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
        modalShow: false,
        facebookUrl: "https://www.facebook.com/sharer/sharer.php?u=",
        twitterUrl: "https://twitter.com/share?url=",
        linkedinUrl: "https://www.linkedin.com/shareArticle?mini=true&url="
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
    cursor: pointer;
  }

  .modalContent {
    color: #343a40;
  }

  img {
    max-height: 300px;
    max-width: 300px;
  }
</style>