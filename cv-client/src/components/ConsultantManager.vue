<template>
  <div class="cons-manager">
    <!-- <md-table md-fixed-header class="md-scrollbar">
      <md-table-row v-for="(i, index) in test" :key="index">
        <md-table-cell md-label="#"> {{ i.index }}</md-table-cell>
        <md-table-cell md-label="Förnamn">{{ i.fName }}</md-table-cell>
        <md-table-cell md-label="Efternamn">{{ i.lName }}</md-table-cell>
      </md-table-row>
    </md-table> -->
    <div class="list-table">
      <div class="search">
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
        </div>
      </div>
      <table>
        <tr>
          <th>#</th>
          <th>Förnamn</th>
          <th>Efternamn</th>
          <th></th>
        </tr>
        <tr v-for="(i, index) in consultantList" :key="index">
          <th>{{ index }}</th>
          <th>{{ i.firstName }}</th>
          <th>{{ i.lastName }}</th>
          <th>
            <p class="icon-click" v-on:click="showCV(i.index)">
              <i class="fas fa-eye"></i>
            </p>
          </th>
        </tr>
      </table>
    </div>
    <div class="reg-user">
      <RegisterUser />
    </div>
  </div>
</template>

<script>
import RegisterUser from "./RegisterUser.vue";
export default {
  name: "ConsultantManager",
  components: {
    RegisterUser
  },
  data() {
    return {
      searchString: ""
      //     test: [
      //       {
      //         index: "0",
      //         fName: "Test",
      //         lName: "Testsson",
      //       },
      //       {
      //         index: "1",
      //         fName: "Test1",
      //         lName: "Testsson1",
      //       },
      //     ],
    };
  },
  computed: {
    consultantList() {
      return this.$store.getters.getConsultantList;
    }
  },
  methods: {
    showCV(index) {
      console.log("click", index);
    },
    search() {
      console.log("search");
      this.$store.dispatch("searchConsultant", this.searchString);
    }
  },
  mounted() {
    this.$store.dispatch("getConsultantList");
  }
};
</script>

<style scoped>
.cons-manager {
  display: flex;
}

.list-table {
  padding: 3%;
  border: solid 1px black;
  height: 87vh;
  margin: 1.3% 5%;
  border-radius: 1%;
  width: 50vw;
}

.search {
  width: 100%;
  display: flex;
  justify-content: flex-end;
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

.list-table > table {
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

.icon-click {
  cursor: pointer;
}

.reg-user {
  width: 25vw;
  margin: 0;
}
</style>
