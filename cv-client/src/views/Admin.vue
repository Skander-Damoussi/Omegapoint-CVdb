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
    <Modal v-show="isAdminModalVisible" @close="closeAdminModal">
      <template v-slot:header>
        Mall för cv
      </template>

      <template v-slot:body>
        <div class="a4mall">
          <div class="sidenote">
            <div class="grid1"><span>sidenote 1</span></div>
            <div class="grid2"><span>sidenote 2</span></div>
            <div class="grid3"><span>sidenote 2</span></div>
            <div class="grid4"><span>sidenote 3</span></div>
          </div>
          <div class="iconsidenote">
            <div class="grid1">
              <i class="fas fa-edit fa-2x"></i>
              <i class="fas fa-file-image fa-2x"></i>
            </div>
            <div class="grid2">
              <i class="fas fa-edit fa-2x"></i>
              <i class="fas fa-file-image fa-2x"></i>
            </div>
            <div class="grid3">
              <i class="fas fa-edit fa-2x"></i>
              <i class="fas fa-file-image fa-2x"></i>
            </div>
            <div class="grid4">
              <i class="fas fa-edit fa-2x"></i>
              <i class="fas fa-file-image fa-2x"></i>
            </div>
          </div>
          <div>
            <p style="margin-top: 50px;">
              What is Lorem Ipsum? Lorem Ipsum is simply dummy text of the
              printing and typesetting industry. Lorem Ipsum has been the
              industry's standard dummy text ever since the 1500s, when an
              unknown printer took a galley of type and scrambled it to make a
              type specimen book. It has survived not only five centuries, but
              also the leap into electronic typesetting, remaining essentially
              unchanged. It was popularised in the 1960s with the release of
              Letraset sheets containing Lorem Ipsum passages, and more recently
              with desktop publishing software like Aldus PageMaker including
              versions of Lorem Ipsum.
            </p>
          </div>
          <div class="iconfootnote">
            <div class="grid1">
              <i class="fas fa-edit fa-2x"></i>
              <i class="fas fa-file-image fa-2x"></i>
            </div>
            <div class="grid2">
              <i class="fas fa-edit fa-2x"></i>
              <i class="fas fa-file-image fa-2x"></i>
            </div>
            <div class="grid3">
              <i class="fas fa-edit fa-2x"></i>
              <i class="fas fa-file-image fa-2x"></i>
            </div>
            <div class="grid4">
              <i class="fas fa-edit fa-2x"></i>
              <i class="fas fa-file-image fa-2x"></i>
            </div>
          </div>
          <div class="footnote">
            <div class="grid1"><span>footnote 1</span></div>
            <div class="grid2"><span>footnote 2</span></div>
            <div class="grid3"><span>footnote 2</span></div>
            <div class="grid4"><span>footnote 3</span></div>
          </div>
        </div>
      </template>

      <template v-slot:footer> </template>
    </Modal>
  </div>
</template>

<script>
import RegisterUser from "../components/RegisterUser";
import Modal from "../components/Modal";

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
    Modal,
  },
  methods: {
    showAdminModal() {
      this.isAdminModalVisible = true;
    },
    closeAdminModal() {
      this.isAdminModalVisible = false;
    },
  },
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
  flex: 75vw;
}

.flex-item-right {
  flex: 20vw;
}
p {
  font-weight: bold;
}
#AddNewTemplate {
  cursor: pointer;
}
</style>
