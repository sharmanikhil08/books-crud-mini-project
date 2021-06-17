<template>
  <div class="home">
    <b-container>
      <h1 class="page-header">BOOKS</h1>
      <books-grid></books-grid>
    </b-container>
  </div>
</template>

<script>
// @ is an alias to /src
import BooksGrid from "@/components/BooksGrid.vue";

export default {
  name: "Home",
  components: { "books-grid": BooksGrid },
};
</script>

<style>
.page-header {
  margin-top: 50px;
}

.modal-title {
  margin: auto;
}

.modal-dialog {
  margin: 0;
}
</style>
