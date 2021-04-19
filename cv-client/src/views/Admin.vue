<template>
  <div class="flex-container">
    <div class="flex-item-left">
      <div class="mainDiv">
        <div class="h2Div">
          <h3>Redigera Mall</h3>
        </div>
        <div class="flex">
          <div v-for="item in cv" :key="item.index" class="templateDiv">
            <i class="fas fa-image fa-10x"></i>
            <p>{{ item.name }}</p>
          </div>
          <div class="templateDiv" @click="showAdminModal">
            <i class="fas fa-plus-square fa-10x"></i>
            <p>Lägg till ny mall</p>
          </div>
        </div>
      </div>
    </div>
    <div class="flex-item-right">
      <div>
        <RegisterUser />
      </div>
    </div>
    <EditCv v-show="isAdminModalVisible" @close="closeAdminModal" />
    <Modal v-show="isSessionModalVisible" @close="closeSessionModal">
      <template v-slot:header>
        Du kommer loggas ut.
      </template>

      <template v-slot:body>
        Du har varit inaktiv i 45minuter, du kommer att loggas ut om 15minuter.
      </template>
    </Modal>
  </div>
</template>

<script>
import store from "../store/store.js";
import RegisterUser from "../components/RegisterUser";
import EditCv from "../components/EditCv";
import router from "../router/index.js";
import Modal from "../components/Modal.vue";
export default {
  name: "Admin",
  data() {
    return {
      isAdminModalVisible: false,
      isSessionModalVisible: false,
      cv: [
        {
          name: "Mall 1",
          img: "https://angelofshiva.com/resources/assets/images/no-img.jpg",
        },
        {
          name: "Mall 2",
          img: "https://angelofshiva.com/resources/assets/images/no-img.jpg",
        }
      ]
    };
  },
  mounted() {
    var timeout;
    var _this = this;
    function refresh() {
      if (_this.isSessionModalVisible) {
        _this.closeSessionModal();
      }

      console.log("timer started");
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        _this.showSessionModal();
        close();
      }, 1 * 10 * 1000);
    }
    function close() {
      console.log("close timer started");
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        document.removeEventListener("mousemove", refresh);
        console.log("you were logged out");
        _this.showSessionModal();
        store.dispatch("logOut");
        router.push("/");
      }, 1 * 8 * 1000);
    }
    document.addEventListener("mousemove", refresh);
    refresh();
  },
  components: {
    RegisterUser,
    EditCv,
    Modal
  },
  methods: {
    showAdminModal() {
      this.isAdminModalVisible = true;
    },
    closeAdminModal() {
      this.isAdminModalVisible = false;
    },
    showSessionModal() {
      this.isSessionModalVisible = true;
    },
    closeSessionModal() {
      this.isSessionModalVisible = false;
    }
  }
};
</script>

<style>
.mainDiv {
  border: solid 1px black;
  min-height: 87vh;
  margin: 25px 2%;
  border-radius: 4px;
}
.templateDiv {
  margin-left: 2%;
  border-radius: 10px;
  max-width: 160px;
}
.templateDiv:hover {
  cursor: pointer;
  box-shadow: 0 0px 11px rgba(33, 33, 33, 0.2);
  transition: box-shadow 0.3s;
}
.createTemplateDiv {
  border-radius: 10px;
}
.createTemplateDiv:hover {
  cursor: pointer;
  box-shadow: 0 0 11px rgba(33, 33, 33, 0.2);
  transition: box-shadow 0.3s;
}
.h2Div {
  display: flex;
  margin: 0px 2%;
}
img {
  max-width: 100%;
}
.flex {
  display: flex;
  flex-wrap: wrap;
}
.flex-container {
  display: flex;
}
.flex-item-left {
  flex: 75%;
}

.flex-item-right {
  flex: 25%;
}
p {
  font-weight: bold;
}
#AddNewTemplate {
  cursor: pointer;
}
</style>
