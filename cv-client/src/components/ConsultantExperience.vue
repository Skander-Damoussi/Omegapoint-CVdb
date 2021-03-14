<template>
  <div class="box">
    <div class="row">
      <p id="nomargin">Uppdrag</p>
      <button id="addButton">Lägg till nytt</button>
    </div>
    <div class="wrapper" v-for="col in collection" :key=col.title>
      <div class="title" @click="col.show = !col.show">
        <p>{{col.title}}</p>
      </div>
      <div class="container" id="container" v-if="col.show">
        <p>
          {{col.text}}
        </p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditUser",
  data: function() {
  return {
    collection: [
      { show: false, title: "Uppdrag 1", text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sit amet nunc eu nisi semper semper. Praesent vestibulum mauris quis volutpat commodo. Etiam convallis orci faucibus massa viverra, at ultrices ex iaculis. Sed a congue turpis, quis vehicula lacus. Donec libero arcu, feugiat sit amet lacinia id, finibus in nulla. Duis vel tellus non" },
      { show: false, title: "Uppdrag 2", text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sit amet nunc eu nisi semper semper. Praesent vestibulum mauris quis volutpat commodo. Etiam convallis orci faucibus massa viverra, at ultrices ex iaculis. Sed a congue turpis, quis vehicula lacus. Donec libero arcu, feugiat sit amet lacinia id, finibus in nulla. Duis vel tellus non" },
      { show: false, title: "Uppdrag 3", text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sit amet nunc eu nisi semper semper. Praesent vestibulum mauris quis volutpat commodo. Etiam convallis orci faucibus massa viverra, at ultrices ex iaculis. Sed a congue turpis, quis vehicula lacus. Donec libero arcu, feugiat sit amet lacinia id, finibus in nulla. Duis vel tellus non" },
    ],
  }
  },
};
</script>

<style>
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}

.wrapper {
  border-radius: 2px;
  border: 2px solid #f7f7f7;
}

.title {
  background: #f7f7f7;
  padding: 10px;
  text-align: left;
}

.container {
  margin: 10px;
}

#nomargin {
  margin: 0px;
}

#addButton {
  margin-left: auto;
  color: white;
  background: #2185d0;
  border: none;
  text-decoration: none;
  border-radius: 2px;
  transition-duration: 0.4s;
  border: 2px solid #2185d0;
  padding: 6px;
}

.row {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  margin-bottom: 10px;
}

.box {
  width: 400px;
  background-color: white;
  margin: 50px; 
  /* ^ Will be removed after component is done */
}
</style>
