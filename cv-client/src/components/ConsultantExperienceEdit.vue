<template>
  <div>
    <div class="tasks">
      <div class="row">
        <button @click="BackClick()">
          <i class="fas fa-chevron-circle-left"></i> Tillbaka
        </button>
        <button class="homebutton" @click="HomeClick()">
          <i class="fas fa-home "></i>
        </button>
        <h2 id="addButton">{{ this.title }}</h2>
        <div
          class="editTitle"
          @click="editTitle = !editTitle"
          title="Redigera titel"
        >
          <i class="fas fa-edit"></i>
        </div>
        <div class="editTitle" @click="Restart()" title="Återställ värden">
          <i class="fas fa-redo"></i>
        </div>
        <button v-if="this.newEntry" @click="SaveClick()" id="addButton">
          Spara erfarenhet
        </button>
        <button v-else @click="SaveClick()" id="addButton">
          Uppdatera erfarenhet <i class="fas fa-check"></i>
        </button>
      </div>
      <div v-if="editTitle">
        <h4>Titel</h4>
        <div class="rownomargin">
          <input type="text" class="titleInput" v-model="title" />
          <div class="editTitle" @click="editTitle = !editTitle">
            <i class="fas fa-arrow-up"></i>
          </div>
        </div>
      </div>
      <div class="wrapper" v-for="col in collection" :key="col.startDate">
        <div class="title" @click="col.show = !col.show">
          <div class="rownomargin">
            <p>
              {{ col.title }}
            </p>
            <p class="stickright"></p>
          </div>
        </div>
        <div class="container" id="container" v-if="col.show">
          <li id="inboxText" v-for="(list, index) in col.list" :key="index">
            <button @click="RemoveClick(col.title, index)" id="listDelete">
              <i class="fas fa-trash-alt"></i>
            </button>
            {{ list }}
          </li>
          <div v-if="col.title != 'Arbetsbeskrivningar'" class="rownomargin">
            <input
              type="text"
              v-model="col.listInput"
              class="listInput"
              @keyup.enter="AddClick(col.title)"
            />
            <button @click="AddClick(col.title)" id="listAdd">
              <i class="fas fa-plus-square"></i>
            </button>
          </div>
          <div v-else class="rownomargin">
            <textarea
              v-model="col.listInput"
              class="listInput"
              @keyup.enter="AddClick(col.title)"
            />
            <button @click="AddClick(col.title)" id="listAddPresentation">
              <i class="fas fa-plus-square"></i>
            </button>
          </div>
        </div>
      </div>
      <div class="date rownomargin">
        <div class="date">
          <p>Start datum</p>
          <input type="date" id="startDate" name="startDate" />
        </div>
        <div class="date">
          <div class="rownomargin centertext">
            <p>Slut datum</p>
            <div @click="resetEndDate()" class="clickNull" title="Pågående">
              <i class="fas fa-times-circle"></i>
            </div>
          </div>
          <input type="date" id="endDate" name="endDate" />
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
      title: "",
      startDate: "",
      endDate: "",
      id: "",
      newEntry: null,
      editTitle: false,
      oldList: {},
      collection: [
        {
          show: true,
          title: "Programeringsspråk & tekniker",
          list: [],
          listInput: "",
        },
        {
          show: false,
          title: "Mjukvara",
          list: [],
          listInput: "",
        },
        {
          show: false,
          title: "Arbetsbeskrivningar",
          list: [],
          listInput: "",
        },
        {
          show: false,
          title: "Arbetsroller",
          list: [],
          listInput: "",
        },
      ],
    };
  },
  created() {
    if (this.$route.params.title === undefined) {
      this.title = "Ny erfarenhet";
      this.newEntry = true;
      this.editTitle = true;
    } else {
      this.oldList = JSON.parse(JSON.stringify(this.$route.params));
      this.title = this.$route.params.title;
      this.startDate = this.$route.params.startDate;
      this.endDate = this.$route.params.endDate;
      this.id = this.$route.params.id;
      this.collection[0].list = this.$route.params.language;
      this.collection[1].list = this.$route.params.software;
      this.collection[2].list = this.$route.params.assignments;
      this.collection[3].list = this.$route.params.role;
      this.collection[0].show = true;
      this.collection[1].show = true;
      this.collection[2].show = true;
      this.collection[3].show = true;
    }
  },
  mounted() {
    if (this.startDate === "") {
      document.getElementById("startDate").value = this.getTodayDate();
    } else {
      document.getElementById("startDate").value = this.startDate;
      document.getElementById("endDate").value = this.endDate;
    }
  },
  methods: {
    getTodayDate() {
      const today = new Date()
        .toJSON()
        .slice(0, 10)
        .replace(/-/g, "-");
      return today;
    },
    BackClick() {
      this.$router.push({ name: "ConsultantExperience" });
    },
    HomeClick() {
      this.$router.push({ name: "Consultant" });
    },
    RemoveClick(title, index) {
      this.collection.forEach(function(entry) {
        if (entry.title == title) {
          entry.list.splice(index, 1);
        }
      });
    },
    async Restart() {
      let temp = JSON.parse(JSON.stringify(this.oldList));
      this.title = temp.title;
      this.collection[0].list = temp.language;
      this.collection[1].list = temp.software;
      this.collection[2].list = temp.assignments;
      this.collection[3].list = temp.role;
      document.getElementById("startDate").value = temp.startDate;
      document.getElementById("endDate").value = temp.endDate;
    },
    AddClick(title) {
      this.collection.forEach(function(entry) {
        if (entry.title == title && entry.listInput != "") {
          entry.list.push(entry.listInput);
        }
        entry.listInput = "";
      });
    },
    async SaveClick() {
      let userID = this.$store.getters.getLoggedInUser;
      if (this.newEntry) {
        await this.$store.dispatch("addExperience", {
          token: this.$store.getters.getUserToken,
          input: {
            title: this.title,
            startDate: this.cutDate(document.getElementById("startDate").value),
            endDate: this.cutDate(document.getElementById("endDate").value),
            Language: this.collection[0].list,
            Software: this.collection[1].list,
            Assignments: this.collection[2].list,
            Role: this.collection[3].list,
            userID: userID.id,
            newExperience: this.newEntry,
            id: this.MakeId(8),
          },
        });
      } else {
        await this.$store.dispatch("updateExperience", {
          token: this.$store.getters.getUserToken,
          input: {
            title: this.title,
            startDate: this.cutDate(document.getElementById("startDate").value),
            endDate: this.cutDate(document.getElementById("endDate").value),
            Language: this.collection[0].list,
            Software: this.collection[1].list,
            Assignments: this.collection[2].list,
            Role: this.collection[3].list,
            userID: userID.id,
            id: this.id,
            newExperience: this.false,
          },
        });
      }
      this.$router.push({ name: "ConsultantExperience" });
    },
    MakeId(length) {
      var result = [];
      var characters =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
      var charactersLength = characters.length;
      for (var i = 0; i < length; i++) {
        result.push(
          characters.charAt(Math.floor(Math.random() * charactersLength))
        );
      }
      return result.join("");
    },
    resetEndDate() {
      document.getElementById("endDate").value = "";
    },
    cutDate(date){
      return date;
    }
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
  margin-bottom: 10px;
}

