<template>
  <div class="home">
    <div class="row">
      <b-btn class="btn btn-dark col" v-b-modal.newKeep>New Keep</b-btn>
      <b-modal hide-footer id="newKeep" title="Create A New Keep">
        <login v-if="!user.id" />
        <newkeep v-if="user.id" />
      </b-modal>
      <h1 class="col greeting">Today Is a New Day {{user.username}}</h1>
      <button class="btn btn-dark col" @click="RouteToDash(user.id, user.username)">My Dashboard</button>
    </div>
    <div class="card-deck">
      <div v-for="keep in keeps" class="card col-4 text-white bg-dark border-info">
        <h1 @click="viewKeep(keep)" class=" viewkeep card-header">{{keep.name}}</h1>
        <div class="card-img-top">
          <img @click="viewKeep(keep)" class="viewkeep" :src="keep.imgUrl" />
        </div>
        <div class="card-body">
          <h4 @click="RouteToDash(keep.userId, keep.creatorName)" class="card-subtitle viewkeep">{{keep.creatorName}}</h4>
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
      RouteToDash(userId, username) {
        this.$store.dispatch('RouteToDash', { userId: userId, targetname: username })
      },
      addShare(keep) {
        keep.shares++
        this.$store.dispatch('UpdateKeep', keep)
      },
      viewKeep(keep) {
        keep.views++
        this.$store.dispatch('UpdateKeep', keep)
        this.$router.push({ name: 'keep', params: { keepId: keep.id } })
      }
    },
  };
</script>
<style>
  html {
    width: 101%;
  }

  .btn {
    margin: 1rem;
    height: 5vh;
  }

  .greeting {
    margin: 1rem;
  }

  i {
    font-size: 2rem;
    cursor: pointer;
  }

  .modalContent {
    color: #343a40;
  }

  .viewkeep {
    cursor: pointer;
  }

  img {
    max-height: 300px;
    max-width: 300px;
  }
</style>