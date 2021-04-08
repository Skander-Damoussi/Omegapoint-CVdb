import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import ConsultantManager from "../components/ConsultantManager.vue";
import Consultant from "../components/Consultant";
import Admin from "../views/Admin";
import EditUser from "../components/EditUser.vue";
import ConsultantExperience from "../components/ConsultantExperience.vue";
import store from "../store/store.js";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/consultantmanager",
    name: "ConsultantManager",
    component: ConsultantManager,
    meta: { reqAuth: true, consultantManagerAuth: true }
  },
  {
    path: "/consultant",
    name: "Consultant",
    component: Consultant,
    meta: { reqAuth: true, consultAuth: true }
  },
  {
    path: "/consultantExperience",
    name: "Consultant",
    component: ConsultantExperience,
    meta: { reqAuth: true, consultAuth: true }
  },
  {
    path: "/admin",
    name: "Admin",
    component: Admin,
    meta: { reqAuth: true, adminAuth: true }
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

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.reqAuth)) {
    if (store.getters.getLoggedIn) {
      switch (store.getters.getLoggedInUser.role) {
        case "Admin":
          if (!to.meta.adminAuth) {
            next("admin");
          } else {
            next();
          }
          break;
        case "Konsultchef":
          if (!to.meta.consultantManagerAuth) {
            next({ path: "consultantmanager" });
          } else {
            next();
          }
          break;
        case "Konsult":
          if (!to.meta.consultAuth) {
            next("consultant");
          } else {
            next();
          }
          break;
        default:
          break;
      }
      next();
      return;
    }
    next("/");
  } else {
    next();
  }
});

export default router;
