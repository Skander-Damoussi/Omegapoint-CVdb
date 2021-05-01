<template>
  <div>
    <div class="row">
      <div class="icon-click" v-on:click="editMethod()">
        <div class="row">
          <i
            class="fas fa-edit fa-2x"
            v-on:click="editMethod()"
            title="Tryck för att redigera"
          ></i>
          <p>Redigera innehåll</p>
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
    <div class="wrapper row">
      <div class="editBox">
        <div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(1)">
              <div class="rownomargin">
                <p>
                  Allmänt
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 1">
              <div class="">
                <p>Företagsnamn</p>
                <input type="text" class="textInput" v-model="company_name" />
                <p class="inputSpace">Färgkod</p>
                <div class="row">
                  <input type="text" class="textInput" v-model="color" />
                  <i
                    class="fas fa-sync iconInputSpace"
                    v-on:click="editMethod()"
                    title="Tryck för att redigera"
                  ></i>
                </div>
              </div>
            </div>
          </div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(2)">
              <div class="rownomargin">
                <p>
                  Företagslogo
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 2">
              <div class="">
                <p>Ladda upp ny</p>
                <input
                  class="textInput"
                  type="file"
                  name="avatar"
                  accept="image/png, image/jpeg"
                />
              </div>
            </div>
          </div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(3)">
              <div class="rownomargin">
                <p>
                  Kontakt
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 3">
              <div class="">
                <p>Mobilnummer</p>
                <input
                  type="text"
                  class="textInput"
                  v-model="contact_phoneNumber"
                />
                <p class="inputSpace">Hemsida</p>
                <input
                  type="text"
                  class="textInput"
                  v-model="contact_website"
                />
                <p class="inputSpace">Email</p>
                <input type="text" class="textInput" v-model="contact_email" />
              </div>
            </div>
          </div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(4)">
              <div class="rownomargin">
                <p>
                  Profilbild
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 4">
              <div class="">
                <p>Ladda upp ny</p>
                <input
                  class="textInput"
                  type="file"
                  name="avatar"
                  accept="image/png, image/jpeg"
                />
              </div>
            </div>
          </div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(5)">
              <div class="rownomargin">
                <p>
                  Namn och Roll
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 5">
              <div class="">
                <p>Titel</p>
                <input type="text" class="textInput" v-model="consult_name" />
                <div class="row">
                  <p class="inputSpace">Roll</p>
                  <div @click="role_freeEdit = !role_freeEdit">
                    <i
                      class="fas fa-edit iconInputSpace inputSpace"
                      title="Tryck för att redigera"
                    ></i>
                  </div>
                </div>
                <input
                  type="text"
                  class="textInput"
                  v-model="consult_role"
                  v-if="role_freeEdit"
                />
                <div class="checkboxDiv row textInput" v-if="!role_freeEdit">
                  <select name="role" v-model="consult_role">
                    <option
                      v-for="(option, index) in consult_role_options"
                      :key="index"
                      >{{ option }}</option
                    >
                  </select>
                </div>
              </div>
            </div>
          </div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(6)">
              <div class="rownomargin">
                <p>
                  Introtext
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 6">
              <div class=""></div>
              <div
                class="wrapper"
                v-for="(obj, index) in consult_presentations_options"
                :key="index"
              >
                <div class="title" @click="obj.show = !obj.show">
                  <div class="rownomargin">
                    <p>
                      {{ obj.title }}
                    </p>
                    <p class="stickright"></p>
                  </div>
                </div>
                <div class="container2" id="container" v-if="obj.show">
                  <div class="">
                    <li
                      id="inboxText"
                      v-for="(list, index) in obj.paragraph"
                      :key="index"
                    >
                      {{ list }}
                    </li>

                    <button
                      @click="consult_presentations = obj.paragraph"
                      class=""
                    >
                      Välj
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(7)">
              <div class="rownomargin">
                <p>
                  Uppdrag i fokus
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 7">
              <div class=""></div>
            </div>
          </div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(8)">
              <div class="rownomargin">
                <p>
                  Säljkontakt
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 8">
              <div class="">
                <p>Namn</p>
                <input type="text" class="textInput" v-model="sale_name" />
                <p class="inputSpace">Email</p>
                <input type="text" class="textInput" v-model="sale_email" />
                <p class="inputSpace">Mobilnummer</p>
                <input type="text" class="textInput" v-model="sale_phone" />
              </div>
            </div>
          </div>
          <div class="wrapper">
            <div class="title" @click="toggleTabs(9)">
              <div class="rownomargin">
                <p>
                  Övriga uppdrag
                </p>
                <p class="stickright"></p>
              </div>
            </div>
            <div class="container" id="container" v-if="show === 9">
              <div class="">
                <p>ddd</p>
                <textarea name="" id="" cols="30" rows="10"></textarea>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="wrapperPdfbox">
        <div id="pdfBox">
          <div id="pdf" ref="document">
            <div class="row">
              <img src="../assets/templogo.png" height="23px" class="logo" />
              <div class="contactdiv">
                <p class="contact contactTitel">{{ this.company_name }}</p>
                <p class="contact">{{ this.contact_phoneNumber }}</p>
                <p class="contact">{{ this.contact_website }}</p>
                <p class="contact">{{ this.contact_email }}</p>
              </div>
            </div>

            <div class="row">
              <img src="../assets/temp.png" height="150px" class="selfie" />
              <div class="cvTitelDiv">
                <h1 class="cvTitel">{{ consult_name }}</h1>
                <p class="cvTitelRoll">{{ consult_role }}</p>
              </div>
            </div>
            <div class="introstycke">
              <p
                class="stycke"
                v-for="(text, index) in consult_presentations"
                :key="index"
              >
                {{ text }}
              </p>
            </div>
            <div class="fokusBox">
              <h3 class="fokusTitel">Uppdrag i fokus</h3>
              <div>
                <h4 class="fokusRoll">Utvecklare</h4>
                <p class="fokusFöretag">Thage i Skåne AB</p>
                <p class="stycke förstaStycke">
                  Thage vill skicka viktig kommunikation till sina
                  samrbetspartners. För att säkerställa att sådan kommunikation
                  hittar rätt är det centralt att alla uppgifter hålls aktuella.
                  Leverantörsportalen är en webbportal dit Thage bjuder in sina
                  partners. Resultatet blir en högre kvalitet på
                  kontaktuppgifterna och därför högre träffsäkerhet gällande
                  utskickade förfrågningar, offerter med mera.
                </p>
              </div>
            </div>
            <div class="contactFooterBox">
              <h3 class="contactFooterTitel">Säljkontakt</h3>
              <p class="contactFooterText">{{ sale_name }}</p>
              <p class="contactFooterText">{{ sale_email }}</p>
              <p class="contactFooterText">{{ sale_phone }}</p>
            </div>
            <div class="footer">
              <p class="bottomMidText">
                {{ company_name }} - {{ consult_name }}
              </p>
            </div>
          </div>
        </div>
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
    return {
      show: 0,
      company_name: "OmegaPoint AB",
      color: "#006d86",
      company_logo: "",
      contact_phoneNumber: "0702-232325",
      contact_website: "www.omegapoint.se",
      contact_email: "magnus.nilsson@omegapoint.se",
      consult_picture: "",
      consult_name: "Magnus Nilsson",
      consult_role: "Utvecklare",
      consult_role_options: [],
      consult_presentations: [
        "I grunden utbildad interaktionsdesigner, men har i min yrkesroll jobbat väldigt brett inom webb. Jag i alla de olika aspekterna av webben, både design och teknik, men även det sociala och affärsmässiga. Har under mina 10+ år i branschen bidragit med affärsnytta till både stora och små kunder och upphör aldrig att fascineras av den stora skillnad som bra webb kan göra. Minadrivkrafter är nyfikenhet och kunskapstörst.",
        "Av omgivningen ses jag som ödmjuk och prestigelös. Jag trivs med att verka i en öppen miljö, där jag inspireras av duktiga kollegor, och utbyta kunskaper och erfarenheter. Upplevs som tydlig, kunnig och hjälpsam av kunder.",
        "Mikaels erfarenheter inom webbutveckling har gett honom en bred kompetens som kommer väl till pass i de flest projekt. En vana av att ta projekt från ax till limpa med bra förmåga att kommunicer, planera, dokumentera och utveckla.",
      ],
      consult_presentations_options: [],
      sale_name: "Nicklas Söderberg",
      sale_email: "nicklas.soderberg@omegapoint.se",
      sale_phone: "0702-624556",
      role_freeEdit: false,
    };
  },
  created() {
    var temp = this.$store.getters.getUserExperience;
    var i;
    for (i = 0; i < temp.length; i++) {
      for (var ii = 0; ii < temp[i].role.length; ii++) {
        this.consult_role_options.push(temp[i].role[ii]);
      }
    }

    temp = this.$store.getters.getUserPresentation;
    for (i = 0; i < temp.length; i++) {
      this.consult_presentations_options.push(temp[i]);
    }
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
    toggleTabs(input) {
      if (input === this.show) {
        this.show = 0;
      } else {
        this.show = input;
      }
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
  width: 200px;
  margin-left: auto;
  margin-right: auto;
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

.exportPDF {
  text-align: center;
}

.editBox {
  width: 40vw;
  margin-left: auto;
  margin-right: auto;
}

.wrapperPdfbox {
  margin-left: auto;
  margin-right: auto;
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

li {
  list-style-type: none;
  margin-bottom: 25px;
}

input[type="text"],
select {
  padding: 6px 10px;
  display: inline-block;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
  max-width: 15vw;
}

input[type="text"]:focus {
  border: 1px solid #555;
}

.textInput {
  width: 250px;
  margin-top: 4px;
}

.inputSpace {
  margin-top: 15px;
}

.iconInputSpace {
  margin-left: 10px;
}

th {
  font-size: 15px;
  font-weight: normal;
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

.rownomargin {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
}

.container {
  padding: 15px;
}

.container2{
  padding: 15px;
  width: 500px;
}

.center {
  margin-left: auto;
}

.wrapper {
  border-radius: 2px;
  border: 2px solid #f7f7f7;
  margin: auto;
}

.stickright {
  margin-left: auto;
  margin-right: auto;
}
</style>
