<template>
  <div class="cons-manager">
    <div class="list-table">
      <div class="search">
        <div class="searchBox">
          <i class="fas fa-search"></i>
          <input
            type="text"
            id="searchInput"
            class="searchInput"
            v-model="searchString"
            @change="search()"
            @keyup.enter="search()"
            placeholder="Sök"
          />
          <p v-on:click="resetSearch()"><i class="fas fa-times"></i></p>
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
      if (this.searchString.length < 1) {
        this.$store.dispatch("getConsultantList");
      } else {
        this.$store.dispatch("searchConsultant", this.searchString);
      }
    },
    resetSearch() {
      this.searchString = "";
      this.$store.dispatch("getConsultantList");
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

input:-webkit-autofill {
  -webkit-box-shadow: inset 0 0 0px 9999px white;
}
</style>
