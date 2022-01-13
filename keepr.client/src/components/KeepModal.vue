<template>
  <Modal id="keep-modal">
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
              :alt="keep.title"
              :title="keep.title"
            />
          </div>
          <div class="col-6 text-center d-flex align-items-between">
            <div class="row">
              <div class="col-12 mb-4">
                <div class="row">
                  <p class="m-0 col-4" title="keep views">
                    {{ keep.views }} <i class="mdi mdi-eye text-success"></i>
                  </p>
                  <p class="m-0 col-4" title="keep shares">
                    {{ keep.shares }} <i class="mdi mdi-share text-success"></i>
                  </p>
                  <p class="m-0 col-4" title="keep vaults">
                    {{ keep.keeps }}
                    <i class="mdi mdi-safe-square-outline text-success"></i>
                  </p>
                </div>
              </div>
              <div class="info col-12">
                <h2>
                  {{ keep.name }}
                </h2>
                <p class="m-0">{{ keep.description }}</p>
              </div>
              <div class="controls col-12 align-self-end">
                <div class="row pb-3 justify-content-between align-items-end">
                  <div class="vaultsdropdown col-4" v-if="account.id">
                    <button
                      class="btn btn-secondary dropdown-toggle"
                      type="button"
                      id="dropdownMenu"
                      data-bs-toggle="dropdown"
                      title="add to vault"
                      aria-expanded="false"
                    >
                      Add to Vault
                    </button>
                    <ul class="dropdown-menu" aria-labelledby="dropdownMenu">
                      <li
                        @click="addToVault(v.id)"
                        v-for="v in userVaults"
                        :key="v.id"
                        class="selectable my-1"
                      >
                        <p class="m-0">{{ v.name }}</p>
                      </li>
                    </ul>
                  </div>
                  <div class="col-1">
                    <i
                      title="delete keep"
                      class="mdi mdi-trash-can mdi-24px selectable"
                      @click="deleteKeep"
                      v-if="account.id === keep.creatorId"
                    ></i>
                  </div>
                  <div class="col-4">
                    <div class="row align-items-end">
                      <div
                        class="p-0 d-flex flex-column"
                        v-if="account.id === keep.creatorId"
                      >
                        <router-link to="/account">
                          <img
                            :src="keep.creator?.picture"
                            alt="user photo"
                            height="40"
                            class="rounded"
                          />
                          <p class="m-0">
                            {{ keep.creator?.name.split("@")[0].toUpperCase() }}
                          </p>
                        </router-link>
                      </div>
                      <div v-else>
                        <router-link
                          :to="{
                            name: 'Profile',
                            params: { id: keep.creatorId },
                          }"
                        >
                          <img
                            :src="keep.creator?.picture"
                            alt="user photo"
                            height="40"
                            class="rounded"
                          />
                          <p class="m-0">
                            {{ keep.creator?.name.split("@")[0].toUpperCase() }}
                          </p>
                        </router-link>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
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
import { vaultKeepsService } from '../services/VaultKeepsService'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { onBeforeUnmount } from '@vue/runtime-core'
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {

    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      userVaults: computed(() => AppState.userVaults),

      async addToVault(vaultId) {
        try {
          await vaultKeepsService.addToVault(this.keep.id, vaultId)

          Pop.toast('Keep Added to Vault!', 'success')
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm('Are you sure you want to delete this Keep?', 'This Cannot be undone', 'question')) {
            await keepsService.deleteKeep(this.keep.id)
          }
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
          Pop.toast('Keep Deleted!', 'success')
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
          Pop.toast('Something went wrong', 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.mdi-trash-can:hover {
  color: red;
}
</style>