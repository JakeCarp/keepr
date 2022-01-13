<template>
  <Modal id="create-keep-modal">
    <template #modal-body>
      <form>
        <div class="mb-3">
          <label for="keepName" class="form-label">Keep Name</label>
          <input
            type="text"
            v-model="keepData.name"
            class="form-control"
            id="keepName"
            placeholder="Name..."
          />
        </div>
        <div class="mb-3">
          <label for="keepDesc" class="form-label">keep Description</label>
          <textarea
            class="form-control"
            id="keepDesc"
            rows="3"
            v-model="keepData.description"
          ></textarea>
        </div>
        <div class="mb-3">
          <label for="keepImg" class="form-label">Keep Image</label>
          <input
            type="text"
            v-model="keepData.img"
            class="form-control"
            id="keepImg"
            placeholder="Image URL..."
          />
        </div>
      </form>
      <div class="text-end">
        <button class="btn btn-success" @click="createKeep">Create Keep</button>
      </div>
    </template>
  </Modal>
</template>


<script>
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { ref } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    let keepData = ref({})
    return {
      keepData,
      async createKeep() {
        try {
          await keepsService.createKeep(keepData.value)
          Modal.getOrCreateInstance(document.getElementById("create-keep-modal")).hide()
          Pop.toast('Keep Created!', 'success')
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("create-keep-modal")).hide()
          Pop.toast('Somthing Went Wrong!', 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>