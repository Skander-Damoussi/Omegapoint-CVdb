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
          <span class="error" v-if="errorLogin">
            Fel email/lösenord
          </span>
          <span class="error" v-if="errorVerify">
            Du har inte verifierad ditt konto än.<br />
            Vill du ha ny verifierings länk
            <a href="#" @click="newConfirmationLink">klicka här</a>
          </span>
          <span class="error" v-if="errorInactive">
            Ditt konto är inaktiverat kontakta OmegaPoint.
          </span>
          <span v-if="confirmSuccess">
            Ny verifierings länk har skickat till email.
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
      errorLogin: false,
      errorVerify: false,
      errorInactive: false,
      confirmSuccess: false,
    };
  },
  validations: {
    email: {
      email,
      required,
    },
    password: {
      required,
    },
  },
  components: {},
  methods: {
    async newConfirmationLink() {
      const email = this.email;
      await this.$store.dispatch("newConfirmationLink", email);
      var status = this.$store.getters.getStatus;
      if (status.status == 200) {
        this.errorVerify = false;
        this.confirmSuccess = true;
      }
    },
    async login() {
      this.$v.$touch();
      this.checkForm();
      if (this.$v.$invalid && !this.$store.getters.getLoggedIn) {
        this.errorLogin = false;
        this.errorVerify = false;
        return;
      }
      await this.$store.dispatch("login", {
        email: this.email,
        password: this.password,
      });
      if (this.$store.getters.getLoggedIn == false) {
        var status = this.$store.getters.getStatus;
        if (status.status == 401) {
          if (status.data == "Wrong login") {
            this.errorVerify = false;
            this.errorInactive = false;
            this.confirmSuccess = false;
            this.errorLogin = true;
          }
          if (status.data == "Inactive account") {
            this.errorVerify = false;
            this.errorLogin = false;
            this.confirmSuccess = false;
            this.errorInactive = true;
          }
        }
        if (status.status == 403) {
          this.errorLogin = false;
          this.confirmSuccess = false;
          this.errorInactive = false;
          this.errorVerify = true;
        }
      } else {
        this.navigateTo();
      }
    },
    navigateTo() {
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
    },
    checkForm() {
      this.emailField = this.$v.email.required;
      this.passwordField = this.$v.password.required;
    }
  },
  created() {
    if (this.$store.getters.getLoggedIn) {
      this.navigateTo();
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
