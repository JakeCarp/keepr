<template>
  <div>
    <form @submit.prevent="newKeep()">
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
    </form>
    <button class="btn btn-dark" type="submit">Create New Keep</button>
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
    methods: {
      AddNewKeep() {
        this.$store.dispatch('AddKeep', this.keep)
      }
    },
  }
</script>