<template>
  <div class="books-table">
    <b-container>
      <b-table striped hover :items="items" :fields="fields">
        <template #row-details="row">
          <b-card>
            <ul>
              <li v-for="(value, key) in row.item" :key="key">
                {{ key }}: {{ value }}
              </li>
            </ul>
          </b-card>
        </template>
      </b-table>
    </b-container>
  </div>
</template>

<script>
export default {
  data() {
    return {
      fields: ["id", "title", "author", "publicationYear"],
      items: [
        {
          id: 1,
          title: "Clean Architecture",
          author: "Bob Martin",
          publicationYear: 1991,
          isAvailable: true,
          createdDate: "2021-06-17T03:37:24.2169507Z",
          updatedDate: "2021-06-17T03:37:24.2170363Z",
        },
        {
          id: 2,
          title: "Clean Architecture 2",
          author: "Bob Martin",
          publicationYear: 1991,
          isAvailable: true,
          createdDate: "2021-06-17T03:37:24.2169507Z",
          updatedDate: "2021-06-17T03:37:24.2170363Z",
        },
        {
          id: 3,
          title: "Clean Architecture 3",
          author: "Bob Martin",
          publicationYear: 1991,
          isAvailable: true,
          createdDate: "2021-06-17T03:37:24.2169507Z",
          updatedDate: "2021-06-17T03:37:24.2170363Z",
        },
      ],
    };
  },
};
</script>

<style>
.books-table {
  margin-top: 50px;
}
</style>
