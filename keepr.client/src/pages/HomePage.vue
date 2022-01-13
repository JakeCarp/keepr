<template>
  <div class="container-fluid">
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
import { computed, onBeforeUnmount, onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      await keepsService.getAllKeeps()
    })
    return {
      keeps: computed(() => AppState.keeps),
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
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
