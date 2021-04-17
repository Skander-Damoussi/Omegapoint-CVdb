<template>
  <div>
    <div class="tasks">
      <div class="row">
        <button @click="CVClick()">Tillbaka till CV</button>
        <h2 id="addButton">Erfarenheter</h2>
        <button @click="AddClick()" id="addButton">Lägg till erfarenhet</button>
      </div>
      <div class="rownomargin">
        <div
          class="sort sorttitle"
          title="Sort by title"
          @click="sortListTitle()"
        >
          <i class="fas fa-sort"></i>
        </div>
        <div
          class="sort stickright sortdate"
          title="Sort by date"
          @click="sortListDate()"
        >
          <i class="fas fa-sort"></i>
        </div>
      </div>
      <div
        class="wrapper"
        v-for="(col, index) in experienceList"
        :key="col.startDate"
      >
        <div class="title" @click="col.show = !col.show">
          <div class="rownomargin">
            <p>
              {{ col.title }}
            </p>
            <p class="stickright">{{ col.startDate }} - {{ col.endDate }}</p>
          </div>
        </div>
        <div class="container" id="container" v-if="col.show">
          <div class="rownomargin">
            <h3>Assignments</h3>
            <div
              class="stickright experienceedit"
              title="Edit experience"
              @click="EditClick(index)"
            >
              <i class="fas fa-edit"></i>
            </div>
          </div>
          <li id="inboxText" v-for="assign in col.assignments" :key="assign">
            {{ assign }}
          </li>
          <h3 id="h3space">Programming languages</h3>
          <li id="inboxText" v-for="language in col.language" :key="language">
            {{ language }}
          </li>
          <h3 id="h3space">Software</h3>
          <li id="inboxText" v-for="software in col.software" :key="software">
            {{ software }}
          </li>
          <h3 id="h3space">Roles</h3>
          <li id="inboxText" v-for="role in col.role" :key="role">
            {{ role }}
          </li>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditUser",
  data: function() {
    return {
      sortTitle: false,
      sortDate: false,
      experienceList: {},
    };
  },
  mounted() {
    let user = this.$store.getters.getLoggedInUser;
    console.log(user);
    this.$store.dispatch("getUserExperience", {userId: user.id});
  },
  methods: {
    CVClick() {
      this.$router.push({ name: "Consultant" });
    },
    EditClick(index) {
      console.log(index);
      this.$router.push({
        name: "ConsultantExperienceEdit",
        params: this.collection[index],
      });
    },
    AddClick() {
      this.$router.push({ name: "ConsultantExperienceEdit" });
    },
    sortListTitle() {
      if (this.sortTitle) {
        this.collection = this.collection.sort((a, b) =>
          a.title > b.title ? 1 : -1
        );
      } else {
        this.collection = this.collection.sort((b, a) =>
          a.title > b.title ? 1 : -1
        );
      }
      this.sortTitle = !this.sortTitle;
    },
    sortListDate() {
      if (this.sortDate) {
        this.collection = this.collection.sort((a, b) =>
          a.startDate > b.startDate ? 1 : -1
        );
      } else {
        this.collection = this.collection.sort((b, a) =>
          a.startDate > b.startDate ? 1 : -1
        );
      }
      this.sortDate = !this.sortDate;
    },
  },
};
</script>

<style scoped>
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}

.wrapper {
  border-radius: 2px;
  border: 2px solid #f7f7f7;
  max-width: 60vw;
  margin: auto;
}

.tasks {
  text-align: left;
  margin: auto;
  max-width: 60vw;
  margin-top: 20px;
}

.tablewrapper {
  padding: 30px;
  border: 1px solid #f7f7f7;
}

.title {
  background: #f7f7f7;
  padding: 10px;
  text-align: left;
  border: 1px solid #cecece;
  transition-duration: 0.5s;
}

.title:hover {
  background: #f7f7f7;
  padding: 10px;
  text-align: left;
  border: 1px solid #006166;
  cursor: pointer;
}

#addButton {
  margin-left: auto;
}

.row {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  margin-bottom: 10px;
}

.rownomargin {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
}

.box {
  background-color: white;
  margin: 50px;
  display: flex;
}

input[type="text"],
select {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}

input[type="text"]:focus {
  border: 1px solid #555;
}

th {
  font-size: 15px;
  font-weight: normal;
}

textarea {
  height: 230px;
  width: 100%;
  padding: 12px 20px;
  margin: 10px;
  box-sizing: border-box;
  border: 2px solid #ccc;
  border-radius: 4px;
  background-color: #f8f8f8;
  resize: none;
}

.as {
  width: 30vw;
}

.stickright {
  margin-left: auto;
}

button {
  color: white;
  background: #2185d0;
  border: none;
  text-decoration: none;
  border-radius: 4px;
  transition-duration: 0.4s;
  border: 2px solid #2185d0;
  margin-bottom: 1em;
  padding: 10px;
}

button:hover {
  background-color: white; /* Green */
  color: black;
  border: 2px solid #2185d0;
  cursor: pointer;
}

#inboxText {
  font-size: 12px;
}

h3 {
  margin-bottom: 5px;
}

#h3space {
  margin-top: 25px;
}

h2 {
}

.container {
  padding: 15px;
}

.sort {
  margin-bottom: 5px;
}

.sort:hover {
  cursor: pointer;
  color: #2185d0;
}

.sortdate {
  margin-right: 5vw;
}

.sorttitle {
  margin-left: 5vw;
}

.experienceedit:hover {
  cursor: pointer;
  color: #2185d0;
}
</style>
