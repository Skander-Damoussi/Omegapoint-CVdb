<template>
  <div class="mainDiv">
    <h3>Registrera ny användare</h3>
    <div class="formDiv">
      <form>
        <label for="fname">Förnamn</label><br />
        <input type="text" id="fname" name="fname" class="input" /><br /><br />
        <label for="lname">Efternamn</label><br />
        <input type="text" id="lname" name="lname" /><br /><br />
        <label for="lname">E-post</label><br />
        <input type="text" id="email" name="email" /><br /><br />
        <label for="lname">Roll</label><br />
        <select id="animal-select">
          <option value="Konsult" selected>Konsult</option>
          <option value="Admin">Admin</option>
          <option value="Konsultchef">Konsultchef</option>
        </select>
        <br /><br /><label for="lname">Lösenord</label><br />
        <input type="text" id="email" name="email" /><br /><br />
        <input type="submit" value="Registrera" class="button" />
      </form>
    </div>
    <div></div>
    <div></div>
    <div></div>
    <div></div>
  </div>
</template>

<script>
export default {
  name: "RegisterUser"
};
</script>

<style>
.formDiv {
  margin-left: 2%;
}
form {
  text-align: left;
  width: 100%;
}
.button {
  background-color: rgb(51, 153, 255);
  border-radius: 4px;
  color: white;
  font-size: 16px;
  padding: 10px;
  border: none;
}
input {
  padding: 0.5em 1em;
  border-radius: 4px;
  margin: 0 auto;
  width: 60%;
}
select {
  flex: 1 0 auto;
  padding: 0.5em 1em;
  border-radius: 4px;
}
.mainDiv {
  border: solid 1px black;
  height: 90vh;
  margin: 0px 2%;
}
label {
  text-align: left;
}
</style>
