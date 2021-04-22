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
    token: null,
    role: null
  };
};

export default new Vuex.Store({
  plugins: [createPersistedState()],
  state: getDefaultState(),
  mutations: {
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
    }
  },
  actions: {
    async login({ commit }, user) {
      await Axios.post("user/login", user)
        .then(async resp => {
          var respUser = {
            id: resp.data.userId,
            firstName: resp.data.firstName,
            lastName: resp.data.lastName,
            role: resp.data.role,
            experiences: resp.data.experiences
          }
          await commit("setToken", resp.data.token);
          await commit("setLoggedInUser", respUser);
          await commit("setLoggedIn", true);
        })
        .catch(err => console.log(err));
    },
    async updateUser({ commit }, user) {
      console.log(user.password);
      await Axios.put("user/", user)
        .then(async resp => {
          var respUser = {
            id: resp.data.userId,
            firstName: resp.data.firstName,
            lastName: resp.data.lastName,
            role: resp.data.role,
            experiences: resp.data.experiences
          };
          await commit("setLoggedInUser", respUser);
          console.log(this.loggedInUser);
        })
        .catch(err => console.log(err));
    },
    async updatePassword({ commit }, password) {
      await Axios.put("user/updatePassword", password)
        .then(async resp => {
          var respUser = {
            id: resp.data.userId,
            firstName: resp.data.firstName,
            lastName: resp.data.lastName,
            role: resp.data.role,
            experiences: resp.data.experiences
          };
          await commit("setLoggedInUser", respUser);
          console.log(this.loggedInUser);
        })
        .catch(err => console.log(err));
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
        .then(async resp => {
          this.consultantList = resp.data;
        })
        .catch(err => console.log(err));
      commit("setConsultantList", this.consultantList);
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
          console.log(resp);
        })
        .catch(err => console.log(err));
      commit("setUsers", this.users);
    },
    async addExperience({ commit }, { token, input }) {
      await Axios.post("user/postexperience/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async resp => {
          this.userExperience = resp.data;
        })
        .catch(err => console.log(err));
      commit("setExperience", this.experiences);
    },
    async updateExperience({ commit }, { token, input }) {
      await Axios.post("user/updateexperience/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async resp => {
          this.userExperience = resp.data;
        })
        .catch(err => console.log(err));
      commit("setExperience", this.experiences);
    },
    async removeExperience({ commit }, { token, input }) {
      await Axios.post("user/removeexperience/", input, {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
        }
      })
        .then(async resp => {
          this.userExperience = resp.data;
        })
        .catch(err => console.log(err));
      commit("setExperience", this.experiences);
    },
    async getUserExperience({ commit }, userId) {
      await Axios.get(`user/getConsultantExperienceList/${userId}`)
        .then(async resp => {
          this.userExperience = resp.data;
        })
        .catch(err => console.log(err));
      commit("setUserExperience", this.userExperience);
    }
  },
  getters: {
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
    }
  }
});
