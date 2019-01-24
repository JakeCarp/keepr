<template>
  <div class="container-fluid">
    <navbar></navbar>
    <div class="row">
      <h1 class="col-12">{{vault.name}}</h1>
      <h2 class="col-12">{{vault.description}}</h2>
    </div>
  </div>
</template>
<script>
  import navbar from '@/Components/Navbar.vue'
  export default {
    name: 'vault',
    mounted() {
      this.$store.dispatch('GetVaultById', this.$route.params.vaultId)
    },
    computed: {
      vault() {
        return this.$store.state.targetVault
      },
    },
    components: {
      navbar,
    }
  }

</script>