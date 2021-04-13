<template>
  <div class="mainDiv">
    <h3>Registrera ny användare</h3>
    <div class="formDiv">
      <form>
        <div>
          <label for="fname">Förnamn</label><br />
          <input
            type="text"
            id="fname"
            name="fname"
            class="input"
            v-model.trim="$v.firstName.$model"
          />
        </div>
        <div class="error" v-if="!nameField">
          Fält måste vara ifyllt
        </div>
        <br />

        <div>
          <label for="lname">Efternamn</label><br />
          <input
            type="text"
            id="lname"
            name="lname"
            v-model.trim="$v.lastName.$model"
          />
        </div>
        <div class="error" v-if="!lastNameField">
          Fält måste vara ifyllt
        </div>
        <br />

        <div>
          <label for="email">E-post</label><br />
          <input
            type="email"
            id="email"
            name="email"
            v-model.trim="$v.email.$model"
          />
        </div>
        <div class="error" v-if="!emailField">
          Fält måste vara ifyllt
        </div>
        <div class="error" v-if="!$v.email.email">
          Ange ett gilltigt email
        </div>
        <br />

        <div>
          <label for="lname">Roll</label><br />
          <select id="role-select" v-model.trim="$v.role.$model">
            <option value="Konsult" selected>Konsult</option>
            <option value="Admin">Admin</option>
            <option value="Konsultchef">Konsultchef</option>
          </select>
        </div>
        <div class="error" v-if="!roleField">
          Du måste ange en roll
        </div>
        <br />

        <div
          class="form-group"
          :class="{ 'form-group--error': $v.password.$error }"
        >
          <label for="lname">Lösenord</label><br />
          <input
            type="password"
            id="email"
            name="email"
            v-model.trim="$v.password.$model"
          />
          <div class="error" v-if="!passwordField">
            Password is required.
          </div>
          <div class="error" v-if="!$v.password.minLength">
            Password must have at least
            {{ $v.password.$params.minLength.min }} letters.
          </div>
        </div>
        <br />

        <div
          class="form-group"
          :class="{ 'form-group--error': $v.repeatPassword.$error }"
        >
          <label class="form__label">Bekräfta lösenord</label>
          <input
            class="form__input"
            type="password"
            v-model.trim="$v.repeatPassword.$model"
          />
        </div>
        <div class="error" v-if="!$v.repeatPassword.sameAsPassword">
          Passwords must be identical.
        </div>
        <tree-view
          :data="$v"
          :options="{ rootObjectKey: '$v', maxDepth: 2 }"
        ></tree-view>
        <br />
        <div class="buttonDiv">
          <input
            type="button"
            value="Registrera"
            class="button"
            @click="register"
          />
        </div>
        <div>
          <p class="typo__p" v-if="submitStatus === 'OK'">
            Användaren har registrerats!
          </p>
          <p class="typo__p" v-if="submitStatus === 'ERROR'">
            Fyll i formuläret korrekt.
          </p>
          <p class="typo__p" v-if="submitStatus === 'PENDING'">
            Registrerar...
          </p>
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
import { required, sameAs, minLength, email } from "vuelidate/lib/validators";

export default {
  name: "RegisterUser",
  data() {
    return {
      repeatPassword: "",
      password: "",
      firstName: "",
      lastName: "",
      email: "",
      role: "",
      submitStatus: null,
      nameField: true,
      lastNameField: true,
      roleField: true,
      emailField: true,
      passwordField: true
    };
  },
  validations: {
    password: {
      required,
      minLength: minLength(6)
    },
    repeatPassword: {
      sameAsPassword: sameAs("password")
    },
    firstName: {
      required
    },
    lastName: {
      required
    },
    email: {
      email,
      required
    },
    role: {
      required
    }
  },
  methods: {
    async register() {
      this.$v.$touch();
      this.checkForm();

      if (!this.$v.$invalid) {
        var newUser = {
          FirstName: this.firstName,
          LastName: this.lastName,
          Password: this.password,
          Role: this.role,
          Email: this.email
        };
        console.log(newUser);
        await this.$store.dispatch("registerUser", {
          token: this.$store.getters.getUserToken,
          input: newUser
        });
        this.submitStatus = "PENDING";
        setTimeout(() => {
          this.submitStatus = "OK";
        }, 500);
      } else {
        this.submitStatus = "ERROR";
      }
    },
    checkForm() {
      this.nameField = this.$v.firstName.required;
      this.lastNameField = this.$v.lastName.required;
      this.emailField = this.$v.email.required;
      this.roleField = this.$v.role.required;
      this.passwordField = this.$v.password.required;
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
.error {
  color: red;
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
