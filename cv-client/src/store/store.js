import Vue from "vue";
import Vuex from "vuex";
import Axios from "@/axios.config.js";

Vue.use(Vuex);

const getDefaultState = () => {
  return {
    loggedIn: false,
    user: {},
    users: [],
    cvList: [],
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
    setCvList(state, list) {
      state.cvList = list;
    },
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

        await Axios.put(`user/${user}`)
        .then(async resp => {
          this.user = resp;
        })
        .catch(err => console.log(err));
      commit("setUser", this.user);
    },
    async logOut({ commit }) {
      commit("resetState");
    },
    async getCvList({ commit }) {
      await Axios.get("cvlist/")
        .then(async (resp) => {
          this.users = resp;
        })
        .catch((err) => console.log(err));
      commit("setCvList", this.cvList);
    },
    async getUsers({ commit }) {
      await Axios.get("user/")
        .then(async (resp) => {
          this.users = resp;
        })
        .catch((err) => console.log(err));
      commit("setUsers", this.users);
    },
  },
  getters: {
    getLoggedIn(state) {
      return state.loggedIn;
    },
    getUser(state) {
      return state.user;
    },
    getCvList(state) {
      return state.cvList;
    },
  },
});