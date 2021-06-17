<template>
  <div>
    <b-container>
      <b-button v-b-modal.modal-center variant="success" class="add-button"
        >Add new book</b-button
      >
      <b-modal id="modal-center" centered title="Add new book"> </b-modal>
    </b-container>
  </div>
</template>

<script>
export default {};
</script>

<style>
.add-button {
  float: left;
}
</style>
