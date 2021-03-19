<template>
<div>
  <div class="box">
    <div class="tasks">
      <div class="row">
        <p id="nomargin">Administrations Software</p>
      </div>
      <div class="tablewrapper">
      <table style="width:100%">
        <tr>
          <th>Microsoft Office</th>
          <th>Delete</th>
        </tr>
        <tr>
          <th>Azure</th>
          <th>Delete</th>
        </tr>
        <tr>
          <th>Foo</th>
          <th>Delete</th>
        </tr>
      </table>
      <div class="row">
        <input type="text" id="addSoftware" name="addSoftware" placeholder="....">
        <button id="addButton">Lägg till</button>
      </div>
      </div>
    </div>
    <div class="tasks as">
      <div class="row">
        <p id="nomargin">Uppdrag</p>
        <button id="addButton">Lägg till nytt</button>
      </div>
      <div class="wrapper" v-for="col in collection" :key="col.title">
        <div class="title" @click="col.show = !col.show">
          <p>{{ col.title }}</p>
        </div>
        <div class="container" id="container" v-if="col.show">
          <p>
            {{ col.text }}
          </p>
        </div>
      </div>
    </div>
    <div class="tasks as stickright">
      <div class="row">
        <p id="nomargin">Presentation</p>
        <button id="addButton">Lägg till</button>
      </div>
      <textarea></textarea>
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
        {
          show: false,
          title: "Uppdrag 1",
          text:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sit amet nunc eu nisi semper semper. Praesent vestibulum mauris quis volutpat commodo. Etiam convallis orci faucibus massa viverra, at ultrices ex iaculis. Sed a congue turpis, quis vehicula lacus. Donec libero arcu, feugiat sit amet lacinia id, finibus in nulla. Duis vel tellus non",
        },
        {
          show: false,
          title: "Uppdrag 2",
          text:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sit amet nunc eu nisi semper semper. Praesent vestibulum mauris quis volutpat commodo. Etiam convallis orci faucibus massa viverra, at ultrices ex iaculis. Sed a congue turpis, quis vehicula lacus. Donec libero arcu, feugiat sit amet lacinia id, finibus in nulla. Duis vel tellus non",
        },
        {
          show: false,
          title: "Uppdrag 3",
          text:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sit amet nunc eu nisi semper semper. Praesent vestibulum mauris quis volutpat commodo. Etiam convallis orci faucibus massa viverra, at ultrices ex iaculis. Sed a congue turpis, quis vehicula lacus. Donec libero arcu, feugiat sit amet lacinia id, finibus in nulla. Duis vel tellus non",
        },
      ],
    };
  },
};
</script>

<style scoped>
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}

.wrapper {
  border-radius: 2px;
  border: 2px solid #f7f7f7;
}

.tasks {
  padding: 30px;
  text-align: left;
  flex-basis: 30%;
  margin: 30px;
}

.tablewrapper{
  padding: 30px;
  border: 1px solid #f7f7f7;
}

.title {
  background: #f7f7f7;
  padding: 10px;
  text-align: left;
  border: 1px solid #cecece;
}

.container {
  margin: 10px;
}

#nomargin {
  margin: 0px;
}

#addSoftware {
  flex: 70%;
  margin-right: 10px;
  height: 32px;
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
  background-color: white;
  margin: 50px;
  display: flex;
}

input[type=text], select {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}

input[type=text]:focus {
  border: 1px solid #555;
}

th {
  font-size: 15px;
  font-weight: normal;
}

textarea {
  height: 230px;
  width: 100%;
  padding: 12px 20px;
  margin: 10px;
  box-sizing: border-box;
  border: 2px solid #ccc;
  border-radius: 4px;
  background-color: #f8f8f8;
  resize: none;
}

.as {
  width: 30vw;
}

.stickright {
  margin-left: auto;
}
</style>
