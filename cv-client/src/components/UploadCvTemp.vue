<template>
  <div>
    <h1>Hellow!!</h1>

    <!-- <h1>{{cvTemp.filename}}</h1> -->

    <!-- <button @click="toggle">Toggle</button>
  <div v-if="active">
    Menu
  </div> -->
    <div class="file-container">
      <div class="file">
        <label
          >File
          <p>FileName</p>
          <input v-model="fileName" />
          <p>File</p>
          <input
            accept=".docx"
            type="file"
            id="file"
            ref="file"
            v-on:change="handleFileUpload()"
          />
        </label>
        <button v-on:click="submitFile()">Submit</button>
      </div>
    </div>

    <div class="list-table">
      <table>
        <tr>
          <th>#</th>
          <th>Name</th>
          <th>Aktiv</th>
          <th></th>
        </tr>
        <tr v-for="(i, index) in cvTempList" :key="index">
          <th>{{ index }}</th>
          <th>{{ i.name }}</th>
          <!-- <th>{{ i.active }}</th> -->
          <!-- <th v-if="false"> <p class="fas fa-check"></p></th>          -->
          <!-- <th>{{i.stringId}}</th> -->
          <!-- <th><input type="radio" name="active" value=true @change="updateActiveCv()"></th> -->

          <th><p v-if="i.active" class="fas fa-check" /></th>

          <th>
            <p class="icon-click" v-on:click="showCvTemp(i.stringId)">
              <i class="fas fa-file-download"></i>
            </p>
          </th>
        </tr>
      </table>
    </div>
  </div>
</template>

<script>
// export default {
//     data() {
//     return {
//         cvTemp: {id:"", filename:"", active:false}
//   }
// }
// // computed: {
// //     consultantList() {
// //       return this.$store.getters.getCvTempList;
// //     },

// // },
// // methods:{
// //     async mounted() {
// //     await this.$store.dispatch("getCvTempList");
// //     this.cvTempList = this.cvTempList;
// //   },
// // }
// }
import Axios from "@/axios.config.js";
export default {
  name: "UploadCvTemp",
  data() {
    return {
      fileName: "",
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
    showCvTemp(id) {
      //String inputId=id.id;
      console.log(id);
      this.$store.dispatch("getCvTemp", id);
      //this.getCvTemp(id);
    },
    //Används inte nu
    async getCvTemp(stringId) {
      console.log("Inne i Get", stringId);
      Axios({
        method: "GET",
        url: "cv/GetCvTemplate/" + stringId,
        responseType: "blob",
        //data: dates
      }).then((response) => {
        var fileURL = window.URL.createObjectURL(new Blob([response.data]));
        var fileLink = document.createElement("a");
        fileLink.href = fileURL;
        fileLink.setAttribute("download", "cvTemplate.docx");
        document.body.appendChild(fileLink);
        fileLink.click();
      });
    },
    //   toggle () {
    //     this.active = !this.active
    //   }
  },
};
</script>

<style>
.file-container {
  width: 40vw;
}
.file {
  display: flexbox;
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
}
input {
  padding: 0.5em 1em;
  border-radius: 4px;
  margin: 0 auto;
  width: 100%;
  border: 00.1px solid;
}
select {
  flex: 1 0 auto;
  padding: 0.5em 1em;
  border-radius: 4px;
  width: 100%;
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
</style>
