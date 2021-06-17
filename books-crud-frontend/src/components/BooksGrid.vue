<template>
  <div class="books-table">
    <b-container>
      <b-table
        striped
        hover
        :items="items"
        :fields="fields"
        :current-page="currentPage"
        :per-page="perPage"
        :responsive="true"
      >
        <template #row-details="row">
          <b-card>
            <ul>
              <li v-for="(value, key) in row.item" :key="key">
                {{ key }}: {{ value }}
              </li>
            </ul>
          </b-card>
        </template>

        <template #cell(actions)="row">
          <b-button
            size="sm"
            @click="info(row.item, row.index, $event.target)"
            class="mr-1"
          >
            Edit
          </b-button>
          <b-button
            size="sm"
            @click="info(row.item, row.index, $event.target)"
            class="mr-1"
            variant="danger"
          >
            Delete
          </b-button>
        </template>
      </b-table>
      <b-pagination
        v-model="currentPage"
        :total-rows="totalRows"
        :per-page="perPage"
        align="fill"
        size="sm"
        class="my-0"
      ></b-pagination>
      <!-- Info modal -->
      <b-modal
        :id="infoModal.id"
        :title="infoModal.title"
        ok-only
        @hide="resetInfoModal"
      >
        <pre>{{ infoModal.content }}</pre>
      </b-modal>
    </b-container>
  </div>
</template>

<script>
import BookDataService from "../services/BookDataService";
export default {
  mounted() {
    // Get all books
    this.retrieveBooks();
  },
  data() {
    return {
      fields: [
        {
          key: "id",
          label: "Id",
          sortable: true,
          sortDirection: "desc",
        },
        {
          key: "title",
          label: "Title",
          sortable: true,
          sortDirection: "desc",
        },
        {
          key: "author",
          label: "Author",
          sortable: true,
          sortDirection: "desc",
        },
        {
          key: "publicationYear",
          label: "Published",
          sortable: true,
          sortDirection: "desc",
        },
        { key: "actions", label: "Actions" },
      ],
      items: [],
      currentPage: 1,
      perPage: 7,
      totalRows: 1,
      infoModal: {
        id: "info-modal",
        title: "",
        content: "",
      },
    };
  },
  methods: {
    retrieveBooks() {
      BookDataService.getAll()
        .then((response) => {
          this.items = response.data;
          // Set the initial number of items for pagination
          this.totalRows = this.items.length;
        })
        .catch((e) => {
          console.log(e);
        });
    },
    info(item, index, button) {
      this.infoModal.title = `Row index: ${index}`;
      this.infoModal.content = JSON.stringify(item, null, 2);
      this.$root.$emit("bv::show::modal", this.infoModal.id, button);
    },
    resetInfoModal() {
      this.infoModal.title = "";
      this.infoModal.content = "";
    },
  },
};
</script>

<style>
.books-table {
  margin-top: 50px;
}
</style>
