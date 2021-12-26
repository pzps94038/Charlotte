import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/Home',
    component: ()=> import('./view/Home/Home')
  },
  {
    path: '/:pathMatch(.*)*',
    redirect: '/Home'
  }
]

const router = new createRouter({
  history: createWebHistory(), // history为必填项
  routes,
})

export default router
