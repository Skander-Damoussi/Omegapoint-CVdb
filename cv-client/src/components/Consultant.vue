<template>
  <div>
    <div id="main">
      <div id="top">
        <p class="icon-click" v-on:click="editMethod()">
          <i
            class="btn fas fa-edit fa-2x"
            v-on:click="editMethod()"
            title="Tryck för att redigera"
          ></i>
        </p>

        <p class="icon-click" v-on:click="toggleExport()">
          <i
            class="btn fas fa-file-export fa-2x"
            @click="toggleExport()"
            title="Tryck för att exportera"
          ></i>
        </p>

        <div id="export" v-if="exportMenu">
          <p>Välj ett exporteringformat</p>
          <a href>Exportera PDF</a>
          <a href>Exportera WORD</a>
        </div>
      </div>
      <div id="wrapper">
        <div id="left">
          <h4>Presentation</h4>

          <p>
            XXXXXX är engagerad, har en unik analytisk förmåga och en smittsam
            öppen personlighet. Han är myckt bra på särskilt svåra problem som
            kräver mycket tanke och att samarbeta i dessa situationer. XXXXXX
            bryr sig om att resultatet är bra och han har en ständig drivkraft
            för att förbättra och utveckla sina yrkeskunskaper. Han tar till sig
            ny kunskap snabbt och är mannen att gå till när du behöver hjälp.
            XXXXXX kan förstå och lösa komplexa och abstrakta problem.
          </p>

          <p>
            Han är mycket tekniskt skicklig och med ett lugn driver han
            utvecklingen framåt. Han är grundlig, effektiv och skicklig. Både i
            komplexa situationer och de som börjar känna sig omöjliga kommer
            XXXXXX att hitta nya idéer och förslag till lösningar på problemet.
            XXXXXX är bra på att förstår och lösa problem och kommunicera
            teknisk information på ett enkelt sätt för personer med liten eller
            ingen teknisk erfarenhet. Han inspirerar när han går utanför boxen
            och gör mer än vad som förväntas av honom. XXXXXX har en hög
            arbetsmoral och är en härlig kollega som inte är rädd för att öppna
            sig.
          </p>
        </div>

        <div id="right">
          <div id="right-top">
            <h4>Teknisk profil</h4>

            <b>Administrations Software</b>
            <p>
              Active Directory, Microsoft Exchange, Office 365 Admin Portal,
              Azure AD
            </p>

            <b>Programmeringsspråk och CMS</b>
            <p>
              C#, Javascript, C++, Perl, Bash, PowerShell, TurboPascal,
              EpiServer, SiteCore
            </p>

            <b>Editors och IDE</b>
            <p>
              Visual Studio, UltraEdit, Powershell ISE, MS SQL Server,
              Management Studio
            </p>

            <b>Version kontroll system</b>
            <p>Git, SVN, Bitbucket, Github, Sourcesafe</p>

            <b>Databaser</b>
            <p>MSSQL, MySQL, SQLite</p>

            <b>Operativ System</b>
            <p>Windows Server, Windows, Linux</p>

            <b>Monitorering</b>
            <p>OP5, kinga, Nagios, TMArt</p>

            <b>Dokumentation</b>
            <p>Mediawiki, Confluence</p>

            <b>Virtualisering</b>
            <p>VMWare, Virtualbox</p>

            <b>Webbserver</b>
            <p>NGINX, IIS, Apache</p>

            <b>Cloud Plattformar</b>
            <p>Azure, vCloud Director</p>
          </div>

          <div id="right-mid">
            <h4>Roller</h4>
            <p>
              Systemutvecklare, Webbutvecklare, Applikationsutvecklare,
              Integratör
            </p>
          </div>

          <div id="right-bottom">
            <h4>Certifieringar</h4>
            <p>2014 ITIL-Foundation</p>
          </div>
        </div>
      </div>

      <div id="bottom">
        <h4>Urval av aktuella uppdrag</h4>
        <div id="bottom-wrapper">
          <b>SiteCore utvecklare- 24hr</b>

          <b>2020-2021</b>
        </div>
      </div>

      <footer>
        <div id="footer-left">
          <p>Omegapoint AB</p>
          <p>www.omegapoint.se</p>
        </div>

        <div id="footer-middle">
          <p>Adelgatan 6</p>
          <p>SE-211 22 Malmö</p>
        </div>

        <div id="footer-right">
          <img src="../assets/omegapoint.png" />
        </div>
      </footer>
    </div>
  </div>
</template>

<script>
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

#main {
  background: white;
  justify-content: center;
  display: flex;
  flex-direction: column;
  border: 1px;
  border-color: grey;
  border-style: solid;
  margin-top: 5%;
  margin-left: 25%;
  margin-right: 25%;
  margin-bottom: 5%;
}

#top {
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  position: relative;
}

/* #top > button {
  width: 8vw;
  margin: 2px;
} */

.btn {
  margin: 2px;
  /* width: 20px; */
}

.icon-click {
  cursor: pointer;
}

/* #edit-button {
} */

#export {
  position: absolute;
  z-index: 1;
  background: white;
  display: flex;
  flex-direction: column;
  border: 1px solid grey;
  border-radius: 10px;
  right: -12vw;
  text-align: left;
  height: auto;
}

#export > p {
  font-size: 8px;
  margin: 5px;
}
#export > a {
  font-size: 10px;
  margin: 5px;
  text-decoration: none;
  color: black;
  padding: 2px;
}
#export > a:hover {
  color: grey;
  border: 1px solid grey;
  border-radius: 10px;
  padding: 1px;
}

#wrapper {
  justify-content: center;
  display: flex;
  flex-direction: row;
}

#left {
  background: white;
  width: 35%;
  margin: 20px 10px 10px 10px;
  font-size: 12px;
}

#left > p {
  margin-top: 10px;
}

#right {
  display: flex;
  flex-direction: column;
  width: 20%;
  margin: 20px 10px 10px 10px;
}

#right > div > p {
  font-size: 8px;
  margin: 0;
}
#right > div > b {
  font-size: 8px;
  margin: 0;
}

#right > div > h4 {
  font-size: 12px;
  margin-bottom: 0px;
}

#right-top {
  background: white;
  margin: 0px 10px 10px 10px;
}

#right-mid {
  margin: 10px 10px 10px 10px;
}

#right-bottom {
  margin: 10px 10px 10px 10px;
}

#bottom {
  width: 70%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  margin-left: 21%;
  margin-top: 5%;
}

#bottom-wrapper {
  display: flex;
  flex-direction: row;
}

#bottom > h4 {
  font-size: 12px;
  margin-bottom: 12px;
}

#bottom-wrapper > b {
  font-size: 12px;
}

#bottom-wrapper > b:nth-child(2) {
  margin-left: 35%;
}

footer {
  position: center;
  display: flex;
  margin-top: 10%;
  margin-left: 18%;
  margin-right: 5%;
  margin-bottom: 3%;
}

footer > div > p {
  font-size: 8px;
}

footer > div:nth-child(2) {
  margin-left: 30%;
}
footer > div:nth-child(3) {
  margin-left: 10%;
}

#footer-right {
  width: 10%;
}

h4 {
  color: GREY;
  font-size: 12px;
}

p {
  font-size: 12px;
}
</style>
