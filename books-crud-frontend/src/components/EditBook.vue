<template>
  <div>
    <b-container>
      <b-form @submit="onSubmit">
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

        <b-form-group id="input-group-2" label="Your Name:" label-for="input-2">
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
          >Update</b-button
        >
      </b-form>
      <!-- Toast for success notification -->
      <b-toast
        refs="toast"
        id="notification-toast"
        title="Book Updated"
        :visible="showToast"
      >
        Your book was successfully updated!
      </b-toast>
    </b-container>
  </div>
</template>

<script>
import BookDataService from "../services/BookDataService";
export default {
  props: {
    form: {
      type: Object,
    },
  },
  mounted() {
    // set toast visibility off on auto-hide/close
    this.$root.$on("bv::toast::hide", () => {
      this.showToast = false;
    });
  },
  data() {
    return {
      showToast: false,
    };
  },
  methods: {
    onSubmit(event) {
      event.preventDefault();
      this.updateBook();
    },
    updateBook() {
      // Save data and make any format changes if required.
      var data = {
        id: this.form.id,
        title: this.form.title,
        author: this.form.author,
        publicationYear: this.form.publicationYear,
        isAvailable: this.form.isAvailable,
        createdDate: this.form.createdDate,
        updatedDate: this.form.updatedDate,
      };

      // Add new book via service.
      BookDataService.update(data.id, data)
        .then((response) => {
          if (response.status) {
            // Notify
            this.showToast = true;
            // Close modal
            this.$root.$emit("bv::hide::modal", "edit-modal");
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
.submit-button {
  margin: auto;
}
</style>
