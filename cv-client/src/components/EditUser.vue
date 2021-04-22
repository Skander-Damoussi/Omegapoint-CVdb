<template>
  <div class="edit-user">
    <div class="edit-box">
      <h3>Ändra namn</h3>
      <div class="section">
        <label for="firstName">Förnamn</label>
        <input type="text" v-model="user.firstName" id="firstName" />
      </div>
      <div class="section">
        <label for="lastName">Efternamn</label>
        <input type="text" v-model="user.lastName" id="lastName" />
      </div>
      <div class="section">
        <button class="submit bttn" v-on:click="updateUser()">Ändra</button>
      </div>
      <div class="edit-password">
        <EditPassword />
      </div>
    </div>
  </div>
</template>

<script>
import EditPassword from "./EditPassword.vue";

export default {
  name: "EditUser",
  components: {
    EditPassword
  },
  computed: {
    user() {
      return this.$store.getters.getLoggedInUser;
    }
  },
  mounted() {
    //this.$store.dispatch("getLoggedInUser");
    console.log("user", this.user.firstName);
  },
  methods: {
    updateUser() {
      var updateUser = {
        Id: this.user.id,
        FirstName: this.user.firstName,
        LastName: this.user.lastName
      };
      this.$store.dispatch("updateUser", updateUser);
    }
  }
};
</script>

<style>
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}
.edit-user {
  display: flex;
  flex-direction: column;
}

h3 {
  text-align: left;
  margin-bottom: 2vh;
}

.edit-box {
  display: flex;
  flex-direction: column;
  align-self: center;
  border: 1px solid rgb(173, 173, 173);
  padding: 5vw;
  margin-top: 5vh;
}

.edit-password {
  margin-top: 2vh;
}

.section > input {
  height: 3vh;
  margin-bottom: 1vh;
  width: 100%;
  padding: 0.5vw;
}

.section {
  display: flex;
  flex-wrap: wrap;
  justify-content: left;
}

.section > label {
  font-weight: bold;
}

.section > button {
  width: 5vw;
  padding: 1.5%;
  color: white;
  background-color: #2185d0;
  border: none;
}
</style>
