import Vue from "vue";
import Vuex from "vuex";
import Axios from "@/axios.config.js";

Vue.use(Vuex);

const getDefaultState = () => {
  return {
    loggedIn: false,
    user: {},
    users: [],
    consultantList: []
  };
};

export default new Vuex.Store({
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
    setConsultantList(state, list) {
      state.consultantList = list;
    }
  },
  actions: {
    async login({ commit }) {
      commit("setUser");
    },
    async updateUser({ commit }, user) {
      /* string field,string finthis, update */

      // const url = `user/${field}/${findThis}/${update}`;
      // await Axios.put(url, field, findThis, update)
      //   .then(async (resp) => {
      //     this.user = resp;
      //   })
      //   .catch((err) => console.log(err));
      // commit("setUser", this.user);

      await Axios.put(`user/${user.Email}`, user)
        .then(async resp => {
          this.user = resp;
        })
        .catch(err => console.log(err));
      commit("updateUser", this.user);
    },
    async logOut({ commit }) {
      commit("resetState");
    },
    async getConsultantList({ commit }) {
      let role = "consultant";
      await Axios.get(`user/`, role)
        .then(async resp => {
          this.consultantList = resp;
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
    }
  }
});
