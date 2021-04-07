<template>
  <div v-if="loggedIn" id="nav">
    <div class="row">
      <div class="left">
        <h2>{{ user.role }}</h2>
        <!-- This becomes a store binding @ user.role -->
      </div>
      <div class="right">
        <div class="box">
          <div class="row">
            <h2>Hej! {{ user.firstName }}</h2>
            <!-- This "Sven" becomes a store binding @ user.name -->
            <button id="logout" v-on:click="signOut">Logout</button>
            <!-- Needs icon instead, works for now -->
          </div>
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
      await this.$store.dispatch("logOut");
      if (
        !this.$store.getters.getLoggedIn &&
        this.$router.currentRoute.path !== "/"
      )
        this.$router.push("/");
    }
  },
  computed: {
    loggedIn() {
      return this.$store.getters.getLoggedIn;
    },
    user() {
      return this.$store.getters.getLoggedInUser;
    }
  }
};
</script>

<style scoped>
#nav {
  padding: 15px;
  padding-left: 60px;
  background-color: #006166;
  color: white;
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
  align-items: center;
}

.left {
  flex: 40%;
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

#logout {
  margin-left: 35px;
}

h2 {
  font-size: calc(14px + (18 - 14) * ((100vw - 300px) / (1600 - 300)));
}
</style>
