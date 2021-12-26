<template>
  <div>
    <Navbar/>
    <router-view/>
  </div>
</template>

<script>
import Navbar from './view/NavBar'

export default {

  components: {
    Navbar
  }
}
</script>

