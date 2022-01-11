<template>
  <div class="card bg-dark text-white">
    <img :src="keep.img" class="card-img" alt="keep img" />
    <div class="card-img-overlay">
      <h5 class="card-title">Card title</h5>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>