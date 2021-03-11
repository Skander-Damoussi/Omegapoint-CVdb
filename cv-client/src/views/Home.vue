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
          placeholder="demo@omegapoint.se"
          name="email"
          required
        />

        <label for="password">Lösenord</label>
        <input
          v-model="password"
          type="password"
          placeholder="*********"
          name="password"
          required
        />

        <div class="formRow">
          <input id="loginButton" type="submit" value="Logga in" />
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
  height: calc(100vh)
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

#loginButton{
  color: white;
  background: #2185D0;
  border: none;
  text-decoration: none;
  border-radius: 4px;
  transition-duration: 0.4s;
  border: 2px solid #2185D0;
}

#loginButton:hover {
  background-color: white; /* Green */
  color: black;
  border: 2px solid #2185D0;
}

</style>