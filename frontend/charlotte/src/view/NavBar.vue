<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark sticky-top">
        <div class="container-fluid">            
            <router-link class="navbar-brand d-flex align-items-center logo" to="/home">
                <img src="../assets/WhiteCat.png" alt="logo">
                <h1 class="fs-4 d-inline">Charlotte</h1>
            </router-link>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
                <li class="nav-item">
                <a class="nav-link" href="#">Link</a>
                </li>
                <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Dropdown
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                    <li><a class="dropdown-item" href="#">Action</a></li>
                    <li><a class="dropdown-item" href="#">Another action</a></li>
                    <li><hr class="dropdown-divider"></li>
                    <li><a class="dropdown-item" href="#">Something else here</a></li>
                </ul>
                </li>                
            </ul>
            <div class="d-flex">
                <input type="text" name="serch" v-model="serchName">
                <button type="button" class="btn btn-secondary fs-6" @click="serch">查詢</button>
            </div>
            </div>
        </div>
    </nav>
</template>
<style lang="scss">
.logo{
    img{
        height: 40px
    }
    h1{
        margin-top: 8px
    } 
}
</style>
<script>
export default {
    data(){
        return{
            serchName: ''
        }
    },
    create(){

    },
    methods:{
        serch(){
            console.log(this.serchName)
        }
    }
}
</script>