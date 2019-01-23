<template>
  <div>
    <form @submit.prevent="newKeep()">
      <input type="text" v-model="this.keep.name" placeholder="Title">
      <input type="text-field" v-model="this.keep.text" placeholder="Description">
      <input type="url" v-model="this.keep.imgUrl" placeholder="Image URL"> </form>
    <button type="submit">Create New Keep</button>
  </div>
</template>
<script>
  export default {
    name: "newkeep",
    data() {
      return {
        keep: {
          name: "",
          text: "",
          imgUrl: "",
        }
      }
    },
  }
</script>