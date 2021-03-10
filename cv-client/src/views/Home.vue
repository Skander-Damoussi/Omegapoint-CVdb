<template>
  <div class="row">
    <div class="side">
      <h1>Omegapoint.</h1>
    </div>
    <div class="login">
      <form id="loginForm">
        <label for="email">Användarnamn</label>
        <input
          v-model="email"
          type="text"
          placeholder="Enter email"
          name="email"
          required
        />

        <label for="password">Lösenord</label>
        <input
          v-model="password"
          type="password"
          placeholder="Enter password"
          name="password"
          required
        />

        <div class="formRow">
          <input type="submit" value="Sign In" />
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "Home",
  components: {},
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
}

.side {
  flex: 70%;
  padding: 20px;
  color: rgb(0, 0, 0);
  display: flex;
  justify-content: center;
  align-items: center;
}

/* Main column */
.login {
  flex: 30%;
  padding: 60px;
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
  width: 100%;
}
.formRow {
  display: flex;
  justify-content: space-between;
}
input {
  padding: 0.5em 1em;
}
input[type="text"],
input[type="password"] {
  margin-bottom: 2em;
}
input[type="submit"],
input[type="reset"] {
  width: 6em;
  flex: 1 0 auto;
  cursor: pointer;
}
h2 {
  margin: 0;
}
hr {
  width: 100%;
  margin-bottom: 2em;
}
a {
  text-align: center;
  margin-top: 0.5em;
}
</style>