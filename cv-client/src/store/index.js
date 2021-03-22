import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

const getDefaultState = () => {
  return {
    loggedIn: false,
    user: {},
    cvList: []
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
    updateUser(state, updatedUser) {
      state.user = updatedUser;
    },
    setCvList(state, list) {
      state.cvList = list;
    }
  },
  actions: {
    async login({ commit }) {
      commit("setUser");
    },
    async updateUser({ commit }) {
      commit("updateUser");
    },
    async logOut({ commit }) {
      commit("resetState");
    },
    async getCvList({ commit }) {
      //fetch from api
      const list = [];
      commit("setCvList", list);
    }
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
    }
  }
});
