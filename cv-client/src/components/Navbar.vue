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
              <a id="cvEdit" v-if="user.role=='Konsultchef'" v-on:click="cvEdit" title="Ladda upp cv-mall">
              <i class="far fa-file-word"></i>
            </a>
            <!-- Needs icon instead, works for now -->
            <a @click="signOut" id="logout" title="Logga ut"
              ><i class="fas fa-sign-out-alt"></i
            ></a>
          </div>
        </div>
      </div>
    </div>
    <Modal v-show="isSessionModalVisible" @close="closeSessionModal">
      <template v-slot:header>
        <p>Du kommer loggas ut.</p>
      </template>

      <template v-slot:body>
        <p style="color: black;">
          Du har varit inaktiv i 45minuter. Du kommer att loggas ut om
          15minuter.
        </p>
      </template>
    </Modal>
  </div>
</template>

<script>
import store from "../store/store.js";
import router from "../router/index.js";
import Modal from "../components/Modal.vue";

export default {
  name: "Navbar",
  data() {
    return {
      isAdminModalVisible: false,
      isSessionModalVisible: false,
      loggedInUser: false
    };
  },
  async mounted() {
    var timeout;
    var _this = this;
    function refresh() {
      if (_this.isSessionModalVisible) {
        _this.closeSessionModal();
      }
      if (_this.checkStatus()) {
        clearTimeout(timeout);
        timeout = setTimeout(() => {
          _this.closeSessionModal();
          _this.showSessionModal();
          close();
        }, 45 * 60 * 1000);
      }
    }

    function close() {
      if (_this.checkStatus()) {
        clearTimeout(timeout);
        timeout = setTimeout(() => {
          document.removeEventListener("mousemove", refresh);
          _this.closeSessionModal();
          store.dispatch("logOut");
          router.push("/");
        }, 15 * 60 * 1000);
      }
    }
    setInterval(function() {
      document.addEventListener("mousemove", refresh);
    }, 10000);
    refresh();
  },
  methods: {
    showSessionModal() {
      this.isSessionModalVisible = true;
    },
    async closeSessionModal() {
      this.isSessionModalVisible = false;
    },
    checkStatus() {
      this.loggedInUser = this.loggedIn;
      return this.loggedInUser;
    },
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
    },
    cvEdit() {
      this.$router.push("/uploadCvTemp");
    },
  },
  computed: {
    loggedIn() {
      return this.$store.getters.getLoggedIn;
    },
    user() {
      return this.$store.getters.getLoggedInUser;
    },
  },
  components: {
    Modal,
  },
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
#cvEdit{
  margin-left: 0.5vw;
}
</style>
