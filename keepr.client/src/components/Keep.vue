<template>
  <div
    class="card bg-dark text-white rounded elevation-2 grad selectable"
    data-bs-toggle="modal"
    data-bs-target="#keep-modal"
    @click="setActive"
  >
    <img :src="keep.img" class="card-img" alt="keep img" />
    <div
      class="
        card-img-overlay
        grad
        d-flex
        align-items-end
        justify-content-between
      "
    >
      <div class="text-center">
        <h5 class="card-title title-text">{{ keep.name }}</h5>
      </div>
      <div>
        <img
          :src="keep.creator.picture"
          alt="user photo"
          height="40"
          class="rounded"
        />
      </div>
    </div>
  </div>
</template>


<script>
import { keepsService } from '../services/KeepsService'
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      async setActive() {
        await keepsService.setActive(props.keep)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.grad {
  background-image: linear-gradient(black);
}
.title-text {
  font-weight: 800;
  color: white;
  text-shadow: -1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000,
    1px 1px 0 #000;
}
</style>