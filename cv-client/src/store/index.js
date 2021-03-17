import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

const getDefaultState = () => {
  return {
    loggedIn: false,
    user: {},
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
    setCvList(state, list) {
      state.cvList = list;
    },
  },
  actions: {
    async login({ commit }) {
      commit("setUser");
    },
    async logOut({ commit }) {
      commit("resetState");
    },
    async getCvList({ commit }) {
      const list = [];
      commit("setCvList", list);
    },
  },
  getters: {
    getLoggedIn(state) {
      return state.loggedIn;
    },
    getCvList(state) {
      return state.cvList;
    },
  },
});
