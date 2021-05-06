<template>
  <div class="modal-backdrop">
    <div class="modal">
      <header class="modal-header">
        <slot name="header">
          Du kommer loggas ut.
        </slot>
        <button type="button" class="btn-close" @click="close">
          x
        </button>
      </header>

      <section class="modal-body">
        <slot name="body"> </slot>
      </section>

      <footer class="modal-footer">
        <slot name="footer"> </slot>
      </footer>
    </div>
  </div>
</template>

<script>
export default {
  name: "Modal",
  methods: {
    close() {
      this.$emit("close");
    }
  }
};
</script>

<style scoped>
.modal-backdrop {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background: #ffffff;
  box-shadow: 2px 2px 20px 1px;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
}

.modal-header,
.modal-footer {
  padding: 15px;
  display: flex;
}

.modal-header {
  position: relative;
  border-bottom: 1px solid #eeeeee;
  color: red;
  justify-content: space-between;
  font-weight: bold;
}

.modal-footer {
  border-top: 1px solid #eeeeee;
  flex-direction: column;
  justify-content: flex-end;
}

.modal-body {
  position: relative;
  padding: 20px 10px;
}

.btn-close {
  position: absolute;
  top: 0;
  right: 0;
  border: none;
  font-size: 20px;
  padding: 10px;
  cursor: pointer;
  font-weight: bold;
  color: #4aae9b;
  background: transparent;
}

.btn-blue {
  color: white;
  background: #2185d0;
  border: none;
  text-decoration: none;
  border-radius: 4px;
  transition-duration: 0.4s;
  border: 2px solid #2185d0;
}

.flex {
  display: flex;
}
.footnote {
  border-top: solid black 0.005px;
  border-right: solid black 0.005px;
  position: absolute;
  bottom: 20px;
  height: 80px;
  width: 495px;
  display: grid;
  grid-template-columns: repeat(4, 1fr);
}
.button {
  color: white;
  background: #2185d0;
  border: none;
  text-decoration: none;
  border-radius: 4px;
  transition-duration: 0.4s;
  border: 2px solid #2185d0;
}
.sidenote {
  border-bottom: solid black 0.005px;
  border-right: solid black 0.005px;
  height: 80px;
  width: 495px;
  display: grid;
  grid-template-columns: repeat(4, 1fr);
}
.iconfootnote {
  position: absolute;
  bottom: 105px;
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  width: 96%;
}
.iconsidenote {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  width: 100%;
}
.grid1 {
  grid-column: 1;
}
.grid2 {
  grid-column: 2;
}
.grid3 {
  grid-column: 3;
}
.grid4 {
  grid-column: 4;
}
.a4mall {
  border: 0.005px solid black;
  width: 496px;
  height: 701.6px;
}
.inputDiv {
  border: solid 0.5px black;
  padding: 5px;
  margin: 10px 0px;
}
.modal-backdrop {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background: #ffffff;
  box-shadow: 2px 2px 20px 1px;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
  border-radius: 10px;
}

.modal-header,
.modal-footer {
  padding: 15px;
  display: flex;
}

.modal-header {
  position: relative;
  border-bottom: 1px solid #eeeeee;
  color: #4aae9b;
  justify-content: space-between;
}

.modal-footer {
  border-top: 1px solid #eeeeee;
  flex-direction: column;
  justify-content: flex-end;
}

.modal-body {
  position: relative;
  padding: 20px 10px;
}

.btn-close {
  position: absolute;
  top: 0;
  right: 0;
  border: none;
  font-size: 20px;
  padding: 10px;
  cursor: pointer;
  font-weight: bold;
  color: #4aae9b;
  background: transparent;
}

.btn-green {
  color: white;
  background: #4aae9b;
  border: 1px solid #4aae9b;
  border-radius: 2px;
}
</style>