.rownomargin {
  display: flex;
  flex-wrap: wrap;
}

li {
  list-style-type: none;
}

.box {
  background-color: white;
  margin: 50px;
  display: flex;
}

input[type="text"],
select {
  padding: 6px 10px;
  margin: 2px 0;
  display: inline-block;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
  max-width: 15vw;
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
  font-size: 16px;
  width: 500px;
}

#inboxTextArea {
  font-size: 16px;
  width: 400px;
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

#listAdd {
  margin-left: 10px;
  padding: 5px;
  margin: 2px;
}

#listDelete {
  margin-left: 10px;
  padding: 0px;
  margin: 1px;
  margin-right: 5px;
}

#listDelete:hover {
  background-color: white; /* Green */
  color: red;
  border: 2px solid red;
  cursor: pointer;
}

.editTitle {
  margin-left: 10px;
  color: #2185d0;
}

.editTitle:hover {
  color: black;
  cursor: pointer;
}

.date {
  margin-left: auto;
  margin-right: auto;
  margin-top: 10px;
}

.clickNull {
  margin-left: 8px;
  margin-left: 10px;
  color: #2185d0;
}

.clickNull:hover {
  color: black;
  cursor: pointer;
}

.homebutton {
  margin-left: 15px;
}

textarea {
  height: 150px;
  width: 30vw;
  padding: 6px 6px;
  margin: 10px;
  box-sizing: border-box;
  border: 2px solid #ccc;
  border-radius: 4px;
  background-color: #f8f8f8;
  resize: none;
}

#listAddPresentation {
  margin-top: auto;
  padding: 5px;
  margin-right: auto;
}
</style>
