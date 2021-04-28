<template>
  <div class="row wrapper">
    <div id="pdfBox">
      <div id="pdf" ref="document">
        <div class="row">
          <img src="../assets/templogo.png" height="23px" class="logo" />
          <div class="contactdiv">
            <p class="contact contactTitel">Frontwalker AB</p>
            <p class="contact">0702-23 23 25</p>
            <p class="contact">www.frontwalker.se</p>
            <p class="contact">magnus.nilsson@frontwalker.se</p>
          </div>
        </div>

        <div class="row">
          <img src="../assets/temp.png" height="150px" class="selfie" />
          <div class="cvTitelDiv">
            <h1 class="cvTitel">Mikael Strömgren</h1>
            <p class="cvTitelRoll">Utvecklare</p>
          </div>
        </div>
        <div class="introstycke">
          <p class="stycke">
            I grunden utbildad interaktionsdesigner, men har i min yrkesroll
            jobbat väldigt brett inom webb. Jag i alla de olika aspekterna av
            webben, både design och teknik, men även det sociala och
            affärsmässiga. Har under mina 10+ år i branschen bidragit med
            affärsnytta till både stora och små kunder och upphör aldrig att
            fascineras av den stora skillnad som bra webb kan göra. Mina
            drivkrafter är nyfikenhet och kunskapstörst.
          </p>
          <p class="stycke">
            Av omgivningen ses jag som ödmjuk och prestigelös. Jag trivs med att
            verka i en öppen miljö, där jag inspireras av duktiga kollegor, och
            utbyta kunskaper och erfarenheter. Upplevs som tydlig, kunnig och
            hjälpsam av kunder.
          </p>
          <p class="stycke">
            Mikaels erfarenheter inom webbutveckling har gett honom en bred
            kompetens som kommer väl till pass i de flest projekt. En vana av
            att ta projekt från ax till limpa med bra förmåga att kommunicer,
            planera, dokumentera och utveckla.
          </p>
        </div>
        <div class="fokusBox">
          <h3 class="fokusTitel">Uppdrag i fokus</h3>
          <div>
            <h4 class="fokusRoll">Utvecklare</h4>
            <p class="fokusFöretag">Thage i Skåne AB</p>
            <p class="stycke förstaStycke">
              Thage vill skicka viktig kommunikation till sina samrbetspartners.
              För att säkerställa att sådan kommunikation hittar rätt är det
              centralt att alla uppgifter hålls aktuella. Leverantörsportalen är
              en webbportal dit Thage bjuder in sina partners. Resultatet blir
              en högre kvalitet på kontaktuppgifterna och därför högre
              träffsäkerhet gällande utskickade förfrågningar, offerter med
              mera.
            </p>
          </div>
        </div>
        <div class="contactFooterBox">
          <h3 class="contactFooterTitel">Säljkontakt</h3>
          <p class="contactFooterText">Magnus Nilsson</p>
          <p class="contactFooterText">magnus.nilsson@frontwalker.se</p>
          <p class="contactFooterText">070-4586320</p>
        </div>
        <div class="footer">
        <p class="bottomMidText">Frontwalker AB - Mikael Strömgren</p>
        </div>
      </div>
    </div>
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
        margin: [0, 0, 0, 0],
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

.wrapper {
  margin-top: 20px;
}

#pdf {
  width: 210mm;
  margin-right: auto;
  margin-left: auto;
}

#pdfBox {
  margin-right: auto;
  margin-left: auto;
  width: 250mm;
  border-style: solid;
  border-width: 1px;
  margin-bottom: 10vh;
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
  font-size: 13px;
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

.fokusBox {
  width: 50%;
  margin-left: 40px;
  margin-top: 25px;
}

.fokusTitel {
  color: #006d86;
  border-bottom: solid;
  border-width: 1px;
}

.fokusRoll {
  margin-top: 10px;
}

.fokusFöretag {
  margin-top: 5px;
  font-size: 13px;
  margin-bottom: 3px;
}

.förstaStycke {
  margin-top: 0px;
}

.contactFooterBox {
  background: #006d86;
  color: white;
  border-style: none;
  border-radius: 10px;
  width: 25%;
  padding-left: 8px;
  padding-top: 10px;
  padding-bottom: 10px;
  margin-left: auto;
  margin-right: 40px;
}

.contactFooterTitel {
  font-size: 18px;
}

.contactFooterText {
  font-size: 10px;
  font-weight: lighter;
  margin-bottom: 3px;
}

.footer {
  margin-top: 20px;
}

.bottomMidText {
  text-align: center;
  color: black;
}
</style>
