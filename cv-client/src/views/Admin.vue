<template>
  <div class="flex-container">
    <div class="flex-item-left">
      <div class="mainDiv">
        <div class="h2Div">
          <h3>Redigera Mall</h3>
        </div>
        <div class="flex">
          <div v-for="item in cv" :key="item" class="templateDiv">
            <i class="fas fa-image fa-10x"></i>
            <p>{{ item.name }}</p>
          </div>
          <div class="templateDiv" @click="showModal">
            <i class="fas fa-plus-square fa-10x "></i>
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
    <EditCv v-show="isModalVisible" @close="closeModal" />
  </div>
</template>

<script>
import RegisterUser from "../components/RegisterUser";
import EditCv from "../components/EditCv";
export default {
  name: "Home",
  data() {
    return {
      isModalVisible: false,
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
  components: {
    RegisterUser,
    EditCv
  },
  methods: {
    showModal() {
      this.isModalVisible = true;
    },
    closeModal() {
      this.isModalVisible = false;
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
