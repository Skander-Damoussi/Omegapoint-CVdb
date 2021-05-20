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
          <p class="marginleftIcon">Redigera innehåll</p>
        </div>
      </div>
      <div class="icon-clickmiddle" v-on:click="saveMethod()">
        <div class="row">
          <i
            class="fas fa-save fa-2x"
            v-on:click="saveMethod()"
            title="Tryck för att redigera"
          ></i>
          <p class="marginleftIcon">Spara CV</p>
        </div>
      </div>
      <div class="icon-click row" v-on:click="exportToPDF">
        <i
          class="btn fas fa-file-export fa-2x"
          title="Tryck för att exportera"
        ></i>
        <p class="marginleftIcon">Exportera till PDF</p>
      </div>
      <!-- cvdocx -->
        <div class="icon-click row" v-on:click="exportToDocx">
        <i
          class="btn fas fa-file-export fa-2x"
          title="Tryck för att exportera"
        ></i>
        <p class="marginleftIcon">Exportera till Word</p>
      </div>
            <!-- exdocxList -->
      <div id="exDocx" v-if="exDocxToggle">
      <div class="list-table">
      <table>
        <p>Välj mall</p>
        <tr v-for="(i, index) in cvTempList" :key="index">
          <th title="exportera till Word" v-on:click="clickCvDocx(i.stringId)">{{ i.name }}</th>
          </tr>
          <!-- <th>
            <p class="icon-click" v-on:click="showCvTemp(i.stringId)">
          </th>
        </tr> -->        
      </table>
    </div>
      </div>
      <!-- cvdocx -->
      <div class="row icon-clickHome" v-on:click="goManagerHome">
        <i
          class="btn fas fa-home fa-2x"
          title="Hem"
        ></i>
        <p class="marginleftIcon marginright">Hem</p>
      </div>
    </div>
    <div class="wrapper row">
      <div class="editBox mainwrapper">
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
                  accept="image/png, image/jpg, image/jpeg"
                  ref="logo"
                  @change="PreviewLogo"
                />
              </div>
              <div class="errorMessage" v-if="errorLogo">
                <p>{{ errorMessage }}</p>
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
                  accept="image/png, image/jpg, image/jpeg"
                  ref="consult_pic"
                  @change="PreviewProfilePic"
                />
              </div>
              <div class="errorMessage" v-if="error">
                <p>{{ errorMessage }}</p>
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
                <p>Namn</p>
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
              <div v-if="consult_presentations_options.length === 0" class="">
                <p>
                  <button @click="AddClickPresentation()" id="nomargin">
                    Lägg till <i class="fas fa-plus"></i>
                  </button>
                  Var vänlig lägg till presentationer.
                </p>
              </div>
              <div
                class="wrapper"
                v-for="(obj, Arrindex) in consult_presentations_options"
                :key="Arrindex"
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

                    <button
                      @click="EditClickPresentation(Arrindex)"
                      class="editButtonIntroText"
                    >
                      Redigera
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
              <div v-if="consult_experience_options.length > 0" class="">
                <p>
                  Välj ett uppdrag i fokus
                </p>
                <select
                  name="focus"
                  v-model="consult_experience_focus_title"
                  @change="setFocusExperience(consult_experience_focus_title)"
                >
                  <option
                    v-for="(option, index) in consult_experience_options"
                    :key="index"
                    >{{ option.title }}</option
                  >
                </select>
                <button
                  v-if="consult_experience_focus_title != ''"
                  @click="EditClickExperience(consult_experience_focus)"
                  class="editButtonIntroText"
                >
                  Redigera
                </button>
                <div v-if="consult_experience_focus_title != ''">
                  <p class="textInput">Välj en roll</p>
                  <select
                    name="rolefocus"
                    v-model="consult_experience_focus_role"
                  >
                    <option
                      v-for="(option, index) in consult_experience_focus.role"
                      :key="index"
                      >{{ option }}</option
                    >
                  </select>
                  <p class="textInput">Välj en uppdragsbeskrivning</p>
                  <div
                    class="focusDescription"
                    v-for="(assignments,
                    index) in consult_experience_focus.assignments"
                    :key="index"
                    @click="consult_experience_focus_description = assignments"
                  >
                    {{ assignments }}
                  </div>
                </div>
              </div>
              <div v-else>
                <p>
                  <button @click="AddClick()" id="nomargin">
                    Lägg till <i class="fas fa-plus"></i>
                  </button>
                  Var vänlig lägg till uppdrag.
                </p>
              </div>
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
              <div v-if="consult_experience_options.length === 0">
                <p>
                  <button @click="AddClick()" id="nomargin">
                    Lägg till <i class="fas fa-plus"></i>
                  </button>
                  Var vänlig lägg till uppdrag.
                </p>
              </div>
              <div
                class="row"
                v-for="(obj, index) in consult_experience_options"
                :key="index"
              >
                <div class="rownomargin">
                  <input
                    type="checkbox"
                    @change="handleOtherList(index)"
                    v-model="consult_experience_other[index].isChecked"
                  />
                  <p class="stickright"></p>
                </div>
                <div class="title row barwidth" @click="obj.show = !obj.show">
                  <div class="rownomargin">
                    <p>
                      {{ obj.title }}
                    </p>
                    <p class="stickright"></p>
                  </div>
                </div>
                <div class="container3" id="container" v-if="obj.show">
                  <div class="">
                    <div>
                      <p class="textInput">Välj en roll</p>
                      <select
                        name="rolefocus"
                        v-model="consult_experience_other[index].role"
                        @change="refreshVars()"
                      >
                        <option
                          v-for="(option, index) in obj.role"
                          :key="index"
                          >{{ option }}</option
                        >
                      </select>
                      <p class="textInput textSpaceUpper">
                        Välj en uppdragsbeskrivning
                      </p>
                      <div
                        class="focusDescription"
                        v-for="(assignments, indexx) in obj.assignments"
                        :key="indexx"
                        @click="
                          consult_experience_other[
                            index
                          ].description = assignments;
                          refreshVars();
                        "
                      >
                        {{ assignments }}
                      </div>
                    </div>
                    <button
                      @click="EditClickExperience(obj)"
                      class="editButtonIntroText textSpaceUpper"
                    >
                      Redigera
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="wrapperPdfbox mainwrapper">
        <div class="row centerrow">
          <div @click="page--">
            <i class="fas fa-arrow-left icon-click-next"></i>
          </div>
          <p>Sida {{ this.page }}</p>
          <div @click="page++">
            <i class="fas fa-arrow-right icon-click-next"></i>
          </div>
        </div>
        <div id="pdfBox">
          <div v-if="page === 1" id="pdf" ref="document">
            <div class="row">
              <img :src="company_logo" alt="" height="53px" class="logo" />
              <div class="contactdiv">
                <p class="contact contactTitel">{{ this.company_name }}</p>
                <p class="contact">{{ this.contact_phoneNumber }}</p>
                <p class="contact">{{ this.contact_website }}</p>
                <p class="contact">{{ this.contact_email }}</p>
              </div>
            </div>

            <div class="row">
              <img
                :src="consult_picture"
                height="200px"
                alt=""
                v-if="consult_picture"
                class="selfie"
              />
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
            <div v-if="consult_experience_focus.length != 0" class="fokusBox">
              <h2 class="fokusTitel">Uppdrag i fokus</h2>
              <div>
                <h3 class="fokusRoll justify-left">
                  {{ consult_experience_focus_role }}
                </h3>
                <p
                  v-if="consult_experience_focus.endDate === ''"
                  class="fokusFöretag"
                >
                  {{ consult_experience_focus_title }}
                  <span class="smallText"
                    >{{ consult_experience_focus.startDate }} - Pågående</span
                  >
                </p>
                <p v-else class="fokusFöretag">
                  {{ consult_experience_focus_title }}
                  <span class="smallText">
                    {{ consult_experience_focus.startDate }} -
                    {{ consult_experience_focus.endDate }}
                  </span>
                </p>
                <p class="stycke förstaStycke">
                  {{ consult_experience_focus_description }}
                </p>
              </div>
            </div>
            <div
              v-if="sale_name !== null && sale_name !== ''"
              class="contactFooterBox"
            >
              <h3 class="contactFooterTitel">Säljkontakt</h3>
              <p class="contactFooterText">{{ sale_name }}</p>
              <p class="contactFooterText">{{ sale_email }}</p>
              <p class="contactFooterText">{{ sale_phone }}</p>
            </div>
            <div v-if="company_name !== null" class="footer">
              <p class="bottomMidText">
                {{ company_name }}
                <span v-if="consult_name !== null && consult_name !== ''"
                  >-</span
                >
                {{ consult_name }}
              </p>
            </div>
          </div>
          <div v-if="page === 2" id="pdf" ref="document">
            <h3
              v-if="consult_experience_other_list.length > 0"
              class="tidigareTitel justify-left"
            >
              Tidigare projekt och uppdrag
            </h3>
            <div
              class="blobspace"
              v-for="(obj, index) in consult_experience_other_list"
              :key="index"
            >
              <div v-if="getInfoByID('checked', obj.id)" class="row">
                <div class="leftbox">
                  <h4>{{ getInfoByID("title", obj.id) }}</h4>
                  <p
                    v-if="getInfoByID('endDate', obj.id) === ''"
                    class="lightText"
                  >
                    {{ getInfoByID("startDate", obj.id) }} - Pågående
                  </p>
                  <p v-else class="lightText">
                    {{ getInfoByID("startDate", obj.id) }} -
                    {{ getInfoByID("endDate", obj.id) }}
                  </p>
                </div>
                <div class="rightbox">
                  <h3 class="justify-left">{{ obj.role }}</h3>
                  <p class="lighterBreadText">
                    {{ obj.description }}
                  </p>
                  <div class="row blobspace">
                    <p
                      class="blob"
                      v-for="blob in getInfoByID('software', obj.id)"
                      :key="blob"
                    >
                      {{ blob }}
                    </p>
                    <p
                      class="blob"
                      v-for="blob in getInfoByID('language', obj.id)"
                      :key="blob"
                    >
                      {{ blob }}
                    </p>
                  </div>
                </div>
              </div>
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
  props: ["userID"],

  data() {
    return {
      show: 0,
      page: 1,
      company_name: "",
      color: "",
      company_logo: "",
      contact_phoneNumber: "",
      contact_website: "",
      contact_email: "",
      consult_picture: [],
      consult_name: "",
      consult_experience_focus_title: "",
      consult_experience_focus_role: "",
      consult_experience_focus_description: "",
      consult_experience_focus: [],
      consult_experience: [],
      consult_experience_options: [],
      consult_role: "",
      consult_role_options: [],
      consult_presentations: [],
      consult_presentations_options: [],
      consult_experience_other: [],
      consult_experience_other_list: [],
      sale_name: "",
      sale_email: "",
      sale_phone: "",
      role_freeEdit: false,
      showUserID: '',
      errorMessage: "",
      error: false,
      errorLogo: false,
      // docxToggle
      exDocxToggle:false
    };
  },
    computed: {
    cvTempList() {
      return this.$store.getters.getCvTempList;
    }
    },
  async mounted() {
    if (this.userID == null) {
      this.showUserID = await this.$store.getters.getLoggedInUser.id;
    } else {
      this.showUserID = this.userID;
    }
    await this.$store.dispatch("getUserExperience", this.showUserID);
    await this.$store.dispatch("getUserPresentation", this.showUserID);
    await this.$store.dispatch("getCV", this.showUserID);
    //Cvtemplatelist
    await this.$store.dispatch("getCvTempList");
    let cv = await this.$store.getters.getCV;

    this.company_name = cv.company_name;
    this.color = cv.color;
    this.company_logo = cv.company_logo;
    this.contact_phoneNumber = cv.contact_phoneNumber;
    this.contact_website = cv.contact_website;
    this.contact_email = cv.contact_email;
    this.consult_picture = cv.consult_picture;
    this.consult_role = cv.consult_role;
    this.consult_presentations = cv.consult_presentations;
    this.consult_name = cv.consult_name;
    this.consult_experience_focus_title = cv.consult_experience_focus_title;
    this.consult_experience_focus_role = cv.consult_experience_focus_role;
    this.consult_experience_focus_description =
      cv.consult_experience_focus_description;
    this.sale_name = cv.sale_name;
    this.sale_email = cv.sale_email;
    this.sale_phone = cv.sale_phone;
    if (cv.consult_experience_other_list != null) {
      this.consult_experience_other_list = cv.consult_experience_other_list;
    }
    var temp = this.$store.getters.getUserExperience;
    this.consult_experience_options = temp;
    var i;
    var ii;
    for (i = 0; i < temp.length; i++) {
      this.consult_experience_other.push({
        isChecked: false,
        role: "",
        description: "",
        id: this.consult_experience_options[i].id,
      });
      for (ii = 0; ii < temp[i].role.length; ii++) {
        this.consult_role_options.push(temp[i].role[ii]);
      }
    }

    for (i = 0; i < this.consult_experience_other.length; i++) {
      for (ii = 0; ii < this.consult_experience_other_list.length; ii++) {
        if (
          this.consult_experience_other[i].id ===
          this.consult_experience_other_list[ii].id
        ) {
          this.consult_experience_other[i] = this.consult_experience_other_list[
            ii
          ];
        }
      }
    }

    temp = this.$store.getters.getUserPresentation;
    for (i = 0; i < temp.length; i++) {
      this.consult_presentations_options.push(temp[i]);
    }

    this.setFocusExperience(this.consult_experience_focus_title);
  },
  created() {
    if (
      this.$store.getters.getLoggedInUser.role === "Konsultchef" &&
      typeof this.userID === "undefined"
    ) {
      this.$router.push({ name: "ConsultantManager" });
    }
  },
  methods: {
    getInfoByID(what, id) {
      for (var i = 0; i < this.consult_experience_options.length; i++) {
        if (this.consult_experience_options[i].id === id) {
          var temp = this.consult_experience_options[i];
          switch (what) {
            case "title":
              return temp.title;
            case "startDate":
              return temp.startDate;
            case "endDate":
              return temp.endDate;
            case "software":
              return temp.software;
            case "language":
              return temp.language;
            case "checked":
              return this.consult_experience_other[i].isChecked;
          }
          break;
        }
      }
    },
    AddClick() {
      this.$router.push({ name: "ConsultantExperienceEdit" });
    },
    PreviewProfilePic(e) {
      this.error = false;
      let files = e.target.files;
      if (files.length === 0) {
        return;
      }
      let holder = this.consult_picture;
      let reader = new FileReader();
      reader.onload = e => {
        this.consult_picture = e.target.result;
        let string = this.consult_picture.split(",");
        if (
          string[0].includes("jpeg") ||
          string[0].includes("jpg") ||
          string[0].includes("png")
        ) {
          console.log("jpeg, jpg, png");
        } else {
          this.errorMessage = "Tillåtna filformat: .jpg, .jpeg och .png";
          this.error = true;
          this.consult_picture = holder;
        }
      };
      reader.readAsDataURL(files[0]);
    },
    PreviewLogo(e) {
      this.error = false;
      let files = e.target.files;
      if (files.length === 0) {
        return;
      }
      let holder = this.company_logo;
      let reader = new FileReader();
      reader.onload = e => {
        this.company_logo = e.target.result;
        let string = this.company_logo.split(",");
        if (
          string[0].includes("jpeg") ||
          string[0].includes("jpg") ||
          string[0].includes("png")
        ) {
          console.log("jpeg, jpg, png");
        } else {
          this.errorMessage = "Tillåtna filformat: .jpg, .jpeg och .png";
          this.errorLogo = true;
          this.company_logo = holder;
        }
      };
      reader.readAsDataURL(files[0]);
    },
    goManagerHome() {
      this.$router.push({ name: "ConsultantManager" });
    },
    AddClickPresentation() {
      this.$router.push({ name: "ConsultantPresentationEdit" });
    },
    refreshVars() {
      for (var i = 0; i < this.consult_experience_other_list.length; i++) {
        for (var ii = 0; ii < this.consult_experience_other.length; ii++) {
          if (
            this.consult_experience_other_list[i].id ===
            this.consult_experience_other[ii].id
          ) {
            this.consult_experience_other_list[
              i
            ].description = this.consult_experience_other[ii].description;
            this.consult_experience_other_list[
              i
            ].role = this.consult_experience_other[ii].role;
            this.consult_experience_other_list[
              i
            ].isChecked = this.consult_experience_other[ii].isChecked;
            continue;
          }
        }
      }
    },
    handleOtherList(index) {
      if (this.consult_experience_other[index].isChecked) {
        this.consult_experience_other_list.push(
          this.consult_experience_other[index]
        );
      } else {
        for (var i = 0; i < this.consult_experience_other_list.length; i++) {
          if (
            this.consult_experience_other_list[i].id ===
            this.consult_experience_other[index].id
          ) {
            this.consult_experience_other_list.splice(i, 1);
            break;
          }
        }
      }
    },
    setFocusExperience(input) {
      for (var i = 0; i < this.consult_experience_options.length; i++) {
        if (this.consult_experience_options[i].title === input) {
          this.consult_experience_focus = this.consult_experience_options[i];
          return;
        }
      }
    },
    editMethod() {
      this.$router.push({ name: "ConsultantExperience", params: { userID: this.showUserID } });
    },
    async saveMethod() {
      console.log(this.consult_picture);
      await this.$store.dispatch("cvSave", {
        token: this.$store.getters.getUserToken,
        input: {
          userID: this.showUserID,
          company_name: this.company_name,
          color: this.color,
          company_logo: this.company_logo,
          contact_phoneNumber: this.contact_phoneNumber,
          contact_website: this.contact_website,
          contact_email: this.contact_email,
          consult_picture: this.consult_picture,
          consult_name: this.consult_name,
          consult_role: this.consult_role,
          consult_presentations: this.consult_presentations,
          consult_experience_focus_title: this.consult_experience_focus_title,
          consult_experience_focus_role: this.consult_experience_focus_role,
          consult_experience_focus_description: this
            .consult_experience_focus_description,
          sale_name: this.sale_name,
          sale_email: this.sale_email,
          sale_phone: this.sale_phone,
          consult_experience_other_list: this.consult_experience_other_list,
        },
      });
    },
    exportToPDF() {
      html2pdf(this.$refs.document, {
        margin: [0, 0, 0, 0],
        filename: "document.pdf",
        image: { type: "jpeg", quality: 1 },
        html2canvas: { dpi: 192, letterRendering: true },
        jsPDF: { unit: "mm", format: "a4", orientation: "portrait" },
      });
    },
    // exportDocx
    clickCvDocx(CvTempId)
    {
      //Dispatch, med Id och consultId
      //this.$store.dispatch("getCvTemp", id);
      console.log("cvTempId", CvTempId);
      this.exDocxToggle = !this.exDocxToggle;
    },
    exportToDocx()
    {
       this.exDocxToggle = !this.exDocxToggle;
       console.log("Toggle exDocx", this.exDocxToggle)
    },
    toggleTabs(input) {
      if (input === this.show) {
        this.show = 0;
      } else {
        this.show = input;
      }
    },
    EditClickPresentation(arr) {
      var temp = this.$store.getters.getUserPresentation;
      this.$router.push({
        name: "ConsultantPresentationEdit",
        params: temp[arr],
      });
    },
    EditClickExperience(arr) {
      arr.keepID = this.showUserID;
      this.$router.push({
        name: "ConsultantExperienceEdit",
        params: arr,
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
  height: 297mm;
  margin-right: auto;
  margin-left: auto;
  padding-left: 40px;
  padding-right: 40px;
  padding-top: 20px;
  padding-bottom: 20px;
}

#pdfBox {
  margin-right: auto;
  margin-left: auto;
  width: 250mm;
  border-style: solid;
  border-width: 1px;
  position: relative;
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
  margin-top: 25px;
  margin-left: -25px;
}

.logo {
}

.introstycke {
  width: 75%;
  font-style: normal;
  height: 340px;
  overflow: hidden;
}

.stycke {
  margin-top: 15px;
  font-weight: normal;
}

.contactdiv {
  margin-left: auto;
  text-align: right;
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
  cursor: pointer;
}

.icon-click:hover {
  color: #006166;
}

.icon-clickHome {
  cursor: pointer;
}

.icon-clickHome:hover {
  color: #006166;
}

.icon-click-next {
  margin: 15px;
  cursor: pointer;
}

.icon-click-next:hover {
  color: #006166;
}

.icon-clickmiddle {
  margin: 15px;
  width: 200px;
  cursor: pointer;
}

.icon-clickmiddle:hover {
  color: #006166;
}

.fokusBox {
  width: 65%;
  overflow: hidden;
  height: 260px;
}

.fokusTitel {
  color: #006d86;
  border-bottom: solid;
  border-width: 1px;
  justify-content: left;
}

.fokusRoll {
  margin-top: 10px;
}

.fokusFöretag {
  margin-top: 5px;
  font-size: 16px;
  margin-bottom: 5px;
}

.förstaStycke {
  margin-top: 0px;
}

.contactFooterBox {
  background: #006d86;
  color: white;
  border-style: none;
  border-radius: 10px;
  min-width: 25%;
  padding-left: 8px;
  padding-right: 8px;
  padding-top: 10px;
  padding-bottom: 10px;
  margin-left: auto;
  position: absolute;
  bottom: 10%;
  left: 60%;
  height: 100px;
}

.contactFooterTitel {
  font-size: 18px;
  justify-content: left;
  margin-bottom: 5px;
}

.contactFooterText {
  font-size: 15px;
  font-weight: lighter;
  margin-bottom: 3px;
}

.footer {
  width: 100%;
  margin-top: auto;
  margin-bottom: auto;
  position: absolute;
  bottom: 5%;
  left: 0;
  right: 0;
}

.bottomMidText {
  text-align: center;
  color: black;
}

.exportPDF {
  text-align: center;
}

.editBox {
  width: 25vw;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: auto;
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

input[type="checkbox"] {
  width: 25px;
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

.container2 {
  padding: 15px;
  width: 500px;
}

.container3 {
  padding: 15px;
  width: 500px;
  margin-left: 15px;
}

.textSpaceUpper {
  margin-top: 15px;
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

.mainwrapper {
  margin-top: 10px;
  margin-bottom: auto;
}

.editButtonIntroText {
  margin-left: 15px;
}

.focusDescription {
  margin-top: 10px;
  padding: 10px;
  border-width: 3px;
  border-color: #2185d0;
  border-style: dotted;
}

.focusDescription:hover {
  margin-top: 10px;
  padding: 10px;
  border-width: 3px;
  border-color: black;
  border-style: solid;
  cursor: pointer;
}

.smallText {
  font-weight: bold;
  margin-left: 30%;
}

.tidigareTitel {
  color: #006d86;
  border-bottom: solid;
  border-width: 1px;
  margin-top: 50px;
  margin-bottom: 20px;
}

.justify-left {
  justify-content: left;
}

.leftbox {
  width: 25%;
  margin-bottom: auto;
}

.rightbox {
  width: 66%;
}

.marginright{
  margin-right: 20px;
}

.blob {
  background: #f1f1f1;
  border: solid;
  border-color: #e2e2e2;
  border-width: 1px;
  margin-right: 7px;
  border-radius: 15px;
  padding-left: 7px;
  padding-right: 7px;
  padding-top: 2px;
  padding-bottom: 2px;
  font-weight: lighter;
  margin-bottom: 7px;
}

.blobspace {
  margin-top: 15px;
}

.lightText {
  font-weight: lighter;
  font-size: 14px;
  margin-top: 7px;
}

.lighterBreadText {
  font-weight: lighter;
  font-size: 16px;
  margin-top: 7px;
}

.barwidth {
  width: 90%;
}

.marginleftIcon {
  margin-left: 5px;
}

.centerrow {
  width: 145px;
  margin-left: auto;
  margin-right: auto;
}

#nomargin {
  margin: 0px;
  margin-right: 15px;
}

.errorMessage > p {
  font-weight: normal;
  font-size: 1.5vh;
  color: red;
}
/* docxexport */
#exDocx {
  position: absolute;
  z-index: 1;
  background: white;
  display: flex;
  flex-direction: column;
  border: 1px solid grey;
  border-radius: 5px;
  right: 200px;
  top: 100px;
  text-align: left;
  height: 30vh;
  width: 150px;
  align-items: center;
  overflow-y: scroll;
}
.list-table>table>tr:hover{
   cursor: pointer;
}
</style>
