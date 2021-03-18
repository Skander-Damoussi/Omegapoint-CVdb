<template>
  <div class="edit-user">
    <div class="user-form">
      <h1>Användaruppgifter</h1>
      <div class="section">
        <label for="fName">Förnamn</label>
        <input type="text" v-model="getUser" id="fName" />
      </div>
      <div class="section">
        <label for="lName">Efternamn</label>
        <input type="text" v-model="getUser" id="lName" />
      </div>
      <div class="section">
        <label for="password">Lösenord</label>
        <input type="text" placeholder="******" required id="password" />
      </div>
      <div class="section">
        <label for="confirmPassword">Bekräfta lösenord</label>
        <input type="text" placeholder="******" required id="confirmPassword" />
      </div>
      <div class="submit">
        <input
          type="button"
          class="bttn"
          v-on:click="updateUser(getUser)"
          value="Spara"
        />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditUser",
  computed: {
    getUser() {
      return this.$store.getters.getUser;
    },
  },
  methods: {
    mounted() {
      this.store.dispatch("getUser");
    },
    updateUser(getUser) {
      console.log("update user")
      this.store.dispatch("updateUser", getUser);
    },
  },
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
