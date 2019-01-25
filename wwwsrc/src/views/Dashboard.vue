<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-lg-3 col-sm-12">
        <b-btn v-if="targetUser == user.username" class="btn btn-dark " @click="modal3Show = !modal3Show">New Keep</b-btn>
        <b-modal v-model="modal3Show" hide-footer id="newKeep" title="Create A New Keep">
          <form @submit.prevent="AddNewKeep(user.username)">
            <div class="form-group row">
              <label class="col-3" for="name">Keep Title :</label>
              <input class="col-8" type="text" name="name" v-model="keep.name" placeholder="Title">
            </div>
            <div class="form-group row">
              <label for="text" class="col-3">Keep Description :</label>
              <input name="text" class="col-8" type="text-field" v-model="keep.text" placeholder="Description">
            </div>
            <div class="form-group row">
              <label for="imgurl" class="col-3">Image URL :</label>
              <input name="imgurl" class="col-8" type="url" v-model="keep.imgUrl" placeholder="Image URL">
            </div>
            <div class="form-group row">
              <label for="isprivate" class="col-3">Private?</label>
              <input name="isprivate" type="checkbox" v-model="keep.isPrivate">
            </div>
            <button @click="modal3Show = !modal3Show" class="btn btn-dark" type="submit">Create New Keep</button>
          </form>
        </b-modal>
      </div>
      <h1 class="col-lg-6 col-sm-12" v-if="this.$route.params.userId == user.id && targetUser == user.username">Your
        Keeps</h1>
      <h1 class="col-lg-6 col-sm-12" v-if="targetUser != user.username">{{targetUser}}'s Keeps</h1>
      <div class="col-lg-3 col-sm-12">
        <b-btn @click="modal2Show =!modal2Show" v-if="targetUser == user.username" class="btn btn-dark">New Vault</b-btn>
        <b-modal hide-footer v-model="modal2Show" id="newVault" title="Create A New Vault">
          <form @submit.prevent="createVault()">
            <div class="form-group row">
              <label class="col-3" for="name">Title : </label>
              <input class="col-8" type="text" v-model="newVault.name" name="name">
            </div>
            <div class="form-group row">
              <label class="col-3" for="description">Description : </label>
              <input class="col-8" type="text-field" v-model="newVault.description" name="description">
            </div>
            <div class="form-group row">
              <label class="col-3" for="imgurl">Cover Image URL : </label>
              <input class="col-8" type="url" name="imgurl" v-model="newVault.imgUrl" placeholder="255 character max">
            </div>
            <div class="form-group row">
              <label class="col-3" for="isPrivate">Private?</label>
              <input type="checkbox" name="isPrivate" v-model="newVault.isPrivate">
            </div>
            <button @click="modal2Show = !modal2Show" class="btn btn-dark" type="submit">Create New Vault</button>
          </form>
        </b-modal>
      </div>
    </div>
    <div class="row">
      <div v-for="keep in userKeeps" class="card col-lg-4 col-md-6 col-sm-12 text-white bg-dark border-info">
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
              <button type="submit" @click="hideVKModal(keep.id)" class="btn btn-dark">Add to Vault(s)</button>
            </form>
          </b-modal>
        </div>
      </div>
    </div>
    <div class="row">
      <h1 class="col" v-if="this.$route.params.userId == user.id && targetUser == user.username">Your Vaults</h1>
      <h1 class="col" v-if="targetUser != user.username">{{targetUser}}'s Vaults</h1>
    </div>
    <div class="row">
      <div v-for="vault in vaults" class="card bg-dark text-white col-lg-4 col-md-6 col-sm-12">
        <div class="card-header row">
          <i v-if="!vault.isPrivate && user.id == vault.userId" class="fas fa-lock-open col-4" @click="togglePrivateVault(vault)"></i>
          <i v-if="vault.isPrivate" class="fas fa-lock col-4" @click="togglePrivateVault(vault)"></i>
          <h3 class="col-4" @click="viewTargetVault(vault.id)">{{vault.name}}</h3>
          <i v-if="user.id == vault.userId" class="fas fa-trash col-4" @click="deletevault(vault)"></i>
        </div>
        <img :src="vault.imgUrl" class="card-img-top" @click="viewTargetVault(vault.id)">
        <h3 class="card-footer">{{vault.description}}</h3>
      </div>
    </div>
  </div>
</template>
<script>
  import newkeep from "@/Components/NewKeep"
  export default {
    name: "dashboard",
    mounted() {
      this.$store.dispatch('GetUserKeeps', this.$route.params.userId)
      this.$store.dispatch('GetUserVaults', this.$route.params.userId)
    },
    computed: {
      userKeeps() {
        return this.$store.state.userKeeps
      },
      user() {
        return this.$store.state.user
      },
      vaults() {
        return this.$store.state.vaults
      },
      targetUser() {
        return this.$store.state.targetUser
      }
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
      togglePrivate(keep) {
        keep.isPrivate = !keep.isPrivate
        this.$store.dispatch("UpdateKeep", keep)
      },
      togglePrivateVault(vault) {
        vault.isPrivate = !vault.isPrivate
        this.$store.dispatch('UpdateVault', vault)
      },
      deletekeep(keep) {
        this.$store.dispatch('DeleteKeep', keep)
      },
      deletevault(vault) {
        this.$store.dispatch('DeleteVault', vault)
      },
      createVault() {
        this.$store.dispatch('AddVault', this.newVault)
      },
      viewTargetVault(vaultId) {
        this.$store.dispatch('RouteToVault', vaultId)
      },
      AddNewKeep(username) {
        this.keep.creatorName = username
        this.$store.dispatch('AddKeep', this.keep)
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
    components: {
      newkeep
    },
    data() {
      return {
        modalShow: false,
        modal2Show: false,
        modal3Show: false,
        newVault: {
          name: '',
          description: '',
          imgUrl: '',
        },
        keep: {
          name: "",
          text: "",
          imgUrl: "",
          creatorName: ""
        },
        payload: {}
      }
    },
  }
</script>