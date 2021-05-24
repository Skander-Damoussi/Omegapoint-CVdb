import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import ConsultantManager from "../components/ConsultantManager.vue";
import Consultant from "../components/Consultant";
import Admin from "../views/Admin";
import EditUser from "../components/EditUser.vue";
import ConsultantExperience from "../components/ConsultantExperience.vue";
import ConsultantExperienceEdit from "../components/ConsultantExperienceEdit.vue";
import Verify from "../components/Verify.vue";
import ConsultantPresentationEdit from "../components/ConsultantPresentationEdit.vue";
import UploadCvTemp from "../components/UploadCvTemp.vue";
import store from "../store/store.js";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: {},
  },
  {
    path: "/consultantmanager",
    name: "ConsultantManager",
    component: ConsultantManager,
    meta: { reqAuth: true, consultantManagerAuth: true },
  },
  {
    path: "/consultant",
    name: "Consultant",
    component: Consultant,
    meta: {
      reqAuth: true,
      consultAuth: true,
      consultantManagerAuth: true
    },
    props: true
  },
  {
    path: "/consultantExperience",
    name: "ConsultantExperience",
    component: ConsultantExperience,
    meta: { reqAuth: true, consultAuth: true, consultantManagerAuth: true },
    props: true
  },
  {
    path: "/consultantExperienceEdit",
    name: "ConsultantExperienceEdit",
    component: ConsultantExperienceEdit,
    meta: { reqAuth: true, consultAuth: true, consultantManagerAuth: true },
    props: true
  },
  {
    path: "/consultantPresentationEdit",
    name: "ConsultantPresentationEdit",
    component: ConsultantPresentationEdit,
    meta: { reqAuth: true, consultAuth: true, consultantManagerAuth: true },
    props: true
  },
  {
    path: "/admin",
    name: "Admin",
    component: Admin,
    meta: { reqAuth: true, adminAuth: true },
  },
  {
    path: "/edituser",
    name: "EditUser",
    component: EditUser,
    meta: {
      reqAuth: true,
      consultAuth: true,
      adminAuth: true,
      consultantManagerAuth: true,
    },
  },
  {
    path: "/uploadCvTemp",
    name: "UploadCvTemp",
    component: UploadCvTemp,
    meta: { reqAuth: true, consultAuth: false, consultantManagerAuth: true, adminAuth: true },
    props: true
  },
  {
    path: "/verify/:token/:userId",
    name: "Verify",
    component: Verify,
  },
];

const router = new VueRouter({
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.reqAuth)) {
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
    } else {
      next("/");
    }
  } else {
    next();
  }
});

export default router;
