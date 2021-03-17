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
      <table>
        <tr>
          <th>#</th>
          <th>Förnamn</th>
          <th>Efternamn</th>
          <th></th>
        </tr>
        <tr v-for="(i, index) in cvList" :key="index">
          <th>{{ i.index }}</th>
          <th>{{ i.fName }}</th>
          <th>{{ i.lName }}</th>
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
    RegisterUser,
  },
  // data() {
  //   return {
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
  //   };
  // },
  computed: {
    cvList() {
      console.log("computed");
      return this.$store.getters.getCvList;
    },
  },
  methods: {
    showCV(index) {
      console.log("click", index);
    },
  },
};
</script>

<style scoped>
.cons-manager {
  display: flex;
}

.list-table {
  flex-grow: 0.5;
  padding: 3%;
  border: solid 1px black;
  height: 87vh;
  margin: 1.3% 5%;
  border-radius: 1%;
  width: 50vw;
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
