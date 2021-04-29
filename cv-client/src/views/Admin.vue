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
  </div>
</template>

<script>
import RegisterUser from "../components/RegisterUser";
import EditCv from "../components/EditCv";

export default {
  name: "Admin",
  data() {
    return {
      isAdminModalVisible: false,
      isSessionModalVisible: false,
      cv: [
        {
          name: "Mall 1"
        },
        {
          name: "Mall 2"
        }
      ]
    };
  },
  components: {
    RegisterUser,
    EditCv
  },
  methods: {
    showAdminModal() {
      this.isAdminModalVisible = true;
    },
    closeAdminModal() {
      this.isAdminModalVisible = false;
    }
  }
};
</script>

<style>
.mainDiv {
  border: solid 1px black;
  min-height: 87vh;
  margin: 2vh 1vh;
  border-radius: 4px;
}
.templateDiv {
  margin-left: 2vh;
  border-radius: 10px;
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
  margin: 0px 2vh;
}
.flex {
  display: flex;
  flex-wrap: wrap;
}
.flex-container {
  display: flex;
}
.flex-item-left {
  flex: 70vw;
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
