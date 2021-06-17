<template>
  <div class="home">
    <h1 class="page-header">BOOKS</h1>
    <books-grid></books-grid>
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
@media only screen and (max-width: 360px) {
  .modal {
    max-width: 250px;
  }
}
</style>
