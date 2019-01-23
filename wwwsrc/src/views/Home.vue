<template>
  <div class="home">
    <h1>HELLO WORLD</h1>
    <div class="card-deck">
      <div v-for="keep in keeps" class="card col-4">
        <div class="card-img-top">
          <img src="https://catingtonpost.com/wp-content/uploads/2017/08/457e2d35-4fd2-40dd-83da-cde9cadccdef.gif" />
        </div>
        <h1 class="card-title">{{keep.name}}</h1>
        <div class="row">
          <i class="fas fa-eye col-4"> {{keep.views}}</i>
          <i class="fas fa-share col-4"> {{keep.shares}}</i>
          <i class="fas fa-folder-plus col-4"> {{keep.keeps}}</i>
        </div>
      </div>
    </div>
    <b-btn v-b-modal.modal1>New Keep</b-btn>
    <b-modal id="modal1" title="Create A New Keep">
      <login v-if="!user.id" />
      <newkeep v-if="user.id" />
    </b-modal>
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
    }
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