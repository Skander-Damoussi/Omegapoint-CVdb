<template>
  <div class="edit-user">
    <div class="user-form">
      <h1>Användaruppgifter</h1>
      <div class="section">
        <label for="firstName">Förnamn</label>
        <input type="text" v-model="user.firstName" id="firstName" />
      </div>
      <div class="section">
        <label for="lastName">Efternamn</label>
        <input type="text" v-model="user.lastName" id="lastName" />
      </div>
      <div class="section">
        <label for="currentPassword">Nuvarande lösenord</label>
        <input
          type="currentPassword"
          id="currentPassword"
          v-model="user.currentPassword"
          name="currentPassword"
          ref="currentPassword"
        />
      </div>
      <div class="section">
        <label for="password">Nytt lösenord</label>
        <input
          type="password"
          id="password"
          v-model="user.newPassword"
          name="password"
          ref="password"
        />
      </div>
      <div class="section">
        <label for="confirmPassword">Bekräfta nytt lösenord</label>
        <input
          type="password"
          id="confirmPassword"
          v-model="confirmation"
          name="confirmPassword"
        />
      </div>
      <div class="submit">
        <input
          type="button"
          class="bttn"
          v-on:click="updateUser(user)"
          value="Spara"
        />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditUser",
  data() {
    return {
      confirmation: ""
    };
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
    updateUser(user) {
      if (user.newPassword != this.confirmation) {
        alert("Lösenordsfälten matchar inte, försök igen.");
      } else {
        console.log("update.this", this.user);
        this.$store.dispatch("updateUser", this.user);
      }
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
  width: 100vw;
  height: 100vh;
}

.user-form > h1 {
  margin-bottom: 20%;
  font-size: 2vw;
}

.user-form {
  display: flex;
  flex-direction: column;
  align-self: center;
  justify-content: left;
  width: 30vw;
  border: 1px solid rgb(173, 173, 173);
  padding: 5vw;
  margin-top: 5%;
}

.section > input {
  height: 3vh;
  margin-bottom: 3%;
  width: 100%;
  padding: 1%;
}

.section {
  display: flex;
  flex-wrap: wrap;
  justify-content: left;
}

.section > label {
  font-weight: bold;
}

.submit {
  display: flex;
  justify-content: flex-end;
}
.submit > .bttn {
  width: 5vw;
  padding: 1.5%;
  color: white;
  background-color: #2185d0;
  border: none;
  margin-top: 10%;
}
</style>
