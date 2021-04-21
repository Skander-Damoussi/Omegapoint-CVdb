<template>
  <div class="mainDiv">
    <h3>Ändra lösenord</h3>
    <div class="formDiv">
      <form>
        <!-- <div
          class="form-group"
          :class="{ 'form-group--error': $v.currentPassword.$error }"
        > -->
        <div>
          <label class="form__label">Nuvarande lösenord</label>
          <input
            class="form__input"
            type="password"
            v-model.trim="$v.currentPassword.$model"
          />
        </div>
        <!-- <div class="error" v-if="!$v.currentPasswordField.requierd">
          Skriv in ditt nuvarande lösenord.
        </div>
        <div
          class="form-group"
          :class="{ 'form-group--error': $v.password.$error }"
        > -->
        <div>
          <label for="password">Lösenord</label><br />
          <input
            class="form__input"
            type="password"
            v-model="$v.password.$model"
          />
          <!-- <div class="error" v-if="!passwordField">
            Password is required.
          </div>
          <div class="error" v-if="!$v.password.minLength">
            Password must have at least 6 letters.
          </div> -->
        </div>
        <br />
        <!-- 
        <div
          class="form-group"
          :class="{ 'form-group--error': $v.confirmPassword.$error }"
        > -->
        <div>
          <label class="form__label">Bekräfta lösenord</label>
          <input
            class="form__input"
            type="password"
            v-model="$v.confirmPassword.$model"
          />
        </div>
        <!-- <div class="error" v-if="!$v.confirmPassword.sameAsPassword">
          Passwords must be identical.
        </div> -->
        <tree-view
          :data="$v"
          :options="{ rootObjectKey: '$v', maxDepth: 2 }"
        ></tree-view>
        <br />
        <div class="buttonDiv">
          <input
            type="button"
            value="Ändra lösenord"
            class="button"
            @click="editPassword"
          />
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { requierd, sameAs, minLength } from "vuelidate/lib/validators";

export default {
  name: "EditPassword",
  data() {
    return {
      password: null,
      confirmPassword: "",
      currentPassword: "",
      currentPasswordField: true,
      passwordField: true
    };
  },
  validations: {
    currentPassword: {
      requierd
    },
    password: {
      minLength: minLength(6)
    },
    confirmPassword: {
      sameAsPassword: sameAs("password")
    }
  },
  methods: {
    checkForm() {
      this.currentPasswordField = this.$v.currentPassword.required;
    },
    editPassword() {
      this.$v.$touch();
      this.checkForm();
      console.log("currentpassword", this.currentPassword);
      console.log("password", this.password);
      console.log("confirmpassword", this.confirmPassword);
    }
  }
};
</script>

<style scoped>
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
