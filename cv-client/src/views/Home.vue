<template>
  <div class="row">
    <div class="side">
      <h1>Omegapoint.</h1>
    </div>
    <div class="login">
      <form @submit.prevent="login" id="loginForm">
        <div class="chunk">
          <label for="email">Användarnamn</label>
          <input
            v-model.trim="$v.email.$model"
            type="text"
            placeholder="demo@omegapoint.se"
            name="email"
            required
          />
          <span class="error" v-if="!emailField">
            Ange Email
          </span>
          <span class="error" v-if="!$v.email.email">
            Ange en gilltigt email
          </span>
        </div>
        <div class="chunk">
          <label for="password">Lösenord</label>

          <input
            v-model.trim="$v.password.$model"
            type="password"
            placeholder="*********"
            name="password"
            required
          />
          <span class="error" v-if="!passwordField">
            Ange lösenord
          </span>
        </div>
        <div class="formRow">
          <input
            id="loginButton"
            type="submit"
            value="Logga in"
            @click.prevent="login"
          />
        </div>
        <span class="error" v-if="errorLogin">
          Fel email/lösenord
        </span>
      </form>
    </div>
  </div>
</template>

<script>
import { required, email } from "vuelidate/lib/validators";

export default {
  name: "Home",
  data() {
    return {
      email: "",
      password: "",
      emailField: true,
      passwordField: true,
      errorLogin: false
    };
  },
  validations: {
    email: {
      email,
      required
    },
    password: {
      required
    }
  },
  components: {},
  methods: {
    async login() {
      this.$v.$touch();
      this.checkForm();
      if (this.$v.$invalid && !this.$store.getters.getLoggedIn) {
        return;
      }
      await this.$store.dispatch("login", {
        email: this.email,
        password: this.password
      });
      if (this.$store.getters.getLoggedIn == false) {
        this.errorLogin = true;
      } else {
        var sUser = this.$store.getters.getLoggedInUser;
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
    },
    checkForm() {
      this.emailField = this.$v.email.required;
      this.passwordField = this.$v.password.required;
    }
  },
  created() {
    if (this.$store.getters.getLoggedIn) {
      this.login();
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
.chunk {
  margin-bottom: 1em;
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
  margin-bottom: 1em;
}

#loginButton:hover {
  background-color: white; /* Green */
  color: black;
  border: 2px solid #2185d0;
}

.error {
  font-size: 12px;
}
</style>
