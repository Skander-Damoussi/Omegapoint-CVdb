<template>
  <div>
    <h1>test Verify</h1>
  </div>
</template>

<script>
export default {
  name: "Verify",
  data() {
    return {};
  },
  created() {
    var _this = this;
    var inputToken = this.$route.params.token;
    var inputUserId = this.$route.params.userId;

    // console.log(token);
    // console.log(userId);

    _this.$store.dispatch("verify", { userId: inputUserId, token: inputToken });
  }
};
</script>

<style></style>
