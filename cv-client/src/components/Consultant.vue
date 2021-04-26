<template>
  <div>
    <div>
      <div class="icon-click" v-on:click="editMethod()">
        <div class="row">
        <i
          class="fas fa-edit fa-2x"
          v-on:click="editMethod()"
          title="Tryck för att redigera"
        ></i>
        <p>Redigera</p>
        </div>
      </div>

      <div class="icon-click row" v-on:click="exportToPDF">
        <i
          class="btn fas fa-file-export fa-2x"
          title="Tryck för att exportera"
        ></i>
        <p>Exportera till PDF</p>
      </div>
    </div>
    <div id="pdf" ref="document">
      <div class="row">
        <img src="../assets/templogo.png" height="23px" class="logo"/>
        <div class="contactdiv">
          <p class="contact contactTitel">Frontwalker AB</p>
          <p class="contact">0702-23 23 25</p>
          <p class="contact">www.frontwalker.se</p>
          <p class="contact">magnus.nilsson@frontwalker.se</p>
        </div>
      </div>
      
      <div class="row">
        <img src="../assets/temp.png" height="150px" class="selfie"/>
        <div class="cvTitelDiv">
          <h1 class="cvTitel">Mikael Strömgren</h1>
          <p class="cvTitelRoll">Utvecklare</p>
        </div>
      </div>
      <div class="introstycke">
      <p class="stycke">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum
        blandit vitae nisi id malesuada. Orci varius natoque penatibus et magnis
        dis parturient montes, nascetur ridiculus mus. Vivamus sit amet massa
        vitae elit congue maximus et at mi. Aenean eget consectetur nisi.
        Aliquam vitae sem diam. Vestibulum volutpat lectus non orci egestas
        vehicula. Fusce sit amet neque vitae nulla egestas rutrum. Nam eget nisi
        ultricies, sodales sem vitae, consequat sapien. Fusce blandit risus quis
        risus tincidunt, sit amet fermentum tortor lobortis
      </p>
      <p class="stycke">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum
        blandit vitae nisi id malesuada. Orci varius natoque penatibus et magnis
        dis parturient montes, nascetur ridiculus mus. Vivamus sit amet massa
        vitae elit congue maximus et at mi. Aenean eget consectetur nisi.
        Aliquam vitae sem diam. Vestibulum volutpat lectus non orci egestas
        vehicula. Fusce sit amet neque vitae nulla egestas rutrum. Nam eget nisi
        ultricies, sodales sem vitae, consequat sapien. Fusce blandit risus quis
        risus tincidunt, sit amet fermentum tortor lobortis
      </p>
      <p class="stycke">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum
        blandit vitae nisi id malesuada. Orci varius natoque penatibus et magnis
        dis parturient montes, nascetur ridiculus mus. Vivamus sit amet massa
        vitae elit congue maximus et at mi. Aenean eget consectetur nisi.
        Aliquam vitae sem diam. Vestibulum volutpat lectus non orci egestas
        vehicula. Fusce sit amet neque vitae nulla egestas rutrum. Nam eget nisi
        ultricies, sodales sem vitae, consequat sapien. Fusce blandit risus quis
        risus tincidunt, sit amet fermentum tortor lobortis
      </p>
      </div>
    </div>
  </div>
</template>

<script>
import html2pdf from "html2pdf.js";

export default {
  name: "Consultant",
  props: {},

  data() {
    return { exportMenu: false };
  },
  methods: {
    toggleExport() {
      this.exportMenu = !this.exportMenu;
      console.log("exportMenu=", this.exportMenu);
    },
    editMethod() {
      this.$router.push("ConsultantExperience/");
    },
    exportToPDF() {
      html2pdf(this.$refs.document, {
        margin: 0,
        filename: "document.pdf",
        image: { type: "jpeg", quality: 1 },
        html2canvas: { dpi: 192, letterRendering: false },
        jsPDF: { unit: "mm", format: "a4", orientation: "portrait" },
      });
    },
  },
};
</script>

<style scoped>
* {
  /* all */
  /* font-size: 12px; */
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}

div {
  text-align: left;
}

#pdf {
  width: 210mm;
  margin-right: auto;
  margin-left: auto;
}

.row {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
}

.cvTitelDiv {
  margin-top: auto;
  margin-bottom: 15px;
}

.cvTitel {
  color: #006d86;
}

.cvTitelRoll {
  color: gray;
  font-style: unset;
  font-size: 18px;
  font-weight: normal;
}

.selfie {
  margin-left: 18px;
}

.logo {
  margin-left: 40px;
  margin-bottom: 60px;
  margin-top: 60px;
}

.introstycke {
  width: 50%;
  font-style: normal;
  margin-left: 40px;
}

.stycke {
  margin-top: 15px;
  font-weight: normal;
}

.contactdiv {
  margin-left: auto;
  text-align: right;
  margin-right: 40px;
}

.contact {
  font-weight: normal;
  font-size: 14px;
}

.contactTitel {
  color: #006d86;
  font-weight: bold;
}

.icon-click {
  margin: 15px;
}
</style>
