<template>
  <div>
    <h1>Consultant</h1>

    <div id="main">
      <div id="left">
        <h4>Presentation</h4>

        <p>
          XXXXXX är engagerad, har en unik analytisk förmåga och en smittsam
          öppen personlighet. Han är myckt bra på särskilt svåra problem som
          kräver mycket tanke och att samarbeta i dessa situationer. XXXXXX bryr
          sig om att resultatet är bra och han har en ständig drivkraft för att
          förbättra och utveckla sina yrkeskunskaper. Han tar till sig ny
          kunskap snabbt och är mannen att gå till när du behöver hjälp. XXXXXX
          kan förstå och lösa komplexa och abstrakta problem.
        </p>

        <p>
          Han är mycket tekniskt skicklig och med ett lugn driver han
          utvecklingen framåt. Han är grundlig, effektiv och skicklig. Både i
          komplexa situationer och de som börjar känna sig omöjliga kommer
          XXXXXX att hitta nya idéer och förslag till lösningar på problemet.
          XXXXXX är bra på att förstår och lösa problem och kommunicera teknisk
          information på ett enkelt sätt för personer med liten eller ingen
          teknisk erfarenhet. Han inspirerar när han går utanför boxen och gör
          mer än vad som förväntas av honom. XXXXXX har en hög arbetsmoral och
          är en härlig kollega som inte är rädd för att öppna sig.
        </p>
      </div>

      <div id="right">
        <div id="right-top">
          <h4>Teknisk profil</h4>

          <b>Administrations Software</b>
          <p>
            Active Directory, Microsoft Exchange, Office 365 Admin Portal, Azure
            AD
          </p>

          <b>Programmeringsspråk och CMS</b>
          <p>
            C#, Javascript, C++, Perl, Bash, PowerShell, TurboPascal, EpiServer,
            SiteCore
          </p>

          <b>Editors och IDE</b>
          <p>
            Visual Studio, UltraEdit, Powershell ISE, MS SQL Server, Management
            Studio
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
          <p>Systemutvecklare, Webbutvecklare,
        Applikationsutvecklare, Integratör</p>
        </div>

        <div id="right-bottom">
          <h4>Certifieringar</h4>
          <p>2014 ITIL-Foundation</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Consultant",
};
</script>

<style scoped>
*{
    /* all */
    /* font-size: 12px; */
}

div {
  text-align: left;
}

#main {
  background: white;
  justify-content: center;
  display: flex;
  flex-direction: row;
  border: 1px;
  border-color: grey;
  border-style: solid;
  margin-left: 80px;
  margin-right: 80px;
}

#left {
  background: white;
  width: 35%;
  border: 2px;
  border-color: grey;
  margin: 10px 10px 10px 10px;
  font-size: 12px;
}

#right {
  display: flex;
  flex-direction: column;
  width: 20%;
  margin: 10px 10px 10px 10px;

}

#right>div>p{
      font-size: 8px;
      margin:0;
}
#right>div>b{
    font-size: 8px;
    margin:0;
}

#right>div>h4{
      font-size: 12px;
      margin-bottom: 0;
}


#right-top {
  background:white;
  margin: 0px 10px 10px 10px;

}

#right-mid {

  margin: 10px 10px 10px 10px;

}

#right-bottom {

  margin: 10px 10px 10px 10px;

}

H4{
    color: GREY;
}

p {

}
</style>