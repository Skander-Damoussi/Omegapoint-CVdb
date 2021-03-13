<template>
  <div id="app">
    <Navbar/>
    <router-view />
    <div>
      <router-link to="/">Home</router-link> |
      <router-link to="/consultant">Consultant</router-link>|
      <router-link to="/edituser">Edit User</router-link>|
      <router-link to="/admin">Admin</router-link>
    </div>
  </div>
</template>

<script>
import Navbar from "./components/Navbar";

export default {
  name: "App",
   components:{
     Navbar
   }
};
</script>

<style>
body {
  margin: 0px;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
