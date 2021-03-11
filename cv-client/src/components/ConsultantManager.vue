<template>
  <div class="cons-manager">
    <!-- <md-table md-fixed-header class="md-scrollbar">
      <md-table-row v-for="(i, index) in test" :key="index">
        <md-table-cell md-label="#"> {{ i.index }}</md-table-cell>
        <md-table-cell md-label="Förnamn">{{ i.fName }}</md-table-cell>
        <md-table-cell md-label="Efternamn">{{ i.lName }}</md-table-cell>
      </md-table-row>
    </md-table> -->
    <div class="table">
      <table>
        <tr>
          <th>#</th>
          <th>Förnamn</th>
          <th>Efternamn</th>
        </tr>
        <tr v-for="(i, index) in test" :key="index">
          <th>{{ i.index }}</th>
          <th>{{ i.fName }}</th>
          <th>{{ i.lName }}</th>
        </tr>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  name: "ConsultantManager",
  data() {
    return {
      test: [
        {
          index: "0",
          fName: "Test",
          lName: "Testsson",
        },
        {
          index: "1",
          fName: "Test1",
          lName: "Testsson1",
        }
      ],
    };
  },
  methods: {},
};
</script>

<style>
.cons-manager {
  display: flex;
}

.table {
  flex-grow: 0.5;
  padding: 5%;
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
</style>
