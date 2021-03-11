<template>
  <div class="flex-container">
    <div class="flex-item-left">
      <div class="mainDiv">
        <div class="h2Div">
          <h3>Redigera Mall</h3>
        </div>
        <div class="flex">
          <div v-for="item in cv" :key="item" class="imgDiv">
            <img :src="item.img" />
            <p>{{ item.name }}</p>
          </div>
          <div class="imgDiv">
            <img
              src="../assets/plusicon.png"
              style="width: 175px; height: 175px;"
            />
          </div>
        </div>
      </div>
    </div>
    <div class="flex-item-right">
      <div>
        <RegisterUser />
      </div>
    </div>
  </div>
</template>

<script>
import RegisterUser from "../components/RegisterUser";
export default {
  name: "Home",
  data() {
    return {
      cv: [
        {
          name: "Mall 1",
          img: "https://angelofshiva.com/resources/assets/images/no-img.jpg"
        },
        {
          name: "Mall 2",
          img: "https://angelofshiva.com/resources/assets/images/no-img.jpg"
        }
      ]
    };
  },
  components: {
    RegisterUser
  }
};
</script>

<style>
.mainDiv {
  border: solid 1px black;
  height: 90vh;
  margin: 0px 2%;
}
.imgDiv {
  margin-left: 2%;
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
</style>
