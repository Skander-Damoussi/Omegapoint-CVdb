<template>
  <div>
    <div class="tasks">
      <div class="row">
        <button class="homebutton" @click="CVClick()">
          <i class="fas fa-home "></i>
        </button>
        <input
          type="text"
          v-if="experienceList.length > 0 || presentationList.length > 0"
          v-model="searchText"
          v-on:input="search()"
          @keyup.enter="search()"
        />
      </div>
      <div class="rownomargin" v-if="experienceList.length > 0">
        <div
          class="sort stickleft sorttitle"
          title="Sort by title"
          @click="sortListTitle()"
        >
          <i class="fas fa-sort"></i>
        </div>
        <h2 id="addButton">Uppdrag</h2>
        <div class="sort sortdate" title="Sort by date" @click="sortListDate()">
          <i class="fas fa-sort"></i>
        </div>
        <button @click="AddClick()" id="addButton">
          Lägg till <i class="fas fa-plus"></i>
        </button>
      </div>
      <div v-else>
        <p id="textcenter">
          Var vänlig lägg till dina uppdrag.
          <button @click="AddClick()" id="addButton">
            Lägg till <i class="fas fa-plus"></i>
          </button>
        </p>
      </div>
      <div class="wrapper" v-for="(col, index) in experienceList" :key="index">
        <div class="title" @click="col.show = !col.show">
          <div class="rownomargin">
            <p>
              {{ col.title }}
            </p>
            <p v-if="col.endDate === ''" class="stickright">
              {{ col.startDate }} - Pågående
            </p>
            <p v-else class="stickright">
              {{ col.startDate }} - {{ col.endDate }}
            </p>
          </div>
        </div>
        <div class="container" id="container" v-if="col.show">
          <div class="rownomargin">
            <h3 v-if="col.language.length > 0">
              Programeringsspråk & tekniker
            </h3>
            <div class="stickright">
              <div
                class="stickright experienceedit"
                title="Edit experience"
                @click="EditClick(index)"
              >
                <i class="fas fa-edit"></i>
              </div>
              <div
                class="stickright experienceremove"
                title="Remove experience"
                @click="RemoveClick(index)"
              >
                <i class="fas fa-trash-alt"></i>
              </div>
            </div>
          </div>
          <li id="inboxText" v-for="language in col.language" :key="language">
            {{ language }}
          </li>
          <h3 id="h3space" v-if="col.software.length > 0">Mjukvara</h3>
          <li id="inboxText" v-for="software in col.software" :key="software">
            {{ software }}
          </li>
          <h3 id="h3space" v-if="col.assignments.length > 0">
            Arbetsbeskrivningar
          </h3>
          <li
            id="inboxTextArea"
            v-for="assign in col.assignments"
            :key="assign"
          >
            {{ assign }}
          </li>
          <h3 id="h3space" v-if="col.role.length > 0">Arbetsroller</h3>
          <li id="inboxText" v-for="role in col.role" :key="role">
            {{ role }}
          </li>
        </div>
      </div>
    </div>
    <div class="tasks">
      <div class="row"></div>
      <div class="rownomargin" v-if="presentationList.length > 0">
        <div
          class="sort stickleft sorttitle"
          title="Sort by title"
          @click="sortListTitlePresentation()"
        >
          <i class="fas fa-sort"></i>
        </div>
        <h2 id="addButtonPresentation">Presentationer</h2>
        <button @click="AddClickPresentation()" id="addButton">
          Lägg till <i class="fas fa-plus"></i>
        </button>
      </div>
      <div v-else>
        <p id="textcenter">
          Var vänlig lägg till presentationer.
          <button @click="AddClickPresentation()" id="addButton">
            Lägg till <i class="fas fa-plus"></i>
          </button>
        </p>
      </div>
      <div
        class="wrapperPresentation"
        v-for="(col, index) in presentationList"
        :key="index"
      >
        <div class="title" @click="col.show = !col.show">
          <div class="rownomargin">
            <p>
              {{ col.title }}
            </p>
          </div>
        </div>
        <div class="container" id="container" v-if="col.show">
          <div class="rownomargin">
            <h3>Stycken</h3>
            <div class="stickright">
              <div
                class="stickright experienceedit"
                title="Edit experience"
                @click="EditClickPresentation(index)"
              >
                <i class="fas fa-edit"></i>
              </div>
              <div
                class="stickright experienceremove"
                title="Remove experience"
                @click="RemoveClickPresentation(index)"
              >
                <i class="fas fa-trash-alt"></i>
              </div>
            </div>
          </div>
          <li
            id="inboxTextPresentation"
            v-for="paragraph in col.paragraph"
            :key="paragraph"
          >
            {{ paragraph }}
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
      searchText: "",
      experienceList: {},
      presentationList: {},
    };
  },
  async mounted() {
    let user = await this.$store.getters.getLoggedInUser;
    await this.$store.dispatch("getUserExperience", user.id);
    this.experienceList = this.$store.getters.getUserExperience;
    await this.$store.dispatch("getUserPresentation", user.id);
    this.presentationList = this.$store.getters.getUserPresentation;
  },
  methods: {
    CVClick() {
      this.$router.push({ name: "Consultant" });
    },
    EditClick(index) {
      this.$router.push({
        name: "ConsultantExperienceEdit",
        params: this.experienceList[index],
      });
    },
    search() {
      var elements = document.getElementsByClassName("wrapper");

      var i;
      for (i = 0; i < elements.length; i++) {
        var title =
          elements[i].childNodes[0].childNodes[0].children[0].innerText;
        var date =
          elements[i].childNodes[0].childNodes[0].children[1].innerText;

        if (
          title.includes(this.searchText) ||
          date.includes(this.searchText) ||
          this.experienceList[i].language.includes(this.searchText) ||
          this.experienceList[i].role.includes(this.searchText) ||
          this.experienceList[i].software.includes(this.searchText)
        ) {
          elements[i].style.display = "block";
        } else {
          elements[i].style.display = "none";
        }
      }

      elements = document.getElementsByClassName("wrapperPresentation");

      for (i = 0; i < elements.length; i++) {
        title = elements[i].childNodes[0].childNodes[0].children[0].innerText;

        if (
          title.includes(this.searchText) ||
          this.presentationList[i].paragraph.find((a) =>
            a.includes(this.searchText)
          )
        ) {
          elements[i].style.display = "block";
        } else {
          elements[i].style.display = "none";
        }
      }
    },
    AddClick() {
      this.$router.push({ name: "ConsultantExperienceEdit" });
    },
    async RemoveClick(index) {
      if (!confirm("Are you sure?")) {
        return;
      }
      let user = await this.$store.getters.getLoggedInUser;
      await this.$store.dispatch("removeExperience", {
        token: this.$store.getters.getUserToken,
        input: {
          title: this.experienceList[index].title,
          startDate: null,
          endDate: null,
          Language: null,
          Software: null,
          Assignments: null,
          Role: null,
          userID: user.id,
          newExperience: false,
          id: this.experienceList[index].id,
        },
      });
      this.experienceList.splice(index, 1);
    },
    sortListTitle() {
      if (this.sortTitle) {
        this.experienceList = this.experienceList.sort((a, b) =>
          a.title > b.title ? 1 : -1
        );
      } else {
        this.experienceList = this.experienceList.sort((b, a) =>
          a.title > b.title ? 1 : -1
        );
      }
      this.sortTitle = !this.sortTitle;
    },
    sortListDate() {
      if (this.sortDate) {
        this.experienceList = this.experienceList.sort((a, b) =>
          a.startDate > b.startDate ? 1 : -1
        );
      } else {
        this.experienceList = this.experienceList.sort((b, a) =>
          a.startDate > b.startDate ? 1 : -1
        );
      }
      this.sortDate = !this.sortDate;
    },
    EditClickPresentation(index) {
      this.$router.push({
        name: "ConsultantPresentationEdit",
        params: this.presentationList[index],
      });
    },
    AddClickPresentation() {
      this.$router.push({ name: "ConsultantPresentationEdit" });
    },
    async RemoveClickPresentation(index) {
      if (!confirm("Are you sure?")) {
        return;
      }
      let user = await this.$store.getters.getLoggedInUser;
      await this.$store.dispatch("removePresentation", {
        token: this.$store.getters.getUserToken,
        input: {
          title: this.presentationList[index].title,
          paragraph: null,
          userID: user.id,
          newExperience: false,
          id: this.presentationList[index].id,
        },
      });
      this.presentationList.splice(index, 1);
    },
    sortListTitlePresentation() {
      if (this.sortTitle) {
        this.presentationList = this.presentationList.sort((a, b) =>
          a.title > b.title ? 1 : -1
        );
      } else {
        this.presentationList = this.presentationList.sort((b, a) =>
          a.title > b.title ? 1 : -1
        );
      }
      this.sortTitle = !this.sortTitle;
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

.wrapperPresentation {
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
  margin-left: 10px;
  padding: 5px;
  margin-top: auto;
  margin-bottom: auto;
}

#addButtonPresentation {
  margin-left: 10px;
  padding: 5px;
  margin-top: auto;
  margin-bottom: auto;
  margin-right: auto;
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
  padding: 6px 10px;
  margin-left: auto;
  display: inline-block;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
  width: 200px;
}

.center {
  margin-left: auto;
  margin-right: auto;
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

.stickleft {
  margin-right: auto;
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
}

#inboxTextArea {
  width: 500px;
}

#inboxTextPresentation {
  font-size: 16px;
  margin-bottom: 10px;
  margin-top: 10px;
  width: 400px;
  list-style-type: none;
}

h3 {
  margin-bottom: 5px;
}

#h3space {
  margin-top: 25px;
  justify-content: left;
}

.container {
  padding: 15px;
}

.sort {
  margin-bottom: 5px;
  margin-top: auto;
  margin-bottom: auto;
}

.sort:hover {
  cursor: pointer;
  color: #2185d0;
}

.sortdate {
  margin-left: auto;
}

.sorttitle {
  margin-left: 5vw;
}

.experienceedit:hover {
  cursor: pointer;
  color: #2185d0;
}

.experienceremove:hover {
  cursor: pointer;
  color: #2185d0;
}

#textcenter {
  margin-top: 30vh;
  text-align: center;
}
</style>
