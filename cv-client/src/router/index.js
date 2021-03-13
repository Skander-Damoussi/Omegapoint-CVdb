import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Consultant from "../components/Consultant";
import Admin from "../views/Admin";
import EditUser from "../components/EditUser.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/consultant",
    name: "Consultant",
    component: Consultant
  },
  {
    path: "/admin",
    name: "Admin",
    component: Admin
  },
  {
    path: "/edituser",
    name: "EditUser",
    component: EditUser
  }
];

const router = new VueRouter({
  routes
});

export default router;
