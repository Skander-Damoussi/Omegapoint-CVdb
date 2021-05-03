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
            <th>Förnamn</th>
            <th>Efternamn</th>
            <th>Email</th>
            <th></th>
          </tr>
          <tr v-for="(i, index) in searchList" :key="index">
            <th>{{ i.firstName }}</th>
            <th>{{ i.lastName }}</th>
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
      searchString: ""
    };
  },
  computed: {
    searchList() {
      return this.$store.getters.getConsultantList;
    }
  },
  methods: {
    disableUser() {
      console.log("disable");
    },
    search() {
      console.log("search");
      if (this.searchString.length > 0) {
        console.log(this.searchString);
        this.$store.dispatch("searchConsultant", this.searchString);
      }
    },
    resetSearch() {
      console.log("reset");
      this.searchString = "";
      this.searchList = [];
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
  margin-top: 1vh;
  margin-bottom: 5vh;
}

.main {
  width: 40vw;
}

.search {
  display: flex;
  flex-direction: column;
  align-self: center;
  border: 1px solid rgb(173, 173, 173);
  padding: 5vw;
  margin-top: 5vh;
  margin-right: 5vw;
}

.searchBox {
  display: flex;
  border: solid 1px black;
  -moz-border-radius: 15px;
  -webkit-border-radius: 15px;
  border-radius: 15px;
  font-size: 20px;
  padding: 0.5%;
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
  overflow-y: scroll;
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
  padding-top: 2vh;
  padding-bottom: 1vh;
}

::-webkit-scrollbar {
  display: none;
}
</style>
