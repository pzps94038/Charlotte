<template>
    <div class="container-fluid">
        <div class="banner"></div>
        <Story/>
    </div>
</template>
<style lang="scss">
.banner{
    background: url(../../assets/Home.jpg) no-repeat;
    height: 400px;
    background-position:40% center;
    background-size: cover;
}
@media(max-width:900px){
    .banner{
        height:auto;
        min-height:200px;    
    }
}
</style>
<script>
import Story from './Cards.vue'
export default {

    components:{
        Story
    },
    props:{

    },
    data(){
        return{

        }
    },
    create(){

    },
    mounted(){

    },
    method(){

    },
    computed(){

    },
}
</script>