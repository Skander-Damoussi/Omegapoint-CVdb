<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <HelloWorld msg="Welcome to Your Vue.js App" />
    <EditUser />
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from "@/components/HelloWorld.vue";
import EditUser from "@/components/EditUser.vue";

export default {
  name: "Home",
  components: {
    HelloWorld,
    EditUser
  }
};
</script>
