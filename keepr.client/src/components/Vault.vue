<template>
  <div
    class="card bg-dark text-white rounded elevation-2 grad selectable"
    @click="setActiveVault"
  >
    <img
      :src="'https://images.unsplash.com/photo-1609358905581-e5381612486e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=711&q=80'"
      class="card-img"
      alt="keep img"
    />
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
        <h5 class="card-title title-text">{{ vault.name }}</h5>
      </div>
    </div>
  </div>
</template>


<script>
import { useRouter } from 'vue-router'

import { vaultsService } from '../services/VaultsService'
export default {
  props: {
    vault: { type: Object, required: true },
  },
  setup(props) {
    const router = useRouter()
    return {
      router,
      setActiveVault() {
        router.push({
          name: 'Vault',
          params: { id: `${props.vault.id}` }
        })
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