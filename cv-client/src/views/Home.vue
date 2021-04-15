<template>
  <div class="row">
    <div class="side">
      <h1>Omegapoint.</h1>
    </div>
    <div class="login">
      <form @submit.prevent="login" id="loginForm">
        <label for="email">Användarnamn</label>
        <input
          v-model.trim="$v.email.$model"
          type="text"
          placeholder="demo@omegapoint.se"
          name="email"
          required
        />
        <div class="error" v-if="!emailField">
          Fyll i email fält.
        </div>

        <label for="password">Lösenord</label>
        <input
          v-model.trim="$v.password.$model"
          type="password"
          placeholder="*********"
          name="password"
          required
        />
        <div class="error" v-if="!passwordField">
          Fyll i lösenord fält.
        </div>

        <div class="error" v-if="wrongLogin">
          Email och lösenord felaktigt.
        </div>

        <div class="formRow">
          <input
            id="loginButton"
            type="submit"
            value="Logga in"
            @click.prevent="login"
          />
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { required } from "vuelidate/lib/validators";

export default {
  name: "Home",
  data() {
    return {
      password: "",
      email: "",
      wrongLogin: false,
      emailField: true,
      passwordField: true,
    };
  },
  components: {},
  validations: {
    password: {
      required
    },
    email: {
      required
    }
  },
  methods: {
    async login() {
      this.$v.$touch();
      this.checkForm();

      var userInput = {
        Email: this.email,
        Password: this.password
      };

      if (!this.$v.$invalid) {
        await this.$store.dispatch("login", userInput);
        var sUser = this.$store.getters.getLoggedInUser;
        if (sUser == null) {
          this.wrongLogin = true;
        } else {
          console.log("test")
          this.wrongLogin = false;
          switch (sUser.role) {
            case "Admin":
              this.$router.push("/admin");
              break;
            case "Konsultchef":
              this.$router.push("/consultantmanager");
              break;
            case "Konsult":
              this.$router.push("/consultant");
              break;
            default:
          }
        }
      } else {
        this.wrongLogin = false;
      }
    },
    checkForm() {
      this.emailField = this.$v.email.required;
      this.passwordField = this.$v.password.required;
    }
  }
};
</script>

<style scoped>
* {
  box-sizing: border-box;
}

h1 {
  font-size: calc(40px + (56 - 14) * ((100vw - 300px) / (1600 - 300)));
}

/* Column container */
.row {
  display: flex;
  flex-wrap: wrap;
  height: calc(100vh);
}

.side {
  flex: 65%;
  color: white;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #006166;
}

/* Main column */
.login {
  flex: 35%;
  display: flex;
  justify-content: center;
  align-items: center;
}

/* Responsive layout - when the screen is less than 700px wide, make the two columns stack on top of each other instead of next to each other */
@media screen and (max-width: 700px) {
  .row,
  .navbar {
    flex-direction: column;
  }
}

form {
  display: flex;
  flex-direction: column;
  justify-content: center;
  box-sizing: border-box;
  width: 50%;
}
.formRow {
  display: flex;
  flex-direction: column;
  justify-content: center;
  box-sizing: border-box;
  align-items: center;
}
input {
  padding: 0.5em 1em;
}
input[type="text"],
input[type="password"] {
  /* margin-bottom: 1em; */
}
input[type="submit"],
input[type="reset"] {
  width: 6em;
  flex: 1 0 auto;
  cursor: pointer;
  margin-top: 1em;
}
h2 {
  margin: 0;
}

#loginButton {
  color: white;
  background: #2185d0;
  border: none;
  text-decoration: none;
  border-radius: 4px;
  transition-duration: 0.4s;
  border: 2px solid #2185d0;
}

#loginButton:hover {
  background-color: white; /* Green */
  color: black;
  border: 2px solid #2185d0;
}
</style>
