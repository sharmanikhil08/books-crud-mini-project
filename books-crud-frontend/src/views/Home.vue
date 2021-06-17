<template>
  <div class="home">
    <h1 class="page-header">BOOKS</h1>
    <add-book></add-book>
    <books-grid></books-grid>
  </div>
</template>

<script>
// @ is an alias to /src
import BooksGrid from "@/components/BooksGrid.vue";
import AddBook from "@/components/AddBook.vue";

export default {
  name: "Home",
  components: { "books-grid": BooksGrid, "add-book": AddBook },
};
</script>

<style>
.page-header {
  margin-top: 50px;
}

.modal-title {
  margin: auto;
}
</style>
