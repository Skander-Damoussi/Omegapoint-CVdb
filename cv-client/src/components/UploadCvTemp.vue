<template>
  <div class="main-div">
    <div class="file-container">
      <div class="file">
        <h3>Ladda upp fil</h3>
          
          <p>Namn</p>
          <input class="fileInput" v-model="fileName" />
          <p>Fil</p>
          <div class="upload">
          <input          
            accept=".docx"
            type="file"
            id="file"
            ref="cvTemplate"
            v-on:change="PreviewTemplate"/> 
        <button class="button" v-on:click="SubmitFile">Upload</button>
        </div>
      </div>
    </div>

    <div class="list-table">
      <table>
        <tr>
          <th>#</th>
          <th>Name</th>
        </tr>
        <tr v-for="(i, index) in cvTempList" :key="index">
          <th>{{ index }}</th>
          <th>{{ i.name }}</th>          
          <th>
            <p class="icon-click" v-on:click="showCvTemp(i.stringId)">
              <i title="Ladda ner" class="fas fa-file-download" ></i>
            </p>
          </th>
          <th>
            <p class="icon-click" v-on:click="showModal(i.stringId)">
              <i title="Ta bort" i class="fas fa-trash-alt"></i>
            </p>
          </th>
        </tr>
      </table>
    </div>  
      <Modal v-show="isModalVisible" @close="cancel()">
      <template v-slot:header>
        Ta bort Cv-mall
      </template>

      <template v-slot:body>
        <p>
          Är du säker på att du vill ta bort cv-mall?
        </p>
        <div class="section">
          <button class="submit bttn cancel" v-on:click="cancel()">
            Avbryt
          </button>
          <button class="submit bttn" v-on:click="removeCvTemp()">
            OK
          </button>
        </div>
      </template>

      <template v-slot:footer> </template>
    </Modal>
  </div>
</template>

<script>
import Modal from "./Modal";
export default {
  name: "UploadCvTemp",
  components: {
    Modal
  },
  data() {
    return {
      fileName: "",
      cvTemplate: "",
      isModalVisible: false,
    };
  },

  computed: {
    cvTempList() {
      return this.$store.getters.getCvTempList;
    },
  },
  async mounted() {
    await this.$store.dispatch("getCvTempList");
  },
  methods: {
    updateActiveCv() {
      console.log("onchange");
    },
    showModal(input) {
      this.isModalVisible = true;
      this.cvTemplate = input;
    },
    showCvTemp(id) {
      console.log(id);
      this.$store.dispatch("getCvTemp", id);
    },
    async removeCvTemp() {
      console.log(this.cvTemplate);
      await this.$store.dispatch("removeCvTemp", this.cvTemplate);
      this.cancel();
      await this.$store.dispatch("getCvTempList");
    },
    cancel(){
        this.cvTemplate="";
        this.isModalVisible=false;
    },    
    PreviewTemplate(e) {
      this.error = false;
      let files = e.target.files;
      if (files.length === 0) {
        return;
      }
      let reader = new FileReader();
      reader.onload = (e) => {
        this.cvTemplate = e.target.result;

      };
      console.log("1", this.cvTemplate);
      reader.readAsDataURL(files[0]);
      console.log("2", this.cvTemplate);
      console.log(this.fileName);
    },
    async SubmitFile() {
      var InputObject = {
        Name: this.fileName,
        Base64String: this.cvTemplate        
      };
      console.log("SubmitFile", this.cvTemplate);
      await this.$store.dispatch("postCvTemplate", InputObject);
      await this.$store.dispatch("getCvTempList");
      this.fileName="";
      this.$refs.cvTemplate.value=null;
      console.log("efter clear", this.cvTemplate);
    },
  },
};
</script>

<style scoped>
div>p{
    text-align: left;
}

.main-div{
      border: solid 1px black; 
      border-radius: 4px;
      padding:3vh;
      margin:2vh 1vh;
      height:87vh;
}
.list-table {
  padding: 1vh;
  
  height: 40vh;
    width: 40vw;
  overflow-y: scroll;
  margin-top: 2vh;
  margin-right: 3vw;
  margin-left: 2vw;
  
}

.file-container {
  width: 40vw;
  margin: 2vw;
}
.file {
  display: flex;
  flex-direction: column;
}
.upload{
    display: flex;
}

.formDiv {
  margin: 0px 1vh;
}
form {
  text-align: left;
  width: 100%;
}
.button {
  color: white;
  background: #2185d0;
  border: none;
  text-decoration: none;
  border-radius: 4px;
  transition-duration: 0.4s;
  border: 2px solid #2185d0;
  height: 5vh;
  margin: 1vh;
  padding: 1vh;
}
input {
  width: 100%;
  border: 00.1px solid;
  align-self: center;
}
select {

}
.error {
  color: red;
}
.mainDiv {
  border: solid 1px black;
  height: 87vh;
}
label {
  text-align: left;
}
h3 {
  margin: 2vh 0vh;
  display: flex;
  justify-content: center;
}
.buttonDiv {
  display: flex;
  align-content: flex-end;
  text-align: end;
}

.icon-click:hover {
  cursor: pointer;
}
td,
th {
  border-bottom: 1px solid #dddddd;
  text-align: left;

}
.list-table > table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}
.fileInput{
    margin-bottom: 1vh;
    margin-top: 1vh;
}

.section > button {
  margin-top: 1vh;
  padding: 0.25vw;
}

.cancel {
  background-color: red;
  margin-right: 1vw;
}
</style>
