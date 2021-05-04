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
          v-on:input="search()"
          @keyup.enter="search()"
          placeholder="Sök"
        />
        <p v-on:click="resetSearch()"><i class="fas fa-times"></i></p>
      </div>
      <div class="table">
        <table>
          <tr>
            <th>Namn</th>
            <th>Email</th>
            <th></th>
          </tr>
          <tr v-for="(i, index) in displayList" :key="index">
            <th>{{ i.firstName }} {{ i.lastName }}</th>
            <th>{{ i.email }}</th>
            <th>
              <p v-on:click="disableUser()"><i class="fas fa-times"></i></p>
            </th>
          </tr>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "DisableUser",
  data() {
    return {
      searchString: "",
      displayList: []
    };
  },
  //   computed: {
  //     searchList() {
  //       return this.$store.getters.getSearchList;
  //     }
  //   },
  methods: {
    disableUser() {
      console.log("disable");
    },
    search() {
      console.log("search");
      if (this.searchString.length > 0) {
        console.log(this.searchString);
        this.$store.dispatch("searchConsultant", this.searchString);
        this.displayList = this.$store.getters.getSearchList;
      }
    },
    resetSearch() {
      console.log("reset");
      this.searchString = "";
      this.displayList = [];
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
  margin-top: 5vh;
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
</style>
