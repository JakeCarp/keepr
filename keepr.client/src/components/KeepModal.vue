<template>
  <Modal id="keep-modal" class="p-5">
    <template #modal-body>
      <div class="container-fluid">
        <div class="text-end">
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="row">
          <div class="col-6 mb-3">
            <img
              height="500"
              class="w-100 object-fit-cover rounded"
              :src="keep.img"
              alt=""
            />
          </div>
          <div class="col-6 text-center justify-content-center align-items-top">
            <div class="row">
              <p class="m-0 col-4">
                {{ keep.views }}<i class="mdi mdi-eye"></i>
              </p>
              <p class="m-0 col-4">
                {{ keep.shares }}<i class="mdi mdi-share"></i>
              </p>
              <p class="m-0 col-4">
                {{ keep.views }}<i class="mdi mdi-eye"></i>
              </p>
            </div>
            <div class="info">
              <h2>
                {{ keep.name }}
              </h2>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>