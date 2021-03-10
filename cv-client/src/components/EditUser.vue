<template>
  <div class="edit-user">
    <div>
      <h1>Användaruppgifter</h1>
    </div>
    <div class="user-form">
      <label for="fName">Förnamn</label>
      <input type="text" id="fName" />
      <label for="lName">Efternamn</label>
      <input type="text" id="lName" />
      <label for="password">Lösenord</label>
      <input type="text" id="password" />
      <label for="confirmPassword">Bekräfta lösenord</label>
      <input type="text" id="confirmPassword" />
    </div>
  </div>
</template>

<script>
export default {
  name: "EditUser"
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
  justify-content: center;
  width: 70vw;
  height: 70vh;
}

.edit-user > div > h1 {
  margin-bottom: 5%;
}

.user-form {
  display: flex;
  flex-direction: column;
}
</style>
