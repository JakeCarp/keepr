<template>
  <div
    class="card bg-dark text-white rounded elevation-2 grad selectable"
    @click.stop="setActive"
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
        <button
          v-if="route.name === 'Vault'"
          class="btn btn-danger"
          @click="removeFromVault"
        >
          Remove from Vault
        </button>
      </div>

      <div
        v-if="
          account.id === keep.creatorId &&
          route.name != 'Profile' &&
          route.name != 'Account'
        "
      >
        <div @click.stop="routeAcct">
          <img
            :src="keep.creator?.picture"
            alt="user photo"
            height="40"
            class="rounded"
          />
        </div>
      </div>
      <div
        v-if="
          route.name != 'Profile' &&
          route.name != 'Account' &&
          keep.creatorId != account.id
        "
      >
        <div @click.stop="routeProfile">
          <img
            :src="keep.creator?.picture"
            alt="user photo"
            height="40"
            class="rounded"
          />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService'
import Pop from '../utils/Pop'
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    return {
      route,
      account: computed(() => AppState.account),
      async setActive() {
        try {
          await keepsService.setActive(props.keep)
          if (document.getElementById("keep-modal")) {
            Modal.getOrCreateInstance(document.getElementById("keep-modal")).toggle()
          }
        } catch (error) {
          logger.error(error)
        }
      },
      routeProfile() {
        router.push({
          name: 'Profile',
          params: { id: `${props.keep.creatorId}` }
        })
      },
      routeAcct() {
        router.push({
          name: 'Account',
        })
      },
      async removeFromVault() {
        try {
          await vaultKeepsService.removeFromVault(props.keep.vaultKeepId)
          Pop.toast('Keep removed from vault', 'success')
        } catch (error) {
          logger.error(error)
          Pop.toast('Something went wrong', 'error')
        }
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