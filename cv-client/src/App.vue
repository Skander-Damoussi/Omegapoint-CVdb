<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link> |
      <router-link to="/edituser">Edit User</router-link>
    </div>
    <router-view />
  </div>
</template>

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

#nav {
  padding: 30px;
  background-color: #006166;
  border-radius: 4px;
}

#nav a {
  font-weight: bold;
  color: #f4f5f7;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
