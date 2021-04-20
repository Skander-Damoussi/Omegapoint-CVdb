<template>
  <div class="edit-user">
    <div class="edit-name">
      <h1>Användaruppgifter</h1>
      <div class="section">
        <label for="firstName">Förnamn</label>
        <input type="text" v-model="user.firstName" id="firstName" />
      </div>
      <div class="section">
        <label for="lastName">Efternamn</label>
        <input type="text" v-model="user.lastName" id="lastName" />
      </div>
      <!-- <form id="password-form">
        <div class="section">
          <label for="currentPassword">Nuvarande lösenord</label>
          <input
            type="password"
            id="currentPassword"
            v-model="currentPassword"
            name="currentPassword"
            ref="currentPassword"
          />
        </div>
        <div class="section">
          <label for="password">Nytt lösenord</label>
          <input
            type="password"
            id="password"
            v-model="password"
            name="password"
            ref="password"
          />
          <div class="error" v-if="!checkPasswordLength">
            Lösenordet måste bestå av minst sex tecken varav en stor bokstav.
          </div>
        </div>
        <div class="section">
          <label for="confirmPassword">Bekräfta nytt lösenord</label>
          <input
            type="password"
            id="confirmPassword"
            v-model="confirmPassword"
            name="confirmPassword"
          />
          <div class="error" v-if="!matchPasswords">
            Lösenordsfälten stämmer inte.
          </div>
        </div>
      </form> -->

      <div class="submit">
        <input
          type="button"
          class="bttn"
          v-on:click="updateUser()"
          value="Spara"
        />
      </div>
    </div>
    <div class="edit-password">
      <h1>Ändra lösenord</h1>
      <EditPassword />
    </div>
  </div>
</template>

<script>
//import { requierd, sameAs, minLength } from "vuelidate/lib/validators";
import EditPassword from "./EditPassword.vue";

export default {
  name: "EditUser",
  components: {
    EditPassword
  },
  data() {
    return {
      currentPassword: "",
      password: "",
      confirmPassword: "",
      matchPasswords: true,
      checkPasswordLength: true,
      valid: false
    };
  },
  // validations: {
  //   currentPassword: {
  //     requierd
  //   },
  //   password: {
  //     minLength: minLength(6)
  //   },
  //   confirmPassword: {
  //     minLength: minLength(6),
  //     sameAsPassword: sameAs(this.password)
  //   }
  // },
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
      if (this.password != null) {
        this.checkForm();
      } else {
        this.valid = true;
      }
      var updateUser = {
        Id: this.user.id,
        FirstName: this.user.firstName,
        LastName: this.user.lastName,
        CurrentPassword: this.user.currentPassword
      };
      console.log("update.this", updateUser);
      if (this.valid && this.password != null) {
        updateUser.NewPassword = this.password;
        this.$store.dispatch("updateUser", updateUser);
      } else {
        this.$store.dispatch("updateUser", updateUser);
      }
    },
    checkForm() {
      if (this.password.length > 1 && this.password.length < 6) {
        this.checkPasswordLength = false;
      } else {
        this.checkPasswordLength = true;
      }
      if (this.password != this.confirmPassword) {
        this.matchPasswords = false;
        console.log("matchPasswords", this.matchPasswords);
      } else {
        this.matchPasswords = true;
      }
      if (this.checkPasswordLength == true && this.matchPasswords == true) {
        this.valid = true;
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
}

.edit-user > h1 {
  font-size: 2vw;
}

.edit-name {
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
