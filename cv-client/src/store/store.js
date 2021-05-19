import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";
import Axios from "@/axios.config.js";

Vue.use(Vuex);

const getDefaultState = () => {
  return {
    loggedIn: false,
    user: {},
    loggedInUser: null,
    users: [],
    consultantList: [],
    userExperience: [],
    userPresentation: [],
    token: null,
    role: null,
    verified: null,
    searchList: [],
    CV: [],
    status: null,
    //cvtemp
    cvTempList: [],
    cvTemp: null
  };
};

export default new Vuex.Store({
  plugins: [createPersistedState()],
  state: getDefaultState(),
  mutations: {
    setStatus(state, status) {
      state.status = status;
    },
    resetState(state) {
      let defaultState = getDefaultState();
      for (let key in state) state[key] = defaultState[key];
    },
    setUser(state) {
      state.loggedIn = true;
    },
    setUsers(state) {
      state.users = state;
    },
    updateUser(state, updatedUser) {
      state.user = updatedUser;
    },
    setExperience(state, updatedExperiences) {
      state.loggedInUser.experiences = updatedExperiences;
    },
    setConsultantList(state, list) {
      state.consultantList = list;
    },
    setToken(state, token) {
      state.token = token;
    },
    setLoggedInUser(state, token) {
      state.loggedInUser = token;
    },
    setLoggedIn(state, token) {
      state.loggedIn = token;
    },
    setUserExperience(state, token) {
      state.userExperience = token;
    },
    setVerify(state, verified) {
      state.verified = verified;
    },
    setUserPresentation(state, token) {
      state.userPresentation = token;
    },
    setSearchList(state, list) {
      state.searchList = list;
    },
    setCV(state, token) {
      state.CV = token;
    },
    // cvtemp
    setCvTempList(state, list) {
      state.cvTempList = list;
    },
    setCvTemp(state, list) {
      state.cvTemp = list;
    },
    
  },
  actions: {
    async login({ commit }, user) {
      await Axios.post("user/login", user)
        .then(async (resp) => {
          console.log(resp.status + "resp");
          this.status = resp.status;
          var respUser = {
            id: resp.data.userId,
            firstName: resp.data.firstName,
            lastName: resp.data.lastName,
            role: resp.data.role,
            experiences: resp.data.experiences
          };
          await commit("setToken", resp.data.token);
          await commit("setLoggedInUser", respUser);
          await commit("setLoggedIn", true);
        })
        .catch(error => {
          commit("setStatus", error.response);
        });
    },
    async updateUser({ commit }, user) {
      await Axios.put("user/", user)
        .then(async (resp) => {
          console.log("store", resp);
          this.status = resp.status;
          var respUser = {
            id: resp.data.userId,
            firstName: resp.data.firstName,
            lastName: resp.data.lastName,
            role: resp.data.role,
            experiences: resp.data.experiences
          };
          await commit("setLoggedInUser", respUser);
          await commit("setStatus", this.status);
        })
        .catch(err => {
          console.log("err resp", err.response.data);
          commit("setStatus", err.response.data);
        });
    },
    async updatePassword({ commit }, password) {
      await Axios.put("user/updatePassword", password)
        .then(async (resp) => {
          this.status = resp.status;
          var respUser = {
            id: resp.data.userId,
            firstName: resp.data.firstName,
            lastName: resp.data.lastName,
            role: resp.data.role,
            experiences: resp.data.experiences
          };
          await commit("setLoggedInUser", respUser);
          await commit("setStatus", this.status);
        })
        .catch(err => {
          console.log("err resp", err.response);
          commit("setStatus", err.response.data);
        });
    },
    async logOut({ commit }) {
      commit("resetState");
    },
    async getConsultantList({ commit }) {
      await Axios.get("user/getConsultantList")
        .then(async resp => {
          this.consultantList = resp.data;
        })
        .catch(err => console.log(err));
      commit("setConsultantList", this.consultantList);
    },
    async searchConsultant({ commit }, searchString) {
      await Axios.get(`search/getSearchResult/${searchString}`)
        .then(async (resp) => {
          this.searchList = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setSearchList", this.searchList);
    },
    async getUsers({ commit }) {
      await Axios.get("user/")
        .then(async (resp) => {
          this.users = resp;
        })
        .catch((err) => console.log(err));
      commit("setUsers", this.users);
    },
    async registerUser({ commit }, { token, input }) {
      await Axios.post("user/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async resp => {
          console.log(resp.status);
          commit("setStatus", resp.status);
        })
        .catch(err => {
          commit("setStatus", err.response.status);
        });
    },
    async cvSave({ commit }, { token, input }) {
      await Axios.post("user/updateCV/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async (resp) => {
          this.CV = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setCV", this.CV);
    },
    async addExperience({ commit }, { token, input }) {
      await Axios.post("user/postexperience/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async (resp) => {
          this.userExperience = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setExperience", this.experiences);
    },
    async updateExperience({ commit }, { token, input }) {
      await Axios.post("user/updateexperience/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async (resp) => {
          this.userExperience = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setExperience", this.experiences);
    },
    async removeExperience({ commit }, { token, input }) {
      await Axios.post("user/removeexperience/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async (resp) => {
          this.userExperience = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setExperience", this.experiences);
    },
    async getUserExperience({ commit }, userId) {
      await Axios.get(`user/getConsultantExperienceList/${userId}`)
        .then(async (resp) => {
          this.userExperience = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setUserExperience", this.userExperience);
    },
    async verify({ commit }, { userId, token }) {
      console.log(userId);
      console.log(token);
      await Axios.post("user/verify", userId, token)
        .then(async (resp) => {
          this.verified = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setUserExperience", this.verified);
    },
    async addPresentation({ commit }, { token, input }) {
      await Axios.post("user/postPresentation/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async (resp) => {
          this.userPresentation = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setUserPresentation", this.presentations);
    },
    async updatePresentation({ commit }, { token, input }) {
      await Axios.post("user/updatepresentation/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async (resp) => {
          this.userPresentation = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setUserPresentation", this.presentations);
    },
    async removePresentation({ commit }, { token, input }) {
      await Axios.post("user/removepresentation/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async (resp) => {
          this.userExperience = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setUserPresentation", this.presentations);
    },
    async getUserPresentation({ commit }, userId) {
      await Axios.get(`user/getConsultantPresentationList/${userId}`)
        .then(async (resp) => {
          this.userPresentation = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setUserPresentation", this.userPresentation);
    },
    async updateActiveUser({ commit }, email) {
      await Axios.put(`user/updateActiveConsultant/${email}`)
        .then(async (resp) => {
          console.log(resp);
        })
        .catch((err) => console.log(err));
      commit("setSearchList", this.searchList);
    },
    async getCV({ commit }, userId) {
      await Axios.get(`user/getUserCV/${userId}`)
        .then(async (resp) => {
          this.CV = resp.data;
        })
        .catch((err) => console.log(err));
      commit("setCV", this.CV);
    },
    async getDeactivatedConsultants({ commit }) {
      await Axios.get(`user/getDeactivatedConsultants`)
        .then(async (resp) => {
          this.searchList = resp.data;
          console.log(resp.data);
        })
        .catch((err) => console.log(err));
      commit("setSearchList", this.searchList);
    },
    // cvtemp
    async getCvTempList({ commit }) {
      await Axios.get("cv/GetAllCvTemplate")
        .then(async (resp) => {
          this.cvTempList = resp.data;
          console.log(resp.data)
        })
        .catch((err) => console.log(err));
        console.log("get cv templist", JSON.stringify(this.cvTempList));
      commit("setCvTempList", this.cvTempList);
    },
    // async getCvTemp({ commit }, id) {
    //   await Axios.get(`cv/GetCvTemplate/${id}`)
    //     .then(async (resp) => {
    //       //this.CvTemp = resp.data;
    //       console.log(resp.data)
    //     })
    //     .catch((err) => console.log(err));
    //   commit("setCvTemp", this.CvTemp);
    // },
    //Funkar om response är Ok är borttaget, problem med stringId
    // async getCvTemp( id) {
    //   Axios({
    //     method: 'post',
    //     url: `cv/PostGetCvTemplate/${id}`,
    //     responseType: 'arraybuffer',
    //     //data: dates
    //   }).then(function(response) {
    //     let blob = new Blob([response.data], { type: 'application/docx' })
    //     let link = document.createElement('a')
    //     link.href = window.URL.createObjectURL(blob)
    //     link.download = 'Template.docx'
    //     link.click()
    //   })
    // },
    // // Funkar,senaste, problem med stringId
    // async getCvTemp( id) {
    //   Axios({
    //     method: 'GET',
    //     url: `cv/GetCvTemplate/${id}`,
    //     responseType: 'blob',
    //     //data: dates
    //   }).then((response) =>{
    //     var fileURL = window.URL.createObjectURL(new Blob([response.data]));
    //     var fileLink = document.createElement('a');   
    //     fileLink.href = fileURL;
    //     fileLink.setAttribute('download', 'cvTemplate.docx');
    //     document.body.appendChild(fileLink);   
    //     fileLink.click();
    //   })
    // },
    async getCvTemp({ commit }, stringId) {
      console.log("Inne i Get", stringId)
      Axios({
        method: 'GET',
        url: "cv/GetCvTemplate/"+stringId,
        responseType: 'blob',
        //data: dates
      }).then((response) =>{
        var fileURL = window.URL.createObjectURL(new Blob([response.data]));
        var fileLink = document.createElement('a');   
        fileLink.href = fileURL;
        fileLink.setAttribute('download', 'cvTemplate.docx');
        document.body.appendChild(fileLink);   
        fileLink.click();
        commit("setCvTemp", response.data);
      })
    },
    async newConfirmationLink({ commit }, email) {
      console.log(email);
      await Axios.post(`user/Confirmation/${email}`, email)
        .then(async resp => {
          console.log(resp.data);
          commit("setStatus", resp);
        })
        .catch(err => console.log(err));
    }
  },
  getters: {
    getStatus(state) {
      return state.status;
    },
    getLoggedIn(state) {
      return state.loggedIn;
    },
    getUser(state) {
      return state.user;
    },
    getConsultantList(state) {
      return state.consultantList;
    },
    getUserToken(state) {
      return state.token;
    },
    getLoggedInUser(state) {
      return state.loggedInUser;
    },
    getUserExperience(state) {
      return state.userExperience;
    },
    getUserPresentation(state) {
      return state.userPresentation;
    },
    getSearchList(state) {
      return state.searchList;
    },
    getCV(state) {
      return state.CV;
    },
    // cvtemp
    getCvTempList(state) {
      return state.cvTempList;
    },
    getCVTemp(state) {
      return state.CvTemp;
    },
  },
});
