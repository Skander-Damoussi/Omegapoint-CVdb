<template>
  <div>
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
        <input list="select" name="select" /><br /><br />
        <datalist class="form-control" id="select">
          <option value="Trans" />
          <option value="Fund" />
          <option value="Insta" />
        </datalist>
        <label for="lname">Lösenord</label><br />
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
.formbar {
  position: relative;
  border-radius: 4px;
}
.formDiv {
  display: flex;
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
  flex: 1 0 auto;
  padding: 0.5em 1em;
  border-radius: 4px;
  width: 80%;
}
</style>
