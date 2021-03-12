<template>
  <div v-if="loggedIn" id="nav">
    <div class="row">
      <div class="left">
        <h2>Consultant</h2> <!-- This becomes a store binding @ role -->
      </div>

      <div class="right">
        <div class="box"> <!-- This needs to be changed to hamburger menu, works for now -->
          <router-link to="/about">About</router-link>
          <router-link to="/consultant">Consultant</router-link>
          <router-link to="/edituser">Edit User</router-link>
          <router-link to="/admin">Admin</router-link>
          <button v-on:click="signOut">Logout</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Navbar",
  methods: {
        async signOut() {
            await this.$store.dispatch('logOut')
            if (!this.$store.getters.getLoggedIn && this.$router.currentRoute.path !== '/')
                this.$router.push('/')
        }
    },
  computed: {
    loggedIn() {
      return this.$store.getters.getLoggedIn;
    },
  },
};
</script>

<style scoped>
#nav {
  padding: 30px;
  background-color: #006166;
}

#nav a {
  font-weight: bold;
  color: #f4f5f7;
  padding: 15px;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

.row {
  display: flex;
  flex-wrap: wrap;
}

.left {
  flex: 40%;
  color: white;
  display: flex;
  justify-content: left;
  align-items: center;
  background-color: #006166;
}

.right {
  flex: 60%;
  display: flex;
  align-items: center;
}

.box {
  margin-left: auto;
}
</style>