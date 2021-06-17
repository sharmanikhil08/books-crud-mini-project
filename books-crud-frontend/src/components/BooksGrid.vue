<template>
  <div class="books-table">
    <b-button v-b-modal.add-modal variant="success" class="add-button"
      >Add new book</b-button
    >
    <b-container>
      <b-modal
        id="add-modal"
        size="sm"
        title="Add new book"
        centered
        hide-footer
      >
        <add-book @refreshGrid="retrieveBooks()"></add-book>
      </b-modal>
    </b-container>
    <b-table
      ref="books-grid"
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

      <template #cell(isAvailable)="row">
        {{ row.item.isAvailable ? "Yes" : "No" }}
      </template>

      <template #cell(actions)="row">
        <b-button size="sm" @click="edit(row.item)" class="mr-1">
          Edit
        </b-button>
        <b-button
          size="sm"
          @click="remove(row.item)"
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
    <!-- Edit modal -->
    <b-modal id="edit-modal" size="sm" title="Edit Book" centered hide-footer>
      <edit-book :form="selectedItem"></edit-book>
    </b-modal>
    <!-- Delete modal -->
    <b-modal
      id="delete-modal"
      size="sm"
      title="Delete Book"
      centered
      hide-footer
    >
      <p>
        Are you sure you want to delete this book: <br />
        <span style="color: red">{{ selectedItem.title }}</span
        >?
      </p>
      <b-button
        size="md"
        variant="danger"
        class="mr-3"
        @click="confirmDelete()"
      >
        Delete
      </b-button>
      <b-button
        size="md"
        class="mr-3"
        style="float: right"
        @click="$bvModal.hide('delete-modal')"
      >
        Cancel
      </b-button>
    </b-modal>
    <!-- Toast for success notification -->
    <b-toast
      refs="toast"
      id="notification-toast"
      title="Book Deleted"
      :visible="showToast"
    >
      Book was successfully deleted!
    </b-toast>
  </div>
</template>

<script>
import AddBook from "@/components/AddBook.vue";
import EditBook from "@/components/EditBook.vue";
import BookDataService from "../services/BookDataService";
export default {
  components: { "edit-book": EditBook, "add-book": AddBook },
  mounted() {
    // Get all books
    this.retrieveBooks();
    // On add book, refresh list
    this.$root.$on("bv::modal::hide", () => {
      this.retrieveBooks();
    });
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
        {
          key: "isAvailable",
          label: "Available?",
          sortable: true,
          sortDirection: "desc",
        },
        { key: "actions", label: "Actions" },
      ],
      items: [],
      currentPage: 1,
      perPage: 7,
      totalRows: 1,
      selectedItem: {},
      showToast: false,
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
    // show edit modal
    edit(item) {
      this.selectedItem = item;
      this.$root.$emit("bv::show::modal", "edit-modal");
    },
    // show delete modal
    remove(item) {
      this.selectedItem = item;
      this.$root.$emit("bv::show::modal", "delete-modal");
    },
    // Delete via API call
    confirmDelete() {
      BookDataService.delete(this.selectedItem.id)
        .then((response) => {
          if (response.status) {
            // Notify
            this.showToast = true;
            // Close modal
            this.$root.$emit("bv::hide::modal", "delete-modal");
            // Refresh list
            this.retrieveBooks();
          }
        })
        .catch((e) => {
          console.log(e);
        });
    },
  },
};
</script>

<style>
.add-button {
  float: left;
}
.books-table {
  margin-top: 50px;
}
</style>
