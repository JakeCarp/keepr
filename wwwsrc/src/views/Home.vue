<template>
  <div class="container-fluid">
    <div class="row">
      <b-btn class="btn btn-dark col-sm-12 col-md" @click="modal3Show = !modal3Show">New Keep</b-btn>
      <b-modal hide-footer v-model="modal3Show" id="newKeep" title="Create A New Keep">
        <login v-if="!user.id" />
        <newkeep v-if="user.id" />
      </b-modal>
      <h1 class="col-sm-12 col-md greeting">Today Is a New Day {{user.username}}</h1>
      <button class="btn btn-dark col-sm-12 col-md" @click="RouteToDash(user.id, user.username)">My Dashboard</button>
    </div>
    <div class="row">

      <div v-for="keep in keeps" class="card col-lg-4 col-md-6 col-sm-12  text-white bg-dark border-info">
        <h1 @click="viewKeep(keep)" class=" viewkeep card-header">{{keep.name}}</h1>
        <div class="card-img-top img-fluid">
          <img @click="viewKeep(keep)" class="viewkeep" :src="keep.imgUrl" />
        </div>
        <div class="card-body">
          <h4 @click="RouteToDash(keep.userId, keep.creatorName)" class="card-subtitle viewkeep">{{keep.creatorName}}</h4>
        </div>
        <div class="row card-footer">
          <i class="fas fa-eye col"> {{keep.views}}</i>
          <i @click="modalShow = !modalShow" class="fas fa-share col"> {{keep.shares}}</i>
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
          <i @click="showVKModal(keep.id)" class="fas fa-folder-plus col-4"> {{keep.keeps}}</i>
          <b-modal :ref="'vkModal' + keep.id" hide-footer hide-header>
            <div class="row">
              <h3 class="modalContent">Add Keep to Which Vault(s)</h3>
            </div>
            <form @submit.prevent="createVaultKeeps(keep)">
              <div v-for="vault in vaults" class="form-group row">
                <input type="checkbox" :name="vault.id" v-model="payload[vault.id]">
                <label class="modalContent col-3" :for="vault.id">{{vault.name}}</label>
              </div>
              <button type="submit" class="btn btn-dark">Add to Vault(s)</button>
            </form>
          </b-modal>
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
      },
      vaults() {
        return this.$store.state.vaults
      }
    },
    data() {
      return {
        modal2Show: false,
        modalShow: false,
        modal3Show: false,
        facebookUrl: "https://www.facebook.com/sharer/sharer.php?u=",
        twitterUrl: "https://twitter.com/share?url=",
        linkedinUrl: "https://www.linkedin.com/shareArticle?mini=true&url=",
        payload: {}
      }
    },
    components: {
      login,
      newkeep,
    },
    methods: {
      showVKModal(id) {
        let mod = 'vkModal' + id
        this.$refs[mod][0].show()
      },
      hideVKModal(id) {
        let mod = 'vkModal' + id
        this.$refs[mod][0].hide()
      },
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
  };
</script>
<style>
  .keepsButton {
    display: flex;
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