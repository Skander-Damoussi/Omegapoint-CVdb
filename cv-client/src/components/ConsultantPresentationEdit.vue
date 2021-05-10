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
          Spara presentation
        </button>
        <button v-else @click="SaveClick()" id="addButton">
          Uppdatera presentation <i class="fas fa-check"></i>
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
            <span class="inboxTextSpan">{{ list }}</span>
          </li>
          <div class="rownomargin">
            <textarea
              type="text"
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
    </div>
  </div>
</template>

<script>
export default {
  name: "PresentationEdit",
  data: function() {
    return {
      title: "",
      startDate: "",
      endDate: "",
      id: "",
      newEntry: null,
      editTitle: false,
      oldList: {},
      showUserID: "",
      collection: [
        {
          show: true,
          title: "Stycken",
          list: [],
          listInput: "",
        },
      ],
    };
  },
  created() {
    if (this.$route.params.title === undefined) {
      this.title = "Ny presentation";
      this.newEntry = true;
      this.editTitle = true;
    } else {
      this.oldList = JSON.parse(JSON.stringify(this.$route.params));
      this.title = this.$route.params.title;
      this.id = this.$route.params.id;
      this.collection[0].list = this.$route.params.paragraph;
    }
    this.showUserID = this.$route.params.keepID;
  },
  async mounted() {
    if (this.showUserID == null) {
      this.showUserID = await this.$store.getters.getLoggedInUser.id;
    }
  },
  methods: {
    BackClick() {
      this.$router.push({ name: "ConsultantExperience", params: { userID: this.showUserID } });
    },
    HomeClick() {
      this.$router.push({ name: "Consultant", params: { userID: this.showUserID } });
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
      this.collection[0].list = temp.paragraph;
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
      if (this.newEntry) {
        await this.$store.dispatch("addPresentation", {
          token: this.$store.getters.getUserToken,
          input: {
            title: this.title,
            Paragraph: this.collection[0].list,
            userID: this.showUserID,
            newPresentation: this.newEntry,
            id: this.MakeId(8),
          },
        });
      } else {
        await this.$store.dispatch("updatePresentation", {
          token: this.$store.getters.getUserToken,
          input: {
            title: this.title,
            Paragraph: this.collection[0].list,
            userID: this.showUserID,
            id: this.id,
            newPresentation: this.false,
          },
        });
      }
      this.$router.push({ name: "ConsultantExperience", params: { userID: this.showUserID } });
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
  align-items: center;
  margin-bottom: 10px;
}

.rownomargin {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
}

li {
  list-style-type: none;
  margin-bottom: 25px;
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
  height: 150px;
  width: 30vw;
  padding: 6px 6px;
  margin: 10px;
  box-sizing: border-box;
  border: 2px solid #ccc;
  border-radius: 4px;
  background-color: #f8f8f8;
  resize: none;
  margin-left: auto;
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

#listAddPresentation {
  margin-top: auto;
  padding: 5px;
  margin-right: auto;
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
</style>
