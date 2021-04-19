<template>
  <div v-if="loggedIn" id="nav">
    <div class="row">
      <div class="left">
        <h2>{{ user.role }}</h2>
      </div>
      <div class="right">
        <div class="box">
          <div class="row">
            <h2>Hej! {{ user.firstName }}</h2>
            <a id="editUser" v-on:click="editUser">
              <i class="far fa-edit"></i>
            </a>
            <!-- Needs icon instead, works for now -->
            <a @click="signOut" id="logout" title="Logga ut"
              ><i class="fas fa-sign-out-alt"></i
            ></a>
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
    },
    editUser() {
      this.$router.push("/editUser");
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
  padding: 0px;
  font-size: 20px;
  cursor: pointer;
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

#editUser {
  margin-left: 0.5vw;
}

h2 {
  font-size: calc(14px + (18 - 14) * ((100vw - 300px) / (1600 - 300)));
}
</style>
