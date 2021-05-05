<template>
  <div class="main">
    <div class="search">
      <h3>
        Ta bort användare
      </h3>
      <div class="searchBox">
        <i class="fas fa-search"></i>
        <input
          type="text"
          id="searchInput"
          class="searchInput"
          v-model="searchString"
          @keyup.enter="search()"
          placeholder="Sök"
        />
        <p v-on:click="resetSearch()"><i class="fas fa-times"></i></p>
      </div>
      <div class="section">
        <button class="submit bttn" v-on:click="getDeactivatedConsultants()">
          Hämta deaktiverade konsulter
        </button>
      </div>
      <div class="table">
        <table>
          <tr>
            <th>Namn</th>
            <th>Email</th>
            <th></th>
          </tr>
          <tr v-for="(user, index) in displayList" :key="index">
            <th>{{ user.firstName }} {{ user.lastName }}</th>
            <th>{{ user.email }}</th>
            <th>
              <div v-if="icon">
                <p v-on:click="showModal(user.email)">
                  <i class="fas fa-times"></i>
                </p>
              </div>
              <div class="section" v-if="!icon">
                <button class="submit bttn" v-on:click="showModal(user.email)">
                  Återaktivera
                </button>
              </div>
            </th>
          </tr>
        </table>
      </div>
    </div>
    <Modal v-show="isModalVisible" @close="cancel()">
      <template v-slot:header>
        Avaktivering av användare
      </template>

      <template v-slot:body>
        <p>
          Är du säker på att du vill ändra status på denna användaren?
        </p>
        <div class="section">
          <button class="submit bttn cancel" v-on:click="cancel()">
            Avbryt
          </button>
          <button class="submit bttn" v-on:click="updateActiveUser()">
            OK
          </button>
        </div>
      </template>

      <template v-slot:footer> </template>
    </Modal>
  </div>
</template>

<script>
import Modal from "./Modal";

export default {
  name: "DisableUser",
  components: {
    Modal
  },
  data() {
    return {
      searchString: "",
      displayList: [],
      icon: true,
      isModalVisible: false,
      selectedUser: ""
    };
  },
  methods: {
    showModal(user) {
      this.isModalVisible = true;
      this.selectedUser = user;
    },
    cancel() {
      this.selectedUser = "";
      this.isModalVisible = false;
    },
    async updateActiveUser() {
      console.log("selected", this.selectedUser);
      await this.$store.dispatch("updateActiveUser", this.selectedUser);
      this.cancel();
    },
    async search() {
      this.icon = true;
      this.displayList = [];
      if (this.searchString.length > 0) {
        await this.$store.dispatch("searchConsultant", this.searchString);
        this.displayList = this.$store.getters.getSearchList;
      }
    },
    resetSearch() {
      console.log("reset");
      this.searchString = "";
      this.displayList = [];
    },
    async getDeactivatedConsultants() {
      this.icon = false;
      console.log(this.icon);
      await this.$store.dispatch("getDeactivatedConsultants");
      this.displayList = this.$store.getters.getSearchList;
    }
  }
};
</script>

<style scoped>
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}

h3 {
  margin-bottom: 5vh;
}

.main {
  width: 55vw;
  overflow-y: scroll;
  height: 87vh;
  border: 1px solid rgb(173, 173, 173);
  border-radius: 1%;
}

.search {
  display: flex;
  flex-direction: column;
  align-self: center;
  padding: 5vw;
  margin-top: 2vh;
}

.searchBox {
  display: flex;
  border: solid 1px black;
  -moz-border-radius: 15px;
  -webkit-border-radius: 15px;
  border-radius: 15px;
  font-size: 20px;
  padding: 1%;
  outline: 0;
  -webkit-appearance: none;
}

.searchInput {
  width: 100%;
  border: 0;
  outline: none;
}

.table {
  margin-top: 2vh;
}

table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td,
th {
  border-bottom: 1px solid #dddddd;
  text-align: left;
  padding: 2vh;
  padding-bottom: 1vh;
  padding-left: 0;
}

input:-webkit-autofill {
  -webkit-box-shadow: inset 0 0 0px 9999px white;
}

::-webkit-scrollbar {
  display: none;
}

.section > button {
  width: fit-content;
  margin-top: 1vh;
}

.cancel {
    background-color: red;
    margin-right: 1vw;
}
</style>
