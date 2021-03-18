import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

const getDefaultState = () => {
  return {
    loggedIn: false,
    user: {}
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
    }
  },
  actions: {
    async login({ commit }) {
      commit("setUser");
    },
    async logOut({ commit }) {
      commit("resetState");
    }
  },
  getters: {
    getLoggedIn(state) {
      return state.loggedIn;
    }
  }
});
