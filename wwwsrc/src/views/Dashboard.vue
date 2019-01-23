<template>
  <div>
    <h1>Welcome To your Dashboard</h1>
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
        return this.$store.keeps
      }
    },
  }
</script>