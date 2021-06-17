<template>
  <div>
    <b-container>
      <b-button v-b-modal.modal-center variant="success" class="add-button"
        >Add new book</b-button
      >
      <b-modal
        ref="modal"
        id="modal-center"
        centered
        title="Add new book"
        hide-footer
      >
        <b-form @submit="onSubmit" v-if="show">
          <b-form-group
            id="input-group-1"
            label="Book Title:"
            label-for="input-1"
          >
            <b-form-input
              id="input-1"
              v-model="form.title"
              placeholder="Enter book title..."
              required
            ></b-form-input>
          </b-form-group>

          <b-form-group
            id="input-group-2"
            label="Your Name:"
            label-for="input-2"
          >
            <b-form-input
              id="input-2"
              v-model="form.author"
              placeholder="Enter Author's name..."
              required
            ></b-form-input>
          </b-form-group>

          <b-form-group
            id="input-group-3"
            label="Publication Year:"
            label-for="input-3"
          >
            <b-form-input
              id="input-3"
              type="number"
              v-model="form.publicationYear"
              placeholder="Enter Published Year..."
              required
            ></b-form-input>
          </b-form-group>

          <b-form-group
            id="input-group-4"
            label="Is Available?"
            label-for="input-4"
          >
            <b-form-checkbox
              id="input-4"
              v-model="form.isAvailable"
              name="check-button"
              switch
            >
            </b-form-checkbox>
          </b-form-group>

          <b-button type="submit" variant="primary" class="mt-3" block
            >Submit</b-button
          >
        </b-form>
      </b-modal>
      <!-- Toast for success notification -->
      <b-toast
        refs="toast"
        id="notification-toast"
        title="Book Added"
        :visible="showToast"
      >
        Your book was successfully added!
      </b-toast>
    </b-container>
  </div>
</template>

<script>
import BookDataService from "../services/BookDataService";
export default {
  mounted() {
    this.$root.$on("bv::toast::hide", () => {
      this.showToast = false;
    });
  },
  data() {
    return {
      form: {
        title: "",
        author: "",
        publicationYear: 0,
        isAvailable: false,
      },
      show: true,
      showToast: false,
    };
  },
  methods: {
    onSubmit(event) {
      event.preventDefault();
      this.saveBook();
    },
    saveBook() {
      // Save data and make any format changes if required.
      var data = {
        title: this.form.title,
        author: this.form.author,
        publicationYear: this.form.publicationYear,
        isAvailable: this.form.isAvailable,
      };

      // Add new book via service.
      BookDataService.create(data)
        .then((response) => {
          if (response.status) {
            // Reset form
            this.Reset();
            // Close modal
            this.$refs["modal"].hide();
            // Notify
            this.showToast = true;
          }
        })
        .catch((e) => {
          console.log(e);
        });
    },
    Reset() {
      // Reset our form values
      this.form.title = "";
      this.form.author = "";
      this.form.publicationYear = null;
      this.form.isAvailable = false;
      // Trick to reset/clear native browser form validation state
      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    },
  },
};
</script>

<style>
.add-button {
  float: left;
}
.submit-button {
  margin: auto;
}
</style>
