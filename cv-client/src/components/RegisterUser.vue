<template>
  <div class="mainDiv">
    <h3>Registrera ny användare</h3>
    <div class="formDiv">
      <form>
        <label for="fname">Förnamn</label><br />
        <input
          type="text"
          id="fname"
          name="fname"
          class="input"
          v-model="newUser.FirstName"
        /><br /><br />
        <label for="lname">Efternamn</label><br />
        <input
          type="text"
          id="lname"
          name="lname"
          v-model="newUser.LastName"
        /><br /><br />
        <label for="lname">E-post</label><br />
        <input
          type="text"
          id="email"
          name="email"
          v-model="newUser.Email"
        /><br /><br />
        <label for="lname">Roll</label><br />
        <select id="animal-select" v-model="newUser.Role">
          <option value="Konsult" selected>Konsult</option>
          <option value="Admin">Admin</option>
          <option value="Konsultchef">Konsultchef</option>
        </select>
        <br /><br /><label for="lname">Lösenord</label><br />
        <input
          type="text"
          id="email"
          name="email"
          v-model="newUser.Password"
        /><br /><br />
        <div class="buttonDiv">
          <input
            type="button"
            value="Registrera"
            class="button"
            @click="register"
          />
        </div>
      </form>
    </div>
    <div></div>
    <div></div>
    <div></div>
    <div></div>
  </div>
</template>

<script>
export default {
  name: "RegisterUser",
  data() {
    return {
      newUser: {
        FirstName: "",
        LastName: "",
        Password: "",
        Role: "",
        Email: ""
      }
    };
  },
  methods: {
    async register() {
      await this.$store.dispatch("registerUser", {
        token: this.$store.getters.getUserToken,
        input: this.newUser
      });
    }
  }
};
</script>

<style>
.formDiv {
  margin: 0px 2%;
}
form {
  text-align: left;
  width: 100%;
}
.button {
  color: white;
  background: #2185d0;
  border: none;
  text-decoration: none;
  border-radius: 4px;
  transition-duration: 0.4s;
  border: 2px solid #2185d0;
}
input {
  padding: 0.5em 1em;
  border-radius: 4px;
  margin: 0 auto;
  width: 100%;
  border: 00.1px solid;
}
select {
  flex: 1 0 auto;
  padding: 0.5em 1em;
  border-radius: 4px;
  width: 100%;
}
.mainDiv {
  border: solid 1px black;
  height: 87vh;
  margin: 0px 2%;
}
label {
  text-align: left;
}
h3 {
  margin: 30px 0px;
}
.buttonDiv {
  display: flex;
  align-content: flex-end;
  text-align: end;
}
</style>
