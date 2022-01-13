<template>
  <div class="container-fluid">
    <div class="row text-center my-5">
      <h1>{{ vault.name }}</h1>
      <p class="m-0">{{ vault.description }}</p>
    </div>
    <div class="row mt-4">
      <div class="col-12 masonry">
        <div class="item" v-for="k in keeps" :key="k.id">
          <div class="content">
            <Keep :keep="k" />
          </div>
        </div>
      </div>
    </div>
    <KeepModal />
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await vaultsService.getKeepsInVault(route.params.id)
      } catch (error) {
        router.push({
          name: 'Home'
        })
      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry {
  /* Masonry container */
  column-count: 4;
  column-gap: 1em;
}

.item {
  /* Masonry bricks or child elements */
  background-color: #eee;
  display: inline-block;
  margin: 0 0 1em;
  width: 100%;
}
</style>