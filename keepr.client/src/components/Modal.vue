<template>
  <div
    class="modal fade"
    tabindex="-1"
    aria-labelledby="Modal"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-lg modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-body">
          <slot name="modal-body"></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.modal-dialog {
  height: 80% !important;
  padding-top: 10%;
}
</style>