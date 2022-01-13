<template>
  <div class="container-fluid">
    <div class="row mt-5">
      <div class="col-2">
        <img
          class="rounded object-cover-fit"
          :src="focusedUser?.picture"
          alt=""
        />
      </div>
      <div class="col-3">
        <div class="row">
          <div class="col-12">
            <h1>{{ focusedUser.name?.split("@")[0] }}</h1>
          </div>
          <div class="col-12">
            <h4>
              Keeps: <span>{{ keeps?.length }}</span>
            </h4>
          </div>
          <div class="col-12">
            <h4>
              Vaults: <span>{{ vaults?.length }}</span>
            </h4>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <div class="col-12">
        <h3>
          Vaults
          <!-- <i
            title="create vault"
            class="mdi mdi-plus text-success selectable"
          ></i> -->
        </h3>
      </div>
      <div class="col-12">
        <div class="row ms-4">
          <div class="col-2" v-for="v in vaults" :key="v.id">
            <Vault :vault="v" />
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <div class="col-12">
        <h3>
          Keeps
          <!-- <i
            title="create vault"
            class="mdi mdi-plus text-success selectable"
          ></i> -->
        </h3>
      </div>
      <div class="col-12">
        <div class="row ms-4">
          <div class="col-12 masonry">
            <div class="item" v-for="k in keeps" :key="k.id">
              <div class="content">
                <Keep :keep="k" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <KeepModal />
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { useRoute } from 'vue-router'
import { accountService } from '../services/AccountService'
import { AppState } from '../AppState'
import Vault from '../components/Vault.vue'
import { Modal } from 'bootstrap'
export default {
  components: { Vault },
  setup() {
    const route = useRoute()
    onMounted(async () => {
      await vaultsService.getProfileVaults(route.params.id)
      await keepsService.getUserKeeps(route.params.id)
      await accountService.getProfile(route.params.id)
    })
    return {
      focusedUser: computed(() => AppState.focusedUser),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
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