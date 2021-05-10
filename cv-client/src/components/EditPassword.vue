<template>
  <div id="mainDiv">
    <main class="formContain">
      <h3>Ändra lösenord</h3>
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
        <p v-if="errors || (empty && state === 'submit clicked')" class="error">
          Fyll i fälten för att byta lösenord.
        </p>
      </section>
      <section class="status">
        <p v-if="status === 200">
          Lösenord ändrat.
        </p>
        <p v-if="status === 500">
          Något gick fel, vänligen försök igen eller kontakta ansvarig.
        </p>
        <p v-if="status === 400">
          Vänligen fyll i fält med nytt lösenord om du önskar uppdatera.
        </p>
        <p v-if="status === 403">
          Inkorrekt inmatning av nuvarande lösenord, vänligen försök igen.
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
      },
      status: ""
    };
  },
  computed: {
    user() {
      return this.$store.getters.getLoggedInUser;
    }
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
    async submitForm() {
      this.status = "";
      this.empty = !this.$v.formResponses.$anyDirty;
      this.errors = this.$v.formResponses.$anyError;
      this.state = "submit clicked";
      if (this.errors === false && this.empty === false) {
        var updatePassword = {
          id: this.user.id,
          currentPassword: this.formResponses.currentPassword,
          newPassword: this.formResponses.password
        };
        await this.$store.dispatch("updatePassword", updatePassword);
        this.state = "form submitted";
        this.status = await this.$store.getters.getStatus;
        console.log("passwordstatus", this.status);
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
  margin-bottom: 0.5vh;
  width: 100%;
  padding: 0.5vw;
}

h3 {
  text-align: left;
  margin-bottom: 2vh;
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
  border-radius: 0.5vw;
  margin-bottom: 0.5vh;
}

p {
  font-size: small;
  font-weight: normal;
  margin-bottom: 1vh;
}

.status > p {
  text-align: left;
  font-size: small;
  font-weight: bold;
}
</style>
