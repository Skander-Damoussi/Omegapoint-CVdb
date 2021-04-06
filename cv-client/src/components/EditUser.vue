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
      <!-- <div class="section">
        <label for="password">Lösenord</label>
        <input type="text" v-model="user.password" required id="password" />
      </div>
      <div class="section">
        <label for="confirmPassword">Bekräfta lösenord</label>
        <input type="text" required id="confirmPassword" />
      </div> -->
      <div>
        <label for="password">Lösenord</label>
        <input
          type="password"
          id="password"
          v-model="user.password"
          v-validate="'required|min:6|max:35|confirmed'"
          name="password"
          ref="password"
        />
      </div>
      <div>
        <span>{{ errors.first("password") }}</span>
      </div>
      <div>
        <label for="confirmPassword">Bekräfta lösenord</label>
        <input
          type="password"
          id="confirmPassword"
          v-model="confirmation"
          v-validate="'required|confirmed:password'"
          name="confirmPassword"
        />
        <!-- </div>
      ERRORS
      <div class="alert alert-danger" v-show="errors.any()">
        <div v-if="errors.has('password')">
          {{ errors.first("password") }}
        </div>
        <div v-if="errors.has('confirmPassword')">
          {{ errors.first("confirmPassword") }}
        </div> -->
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
      return this.$store.getters.getUser;
    }
  },
  mounted() {
    this.$store.dispatch("getUser");
    console.log("user", this.user.firstName);
  },
  methods: {
    updateUser(user) {
      console.log("update user");
      /*string field,string finthis, update */
      this.$store.dispatch("updateUser", user);
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
