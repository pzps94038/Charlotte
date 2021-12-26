import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import * as VueAos from 'vue-aos'
// #region Boostarp
import 'bootstrap/dist/css/bootstrap.min.css'
import 'jquery/src/jquery.js'
import 'bootstrap/dist/js/bootstrap.min.js'
// #endregion
// #region aos
import AOS from "aos";
import "aos/dist/aos.css";
// #endregion
const app = createApp(App)
app.use(router)
app.use(VueAos)
app.use(AOS.init())
app.mount('#app')

