<template>
  <div class="container-fluid">
    <div class="row mt-5">
      <div class="col-2">
        <img class="rounded object-cover-fit" :src="account.picture" alt="" />
      </div>
      <div class="col-3">
        <div class="row">
          <div class="col-12">
            <h1>{{ account.name?.split("@")[0] }}</h1>
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
          <i
            title="create vault"
            class="mdi mdi-plus text-success selectable"
            data-bs-toggle="modal"
            data-bs-target="#create-vault-modal"
          ></i>
        </h3>
      </div>
      <div class="col-12">
        <div class="row ms-4">
          <div class="col-2 my-2" v-for="v in vaults" :key="v.id">
            <Vault :vault="v" />
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <div class="col-12">
        <h3>
          Keeps
          <i
            data-bs-toggle="modal"
            data-bs-target="#create-keep-modal"
            title="create vault"
            class="mdi mdi-plus text-success selectable"
          ></i>
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
  <CreateVaultModal />
  <KeepModal />
  <CreateKeepModal />
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
    onMounted(async () => {
      await vaultsService.getUserVaults()
      await keepsService.getMyKeeps()
    })
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.userVaults),
      vaultKeeps: computed(() => AppState.vaultKeeps)
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
