<template>
  <div id="mainDiv">
    <main class="formContain">
      <section>
        <label for="cPassword">Nuvarande lösenord</label>
        <input
          id="cPassword"
          v-model="$v.formResponses.currentPassword.$model"
          type="password"
        />
        <p v-if="errors" class="error">
          <span v-if="!$v.formResponses.currentPassword.required"
            >Skriv in ditt nuvarande lösenord.</span
          >
        </p>
      </section>
      <section>
        <label for="newPassword">Nytt lösenord</label>
        <input
          id="newPassword"
          v-model="$v.formResponses.password.$model"
          type="password"
        />
        <p v-if="errors" class="error">
          <span v-if="!$v.formResponses.password.required"
            >Skriv in nytt lösenord.</span
          >
          <span v-if="!$v.formResponses.password.minLength"
            >Lösenordet måste bestå av minst sex tecken.</span
          >
        </p>
      </section>
      <section>
        <label for="confirmPassword">Bekräfta nytt lösenord</label>
        <input
          id="confirmPassword"
          v-model="$v.formResponses.confirmPassword.$model"
          type="password"
        />
        <p v-if="errors" class="error">
          <span v-if="!$v.formResponses.confirmPassword.required"
            >Bekräfta nytt lösenord.</span
          >
          <span v-if="!$v.formResponses.confirmPassword.sameAsPassword"
            >Lösenordsfälten matchar inte.</span
          >
        </p>
      </section>
      <section>
        <button @click.prevent="submitForm" class="submit bttn">Ändra</button>
        <!-- <p v-if="errors" class="error">
          The form above has errors, <br />please get your act together and
          resubmit
        </p> -->
        <p v-if="errors || empty && state === 'submit clicked'" class="error">
          Fyll i fälten för att byta lösenord.
        </p>
      </section>
    </main>
  </div>
</template>

<script>
import { required, minLength, sameAs } from "vuelidate/lib/validators";

export default {
  data() {
    return {
      state: "submit not clicked",
      errors: false,
      empty: true,
      formResponses: {
        currentPassword: null,
        password: null,
        confirmPassword: null
      }
    };
  },
  validations: {
    formResponses: {
      currentPassword: {
        required
      },
      password: {
        required,
        minLength: minLength(6)
      },
      confirmPassword: {
        required,
        sameAsPassword: sameAs("password")
      }
    }
  },
  methods: {
    submitForm() {
      this.empty = !this.$v.formResponses.$anyDirty;
      this.errors = this.$v.formResponses.$anyError;
      this.state = "submit clicked";
      if (this.errors === false && this.empty === false) {
        //this is where you send the responses
        console.log("current", this.formResponses.currentPassword);
        console.log("password", this.formResponses.password);
        console.log("confirm", this.formResponses.confirmPassword);
        this.state = "form submitted";
      }
    }
  }
};
</script>

<style scoped>
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

.mainDiv {
  display: flex;
  flex-direction: column;
}

section {
  text-align: left;
}

section > label {
  font-weight: bold;
}

section > input {
  height: 3vh;
  margin-bottom: 1vh;
  width: 100%;
  padding: 0.5vw;
}

.error {
  color: red;
}

section > button {
  width: 5vw;
  padding: 1.5%;
  color: white;
  background-color: #2185d0;
  border: none;
}

/* .formContain section {
  padding-bottom: 1vh;
  position: relative;
} */
</style>
