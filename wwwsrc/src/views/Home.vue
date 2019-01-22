<template>
  <div class="home">
    <h1>HELLO WORLD</h1>
    <div v-for="keep in keeps">
      <h1>{{keep.name}}</h1>
    </div>
  </div>
</template>

<script>
  export default {
    name: "home",
    mounted() {
      this.$store.dispatch('GetAllKeeps')
    },
    computed: {
      keeps() {
        return this.$store.state.keeps
      }
    },
    data() {
      return {

      }
    },
  };
</script>