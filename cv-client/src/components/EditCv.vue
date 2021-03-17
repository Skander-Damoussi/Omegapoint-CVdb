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
          <div class="a4mall">
            <div>
              <p>
                What is Lorem Ipsum? Lorem Ipsum is simply dummy text of the
                printing and typesetting industry. Lorem Ipsum has been the
                industry's standard dummy text ever since the 1500s, when an
                unknown printer took a galley of type and scrambled it to make a
                type specimen book. It has survived not only five centuries, but
                also the leap into electronic typesetting, remaining essentially
                unchanged. It was popularised in the 1960s with the release of
                Letraset sheets containing Lorem Ipsum passages, and more
                recently with desktop publishing software like Aldus PageMaker
                including versions of Lorem Ipsum.
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
              <div class="grid1">
                <span>footnote 1</span>
              </div>
              <div class="grid2"><span>footnote 2</span></div>
              <div class="grid3"><span>footnote 2</span></div>
              <div class="grid4"><span>footnote 3</span></div>
            </div>
          </div>
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

<script>
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
.iconfootnote {
  position: absolute;
  bottom: 105px;
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  width: 96%;
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
