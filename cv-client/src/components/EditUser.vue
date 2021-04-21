<template>
  <div class="edit-user">
    <div class="edit-name">
      <h3>Ändra namn</h3>
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

      <div class="section">
        <button class="submit bttn" v-on:click="updateUser()">Ändra</button>
      </div>
      <div class="edit-password">
        <h3>Ändra lösenord</h3>
        <EditPassword1 />
      </div>
    </div>

  </div>
</template>

<script>
//import { requierd, sameAs, minLength } from "vuelidate/lib/validators";
import EditPassword1 from "./EditPassword1.vue";
export default {
  name: "EditUser",
  components: {
    EditPassword1
  },
  // data() {
  //   return {
  //     currentPassword: "",
  //     password: "",
  //     confirmPassword: "",
  //     matchPasswords: true,
  //     checkPasswordLength: true,
  //     valid: false
  //   };
  // },
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
      // if (this.password != null) {
      //   this.checkForm();
      // } else {
      //   this.valid = true;
      // }
      var updateUser = {
        Id: this.user.id,
        FirstName: this.user.firstName,
        LastName: this.user.lastName
        //CurrentPassword: this.user.currentPassword
      };
      // console.log("update.this", updateUser);
      // if (this.valid && this.password != null) {
      //   updateUser.NewPassword = this.password;
      //   this.$store.dispatch("updateUser", updateUser);
      // } else {
      //   this.$store.dispatch("updateUser", updateUser);
      // }
      this.$store.dispatch("updateUser", updateUser);
    }
    // checkForm() {
    //   if (this.password.length > 1 && this.password.length < 6) {
    //     this.checkPasswordLength = false;
    //   } else {
    //     this.checkPasswordLength = true;
    //   }
    //   if (this.password != this.confirmPassword) {
    //     this.matchPasswords = false;
    //     console.log("matchPasswords", this.matchPasswords);
    //   } else {
    //     this.matchPasswords = true;
    //   }
    //   if (this.checkPasswordLength == true && this.matchPasswords == true) {
    //     this.valid = true;
    //   }
    //}
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

h3 {
  text-align: left;
  margin-bottom: 2vh;
}

.edit-name {
  display: flex;
  flex-direction: column;
  align-self: center;
  border: 1px solid rgb(173, 173, 173);
  padding: 5vw;
  margin-top: 5vh;
}

.section > input {
  height: 3vh;
  margin-bottom: 1vh;
  width: 100%;
  padding: 0.5vw;
}

.section {
  display: flex;
  flex-wrap: wrap;
  justify-content: left;
}

.section > label {
  font-weight: bold;
}

.section > button {
  width: 5vw;
  padding: 1.5%;
  color: white;
  background-color: #2185d0;
  border: none;
}
</style>
