<template>
  <div class="modal-backdrop">
    <div class="modal">
      <header class="modal-header">
        <slot name="header">
          This is the default title!
        </slot>
        <button type="button" class="btn-close" @click="close">
          x
        </button>
      </header>

      <section class="modal-body">
        <slot name="body">
          <div class="inputDiv">
            <span style="text-align: left;">testnr</span>
            <input type="text" value="Ange telefonnummer" />
          </div>
          <div class="inputDiv">
            <span>0729694095 <i class="fas fa-edit"></i></span>
            <input type="text" value="Ange telefonnummer" />
          </div>

          <input type="text" value="Ange telefonnummer" />
          <input type="text" value="Ange telefonnummer" />
        </slot>
      </section>

      <footer class="modal-footer">
        <slot name="footer">
          This is the default footer!
        </slot>
        <button type="button" class="btn-green" @click="close">
          Close Modal
        </button>
      </footer>
    </div>
  </div>
</template>

<script scoped>
export default {
  name: "EditCv",
  data() {
    return {};
  },
  methods: {
    close() {
      this.$emit("close");
    }
  }
};
</script>

<style>
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
