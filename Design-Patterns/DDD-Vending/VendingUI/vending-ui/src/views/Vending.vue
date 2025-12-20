<template>
    <!-- eslint-disable max-len -->
    <b-container id="container">
        <b-row class="justify-content-lg-center">
            <b-col lg="2">
                <b-button variant="primary">Buy Snack</b-button>
            </b-col>
        </b-row>
        <b-row class="pt-3 text-center">
            <b-col lg="12">
                <span>Money Inserted: $0.00</span>
            </b-col>
        </b-row>
        <b-row class="justify-content-lg-center pt-3">
            <b-col lg="2"><b-button variant="primary" class="btnMoney">Put &cent;1</b-button></b-col>
            <b-col lg="2"><b-button variant="primary" class="btnMoney">Put &cent;10</b-button></b-col>
            <b-col lg="2"><b-button variant="primary" class="btnMoney">Put &cent;25</b-button></b-col>
        </b-row>
        <b-row class="justify-content-lg-center pt-3">
            <b-col lg="2"><b-button variant="primary" class="btnMoney">Put $1</b-button></b-col>
            <b-col lg="2"><b-button variant="primary" class="btnMoney">Put $5</b-button></b-col>
            <b-col lg="2"><b-button variant="primary" class="btnMoney">Put $20</b-button></b-col>
        </b-row>
        <b-row class="justify-content-lg-center pt-3">
            <b-col col lg="3"><b-button variant="primary" class="btnReturnMoney">Return Money</b-button></b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";

@Component
export default class Vending extends Vue {
    
}
</script>

<style scoped>
#container {
    width:750px;
}
.btnMoney {
    width:100px;
}
.btnReturnMoney {
    width:150px;
}
</style>
